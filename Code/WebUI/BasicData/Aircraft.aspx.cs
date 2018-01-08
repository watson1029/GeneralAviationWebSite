using BLL.BasicData;
using Model.EF;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using Untity;

public partial class BasicData_Aircraft : BasePage
{
    AircraftBLL bll = new AircraftBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "submit":
                    Save();
                    break;
                case "del":
                    Delete();
                    break;
                case "batchImport":
                    BatchImport();
                    break;
                default:
                    break;
            }
        }
    }

    private void Delete()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "删除失败！";
        if (Request.Form["cbx_select"] != null)
        {
            if (bll.Delete(Request.Form["cbx_select"].ToString()) > 0)
            {
                result.IsSuccess = true;
                result.Msg = "删除成功！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        int? id = null;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        { id = Convert.ToInt32(Request.Form["id"]); }
        Aircraft model = null;
        if (!id.HasValue)//新增
        {
            model = new Aircraft();
            model.GetEntitySearchPars<Aircraft>(this.Context);
            model.CreateTime = DateTime.Now;
            if (bll.Add(model) > 0)
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model = bll.Get(id.Value);
            if (model != null)
            {
                model.GetEntitySearchPars<Aircraft>(this.Context);
                if (bll.Update(model) > 0)
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var aircraftid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var aircraft = bll.Get(aircraftid);
        var strJSON = JsonConvert.SerializeObject(aircraft);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }


    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        string sort = Request.Form["sort"] ?? "";
        string order = Request.Form["order"] ?? "";

        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();
        var pageList = bll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>



    private Expression<Func<Aircraft, bool>> GetWhere()
    {


        Expression<Func<Aircraft, bool>> predicate = PredicateBuilder.True<Aircraft>();
        predicate = predicate.And(m => 1 == 1);
        //   StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {

           var val= Request.Form["search_value"];
            predicate = u => u.AircraftSign == val;

            //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        return predicate;
    }




    /// <summary>
    /// 导入
    /// </summary>
    private void BatchImport()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        result.Msg = "操作成功！";
        try
        {
            #region 校验数据

            string filepath = Request.Params["PlanFilesPath"].Split(',')[0];
            string temppath = DownFile(filepath);
            DataTable dt = OfficeTools.GetDT(temppath);
            if (dt.Rows.Count > 100)
            {
                result.IsSuccess = false;
                result.Msg = "最多只能导入500条数据！";
                Response.Write(result.ToJsonString());
                Response.ContentType = "application/json";
                Response.End();
            }
            if (dt.Columns.Count != 16)
            {
                result.IsSuccess = false;
                result.Msg = "导入的文件模板不正确，请更新导入模板！";
                Response.Write(result.ToJsonString());
                Response.ContentType = "application/json";
                Response.End();
            }
            int length = 0;
            string baseerrormessage = "第{0}行错误,错误信息为{1}:";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                for (int j = 0; j < rowobj.ItemArray.Length; j++)
                {
                    var colobj = rowobj.ItemArray[j].ToString();

                    switch (j)
                    {
                        case 0:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "国籍和登记标志不能为空！"));
                            if (length > 10)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "国籍和登记标志不能超过10个字符！"));
                            break;
                        case 1:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大加油量不能超过4个字符！"));
                            break;
                        case 2:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "机型不能为空！"));
                            if (length > 20)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "机型不能超过20个字符！"));
                            break;
                        case 3:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            
                            if (length > 6)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大航程不能超过6个字符！"));
                            break;
                        case 4:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器出厂序号不能为空！"));
                            if (length > 10)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器出厂序号不能超过10个字符！"));
                            break;
                        case 5:
                            DateTime bdt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out bdt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "年检日期格式不正确！"));
                            break;
                            
                        case 6:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "飞行器类别不能为空！"));
                            if (length > 30)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "飞行器类别不能超过30个字符！"));
                            break;
                        case 7:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "巡航高度不能超过4个字符！"));
                            break;
                        case 8:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "制造商不能为空！"));
                            if (length > 30)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "制造商不能超过30个字符！"));
                            break;
                        case 9:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length > 5)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "巡航速度不能超过5个字符！"));
                            break;
                        case 10:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length > 5)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大速度不能超过5个字符！"));
                            break;
                        case 11:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length > 5)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大起飞重量不能超过5个字符！"));
                            break;
                        case 12:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大续航时间不能为空！"));
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "最大续航时间不能超过4个字符！"));
                            //if (colobj != /^\d{ })
                            break;
                        case 13:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "乘客人数不能为空！"));
                            if (length > 2)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "乘客人数不能超过2个字符！"));
                            break;
                        case 14:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "适航证颁发单位不能为空！"));
                            if (length > 30)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "适航证颁发单位不能超过30个字符！"));
                            break;
                        case 15:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "公司三字码不能为空！"));
                            if (length > 3)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "公司三字码不能超过3个字符！"));
                            break;
                    }
                }
            }

            #endregion
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                var model = new Aircraft()
                {
                    AircraftSign = rowobj.ItemArray[0].ToString(),
                    FuelCapacity = int.Parse(rowobj.ItemArray[1].ToString()),
                    AcfType = rowobj.ItemArray[2].ToString(),
                    Range = int.Parse(rowobj.ItemArray[3].ToString()),
                    AcfNo = rowobj.ItemArray[4].ToString(),
                    ASdate = DateTime.Parse(rowobj.ItemArray[5].ToString()),
                    AcfClass = rowobj.ItemArray[6].ToString(),
                    CruiseAltd = int.Parse(rowobj.ItemArray[7].ToString()),
                    Manufacture = rowobj.ItemArray[8].ToString(),
                    CruiseSpeed = int.Parse(rowobj.ItemArray[9].ToString()),
                    MaxSpeed = int.Parse(rowobj.ItemArray[10].ToString()),
                    FueledWeight = int.Parse(rowobj.ItemArray[11].ToString()),
                    MaxEndurance = float.Parse(rowobj.ItemArray[12].ToString()),
                    Passenger = int.Parse(rowobj.ItemArray[13].ToString()),
                    Airworthiness = rowobj.ItemArray[14].ToString(),
                    CompanyCode3 = rowobj.ItemArray[15].ToString(),
                    //CompanyCode3 = User.CompanyCode3 ?? "",                
                    CreateTime = DateTime.Now,
     
                };
                bll.Add(model);

                if (result.IsSuccess)
                {
                    result.Msg = "导入成功！";
                }
            }
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Msg = ex.Message;
        }
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    protected void Export()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;

        string title = "飞行器填写信息";
        var strWhere = GetWhere();
        var listData = bll.GetList(strWhere);

        #region
        XSSFWorkbook workbook = new XSSFWorkbook();
        MemoryStream ms = new MemoryStream();

        var sheet = workbook.CreateSheet();
        var headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("国籍和登记标志");
        headerRow.CreateCell(1).SetCellValue("最大加油量");
        headerRow.CreateCell(2).SetCellValue("机型");
        headerRow.CreateCell(3).SetCellValue("最大航程");
        headerRow.CreateCell(4).SetCellValue("航空器出厂序号");
        headerRow.CreateCell(5).SetCellValue("年检日期");
        headerRow.CreateCell(6).SetCellValue("飞行器类别");
        headerRow.CreateCell(7).SetCellValue("巡航高度");
        headerRow.CreateCell(8).SetCellValue("制造商");
        headerRow.CreateCell(9).SetCellValue("巡航速度");
        headerRow.CreateCell(10).SetCellValue("最大速度");
        headerRow.CreateCell(11).SetCellValue("最大起飞重量");
        headerRow.CreateCell(12).SetCellValue("最大续航时间");
        headerRow.CreateCell(13).SetCellValue("乘客人数");
        headerRow.CreateCell(14).SetCellValue("适航证颁发单位");
        headerRow.CreateCell(15).SetCellValue("公司三字码");
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.AircraftSign);
                dataRow.CreateCell(1).SetCellValue(item.FuelCapacity.ToString());
                dataRow.CreateCell(2).SetCellValue(item.AcfType);
                dataRow.CreateCell(3).SetCellValue(item.Range.ToString());
                dataRow.CreateCell(4).SetCellValue(item.AcfNo);
                dataRow.CreateCell(5).SetCellValue(item.ASdate.ToString());
                dataRow.CreateCell(6).SetCellValue(item.AcfClass);
                dataRow.CreateCell(7).SetCellValue(item.CruiseAltd.ToString());
                dataRow.CreateCell(8).SetCellValue(item.Manufacture);
                dataRow.CreateCell(9).SetCellValue(item.CruiseSpeed.ToString());
                dataRow.CreateCell(10).SetCellValue(item.MaxSpeed.ToString());
                dataRow.CreateCell(11).SetCellValue(item.FueledWeight.ToString());
                dataRow.CreateCell(12).SetCellValue(item.MaxEndurance.ToString());
                dataRow.CreateCell(13).SetCellValue(item.Passenger.ToString());
                dataRow.CreateCell(14).SetCellValue(item.Airworthiness);
                dataRow.CreateCell(15).SetCellValue(item.CompanyCode3);
                rowIndex++;
            }
            var dr = sheet.CreateRow(rowIndex);
            rowIndex++;
        }

        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        sheet = null;
        headerRow = null;
        workbook = null;
        #endregion
        string downloadFileName = HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyyMMdd"), System.Text.Encoding.UTF8) + ".xls";
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.End();
        Response.Write(downloadFileName);
    }

    private string DownFile(string attachfile)
    {
        var localNewFileName = Path.GetFileName(attachfile);
        var localTargetCategory = Server.MapPath("~/Files/ImportTemp");
        if (string.IsNullOrEmpty(localNewFileName))
            throw new ApplicationException(string.Format("获取文件路径[{0}]中的文件名为空", attachfile));
        if (!Directory.Exists(localTargetCategory))
            Directory.CreateDirectory(localTargetCategory);

        var filePath = Path.Combine(localTargetCategory, localNewFileName);
        return filePath;
    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "AircraftCheck";
        }
    }
    #endregion
}

