using BLL.BasicData;

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

public partial class BasicData_ExportHandler : BasePage
{
    AircraftBLL bll = new AircraftBLL();
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
        fontTop.FontHeightInPoints = 14;
        fontTop.FontName = "宋体";
        fontTop.Boldweight = (short)FontBoldWeight.Bold;
        styleTop.Alignment = HorizontalAlignment.Center;
        styleTop.SetFont(fontTop);

        //设置样式
        var style = hssfworkbook.CreateCellStyle();
        var font = hssfworkbook.CreateFont();
        font.FontName = "宋体";
        font.FontHeightInPoints = 16;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        headerRow.CreateCell(0).SetCellValue("注册号");
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
                var dataRow = sheet1.CreateRow(rowIndex);
                dataRow.CreateCell(0).SetCellValue(item.AircraftSign.ToString());
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
                              HttpUtility.UrlEncode("飞行器信息列表" + ".xls", System.Text.Encoding.UTF8));
        file.WriteTo(Response.OutputStream);
        file.Close();
        Response.End();
    }
    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<Aircraft, bool>> GetWhere()
    {

        Expression<Func<Aircraft, bool>> predicate = PredicateBuilder.True<Aircraft>();
        //predicate = predicate.And(m => m.PlanState == "0");
        //predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            //predicate = predicate.And(m => m.PlanCode == Request.Form["search_value"]);
        }

        return predicate;
    }
}