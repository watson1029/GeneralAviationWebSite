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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_ExportHandler : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            switch (Request.QueryString["type"])
            {
                case "1"://查询数据
                    MyUnSubmitFlightPlanExport();
                    break;
                default:
                    break;
            }
        }
    }
    private void MyUnSubmitFlightPlanExport()
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
                var dataRow = sheet1.CreateRow(rowIndex);
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
}