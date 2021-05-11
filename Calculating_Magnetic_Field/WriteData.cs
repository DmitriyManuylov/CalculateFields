using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Calculating_Magnetic_Field
{
    public class WriteData
    {
        public static void OrderReport(string fileName, double[] force, double[] mu, double[] error)
        {
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook book = null;
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;
            Sheets worksheets = null;
            Workbooks workbooks = null;
            //Запустить приложение
            app = new Application();

            app.Visible = false;
            //Создать новую книгу
            book = app.Workbooks.Add(Type.Missing);
           // workbooks = app.Workbooks;
            //book = workbooks[1];
            if (book != null)
            {
                sheet = (Worksheet)book.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);

                //worksheets = book.Worksheets;
                

                Range range;
                string cell;
                //Работа с листом


                string[] columns = { "A", "B", "C", "D" };
                cell = columns[0] + (1).ToString();
                range = sheet.get_Range(cell, Type.Missing);
                range.Formula = "Сила";
                cell = columns[1] + (1).ToString();
                range = sheet.get_Range(cell, Type.Missing);
                range.Formula = "Магнитная проницаемость";

                cell = columns[2] + (1).ToString();
                range = sheet.get_Range(cell, Type.Missing);
                range.Formula = "Погрешность";

                cell = columns[3] + (1).ToString();
                range = sheet.get_Range(cell, Type.Missing);
                range.Formula = "Сила при идеализации";
                for (int i = 0; i < mu.Length; i++)
                {
                    cell = columns[0] + (i+2).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = force[i].ToString().Replace(',', '.');

                    cell = columns[1] + (i+2).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = mu[i].ToString().Replace(',', '.');

                    cell = columns[2] + (i+2).ToString();
                    range = sheet.get_Range(cell, Type.Missing);
                    range.Formula = error[i].ToString().Replace(',', '.');


                }
                range = sheet.get_Range("B2:C11", Type.Missing);
                /*range.Activate();
                range.Select();
                Chart chart = (Chart)app.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                chart.Activate();
                chart.Select(Type.Missing);*/

                ChartObjects chartObjects = (ChartObjects)sheet.ChartObjects(Type.Missing);
                ChartObject chartObject = chartObjects.Add(40, 10, 400, 400);
                Chart chart = chartObject.Chart;
                chart.SetSourceData(range, Type.Missing);
                chart.ChartType = XlChartType.xlXYScatterLinesNoMarkers;
                chart.HasTitle = true;
                chart.ChartTitle.Text = "Погрешность идеализации";

                /*
                app.ActiveChart.ChartType = XlChartType.xlXYScatterSmoothNoMarkers;
                app.ActiveChart.HasTitle = true;
                app.ActiveChart.ChartTitle.Text = "Погрешность идеализации";
                ((Axis)app.ActiveChart.Axes(XlAxisType.xlCategory,XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Axis)app.ActiveChart.Axes(XlAxisType.xlCategory,XlAxisGroup.xlPrimary)).AxisTitle.Text = "Магнитная проницаемость";
                ((Axis)app.ActiveChart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).MinimumScale = 2 * mu[0] - mu[1];
                //((Axis)app.ActiveChart.Axes(XlAxisType.xlSeriesAxis,XlAxisGroup.xlPrimary)).HasTitle = false;
                ((Axis)app.ActiveChart.Axes(XlAxisType.xlValue,XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Axis)app.ActiveChart.Axes(XlAxisType.xlValue,XlAxisGroup.xlPrimary)).AxisTitle.Text = "Погрешность";
                //app.ActiveChart.HasLegend = false;
                app.ActiveChart.Location(XlChartLocation.xlLocationAsObject, "Лист2");*/

                ((Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).AxisTitle.Text = "Магнитная проницаемость";
                ((Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).AxisTitle.Font.Size = 14;
                ((Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).AxisTitle.Font.Bold = false;
                ((Axis)chart.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary)).MinimumScale =  mu[0];

                //((Axis)app.ActiveChart.Axes(XlAxisType.xlSeriesAxis,XlAxisGroup.xlPrimary)).HasTitle = false;

                ((Axis)chart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Axis)chart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary)).AxisTitle.Text = "Погрешность";
                ((Axis)chart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary)).AxisTitle.Font.Size = 14;
                ((Axis)chart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary)).AxisTitle.Font.Bold = false;
                chart.HasLegend = false;
            }

            //Закрыть книгу
            if (book != null)
            {
                book.Close(true, fileName, Type.Missing);
            }

            //Закрыть приложение
            if (app != null)
                app.Quit();
        }
    }
}
