using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseForm.Helpers
{
    public class ExportExcel
    {
        public static void ExportData<T>(List<T> collection, string sFile, string sSheet)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // 新增worksheet
                ExcelWorksheet ws = package.Workbook.Worksheets.Add(sSheet);


                ws.Cells["A1"].LoadFromCollection<T>(collection, true);
                //ws.Cells.AutoFitColumns();

                //儲存檔案
                using (FileStream file = new FileStream(sFile, FileMode.Create))
                {
                    package.SaveAs(file);
                }
            }
        }
    }
}
