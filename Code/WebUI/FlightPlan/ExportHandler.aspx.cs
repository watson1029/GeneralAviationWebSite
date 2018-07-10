using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Model.FlightPlan;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
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
                //case "1"://查询数据
                //    MyUnSubmitRepetPlanExport();
                //  break;
                //    case "2":
                //      //  MyUnSubmitCurrentPlanExport();
                //        break;
                //    case "3":
                //        MySubmitFlightPlanExport();
                //        break;
                //    case "4":
                //        MySubmitRepetPlanExport();
                //        break;
                //    case "5":
                //        MyFinishAuditFlightPlanExport();
                //        break;
                case "FlightPlanStatistics":
                    FlightPlanStatisticsExport();
                    break;
                case "FlightPlanData":
                    List<string> plancode = Request.QueryString["plancode"].Split('|').ToList();
                    FlightPlanDataExport(plancode);
                    break;
                default:
                    break;
            }
        }
    }

    private void FlightPlanDataExport(List<string> planlist)
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = true;
        XWPFDocument doc = new XWPFDocument();
        CT_SectPr m_SectPr = new CT_SectPr();
        //设置宽度（这里是一个ulong类型）
        m_SectPr.pgSz.w = 16838;
        //设置高度（这里是一个ulong类型）
        m_SectPr.pgSz.h = 11906;
        m_SectPr.pgMar.left = (ulong)300;//左边距
        m_SectPr.pgMar.right = (ulong)300;//右边距
        m_SectPr.pgMar.top = "300";//上边距
        m_SectPr.pgMar.bottom = "300";//下边距
        //设置页面的尺寸
        doc.Document.body.sectPr = m_SectPr;

        XWPFParagraph p0 = doc.CreateParagraph();//创建段落
        p0.Alignment = ParagraphAlignment.CENTER;//居中显示
        XWPFRun r0 = p0.CreateRun();
        //设置字体
        r0.FontFamily = "宋体";
        //设置字体大小
        r0.FontSize = 16;
        //字体是否加粗，这里加粗了
        r0.IsBold = true;
        r0.SetText("武汉管制分区（河南地区）通用航空计划表");//写入文本

        XWPFParagraph p1 = doc.CreateParagraph();//创建段落
        p1.Alignment = ParagraphAlignment.CENTER;//居中显示
        XWPFRun r1 = p1.CreateRun();
        //设置字体
        r1.FontFamily = "宋体";
        //设置字体大小
        r1.FontSize = 16;
        //字体是否加粗，这里加粗了
        r1.IsBold = true;
        r1.SetText("武基范围内");//写入文本

        var wordlist = new ExportDataBLL().FlightPlanDataExport(planlist);

        XWPFTable table = doc.CreateTable(2 + wordlist.Count, 13);
        table.SetColumnWidth(0, 4 * 256);//设置列的宽度
        table.SetColumnWidth(1, 4 * 256);
        table.SetColumnWidth(2, 4 * 256);
        table.SetColumnWidth(3, 4 * 256);
        table.SetColumnWidth(4, 4 * 256);
        table.SetColumnWidth(5, 20 * 256);
        table.SetColumnWidth(6, 4 * 256);
        table.SetColumnWidth(7, 4 * 256);
        table.SetColumnWidth(8, 3 * 256);
        table.SetColumnWidth(9, 3 * 256);
        table.SetColumnWidth(10, 3 * 256);
        table.SetColumnWidth(11, 3 * 256);
        table.SetColumnWidth(12, 2 * 256);
        CT_Tbl m_CTTbl = doc.Document.body.GetTblArray()[0];//获得文档第一张表
        m_CTTbl.AddNewTblPr().jc = new CT_Jc();
        m_CTTbl.AddNewTblPr().jc.val = ST_Jc.center;//表在页面水平居中


        table.GetRow(0).Height = 1200;
        setCell(table.GetRow(0).GetCell(0), "序号");
        setCell(table.GetRow(0).GetCell(1), "起降场");
        setCell(table.GetRow(0).GetCell(2), "单位");
        setCell(table.GetRow(0).GetCell(3), "机型/架数");
        setCell(table.GetRow(0).GetCell(4), "机号");
        setCell(table.GetRow(0).GetCell(5), "空域/航线");
        setCell(table.GetRow(0).GetCell(6), "高度");
        setCell(table.GetRow(0).GetCell(7), "任务");
        setCell(table.GetRow(0).GetCell(8), "开飞时间");
        setCell(table.GetRow(0).GetCell(10), "结束时间");
        setCell(table.GetRow(0).GetCell(12), "备注");
        table.GetRow(1).Height = 1200;
        setCell(table.GetRow(1).GetCell(8), "计划");
        setCell(table.GetRow(1).GetCell(9), "实施");
        setCell(table.GetRow(1).GetCell(10), "计划");
        setCell(table.GetRow(1).GetCell(11), "实施");

        mergeCellsVertically(table, 0, 0, 1);
        table.GetRow(0).GetCell(0).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 1, 0, 1);
        table.GetRow(0).GetCell(1).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 2, 0, 1);
        table.GetRow(0).GetCell(2).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 3, 0, 1);
        table.GetRow(0).GetCell(3).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 4, 0, 1);
        table.GetRow(0).GetCell(4).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 5, 0, 1);
        table.GetRow(0).GetCell(5).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 6, 0, 1);
        table.GetRow(0).GetCell(6).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 7, 0, 1);
        table.GetRow(0).GetCell(7).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsHorizontal(table, 0, 8, 9);
        table.GetRow(0).GetCell(8).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsHorizontal(table, 0, 10, 11);
        table.GetRow(0).GetCell(10).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        mergeCellsVertically(table, 12, 0, 1);
        table.GetRow(0).GetCell(12).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        table.GetRow(1).GetCell(8).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        table.GetRow(1).GetCell(9).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        table.GetRow(1).GetCell(10).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
        table.GetRow(1).GetCell(11).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);

        
        for (int i = 0; i < wordlist.Count; i++)
        {
            setCell(table.GetRow(2 + i).GetCell(0), (i + 1).ToString());
            setCell(table.GetRow(2 + i).GetCell(1), wordlist[i].airport);
            setCell(table.GetRow(2 + i).GetCell(2), wordlist[i].company);
            setCell(table.GetRow(2 + i).GetCell(3), wordlist[i].airtype);
            setCell(table.GetRow(2 + i).GetCell(4), "");
            setCell(table.GetRow(2 + i).GetCell(5), wordlist[i].airline);
            setCell(table.GetRow(2 + i).GetCell(6), wordlist[i].high);
            setCell(table.GetRow(2 + i).GetCell(7), wordlist[i].messiontype);
            setCell(table.GetRow(2 + i).GetCell(8), wordlist[i].planbegin);
            setCell(table.GetRow(2 + i).GetCell(9), "");
            setCell(table.GetRow(2 + i).GetCell(10), wordlist[i].planend);
            setCell(table.GetRow(2 + i).GetCell(11), "");
            setCell(table.GetRow(2 + i).GetCell(12), wordlist[i].remark);
        }
        
        //var book = new HSSFWorkbook();
        ////设置默认工作表的默认列宽和行高
        //var sheet = book.CreateSheet("通航计划表-范围内");
        //sheet.DefaultRowHeight = 20 * 20;
        //sheet.DefaultColumnWidth = 12;
        //sheet.SetColumnWidth(5, 60 * 256);
        ////设置标题样式
        //var titleStyle = book.CreateCellStyle();
        //titleStyle.Alignment = HorizontalAlignment.Center;
        //titleStyle.VerticalAlignment = VerticalAlignment.Center;
        //var titleFont = book.CreateFont();
        //titleFont.FontHeightInPoints = 20;
        //titleFont.IsBold = true;
        //titleStyle.SetFont(titleFont);
        ////设置表头样式
        //var headStyle = book.CreateCellStyle();
        //headStyle.Alignment = HorizontalAlignment.Center;
        //headStyle.VerticalAlignment = VerticalAlignment.Center;
        //var headFont = book.CreateFont();
        //headFont.FontHeightInPoints = 14;
        //headFont.IsBold = true;
        //headStyle.SetFont(headFont);
        ////设置标题样式
        //var listStyle = book.CreateCellStyle();
        //listStyle.Alignment = HorizontalAlignment.Center;
        //listStyle.VerticalAlignment = VerticalAlignment.Center;
        //var listFont = book.CreateFont();
        //listFont.FontHeightInPoints = 14;
        //listStyle.SetFont(listFont);
        ////填入标题内容并合并
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 12));
        //var titleRow = sheet.CreateRow(0);
        //titleRow.CreateCell(0).SetCellValue("通航计划表-范围内");
        //titleRow.Cells[0].CellStyle = titleStyle;
        //titleRow.Height = 50 * 20;
        ////填入表头内容并合并
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 2, 2));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 3, 3));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 4, 4));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 5, 5));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 6, 6));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 7, 7));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 8, 9));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 10, 11));
        //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 12, 12));
        //var headRow1 = sheet.CreateRow(1);
        //headRow1.CreateCell(0).SetCellValue("序号");
        //headRow1.CreateCell(1).SetCellValue("起降场");
        //headRow1.CreateCell(2).SetCellValue("单位");
        //headRow1.CreateCell(3).SetCellValue("机型/架次");
        //headRow1.CreateCell(4).SetCellValue("机号");
        //headRow1.CreateCell(5).SetCellValue("空域/航线");
        //headRow1.CreateCell(6).SetCellValue("高度");
        //headRow1.CreateCell(7).SetCellValue("任务");
        //headRow1.CreateCell(8).SetCellValue("开飞时间");
        //headRow1.CreateCell(10).SetCellValue("结束时间");
        //headRow1.CreateCell(12).SetCellValue("备注");
        //foreach (var cell in headRow1.Cells)
        //{
        //    cell.CellStyle = headStyle;
        //}
        //headRow1.Height = 25 * 20;
        //var headRow2 = sheet.CreateRow(2);
        //headRow2.CreateCell(8).SetCellValue("计划");
        //headRow2.CreateCell(9).SetCellValue("实施");
        //headRow2.CreateCell(10).SetCellValue("计划");
        //headRow2.CreateCell(11).SetCellValue("实施");
        //foreach (var cell in headRow2.Cells)
        //{
        //    cell.CellStyle = headStyle;
        //}
        //headRow2.Height = 25 * 20;

        //var exportlist = new ExportDataBLL().FlightPlanDataExport(planlist);
        //for (int i = 0; i < exportlist.Count; i++)
        //{
        //    var row = sheet.CreateRow(3 + i);
        //    row.CreateCell(0).SetCellValue(i + 1);
        //    row.CreateCell(1).SetCellValue(exportlist[i].airport);
        //    row.CreateCell(2).SetCellValue(exportlist[i].company);
        //    row.CreateCell(3).SetCellValue(exportlist[i].airtype);
        //    row.CreateCell(4).SetCellValue(exportlist[i].aircraft);
        //    row.CreateCell(5).SetCellValue(exportlist[i].airline);
        //    row.CreateCell(6).SetCellValue(exportlist[i].high);
        //    row.CreateCell(7).SetCellValue(exportlist[i].airtype);
        //    row.CreateCell(8).SetCellValue(exportlist[i].planbegin);
        //    row.CreateCell(10).SetCellValue(exportlist[i].planend);
        //    row.CreateCell(12).SetCellValue(exportlist[i].remark);
        //    foreach (var cell in row.Cells)
        //    {
        //        cell.CellStyle = listStyle;
        //    }
        //    //row.Height = 30 * 20;
        //}
        //生成文件
        var file = new MemoryStream();
        doc.Write(file);
        Response.ContentType = "application/msword";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "";
        Response.Clear();
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("通航计划表-范围内.doc", Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }

    //private void MyUnSubmitRepetPlanExport()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = true;
    //    var listData = bll.GetList(GetWhere());

    //    #region
    //    var hssfworkbook = new HSSFWorkbook();
    //    var sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    sheet1.DefaultRowHeight = 15 * 20;
    //    sheet1.DefaultColumnWidth = 18;

    //    //设置样式
    //    var styleTop = hssfworkbook.CreateCellStyle();
    //    var fontTop = hssfworkbook.CreateFont();
    //    fontTop.FontHeightInPoints = 11;
    //    fontTop.FontName = "宋体";
    //    fontTop.Boldweight = (short)FontBoldWeight.Bold;
    //    styleTop.Alignment = HorizontalAlignment.Center;
    //    styleTop.SetFont(fontTop);

    //    //设置样式
    //    var style = hssfworkbook.CreateCellStyle();
    //    var font = hssfworkbook.CreateFont();
    //    font.FontName = "宋体";
    //    font.FontHeightInPoints = 11;
    //    style.SetFont(font);

    //    var headerRow = sheet1.CreateRow(0);

    //    headerRow.CreateCell(0).SetCellValue("申请单号");
    //    headerRow.CreateCell(1).SetCellValue("公司名称");
    //    headerRow.CreateCell(2).SetCellValue("任务类型");
    //    headerRow.CreateCell(3).SetCellValue("航空器类型");
    //   // headerRow.CreateCell(4).SetCellValue("航线走向和飞行高度");
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
    //            var dataRow = sheet1.CreateRow(rowIndex);
    //            dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //            dataRow.CreateCell(1).SetCellValue(item.CompanyName);
    //            dataRow.CreateCell(2).SetCellValue(item.FlightType);
    //            dataRow.CreateCell(3).SetCellValue(item.AircraftType);
    //            dataRow.CreateCell(4).SetCellValue(item.FlightArea);
    //            dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
    //            dataRow.CreateCell(6).SetCellValue(item.CallSign);
    //            dataRow.CreateCell(7).SetCellValue(item.ADEP);
    //            dataRow.CreateCell(8).SetCellValue(item.ADES);
    //            dataRow.CreateCell(9).SetCellValue(item.StartDate.ToString());
    //            dataRow.CreateCell(10).SetCellValue(item.EndDate.ToString());
    //            dataRow.CreateCell(11).SetCellValue(item.StartDate.ToString());
    //            dataRow.CreateCell(12).SetCellValue(item.EndDate.ToString());
    //            dataRow.CreateCell(13).SetCellValue(item.WeekSchedule);
    //            dataRow.CreateCell(14).SetCellValue(item.Remark);
    //            rowIndex++;
    //        }
    //        var dr = sheet1.CreateRow(rowIndex);
    //        rowIndex++;
    //    }

    //    #endregion
    //    var file = new MemoryStream();
    //    hssfworkbook.Write(file);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.UTF8;
    //    Response.Charset = "";
    //    Response.Clear();
    //    Response.AppendHeader("Content-Disposition",
    //                          "attachment;filename=" +
    //                          HttpUtility.UrlEncode("长期计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
    //    file.WriteTo(Response.OutputStream);
    //    file.Close();
    //    Response.End();
    //}
    //private void MySubmitFlightPlanExport()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = true;
    //    Expression<Func<FlightPlan, bool>> predicate = PredicateBuilder.True<FlightPlan>();
    //    predicate = predicate.And(m => m.PlanState != "0");
    //    predicate = predicate.And(m => m.Creator == User.ID);
    //    if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
    //    {
    //        var val = Request.QueryString["plancode"].Trim();
    //        predicate = predicate.And(m => m.PlanCode == val);
    //    }
    //    var listData = fbll.GetList(predicate);

    //    #region
    //    var hssfworkbook = new HSSFWorkbook();
    //    var sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    sheet1.DefaultRowHeight = 15 * 20;
    //    sheet1.DefaultColumnWidth = 18;

    //    //设置样式
    //    var styleTop = hssfworkbook.CreateCellStyle();
    //    var fontTop = hssfworkbook.CreateFont();
    //    fontTop.FontHeightInPoints = 11;
    //    fontTop.FontName = "宋体";
    //    fontTop.Boldweight = (short)FontBoldWeight.Bold;
    //    styleTop.Alignment = HorizontalAlignment.Center;
    //    styleTop.SetFont(fontTop);

    //    //设置样式
    //    var style = hssfworkbook.CreateCellStyle();
    //    var font = hssfworkbook.CreateFont();
    //    font.FontName = "宋体";
    //    font.FontHeightInPoints = 11;
    //    style.SetFont(font);

    //    var headerRow = sheet1.CreateRow(0);

    //    headerRow.CreateCell(0).SetCellValue("申请单编号");
    //    headerRow.CreateCell(1).SetCellValue("公司名称");
    //    headerRow.CreateCell(2).SetCellValue("任务类型");
    //    headerRow.CreateCell(3).SetCellValue("航空器类型");
    //    headerRow.CreateCell(4).SetCellValue("飞行范围");
    //    headerRow.CreateCell(5).SetCellValue("飞行高度");
    //    headerRow.CreateCell(6).SetCellValue("起飞点");
    //    headerRow.CreateCell(7).SetCellValue("降落点");
    //    headerRow.CreateCell(8).SetCellValue("起飞时刻");
    //    headerRow.CreateCell(9).SetCellValue("降落时刻");
    //    headerRow.CreateCell(10).SetCellValue("其他需要说明的事项");
    //    headerRow.CreateCell(11).SetCellValue("航空器架数");
    //    headerRow.CreateCell(12).SetCellValue("机长（飞行员）姓名");
    //    headerRow.CreateCell(13).SetCellValue("通信联络方法");
    //    headerRow.CreateCell(14).SetCellValue("飞行气象条件");
    //    headerRow.CreateCell(15).SetCellValue("空勤组人数");
    //    headerRow.CreateCell(16).SetCellValue("应答机代码");
    //    headerRow.CreateCell(17).SetCellValue("状态");
    //    int rowIndex = 1;
    //    if (listData != null && listData.Count > 0)
    //    {
    //        foreach (var item in listData)
    //        {
    //            var dataRow = sheet1.CreateRow(rowIndex);
    //            dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //            dataRow.CreateCell(1).SetCellValue(item.CompanyName);
    //            dataRow.CreateCell(2).SetCellValue(item.FlightType);
    //            dataRow.CreateCell(3).SetCellValue(item.AircraftType);
    //         //   dataRow.CreateCell(4).SetCellValue(item.FlightDirHeight);
    //            dataRow.CreateCell(4).SetCellValue(item.FlightArea);
    //            dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
    //            dataRow.CreateCell(6).SetCellValue(item.ADEP);
    //            dataRow.CreateCell(7).SetCellValue(item.ADES);  
    //            dataRow.CreateCell(8).SetCellValue(item.SOBT.ToString());
    //            dataRow.CreateCell(9).SetCellValue(item.SIBT.ToString());
    //            dataRow.CreateCell(10).SetCellValue(item.Remark);
    //            dataRow.CreateCell(11).SetCellValue((item.AircraftNum ?? 0).ToString()); 
    //            dataRow.CreateCell(12).SetCellValue(item.Pilot);
    //            dataRow.CreateCell(13).SetCellValue(item.ContactWay);
    //            dataRow.CreateCell(14).SetCellValue(item.WeatherCondition);
    //            dataRow.CreateCell(15).SetCellValue((item.AircrewGroupNum??0).ToString());          
    //            dataRow.CreateCell(16).SetCellValue(item.RadarCode);
    //               var str = "";
    //                          if (item.PlanState == "end") {
    //                              str = "审核通过";
    //                          }
    //                          else if (item.PlanState == "Deserted") {
    //                              str = "审核不通过";
    //                          }
    //                          else {
    //                              str = item.PlanState + "审核中";
    //                          }
    //            dataRow.CreateCell(17).SetCellValue(str); 
    //            rowIndex++;
    //        }
    //        var dr = sheet1.CreateRow(rowIndex);
    //        rowIndex++;
    //    }

    //    #endregion
    //    var file = new MemoryStream();
    //    hssfworkbook.Write(file);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.UTF8;
    //    Response.Charset = "";
    //    Response.Clear();
    //    Response.AppendHeader("Content-Disposition",
    //                          "attachment;filename=" +
    //                          HttpUtility.UrlEncode("长期计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
    //    file.WriteTo(Response.OutputStream);
    //    file.Close();
    //    Response.End();
    //}
    //private void MySubmitRepetPlanExport()
    //{
    //    Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
    //    predicate = predicate.And(m => m.PlanState != "0" && m.Creator == User.ID);
    //    if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
    //    {
    //        var val = Request.QueryString["plancode"].Trim();
    //        predicate = predicate.And(m => m.PlanCode == val);
    //    }
    //    List<RepetitivePlan>ReList=new RepetitivePlanBLL().GetList(predicate);
    //    #region
    //    var hssfworkbook = new HSSFWorkbook();
    //    var sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    sheet1.DefaultRowHeight = 15 * 20;
    //    sheet1.DefaultColumnWidth = 18;

    //    //设置样式
    //    var styleTop = hssfworkbook.CreateCellStyle();
    //    var fontTop = hssfworkbook.CreateFont();
    //    fontTop.FontHeightInPoints = 11;
    //    fontTop.FontName = "宋体";
    //    fontTop.Boldweight = (short)FontBoldWeight.Bold;
    //    styleTop.Alignment = HorizontalAlignment.Center;
    //    styleTop.SetFont(fontTop);

    //    //设置样式
    //    var style = hssfworkbook.CreateCellStyle();
    //    var font = hssfworkbook.CreateFont();
    //    font.FontName = "宋体";
    //    font.FontHeightInPoints = 11;
    //    style.SetFont(font);

    //    var headerRow = sheet1.CreateRow(0);

    //    headerRow.CreateCell(0).SetCellValue("申请单编号");
    //    headerRow.CreateCell(1).SetCellValue("任务类型");
    //    headerRow.CreateCell(2).SetCellValue("注册号");
    //    headerRow.CreateCell(3).SetCellValue("使用机型");
    //    headerRow.CreateCell(4).SetCellValue("飞行范围");
    //    headerRow.CreateCell(5).SetCellValue("飞行高度");
    //    headerRow.CreateCell(6).SetCellValue("预计开始时间");
    //    headerRow.CreateCell(7).SetCellValue("预计结束时间");
    //    headerRow.CreateCell(8).SetCellValue("起飞时刻");
    //    headerRow.CreateCell(9).SetCellValue("降落时刻");
    //    headerRow.CreateCell(10).SetCellValue("起飞点");
    //    headerRow.CreateCell(11).SetCellValue("降落点");
    //    headerRow.CreateCell(12).SetCellValue("备降点");
    //    headerRow.CreateCell(13).SetCellValue("周执行计划");
    //    headerRow.CreateCell(14).SetCellValue("创建人");
    //    headerRow.CreateCell(15).SetCellValue("状态");
    //    int rowIndex = 1;
    //    if (ReList.Count > 0)
    //    {
    //        foreach (var item in ReList)
    //        {
    //            var dataRow = sheet1.CreateRow(rowIndex);
    //            dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //            dataRow.CreateCell(1).SetCellValue(item.FlightType);
    //            dataRow.CreateCell(2).SetCellValue(item.CallSign);
    //            dataRow.CreateCell(3).SetCellValue(item.AircraftType);
    //            dataRow.CreateCell(4).SetCellValue(item.FlightArea);
    //            dataRow.CreateCell(5).SetCellValue(item.FlightHeight);
    //            dataRow.CreateCell(6).SetCellValue(item.StartDate.ToString("yyyy-MM-dd"));
    //            dataRow.CreateCell(7).SetCellValue(item.EndDate.ToString("yyyy-MM-dd"));
    //            dataRow.CreateCell(8).SetCellValue(item.StartDate.ToString());
    //            dataRow.CreateCell(9).SetCellValue(item.EndDate.ToString());
    //            dataRow.CreateCell(10).SetCellValue(item.ADEP);
    //            dataRow.CreateCell(11).SetCellValue(item.ADES);
    //            dataRow.CreateCell(12).SetCellValue(item.Alternate);
    //            item.WeekSchedule = item.WeekSchedule.Replace("*","");
    //            dataRow.CreateCell(13).SetCellValue("星期"+item.WeekSchedule);
    //            dataRow.CreateCell(14).SetCellValue(item.CreatorName);
    //            //dataRow.CreateCell(17).SetCellValue(item.PlanState);
    //            var str = "";
    //            if (item.PlanState == "end")
    //            {
    //                str = "审核通过";
    //            }
    //            else if (item.PlanState == "Deserted")
    //            {
    //                str = "审核不通过";
    //            }
    //            else
    //            {
    //                str = item.PlanState + "审核中";
    //            }
    //            dataRow.CreateCell(15).SetCellValue(str);
    //            rowIndex++;
    //        }
    //        var dr = sheet1.CreateRow(rowIndex);
    //        rowIndex++;
    //    }

    //    #endregion
    //    var file = new MemoryStream();
    //    hssfworkbook.Write(file);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.UTF8;
    //    Response.Charset = "";
    //    Response.Clear();
    //    Response.AppendHeader("Content-Disposition",
    //                          "attachment;filename=" +
    //                          "长期计划已提交列表" + ".xls");
    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    file.WriteTo(Response.OutputStream);
    //    file.Close();
    //    Response.End();
    //}
    //private void MyFinishAuditFlightPlanExport()
    //{
    //    Expression<Func<vGetFlightPlanNodeInstance, bool>> predicate = PredicateBuilder.True<vGetFlightPlanNodeInstance>();
    //    predicate = predicate.And(m => m.ActorID != m.Creator);
    //    predicate = predicate.And(m => m.ActorID == User.ID);
    //    predicate = predicate.And(m => m.State == 2 || m.State == 3);
    //    if (!string.IsNullOrEmpty(Request.QueryString["plancode"]) )
    //    {
    //        var val = Request.QueryString["plancode"].Trim();
    //        predicate = predicate.And(m => m.PlanCode == val);
    //    }
    //    List<vGetFlightPlanNodeInstance> listData = fbll.GetNodeInstanceList(predicate);

    //    #region
    //    var hssfworkbook = new HSSFWorkbook();
    //    var sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    sheet1.DefaultRowHeight = 15 * 20;
    //    sheet1.DefaultColumnWidth = 18;

    //    //设置样式
    //    var styleTop = hssfworkbook.CreateCellStyle();
    //    var fontTop = hssfworkbook.CreateFont();
    //    fontTop.FontHeightInPoints = 11;
    //    fontTop.FontName = "宋体";
    //    fontTop.Boldweight = (short)FontBoldWeight.Bold;
    //    styleTop.Alignment = HorizontalAlignment.Center;
    //    styleTop.SetFont(fontTop);

    //    //设置样式
    //    var style = hssfworkbook.CreateCellStyle();
    //    var font = hssfworkbook.CreateFont();
    //    font.FontName = "宋体";
    //    font.FontHeightInPoints = 11;
    //    style.SetFont(font);

    //    var headerRow = sheet1.CreateRow(0);

    //    headerRow.CreateCell(0).SetCellValue("申请单编号");
    //    headerRow.CreateCell(1).SetCellValue("航空器架数");
    //    headerRow.CreateCell(2).SetCellValue("机长（飞行员）姓名");
    //    headerRow.CreateCell(3).SetCellValue("通信联络方法");
    //    headerRow.CreateCell(4).SetCellValue("航空器类型");
    //    headerRow.CreateCell(5).SetCellValue("飞行高度");
    //    headerRow.CreateCell(6).SetCellValue("飞行范围");
    //    headerRow.CreateCell(7).SetCellValue("任务类型");
    //    headerRow.CreateCell(8).SetCellValue("起落点");
    //    headerRow.CreateCell(9).SetCellValue("降落点");
    //    headerRow.CreateCell(10).SetCellValue("起飞时刻");
    //    headerRow.CreateCell(11).SetCellValue("降落时刻");
    //    headerRow.CreateCell(12).SetCellValue("飞行气象条件");
    //    headerRow.CreateCell(13).SetCellValue("空勤组人数");
    //    headerRow.CreateCell(14).SetCellValue("应答机代码");
    //    headerRow.CreateCell(15).SetCellValue("公司名称");
    //    headerRow.CreateCell(16).SetCellValue("审核意见");     
    //    headerRow.CreateCell(17).SetCellValue("审核时间");     

    //    int rowIndex = 1;
    //    if (listData != null && listData.Count > 0)
    //    {
    //        foreach (var item in listData)
    //        {
    //            var dataRow = sheet1.CreateRow(rowIndex);
    //            dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //            dataRow.CreateCell(1).SetCellValue(item.AircraftNum==null?0:item.AircraftNum.Value);
    //            dataRow.CreateCell(2).SetCellValue(item.Pilot);
    //            dataRow.CreateCell(3).SetCellValue(item.ContactWay);
    //            dataRow.CreateCell(4).SetCellValue(item.AircraftType);
    //            dataRow.CreateCell(5).SetCellValue(item.FlightDirHeight);
    //            dataRow.CreateCell(6).SetCellValue(item.FlightArea);
    //            dataRow.CreateCell(7).SetCellValue(item.FlightType);
    //            dataRow.CreateCell(8).SetCellValue(item.ADEP);
    //            dataRow.CreateCell(9).SetCellValue(item.ADES);
    //            dataRow.CreateCell(10).SetCellValue(item.SOBT==null?"":item.SOBT.ToString());
    //            dataRow.CreateCell(11).SetCellValue(item.SIBT==null?"":item.SIBT.ToString());
    //            dataRow.CreateCell(12).SetCellValue(item.WeatherCondition);
    //            dataRow.CreateCell(13).SetCellValue(item.AircrewGroupNum==null?0:item.AircrewGroupNum.Value);
    //            dataRow.CreateCell(14).SetCellValue(item.RadarCode);
    //            dataRow.CreateCell(15).SetCellValue(item.CompanyName);
    //            dataRow.CreateCell(16).SetCellValue(item.Comments);
    //            dataRow.CreateCell(17).SetCellValue(item.ActorTime==null?"":item.ActorTime.Value.ToString());
    //            rowIndex++;
    //        }
    //        var dr = sheet1.CreateRow(rowIndex);
    //        rowIndex++;
    //    }

    //    #endregion
    //    var file = new MemoryStream();
    //    hssfworkbook.Write(file);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.UTF8;
    //    Response.Charset = "";
    //    Response.Clear();
    //    Response.AppendHeader("Content-Disposition",
    //                          "attachment;filename=" +
    //                          HttpUtility.UrlEncode("飞行计划已审核列表" + ".xls", System.Text.Encoding.UTF8));
    //    file.WriteTo(Response.OutputStream);
    //    file.Close();
    //    Response.End();
    //}
    private void FlightPlanStatisticsExport()
    {
        var started = Request.QueryString["started"] == null ? DateTime.Now : DateTime.Parse(Request.QueryString["started"]);
        var ended = Request.QueryString["ended"] == null ? DateTime.Now : DateTime.Parse(Request.QueryString["ended"]);
        List<FlightPlanStatistics> fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(new FlightPlanDAL().GetFullTimeFlightStatistics(started, ended)));
        fplist = JsonConvert.DeserializeObject<List<FlightPlanStatistics>>(JsonConvert.SerializeObject(fplist.GroupBy(x => new { x.Creator, x.CompanyName }).Select(group => new
        {
            Creator = group.Key.Creator,
            CompanyName = group.Key.CompanyName,
            AircraftNum = group.Sum(p => p.AircraftNum),
            SecondDiff = group.Sum(p => p.SecondDiff)
        }).ToList()));
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

        headerRow.CreateCell(0).SetCellValue("航空公司");
        headerRow.CreateCell(1).SetCellValue("飞行时长");
        headerRow.CreateCell(2).SetCellValue("飞行架次(数)");
        headerRow.CreateCell(3).SetCellValue("开始时间");
        headerRow.CreateCell(4).SetCellValue("结束时间");

        int rowIndex = 1;
        if (fplist != null && fplist.Count > 0)
        {
            foreach (var item in fplist)
            {
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.CompanyName);
                if (item.SecondDiff > 0 && item.SecondDiff < 60)
                {
                    dataRow.CreateCell(1).SetCellValue(item.SecondDiff.ToString() + "秒");
                }
                else if (item.SecondDiff >= 60 && item.SecondDiff < 60 * 60)
                {
                    dataRow.CreateCell(1).SetCellValue((item.SecondDiff / 60).ToString() + "分" + (item.SecondDiff % 60).ToString() + "秒");
                }
                else if (item.SecondDiff >= 60 * 60)//&& item.SecondDiff < 60 * 60 * 60
                {
                    dataRow.CreateCell(1).SetCellValue((item.SecondDiff / 3600).ToString() + "时" + ((item.SecondDiff - (item.SecondDiff / 3600) * 3600) / 60).ToString() + "分" + ((item.SecondDiff - (item.SecondDiff / 3600) * 3600) % 60).ToString() + "秒");
                }
                else {
                    dataRow.CreateCell(1).SetCellValue(item.SecondDiff);
                }
                //dataRow.CreateCell(1).SetCellValue(item.SecondDiff);
                dataRow.CreateCell(2).SetCellValue(item.AircraftNum);
                dataRow.CreateCell(3).SetCellValue(started.ToString());
                dataRow.CreateCell(4).SetCellValue(ended.ToString());
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
                              HttpUtility.UrlEncode("飞行统计列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    ///// <summary>
    ///// 组合搜索条件
    ///// </summary>
    ///// <returns></returns>
    //private Expression<Func<RepetitivePlan, bool>> GetWhere()
    //{

    //    Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
    //    predicate = predicate.And(m => m.PlanState == "0");
    //    predicate = predicate.And(m => m.Creator == User.ID);
    //    if (!string.IsNullOrEmpty(Request.QueryString["plancode"]))
    //    {
    //        var val = Request.QueryString["plancode"].Trim();
    //        predicate = predicate.And(m => m.PlanCode == val);
    //    }

    //    return predicate;
    //}
    ///*
    //private void MyUnSubmitCurrentPlanExport()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = true;
    //    var listData = currPlanBll.GetList(GetWhere1());

    //    #region
    //    var hssfworkbook = new HSSFWorkbook();
    //    var sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    sheet1.DefaultRowHeight = 15 * 20;
    //    sheet1.DefaultColumnWidth = 18;

    //    //设置样式
    //    var styleTop = hssfworkbook.CreateCellStyle();
    //    var fontTop = hssfworkbook.CreateFont();
    //    fontTop.FontHeightInPoints = 11;
    //    fontTop.FontName = "宋体";
    //    fontTop.Boldweight = (short)FontBoldWeight.Bold;
    //    styleTop.Alignment = HorizontalAlignment.Center;
    //    styleTop.SetFont(fontTop);

    //    //设置样式
    //    var style = hssfworkbook.CreateCellStyle();
    //    var font = hssfworkbook.CreateFont();
    //    font.FontName = "宋体";
    //    font.FontHeightInPoints = 11;
    //    style.SetFont(font);

    //    var headerRow = sheet1.CreateRow(0);

    //    headerRow.CreateCell(0).SetCellValue("申请单号");
    //    headerRow.CreateCell(1).SetCellValue("任务类型");
    //    headerRow.CreateCell(2).SetCellValue("注册号");
    //    headerRow.CreateCell(3).SetCellValue("使用机型");
    //    headerRow.CreateCell(4).SetCellValue("航线走向和飞行高度");
    //    headerRow.CreateCell(5).SetCellValue("预计开始时间");
    //    headerRow.CreateCell(6).SetCellValue("预计结束时间");
    //    headerRow.CreateCell(7).SetCellValue("起飞时刻");
    //    headerRow.CreateCell(8).SetCellValue("降落时刻");
    //    headerRow.CreateCell(9).SetCellValue("起飞点");
    //    headerRow.CreateCell(10).SetCellValue("降落点");
    //    headerRow.CreateCell(11).SetCellValue("周执行计划");
    //    headerRow.CreateCell(12).SetCellValue("其他需要说明的事项");
    //    int rowIndex = 1;
    //    if (listData != null && listData.Count > 0)
    //    {
    //        foreach (var item in listData)
    //        {
    //            var dataRow = sheet1.CreateRow(rowIndex);
    //            dataRow.CreateCell(0).SetCellValue(item.PlanCode);
    //            dataRow.CreateCell(1).SetCellValue(item.FlightType);
    //            dataRow.CreateCell(2).SetCellValue(item.CallSign);
    //            dataRow.CreateCell(3).SetCellValue(item.AircraftType);
    //            dataRow.CreateCell(4).SetCellValue(item.FlightDirHeight);
    //            dataRow.CreateCell(5).SetCellValue(item.StartDate.ToString());
    //            dataRow.CreateCell(6).SetCellValue(item.EndDate.ToString());
    //            dataRow.CreateCell(7).SetCellValue(item.SOBT.ToString());
    //            dataRow.CreateCell(8).SetCellValue(item.SIBT.ToString());
    //            dataRow.CreateCell(9).SetCellValue(item.ADEP);
    //            dataRow.CreateCell(10).SetCellValue(item.ADES);
    //            dataRow.CreateCell(11).SetCellValue(item.WeekSchedule);
    //            dataRow.CreateCell(12).SetCellValue(item.Remark);
    //            rowIndex++;
    //        }
    //        var dr = sheet1.CreateRow(rowIndex);
    //        rowIndex++;
    //    }

    //    #endregion
    //    var file = new MemoryStream();
    //    hssfworkbook.Write(file);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.UTF8;
    //    Response.Charset = "";
    //    Response.Clear();
    //    Response.AppendHeader("Content-Disposition",
    //                          "attachment;filename=" +
    //                          HttpUtility.UrlEncode("当日计划未提交列表" + ".xls", System.Text.Encoding.UTF8));
    //    file.WriteTo(Response.OutputStream);
    //    file.Close();
    //    Response.End();
    //}
    //private Expression<Func<V_CurrentPlan, bool>> GetWhere1()
    //{
    //    Expression<Func<V_CurrentPlan, bool>> predicate = PredicateBuilder.True<V_CurrentPlan>();
    //    var currDate = DateTime.Now.Date;
    //    predicate = predicate.And(m => m.PlanState == "0" && m.Creator == User.ID && m.SOBT == currDate);       

    //    return predicate;
    //}

    public void mergeCellsHorizontal(XWPFTable table, int row, int fromCell, int toCell)
    {
        for (int cellIndex = fromCell; cellIndex <= toCell; cellIndex++)
        {
            XWPFTableCell cell = table.GetRow(row).GetCell(cellIndex);
            if (cellIndex == fromCell)
            {
                // The first merged cell is set with RESTART merge value  
                cell.GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.restart;
            }
            else
            {
                // Cells which join (merge) the first one, are set with CONTINUE  
                cell.GetCTTc().AddNewTcPr().AddNewHMerge().val = ST_Merge.@continue;
            }
        }
    }
    // word跨行并单元格
    public void mergeCellsVertically(XWPFTable table, int col, int fromRow, int toRow)
    {
        for (int rowIndex = fromRow; rowIndex <= toRow; rowIndex++)
        {
            XWPFTableCell cell = table.GetRow(rowIndex).GetCell(col);
            if (rowIndex == fromRow)
            {
                // The first merged cell is set with RESTART merge value  
                cell.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.restart;
            }
            else
            {
                // Cells which join (merge) the first one, are set with CONTINUE  
                cell.GetCTTc().AddNewTcPr().AddNewVMerge().val = ST_Merge.@continue;
            }
        }
    }

    private void setCell(XWPFTableCell cell, string cellText)
    {
        XWPFParagraph pIO = cell.AddParagraph();
        pIO.Alignment = ParagraphAlignment.CENTER;
        XWPFRun rIO = pIO.CreateRun();
        rIO.FontFamily = "宋体";
        rIO.FontSize = 12;
        rIO.SetText(cellText);
    }

    private void setCell(XWPFTableCell cell, List<string> cellText)
    {
        foreach (var txt in cellText)
        {
            XWPFParagraph pIO = cell.AddParagraph();
            pIO.Alignment = ParagraphAlignment.CENTER;
            XWPFRun rIO = pIO.CreateRun();
            rIO.FontFamily = "宋体";
            rIO.FontSize = 12;
            rIO.SetText(txt);
        }
    }
}