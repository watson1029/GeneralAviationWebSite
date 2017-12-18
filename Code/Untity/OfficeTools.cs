using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
namespace Untity
{
    public class OfficeTools
    {
        public static DataTable GetDT(string filename)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {

                    workbook = new XSSFWorkbook(file);

            }

            //获取excel的第一个sheet
            var sheet = workbook.GetSheetAt(0);

            DataTable table = new DataTable();
            //获取sheet的首行
            var headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null
                        && row.GetCell(j).CellType == NPOI.SS.UserModel.CellType.Numeric)
                    {
                        if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                        { dataRow[j] = row.GetCell(j).DateCellValue.ToString(); }
                        else
                        {
                            dataRow[j] = row.GetCell(j).NumericCellValue;
                        }
                    }

                    else if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
