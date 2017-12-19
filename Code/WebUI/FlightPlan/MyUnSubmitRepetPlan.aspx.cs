using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using Untity;

public partial class FlightPlan_MyUnSubmitRepetPlan : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "save":
                    Save();
                    break;
                case "submit":
                    Submit();
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
            if (bll.Delete(Request.Form["cbx_select"].ToString()))
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
        RepetitivePlan model = null;
        if (!id.HasValue)//新增
        {
            model = new RepetitivePlan();
            model.GetEntitySearchPars<RepetitivePlan>(this.Context);
            model.WeekSchedule = Request.Form["qx"];
            model.AttchFile = Request.Params["AttchFilesInfo"];
            model.PlanState = "0";
            model.CompanyCode3 = User.CompanyCode3 ?? "";
            model.CompanyName = User.CompanyName;
            model.Creator = User.ID;
            model.CreatorName = User.UserName;
            model.ActorID = User.ID;
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;

            if (bll.Add(model))
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
                model.GetEntitySearchPars<RepetitivePlan>(this.Context);
                model.ModifyTime = DateTime.Now;
                model.AttchFile = Request.Params["AttchFilesInfo"];
                model.WeekSchedule = Request.Form["qx"];
                if (bll.Update(model))
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
            }
        };
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    private void Submit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;

        if (insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Count > 0)
        {
            result.Msg = "一条长期计划无法创建两条申请流程，请联系管理员！";
        }
        else
        {
            try
            {
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.RepetitivePlan, planid, User.ID, User.UserName);
                insdal.Submit(planid, (int)TWFTypeEnum.RepetitivePlan, "", insdal.UpdateRepetPlan);
                result.IsSuccess = true;
                result.Msg = "提交成功！";
            }
            catch(Exception e)
            {
                result.IsSuccess = false;
                result.Msg = "提交失败！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
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
        //   string sort = Request.Form["sort"] ?? "";
        //   string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        //    string orderField = sort.Replace("JSON_", "");
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
    private Expression<Func<RepetitivePlan, bool>> GetWhere()
    {

        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.PlanState == "0");
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val=Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
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
            if (dt.Columns.Count != 12)
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
                    var colobj =rowobj.ItemArray[j].ToString();
                    
                    switch (j)
                    {
                        case 0:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能为空！"));
                            if (length > 3)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能超过3个字符！"));
                            break;
                        case 1:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能为空！"));
                            if (length > 8)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能超过8个字符！"));
                            break;
                        case 2:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航线走向和飞行高度不能为空！"));
                            if (length > 32)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能超过32个字符！"));
                            break;
                        case 3:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器呼号不能为空！"));
                            if (length > 36)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "航空器呼号不能超过36个字符！"));
                            break;
                        case 4:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞机场不能为空！"));
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞机场不能超过4个字符！"));
                            break;
                        case 5:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落机场不能为空！"));
                            if (length > 4)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落机场不能超过4个字符！"));
                            break;
                        case 6:
                            DateTime bdt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out bdt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计开始日期格式不正确！"));
                            break;
                        case 7:
                            DateTime edt = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out edt))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期格式不正确！"));
                            if (edt < Convert.ToDateTime(rowobj.ItemArray[j - 1]))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期不能小于预计开始日期！"));
                            break;
                        case 8:
                            TimeSpan bts = TimeSpan.MinValue;
                            colobj=Convert.ToDateTime(colobj).ToString("hh:mm:ss");
                            if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out bts))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "起飞时刻格式不正确！"));
                            break;
                        case 9:
                            TimeSpan ets = TimeSpan.MinValue;
                            colobj = Convert.ToDateTime(colobj).ToString("hh:mm:ss");
                            if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out ets))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "降落时刻格式不正确！"));
                            break;
                        case 10:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (colobj == null || string.IsNullOrEmpty(colobj))
                                throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能为空！"));
                            if (length > 7)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能超过7个字符！"));
                            break;
                        case 11:
                            length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

                            if (length == 0)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能为空！"));

                            if (length > 250)
                                throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能超过200个字符！"));
                            break;
                    }
                }
            }

            #endregion
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowobj = dt.Rows[i];
                var model = new RepetitivePlan()
                {
                    FlightType = rowobj.ItemArray[0].ToString(),
                    AircraftType = rowobj.ItemArray[1].ToString(),
                    FlightDirHeight = rowobj.ItemArray[2].ToString(),
                    CallSign = rowobj.ItemArray[3].ToString(),
                    ADEP = rowobj.ItemArray[4].ToString(),
                    ADES = rowobj.ItemArray[5].ToString(),
                    StartDate = DateTime.Parse(rowobj.ItemArray[6].ToString()),
                    EndDate = DateTime.Parse(rowobj.ItemArray[7].ToString()),
                    SOBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[8]).ToString("hh:mm:ss")),
                    SIBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[9]).ToString("hh:mm:ss")),
                    WeekSchedule = rowobj.ItemArray[10].ToString(),
                    Remark = rowobj.ItemArray[11].ToString(),
                    PlanState = "0",
                    CompanyCode3 = User.CompanyCode3 ?? "",
                    CompanyName = User.CompanyName,
                    Creator = User.ID,
                    CreatorName = User.UserName,
                    ActorID = User.ID,
                    CreateTime = DateTime.Now,
                    ModifyTime = DateTime.Now,
                    PlanCode = OrderHelper.GenerateId(OrderTypeEnum.RP, User.CompanyCode3)
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

        string title = "长期计划申请";
        var strWhere = GetWhere();
        var listData = bll.GetList(strWhere);

        #region
        XSSFWorkbook workbook = new XSSFWorkbook();
        MemoryStream ms = new MemoryStream();

        var sheet = workbook.CreateSheet();
        var headerRow = sheet.CreateRow(0);
        headerRow.CreateCell(0).SetCellValue("申请单号");
        headerRow.CreateCell(1).SetCellValue("公司名称");
        headerRow.CreateCell(2).SetCellValue("任务类型");
        headerRow.CreateCell(3).SetCellValue("航空器类型");
        headerRow.CreateCell(4).SetCellValue("航线走向和飞行高度");
        headerRow.CreateCell(5).SetCellValue("航空器呼号");
        headerRow.CreateCell(6).SetCellValue("起飞机场");
        headerRow.CreateCell(7).SetCellValue("降落机场");
        headerRow.CreateCell(8).SetCellValue("预计开始日期");
        headerRow.CreateCell(9).SetCellValue("预计结束日期");
        headerRow.CreateCell(10).SetCellValue("起飞时刻");
        headerRow.CreateCell(11).SetCellValue("降落时刻");
        headerRow.CreateCell(12).SetCellValue("周执行计划");
        headerRow.CreateCell(13).SetCellValue("其他需要说明的事项");
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                    var dataRow = sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                    dataRow.CreateCell(1).SetCellValue(item.CompanyName);
                    dataRow.CreateCell(2).SetCellValue(item.FlightType);
                    dataRow.CreateCell(3).SetCellValue(item.AircraftType);
                    dataRow.CreateCell(4).SetCellValue(item.FlightDirHeight);
                    dataRow.CreateCell(5).SetCellValue(item.CallSign);
                    dataRow.CreateCell(6).SetCellValue(item.ADEP);
                    dataRow.CreateCell(7).SetCellValue(item.ADES);
                    dataRow.CreateCell(8).SetCellValue(item.StartDate.ToString());
                    dataRow.CreateCell(9).SetCellValue(item.EndDate.ToString());
                    dataRow.CreateCell(10).SetCellValue(item.SOBT.ToString());
                    dataRow.CreateCell(11).SetCellValue(item.SIBT.ToString());
                    dataRow.CreateCell(12).SetCellValue(item.WeekSchedule);
                    dataRow.CreateCell(13).SetCellValue(item.Remark);
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
        var localTargetCategory = Server.MapPath("~/Files/PJ/RepetPlanTemp");
        if (string.IsNullOrEmpty(localNewFileName))
            throw new ApplicationException(string.Format("获取文件路径[{0}]中的文件名为空", attachfile));
        if (!Directory.Exists(localTargetCategory))
            Directory.CreateDirectory(localTargetCategory);

        var filePath = Path.Combine(localTargetCategory, localNewFileName);
        return filePath;
    }
}