using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Products_App
{
    public class Reports
    {
        public static void OrderReport(string fileName, double[] force, double[] mu, double[] error)
        {
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook book = null;
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;
            //Запустить приложение
            app = new Application();

            app.Visible = false;
            //Создать новую книгу
            book = app.Workbooks.Add(Type.Missing);

            if (book != null)
            {
                sheet = (Worksheet) book.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);

                Range range;
                string cell;
                //Работа с листом
                /*Range range = sheet.get_Range("A3", Type.Missing);
                range.Formula = "Отчёт по продажам";
                string period = "За период с " + begin.ToShortDateString() + " по " + end.ToShortDateString();

                range = sheet.get_Range("A5", Type.Missing);
                range.Formula = period;

                string product = "Товар: " + productName;
                range = sheet.get_Range("A7", Type.Missing);
                range.Formula = product;

                int row = 9;
                string[] columns = { "A", "B", "C", "D", "E", "F" };
                string cell;
                for (int i = 0; i < 6; i++)
                {
                    //Имя ячейки
                    cell = columns[i] + row.ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = table.Columns[i].HeaderText;
                }*/
                //строки таблицы
                // row = 10;
                string[] columns = { "A", "B" };
                for (int i = 0; i < mu.Length; i++)
                {
                    cell = columns[0] + (i).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = force[i].ToString().Replace(',', '.');

                    cell = columns[1] + (i).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = mu[i].ToString().Replace(',', '.');

                    cell = columns[2] + (i).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = error[i].ToString().Replace(',', '.');

                    
                }
                /*int k = row + table.Rows.Count;
                int m = 5;

                cell = columns[m] + k.ToString();
                range = sheet.get_Range(cell, Type.Missing);
                range.Formula = "=СУММ(" + columns[m] + row.ToString() + ":" + columns[m] + (k - 1).ToString() + ")";*/
            }

            //Закрыть книгу
            if (book != null)
            {
                //book.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   // XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                book.Close(true, fileName, Type.Missing);
            }

            //Закрыть приложение
            if (app != null)
                app.Quit();
        }
    }
}
