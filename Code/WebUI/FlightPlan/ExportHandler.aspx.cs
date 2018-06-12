using BLL.FlightPlan;
using Model.EF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_ExportHandler : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    FlightPlanBLL fbll = new FlightPlanBLL();
    private CurrentPlanBLL currPlanBll = new CurrentPlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            switch (Request.QueryString["type"])
            {
                case "1"://查询数据
                    MyUnSubmitRepetPlanExport();
                    break;
                case "2":
                  //  MyUnSubmitCurrentPlanExport();
                    break;
                case "3":
                    MySubmitFlightPlanExport();
                    break;
                case "4":
                    MySubmitRepetPlanExport();
                    break;
                case "5":
                    MyFinishAuditFlightPlanExport();
                    break;
                default:
                    break;
            }
        }
    }
    private void MyUnSubmitRepetPlanExport()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        var listData = bll.GetList(GetWhere());

        #region
        var hssfworkbook = new HSSFWorkbook();
        var sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.DefaultRowHeight = 15 * 20;
        sheet1.DefaultColumnWidth = 18;

        //设置样式
        var styleTop = hssfworkbook.CreateCellStyle();
        var fontTop = hssfworkbook.CreateFont();
        fontTop.FontHeightInPoints = 11;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 11;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("申请单号");
        headerRow.CreateCell(1).SetCellValue("公司名称");
        headerRow.CreateCell(2).SetCellValue("任务类型");
        headerRow.CreateCell(3).SetCellValue("航空器类型");
       // headerRow.CreateCell(4).SetCellValue("航线走向和飞行高度");
        headerRow.CreateCell(4).SetCellValue("飞行范围");
        headerRow.CreateCell(5).SetCellValue("飞行高度");
        headerRow.CreateCell(6).SetCellValue("注册号");
        headerRow.CreateCell(7).SetCellValue("起飞点");
        headerRow.CreateCell(8).SetCellValue("降落点");
        headerRow.CreateCell(9).SetCellValue("预计开始日期");
        headerRow.CreateCell(10).SetCellValue("预计结束日期");
        headerRow.CreateCell(11).SetCellValue("起飞时刻");
        headerRow.CreateCell(12).SetCellValue("降落时刻");
        headerRow.CreateCell(13).SetCellValue("周执行计划");
        headerRow.CreateCell(14).SetCellValue("其他需要说明的事项");
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                dataRow.CreateCell(1).SetCellValue(item.CompanyName);
                dataRow.CreateCell(2).SetCellValue(item.FlightType);
                dataRow.CreateCell(3).SetCellValue(item.AircraftType);
                dataRow.CreateCell(4).SetCellValue(item.FlightArea);
                dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
                dataRow.CreateCell(6).SetCellValue(item.CallSign);
                dataRow.CreateCell(7).SetCellValue(item.ADEP);
                dataRow.CreateCell(8).SetCellValue(item.ADES);
                dataRow.CreateCell(9).SetCellValue(item.StartDate.ToString());
                dataRow.CreateCell(10).SetCellValue(item.EndDate.ToString());
                dataRow.CreateCell(11).SetCellValue(item.SOBT.ToString());
                dataRow.CreateCell(12).SetCellValue(item.SIBT.ToString());
                dataRow.CreateCell(13).SetCellValue(item.WeekSchedule);
                dataRow.CreateCell(14).SetCellValue(item.Remark);
                rowIndex++;
            }
            var dr = sheet1.CreateRow(rowIndex);
            rowIndex++;
        }

        #endregion
        var file = new MemoryStream();
        hssfworkbook.Write(file);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition",
                              "attachment;filename=" +
                              HttpUtility.UrlEncode("长期计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    private void MySubmitFlightPlanExport()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        Expression<Func<FlightPlan, bool>> predicate = PredicateBuilder.True<FlightPlan>();
        predicate = predicate.And(m => m.PlanState != "0");
        predicate = predicate.And(m => m.Creator == User.ID);
        if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
        {
            var val = Request.QueryString["plancode"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }
        var listData = fbll.GetList(predicate);

        #region
        var hssfworkbook = new HSSFWorkbook();
        var sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.DefaultRowHeight = 15 * 20;
        sheet1.DefaultColumnWidth = 18;

        //设置样式
        var styleTop = hssfworkbook.CreateCellStyle();
        var fontTop = hssfworkbook.CreateFont();
        fontTop.FontHeightInPoints = 11;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 11;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("申请单编号");
        headerRow.CreateCell(1).SetCellValue("公司名称");
        headerRow.CreateCell(2).SetCellValue("任务类型");
        headerRow.CreateCell(3).SetCellValue("航空器类型");
        headerRow.CreateCell(4).SetCellValue("飞行范围");
        headerRow.CreateCell(5).SetCellValue("飞行高度");
        headerRow.CreateCell(6).SetCellValue("起飞点");
        headerRow.CreateCell(7).SetCellValue("降落点");
        headerRow.CreateCell(8).SetCellValue("起飞时刻");
        headerRow.CreateCell(9).SetCellValue("降落时刻");
        headerRow.CreateCell(10).SetCellValue("其他需要说明的事项");
        headerRow.CreateCell(11).SetCellValue("航空器架数");
        headerRow.CreateCell(12).SetCellValue("机长（飞行员）姓名");
        headerRow.CreateCell(13).SetCellValue("通信联络方法");
        headerRow.CreateCell(14).SetCellValue("飞行气象条件");
        headerRow.CreateCell(15).SetCellValue("空勤组人数");
        headerRow.CreateCell(16).SetCellValue("二次雷达应答机代码");
        headerRow.CreateCell(17).SetCellValue("状态");
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                dataRow.CreateCell(1).SetCellValue(item.CompanyName);
                dataRow.CreateCell(2).SetCellValue(item.FlightType);
                dataRow.CreateCell(3).SetCellValue(item.AircraftType);
             //   dataRow.CreateCell(4).SetCellValue(item.FlightDirHeight);
                dataRow.CreateCell(4).SetCellValue(item.FlightArea);
                dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
                dataRow.CreateCell(6).SetCellValue(item.ADEP);
                dataRow.CreateCell(7).SetCellValue(item.ADES);  
                dataRow.CreateCell(8).SetCellValue(item.SOBT.ToString());
                dataRow.CreateCell(9).SetCellValue(item.SIBT.ToString());
                dataRow.CreateCell(10).SetCellValue(item.Remark);
                dataRow.CreateCell(11).SetCellValue((item.AircraftNum ?? 0).ToString()); 
                dataRow.CreateCell(12).SetCellValue(item.Pilot);
                dataRow.CreateCell(13).SetCellValue(item.ContactWay);
                dataRow.CreateCell(14).SetCellValue(item.WeatherCondition);
                dataRow.CreateCell(15).SetCellValue((item.AircrewGroupNum??0).ToString());          
                dataRow.CreateCell(16).SetCellValue(item.RadarCode);
                   var str = "";
                              if (item.PlanState == "end") {
                                  str = "审核通过";
                              }
                              else if (item.PlanState == "Deserted") {
                                  str = "审核不通过";
                              }
                              else {
                                  str = item.PlanState + "审核中";
                              }
                dataRow.CreateCell(17).SetCellValue(str); 
                rowIndex++;
            }
            var dr = sheet1.CreateRow(rowIndex);
            rowIndex++;
        }

        #endregion
        var file = new MemoryStream();
        hssfworkbook.Write(file);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition",
                              "attachment;filename=" +
                              HttpUtility.UrlEncode("长期计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    private void MySubmitRepetPlanExport()
    {
        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.PlanState != "0" && m.Creator == User.ID);
        if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
        {
            var val = Request.QueryString["plancode"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }
        List<RepetitivePlan>ReList=new RepetitivePlanBLL().GetList(predicate);
        #region
        var hssfworkbook = new HSSFWorkbook();
        var sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.DefaultRowHeight = 15 * 20;
        sheet1.DefaultColumnWidth = 18;

        //设置样式
        var styleTop = hssfworkbook.CreateCellStyle();
        var fontTop = hssfworkbook.CreateFont();
        fontTop.FontHeightInPoints = 11;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 11;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("申请单编号");
        headerRow.CreateCell(1).SetCellValue("任务类型");
        headerRow.CreateCell(2).SetCellValue("注册号");
        headerRow.CreateCell(3).SetCellValue("使用机型");
        headerRow.CreateCell(4).SetCellValue("飞行范围");
        headerRow.CreateCell(5).SetCellValue("飞行高度");
        headerRow.CreateCell(6).SetCellValue("预计开始时间");
        headerRow.CreateCell(7).SetCellValue("预计结束时间");
        headerRow.CreateCell(8).SetCellValue("起飞时刻");
        headerRow.CreateCell(9).SetCellValue("降落时刻");
        headerRow.CreateCell(10).SetCellValue("起飞点");
        headerRow.CreateCell(11).SetCellValue("降落点");
        headerRow.CreateCell(12).SetCellValue("备降点");
        headerRow.CreateCell(13).SetCellValue("周执行计划");
        headerRow.CreateCell(14).SetCellValue("创建人");
        headerRow.CreateCell(15).SetCellValue("状态");
        int rowIndex = 1;
        if (ReList.Count > 0)
        {
            foreach (var item in ReList)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                dataRow.CreateCell(1).SetCellValue(item.FlightType);
                dataRow.CreateCell(2).SetCellValue(item.CallSign);
                dataRow.CreateCell(3).SetCellValue(item.AircraftType);
                dataRow.CreateCell(4).SetCellValue(item.FlightArea);
                dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
                dataRow.CreateCell(6).SetCellValue(item.StartDate.ToString("yyyy-MM-dd"));
                dataRow.CreateCell(7).SetCellValue(item.EndDate.ToString("yyyy-MM-dd"));
                dataRow.CreateCell(8).SetCellValue(item.SOBT.ToString());
                dataRow.CreateCell(9).SetCellValue(item.SIBT.ToString());
                dataRow.CreateCell(10).SetCellValue(item.ADEP);
                dataRow.CreateCell(11).SetCellValue(item.ADES);
                dataRow.CreateCell(12).SetCellValue(item.Alternate);
                item.WeekSchedule = item.WeekSchedule.Replace("*","");
                dataRow.CreateCell(13).SetCellValue("星期"+item.WeekSchedule);
                dataRow.CreateCell(14).SetCellValue(item.CreatorName);
                //dataRow.CreateCell(17).SetCellValue(item.PlanState);
                var str = "";
                if (item.PlanState == "end")
                {
                    str = "审核通过";
                }
                else if (item.PlanState == "Deserted")
                {
                    str = "审核不通过";
                }
                else
                {
                    str = item.PlanState + "审核中";
                }
                dataRow.CreateCell(15).SetCellValue(str);
                rowIndex++;
            }
            var dr = sheet1.CreateRow(rowIndex);
            rowIndex++;
        }

        #endregion
        var file = new MemoryStream();
        hssfworkbook.Write(file);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition",
                              "attachment;filename=" +
                              "长期计划已提交列表" + ".xls");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    private void MyFinishAuditFlightPlanExport()
    {
        Expression<Func<vGetFlightPlanNodeInstance, bool>> predicate = PredicateBuilder.True<vGetFlightPlanNodeInstance>();
        predicate = predicate.And(m => m.ActorID != m.Creator);
        predicate = predicate.And(m => m.ActorID == User.ID);
        predicate = predicate.And(m => m.State == 2 || m.State == 3);
        if (!string.IsNullOrEmpty(Request.QueryString["plancode"]) )
        {
            var val = Request.QueryString["plancode"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }
        List<vGetFlightPlanNodeInstance> listData = fbll.GetNodeInstanceList(predicate);

        #region
        var hssfworkbook = new HSSFWorkbook();
        var sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.DefaultRowHeight = 15 * 20;
        sheet1.DefaultColumnWidth = 18;

        //设置样式
        var styleTop = hssfworkbook.CreateCellStyle();
        var fontTop = hssfworkbook.CreateFont();
        fontTop.FontHeightInPoints = 11;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 11;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("申请单编号");
        headerRow.CreateCell(1).SetCellValue("航空器架数");
        headerRow.CreateCell(2).SetCellValue("机长（飞行员）姓名");
        headerRow.CreateCell(3).SetCellValue("通信联络方法");
        headerRow.CreateCell(4).SetCellValue("起飞时刻");
        headerRow.CreateCell(5).SetCellValue("降落时刻");
        headerRow.CreateCell(6).SetCellValue("飞行气象条件");
        headerRow.CreateCell(7).SetCellValue("空勤组人数");
        headerRow.CreateCell(8).SetCellValue("二次雷达应答机代码");
        headerRow.CreateCell(9).SetCellValue("公司名称");
        headerRow.CreateCell(10).SetCellValue("审核意见");
        headerRow.CreateCell(11).SetCellValue("审核时间");     
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                dataRow.CreateCell(1).SetCellValue(item.AircraftNum==null?0:item.AircraftNum.Value);
                dataRow.CreateCell(2).SetCellValue(item.Pilot);
                dataRow.CreateCell(3).SetCellValue(item.ContactWay);
                dataRow.CreateCell(4).SetCellValue(item.SOBT);
                dataRow.CreateCell(5).SetCellValue(item.SIBT);
                dataRow.CreateCell(6).SetCellValue(item.WeatherCondition);
                dataRow.CreateCell(7).SetCellValue(item.AircrewGroupNum==null?0:item.AircrewGroupNum.Value);
                dataRow.CreateCell(8).SetCellValue(item.RadarCode);
                dataRow.CreateCell(9).SetCellValue(item.CompanyName);
                dataRow.CreateCell(10).SetCellValue(item.Comments);
                dataRow.CreateCell(11).SetCellValue(item.ActorTime.Value);
                rowIndex++;
            }
            var dr = sheet1.CreateRow(rowIndex);
            rowIndex++;
        }

        #endregion
        var file = new MemoryStream();
        hssfworkbook.Write(file);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition",
                              "attachment;filename=" +
                              HttpUtility.UrlEncode("飞行计划已审核列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
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
        if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
        {
            var val = Request.QueryString["plancode"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }

        return predicate;
    }
    /*
    private void MyUnSubmitCurrentPlanExport()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        var listData = currPlanBll.GetList(GetWhere1());

        #region
        var hssfworkbook = new HSSFWorkbook();
        var sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.DefaultRowHeight = 15 * 20;
        sheet1.DefaultColumnWidth = 18;

        //设置样式
        var styleTop = hssfworkbook.CreateCellStyle();
        var fontTop = hssfworkbook.CreateFont();
        fontTop.FontHeightInPoints = 11;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 11;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("申请单号");
        headerRow.CreateCell(1).SetCellValue("任务类型");
        headerRow.CreateCell(2).SetCellValue("注册号");
        headerRow.CreateCell(3).SetCellValue("使用机型");
        headerRow.CreateCell(4).SetCellValue("航线走向和飞行高度");
        headerRow.CreateCell(5).SetCellValue("预计开始时间");
        headerRow.CreateCell(6).SetCellValue("预计结束时间");
        headerRow.CreateCell(7).SetCellValue("起飞时刻");
        headerRow.CreateCell(8).SetCellValue("降落时刻");
        headerRow.CreateCell(9).SetCellValue("起飞点");
        headerRow.CreateCell(10).SetCellValue("降落点");
        headerRow.CreateCell(11).SetCellValue("周执行计划");
        headerRow.CreateCell(12).SetCellValue("其他需要说明的事项");
        int rowIndex = 1;
        if (listData != null && listData.Count > 0)
        {
            foreach (var item in listData)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.PlanCode);
                dataRow.CreateCell(1).SetCellValue(item.FlightType);
                dataRow.CreateCell(2).SetCellValue(item.CallSign);
                dataRow.CreateCell(3).SetCellValue(item.AircraftType);
                dataRow.CreateCell(4).SetCellValue(item.FlightDirHeight);
                dataRow.CreateCell(5).SetCellValue(item.StartDate.ToString());
                dataRow.CreateCell(6).SetCellValue(item.EndDate.ToString());
                dataRow.CreateCell(7).SetCellValue(item.SOBT.ToString());
                dataRow.CreateCell(8).SetCellValue(item.SIBT.ToString());
                dataRow.CreateCell(9).SetCellValue(item.ADEP);
                dataRow.CreateCell(10).SetCellValue(item.ADES);
                dataRow.CreateCell(11).SetCellValue(item.WeekSchedule);
                dataRow.CreateCell(12).SetCellValue(item.Remark);
                rowIndex++;
            }
            var dr = sheet1.CreateRow(rowIndex);
            rowIndex++;
        }

        #endregion
        var file = new MemoryStream();
        hssfworkbook.Write(file);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition",
                              "attachment;filename=" +
                              HttpUtility.UrlEncode("当日计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    private Expression<Func<V_CurrentPlan, bool>> GetWhere1()
    {
        Expression<Func<V_CurrentPlan, bool>> predicate = PredicateBuilder.True<V_CurrentPlan>();
        var currDate = DateTime.Now.Date;
        predicate = predicate.And(m => m.PlanState == "0" && m.Creator == User.ID && m.SOBT == currDate);       

        return predicate;
    }*/
}