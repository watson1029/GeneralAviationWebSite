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

public partial class BasicData_PilotExportHandler : BasePage
{
    PilotBLL bll = new PilotBLL();
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
        font.FontHeightInPoints = 14;
        style.SetFont(font);

        var headerRow = sheet1.CreateRow(0);

        
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
                var dataRow = sheet1.CreateRow(rowIndex);
                
                dataRow.CreateCell(0).SetCellValue(item.Pilots);
                dataRow.CreateCell(1).SetCellValue(item.PilotCardNo);
                dataRow.CreateCell(2).SetCellValue(item.PilotDT);
                dataRow.CreateCell(3).SetCellValue(item.PilotAge.ToString());
                dataRow.CreateCell(4).SetCellValue(item.PhoneNo);
                dataRow.CreateCell(5).SetCellValue(item.LicenseNo);
                dataRow.CreateCell(6).SetCellValue(item.Sign);
                dataRow.CreateCell(7).SetCellValue(item.LicenseTime.ToString());
        
                dataRow.CreateCell(8).SetCellValue(item.Licensesort=="0"?"航线运输驾驶执照": (item.Licensesort == "1" ? "商用飞机驾照" : "私用飞机驾照"));
                dataRow.CreateCell(9).SetCellValue(item.Sex == 0 ? "男" : "女");
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
    private Expression<Func<Pilot, bool>> GetWhere()
    {

        Expression<Func<Pilot, bool>> predicate = PredicateBuilder.True<Pilot>();
        //predicate = predicate.And(m => m.PlanState == "0");
        //predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            //predicate = predicate.And(m => m.PlanCode == Request.Form["search_value"]);
        }

        return predicate;
    }
}