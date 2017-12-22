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

public partial class BasicData_Pilot : BasePage
{
      PilotBLL bll = new PilotBLL();
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
            if (bll.Delete(Request.Form["cbx_select"].ToString())>0)
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
        Pilot model = null;
        if (!id.HasValue)//新增
        {
            model = new Pilot();
            model.GetEntitySearchPars<Pilot>(this.Context);
            model.CreateTime = DateTime.Now;
            if (bll.Add(model)>0)
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
                model.GetEntitySearchPars<Pilot>(this.Context);
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
        var pilotid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var pilot = bll.Get(pilotid);
        var strJSON = JsonConvert.SerializeObject(pilot);
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
    private Expression<Func<Pilot, bool>> GetWhere()
    {


        Expression<Func<Pilot, bool>> predicate = PredicateBuilder.True<Pilot>();
        predicate = predicate.And(m => 1 == 1);
        //   StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val=Request.Form["search_value"];
            predicate = u => u.Pilots == val;

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
            if (dt.Columns.Count != 10)
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
                                throw new Exception(string.Format(baseerrormessage, i + 2, "飞行员姓名不能为空！"));
                            if (length > 10)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "飞行员姓名不能超过10个字符！"));
                            break;
                        case 1:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "身份证号不能为空！"));
                            if (length > 18)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "身份证号不能超过18个字符！"));
                            break;
                        case 2:
                            DateTime bdt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out bdt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "出生日期格式不正确！"));
                            break;
                        case 3:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "年龄不能为空！"));
                            if (length > 3)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "年龄不能超过3个字符！"));
                            break;
                        case 4:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "联系电话不能为空！"));
                            if (length > 11)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "联系电话不能超过11个字符！"));
                            break;
                        case 5:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "执照编号不能为空！"));
                            if (length > 30)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "执照编号不能超过30个字符！"));
                            break;
                        case 6:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "签发单位不能为空！"));
                            if (length > 30)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "签发单位不能超过30个字符！"));
                            break;
                        case 7:
                            DateTime edt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out edt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "签发日期格式不正确！"));
                            break;
                        case 8:                        
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "执照类别不能为空！"));
                            if (length > 8)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "执照类别不能超过8个字符！"));
                            break;
                        case 9:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "性别不能为空！"));
                            if (length > 2)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "性别不能超过2个字符！"));
                            break;
                        
                    }
                }
            }

            #endregion
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                var model = new Pilot()
                {
                    Pilots = rowobj.ItemArray[0].ToString(),
                    PilotCardNo = rowobj.ItemArray[1].ToString(),
                    PilotDT = DateTime.Parse(rowobj.ItemArray[2].ToString()),
                    PilotAge = int.Parse(rowobj.ItemArray[3].ToString()),
                    PhoneNo = rowobj.ItemArray[4].ToString(),
                    LicenseNo = rowobj.ItemArray[5].ToString(),
                    Sign = rowobj.ItemArray[6].ToString(),
                    LicenseTime = DateTime.Parse(rowobj.ItemArray[7].ToString()),
                    Licensesort = rowobj.ItemArray[8].ToString() ==  "航线运输驾驶执照" ? "0" : (rowobj.ItemArray[8].ToString() == "商用飞机驾照" ? "1" : "2"),
                    Sex = byte.Parse(rowobj.ItemArray[9].ToString()=="男"?"0":"1"),
             //       CompanyCode3 = CompanyCode3,
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

        string title = "飞行员填写信息";
        var strWhere = GetWhere();
        var listData = bll.GetList(strWhere);

        #region
        XSSFWorkbook workbook = new XSSFWorkbook();
        MemoryStream ms = new MemoryStream();

        var sheet = workbook.CreateSheet();
        var headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("飞行员姓名");
        headerRow.CreateCell(1).SetCellValue("身份证号");
        headerRow.CreateCell(2).SetCellValue("出生日期");
        headerRow.CreateCell(3).SetCellValue("年龄");
        headerRow.CreateCell(4).SetCellValue("联系电话");
        headerRow.CreateCell(5).SetCellValue("执照编号");
        headerRow.CreateCell(6).SetCellValue("签发单位");
        headerRow.CreateCell(7).SetCellValue("签发日期");
        headerRow.CreateCell(8).SetCellValue("执照类别");
        headerRow.CreateCell(9).SetCellValue("性别");

        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.Pilots);
                dataRow.CreateCell(1).SetCellValue(item.PilotCardNo);
                dataRow.CreateCell(2).SetCellValue(item.PilotDT);
                dataRow.CreateCell(3).SetCellValue(item.PilotAge.ToString());
                dataRow.CreateCell(4).SetCellValue(item.PhoneNo);
                dataRow.CreateCell(5).SetCellValue(item.LicenseNo);
                dataRow.CreateCell(6).SetCellValue(item.Sign);
                dataRow.CreateCell(7).SetCellValue(item.LicenseTime.ToString());
                dataRow.CreateCell(8).SetCellValue(item.Licensesort);
                dataRow.CreateCell(9).SetCellValue(item.Sex.ToString());

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
        var localTargetCategory = Server.MapPath("~/Files/Pilot/PilotFilesTemp");
        if (string.IsNullOrEmpty(localNewFileName))
            throw new ApplicationException(string.Format("获取文件路径[{0}]中的文件名为空", attachfile));
        if (!Directory.Exists(localTargetCategory))
            Directory.CreateDirectory(localTargetCategory);

        var filePath = Path.Combine(localTargetCategory, localNewFileName);
        return filePath;
    }
}


