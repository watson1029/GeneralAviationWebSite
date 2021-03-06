﻿using BLL.BasicData;
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
using System.Linq;
using ViewModel.FlightPlan;
using System.Collections.Generic;
public partial class FlightPlan_MyUnSubmitRepetPlan : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    WorkflowTemplateBLL wftbll = new WorkflowTemplateBLL();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
    FlightTaskBLL fll = new FlightTaskBLL();
    AircraftBLL all = new AircraftBLL();
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
                   // BatchImport();
                    break;
                case "queryone":
                    GetData();
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
            var str=Request.Form["cbx_select"].ToString();
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
        AirportInfoBLL airportbll = new AirportInfoBLL();
        var idList = new List<string>();
        var airportText = "";
        var airlineworkText = "";
        if (!string.IsNullOrEmpty(Request.Form["AirportText"]))
        {
            var airportList = (AirportFillTotal)JsonConvert.DeserializeObject(Request.Form["AirportText"], typeof(AirportFillTotal));
            idList = airportbll.AddOrUpdateAirport(airportList.airportArray, User.ID, ref airportText);
        }
        RepetitivePlan entity = null;
        if (string.IsNullOrEmpty(Request.Form["id"]))//新增
        {
            entity = new RepetitivePlan();
            entity.GetEntitySearchPars<RepetitivePlan>(this.Context);
            entity.RepetPlanID = Guid.NewGuid();
            entity.WeekSchedule = Request.Form["qx"];
            entity.AttachFile = Request.Params["AttchFilesInfo"];
            entity.PlanState = "0";
            entity.CompanyCode3 = User.CompanyCode3 ?? "";
            entity.CompanyName = User.CompanyName;
            entity.Creator = User.ID;
            entity.CreatorName = User.UserName;
           // entity.ActorID = User.ID;
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            entity.AirportText = airportText;
            #region 机场起降点、航线、作业区
            bll.AddRepetitivePlanOther(idList, Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.RepetPlanID.ToString(), Request.Form["id"], ref airlineworkText);
            #endregion
            entity.AirlineWorkText = airlineworkText;
            if (bll.Add(entity))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            entity = bll.Get(Guid.Parse(Request.Form["id"]));
            if (entity != null)
            {
                entity.AircraftType =Request.Form["AircraftType"];
                entity.FlightType = Request.Form["FlightType"];
                entity.AircraftNum = Request.Form["AircraftNum"];
                entity.CallSign = Request.Form["CallSign"];
                entity.StartDate =DateTime.Parse(Request.Form["StartDate"]);
                entity.EndDate = DateTime.Parse(Request.Form["EndDate"]);
                entity.ModifyTime = DateTime.Now;
                entity.Remark = Request.Form["Remark"];
                entity.AttachFile = Request.Params["AttachFilesInfo"];
                entity.WeekSchedule = Request.Form["qx"];
                entity.AirportText = airportText;
                #region 机场、起降点航线
                bll.AddRepetitivePlanOther(idList, Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.RepetPlanID.ToString(), Request.Form["id"], ref airlineworkText);
                entity.AirlineWorkText = airlineworkText;
                #endregion
                if (bll.Update(entity))
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
        var planid = Guid.Parse(Request.Form["id"]);

        if (insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Count > 0)
        {
            result.Msg = "一条长期计划无法创建两条申请流程，请联系管理员！";
        }
        else
        {
            try
            {
                wftbll.CreateWorkflowInstance((int)TWFTypeEnum.RepetitivePlan, planid, User.ID, User.UserName);
                insdal.Submit(planid, (int)TWFTypeEnum.RepetitivePlan, User.ID, User.UserName,User.RoleName.First(), "", insdal.UpdateRepetPlan);
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
            predicate = predicate.And(m => m.Code == val);
        }

        return predicate;
    }
    private void GetData()
    {
        var planid= Request.Form["id"];
        RepetitivePlanVM model = new RepetitivePlanVM();
        var data = bll.Get(Guid.Parse(planid));
        if (data != null)
        {
            model.FillObject(data);
            var airportList = bll.GetRepetitivePlanAirport(Guid.Parse(planid));
            if (airportList != null && airportList.Any())
            {
                model.airportList.AddRange(airportList.Select(u => new AirportVM()
                {
                    Name = u.Name,
                    Code4 = u.Code4,
                    LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                }));
            }
            var masterList = bll.GetFileMasterList(u => u.RepetPlanID.Equals(planid));
            var airlinelist = masterList.Where(u => u.WorkType.Equals("airline"));
            if (airlinelist != null && airlinelist.Any())
            {
                foreach (var item in airlinelist)
                {
                    var airlinevm = new AirlineVM();
                    airlinevm.FlyHeight = item.FlyHeight;
                    var detailList = bll.GetFileDetailList(o => o.MasterID == item.ID && o.RepetPlanID.Equals(planid));
                    airlinevm.pointList.AddRange(detailList.Select(u => new PointVM()
                    {
                        Name = u.PointName,
                        LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                    }));
                    if (airlinevm.pointList.Count > model.airLineMaxCol)
                    {
                        model.airLineMaxCol = airlinevm.pointList.Count;
                    }
                    model.airlineList.Add(airlinevm);
                    
                };
            }
            var worklist = masterList.Where(u => u.WorkType.Equals("circle") || u.WorkType.Equals("airlinelr") || u.WorkType.Equals("area"));
            if (worklist != null && worklist.Any())
            {
                foreach (var item in worklist)
                {
                    var workvm = new WorkVM();
                    workvm.FlyHeight = item.FlyHeight;
                    workvm.Raidus = (item.RaidusMile ?? 0).ToString();
                    var detailList = bll.GetFileDetailList(o => o.MasterID == item.ID && o.RepetPlanID.Equals(planid));
                    workvm.pointList.AddRange(detailList.Select(u => new PointVM()
                    {
                        Name = u.PointName,
                        LatLong = (!string.IsNullOrEmpty(u.Latitude) && !string.IsNullOrEmpty(u.Longitude)) ? string.Concat("N", SpecialFunctions.ConvertDigitalToDegrees(u.Latitude), "E", SpecialFunctions.ConvertDigitalToDegrees(u.Longitude)) : ""
                    }));

                    switch (item.WorkType.ToLower())
                    {
                        case "circle":
                            model.cworkList.Add(workvm);
                            break;
                        case "airlinelr":
                            if (workvm.pointList.Count > model.hworkMaxCol)
                            {
                                model.hworkMaxCol = workvm.pointList.Count;
                            }
                            model.hworkList.Add(workvm);
                            break;
                        case "area":
                            if (workvm.pointList.Count > model.pworkMaxCol)
                            {
                                model.pworkMaxCol = workvm.pointList.Count;
                            }
                            model.pworkList.Add(workvm);
                            break;
                        default:
                            break;
                    }

                };
            }
        }
        Response.Write(JsonConvert.SerializeObject(model));
        Response.ContentType = "application/json";
        Response.End();
    }
    ///// <summary>
    ///// 导入
    ///// </summary>
    //private void BatchImport()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = true;
    //    result.Msg = "操作成功！";
    //    try
    //    {
    //        #region 校验数据

    //        string filepath = Request.Params["PlanFilesPath"].Split(',')[0];
    //        string temppath = DownFile(filepath);
    //        DataTable dt = OfficeTools.GetDT(temppath);
    //        if (dt.Rows.Count > 100)
    //        {
    //            result.IsSuccess = false;
    //            result.Msg = "最多只能导入500条数据！";
    //            Response.Write(result.ToJsonString());
    //            Response.ContentType = "application/json";
    //            Response.End();
    //        }
    //        if (dt.Columns.Count != 12)
    //        {
    //            result.IsSuccess = false;
    //            result.Msg = "导入的文件模板不正确，请更新导入模板！";
    //            Response.Write(result.ToJsonString());
    //            Response.ContentType = "application/json";
    //            Response.End();
    //        }
    //        var ftlist = fll.GetList().Select(u => u.TaskCode);
    //        Expression<Func<Aircraft, bool>> predicate = PredicateBuilder.True<Aircraft>();
    //        predicate = predicate.And(m => m.CompanyCode3 == User.CompanyCode3);
    //        var alist = all.GetList(predicate).Select(u => u.AcfType);
    //        int length = 0;
    //        string baseerrormessage = "第{0}行错误,错误信息为{1}:";
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            var rowobj = dt.Rows[i];
    //            for (int j = 0; j < rowobj.ItemArray.Length; j++)
    //            {
    //                var colobj =rowobj.ItemArray[j].ToString();

    //                switch (j)
    //                {
    //                    case 0:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能为空！"));
    //                        if (length > 3)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不能超过3个字符！"));
    //                        //if (!ftlist.Contains(colobj))
    //                        //{
    //                        //    throw new Exception(string.Format(baseerrormessage, i + 2, "任务类型不存在！"));
    //                        //}
    //                        break;
    //                    case 1:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能为空！"));
    //                        if (length > 20)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不能超过20个字符！"));
    //                        if (!alist.Contains(colobj))
    //                        {
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "航空器类型不存在！"));
    //                        }
    //                        break;
    //                    case 2:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "飞行范围不能为空！"));
    //                        if (length > 50)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "飞行范围不能超过50个字符！"));
    //                        break;
    //                    case 3:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "飞行高度不能为空！"));
    //                         int flyheight = int.MinValue;
    //                         if (!string.IsNullOrEmpty(colobj) && !int.TryParse(colobj, out flyheight))
    //                             throw new Exception(string.Format(baseerrormessage, i + 2, "飞行高度格式不正确！"));
    //                        break;
    //                    case 4:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "注册号不能为空！"));
    //                        if (length > 36)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "注册号不能超过36个字符！"));
    //                        break;
    //                    case 5:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "起飞点不能为空！"));
    //                        if (length > 4)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "起飞点不能超过4个字符！"));
    //                        break;
    //                    case 6:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;
    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "降落点不能为空！"));
    //                        if (length > 4)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "降落点不能超过4个字符！"));
    //                        break;
    //                    case 7:
    //                        DateTime bdt = DateTime.MinValue;
    //                        if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out bdt))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "预计开始日期格式不正确！"));
    //                        break;
    //                    case 8:
    //                        DateTime edt = DateTime.MinValue;
    //                        if (!string.IsNullOrEmpty(colobj) && !DateTime.TryParse(colobj, out edt))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期格式不正确！"));
    //                        if (edt < Convert.ToDateTime(rowobj.ItemArray[j - 1]))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "预计结束日期不能小于预计开始日期！"));
    //                        break;
    //                    case 9:
    //                        TimeSpan bts = TimeSpan.MinValue;
    //                        colobj=Convert.ToDateTime(colobj).ToString("hh:mm:ss");
    //                        if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out bts))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "起飞时刻格式不正确！"));
    //                        break;
    //                    case 10:
    //                        TimeSpan ets = TimeSpan.MinValue;
    //                        colobj = Convert.ToDateTime(colobj).ToString("hh:mm:ss");
    //                        if (!string.IsNullOrEmpty(colobj) && !TimeSpan.TryParse(colobj, out ets))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "降落时刻格式不正确！"));
    //                        break;
    //                    case 11:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

    //                        if (colobj == null || string.IsNullOrEmpty(colobj))
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能为空！"));
    //                        if (length > 7)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "周执行计划不能超过7个字符！"));
    //                        break;
    //                    case 12:
    //                        length = string.IsNullOrEmpty(colobj) ? 0 : colobj.Length;

    //                        if (length == 0)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能为空！"));

    //                        if (length > 250)
    //                            throw new Exception(string.Format(baseerrormessage, i + 2, "其他需要说明的事项不能超过200个字符！"));
    //                        break;
    //                }
    //            }
    //        }

    //        #endregion
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            var rowobj = dt.Rows[i];
    //            var model = new RepetitivePlan()
    //            {
    //                FlightType = rowobj.ItemArray[0].ToString(),
    //                AircraftType = rowobj.ItemArray[1].ToString(),
    //                FlightArea = rowobj.ItemArray[2].ToString(),
    //                FlightHeight =rowobj.ItemArray[3].ToString(),
    //                CallSign = rowobj.ItemArray[4].ToString(),
    //                ADEP = rowobj.ItemArray[5].ToString(),
    //                ADES = rowobj.ItemArray[6].ToString(),
    //                StartDate = DateTime.Parse(rowobj.ItemArray[7].ToString()),
    //                EndDate = DateTime.Parse(rowobj.ItemArray[8].ToString()),
    //                SOBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[9]).ToString("hh:mm:ss")),
    //                SIBT = TimeSpan.Parse(Convert.ToDateTime(rowobj.ItemArray[10]).ToString("hh:mm:ss")),
    //                WeekSchedule = rowobj.ItemArray[11].ToString(),
    //                Remark = rowobj.ItemArray[12].ToString(),
    //                PlanState = "0",
    //                CompanyCode3 = User.CompanyCode3 ?? "",
    //                CompanyName = User.CompanyName,
    //                Creator = User.ID,
    //                CreatorName = User.UserName,
    //                ActorID = User.ID,
    //                CreateTime = DateTime.Now,
    //                ModifyTime = DateTime.Now,
    //                PlanCode = OrderHelper.GenerateId(OrderTypeEnum.RP, User.CompanyCode3)
    //            };
    //            bll.Add(model);

    //            if (result.IsSuccess)
    //            {
    //                result.Msg = "导入成功！";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.IsSuccess = false;
    //        result.Msg = ex.Message;
    //    }
    //    Response.Write(result.ToJsonString());
    //    Response.ContentType = "application/json";
    //    Response.End();
    //    }
    //protected void Export()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = true;

    //    string title = "长期计划申请";
    //    var strWhere = GetWhere();
    //    var listData = bll.GetList(strWhere);

    //    #region
    //    XSSFWorkbook workbook = new XSSFWorkbook();
    //    MemoryStream ms = new MemoryStream();

    //    var sheet = workbook.CreateSheet();
    //    var headerRow = sheet.CreateRow(0);
    //    headerRow.CreateCell(0).SetCellValue("申请单号");
    //    headerRow.CreateCell(1).SetCellValue("公司名称");
    //    headerRow.CreateCell(2).SetCellValue("任务类型");
    //    headerRow.CreateCell(3).SetCellValue("航空器类型");
    //    headerRow.CreateCell(4).SetCellValue("飞行范围");
    //    headerRow.CreateCell(5).SetCellValue("飞行高度");
    //    headerRow.CreateCell(6).SetCellValue("注册号");
    //    headerRow.CreateCell(7).SetCellValue("起飞点");
    //    headerRow.CreateCell(8).SetCellValue("降落点");
    //    headerRow.CreateCell(9).SetCellValue("预计开始日期");
    //    headerRow.CreateCell(10).SetCellValue("预计结束日期");
    //    headerRow.CreateCell(11).SetCellValue("起飞时刻");
    //    headerRow.CreateCell(12).SetCellValue("降落时刻");
    //    headerRow.CreateCell(13).SetCellValue("周执行计划");
    //    headerRow.CreateCell(14).SetCellValue("其他需要说明的事项");
    //    int rowIndex = 1;
    //    if (listData != null && listData.Count > 0)
    //    {
    //        foreach (var item in listData)
    //        {
    //                var dataRow = sheet.CreateRow(rowIndex);
    //                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //                dataRow.CreateCell(1).SetCellValue(item.CompanyName);
    //                dataRow.CreateCell(2).SetCellValue(item.FlightType);
    //                dataRow.CreateCell(3).SetCellValue(item.AircraftType);
    //                dataRow.CreateCell(4).SetCellValue(item.FlightArea);
    //                dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
    //                dataRow.CreateCell(6).SetCellValue(item.CallSign);
    //                dataRow.CreateCell(7).SetCellValue(item.ADEP);
    //                dataRow.CreateCell(8).SetCellValue(item.ADES);
    //                dataRow.CreateCell(9).SetCellValue(item.StartDate.ToString());
    //                dataRow.CreateCell(10).SetCellValue(item.EndDate.ToString());
    //                dataRow.CreateCell(11).SetCellValue(item.SOBT.ToString());
    //                dataRow.CreateCell(12).SetCellValue(item.SIBT.ToString());
    //                dataRow.CreateCell(13).SetCellValue(item.WeekSchedule);
    //                dataRow.CreateCell(14).SetCellValue(item.Remark);
    //                rowIndex++;
    //            }
    //            var dr = sheet.CreateRow(rowIndex);
    //            rowIndex++;
    //    }

    //    workbook.Write(ms);
    //    ms.Flush();
    //    ms.Position = 0;
    //    sheet = null;
    //    headerRow = null;
    //    workbook = null;
    //    #endregion
    //    string downloadFileName = HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyyMMdd"), System.Text.Encoding.UTF8) + ".xls";
    //    Response.Clear();
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.End();
    //    Response.Write(downloadFileName);
    //}

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
            return "MyUnSubmitRepetPlanCheck";
        }
    } 
#endregion
}