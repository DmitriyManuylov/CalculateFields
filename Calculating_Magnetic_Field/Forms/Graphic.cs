using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Calculating_Magnetic_Field
{
    public partial class FormGraphic : Form
    {
        double[] points1D;
        List<double[]> function;
        GraphPane pane;
        double[] fInf;
        public FormGraphic(double[] mu, List<double[]> error)
        {
            InitializeComponent();
            this.points1D = mu;
            this.function = error;
        }

        public FormGraphic(double[] mu, List<double[]> force, double[] fInf)
        {
            InitializeComponent();
            this.points1D = mu;
            this.function = force;
            this.fInf = fInf;
        }

        private void Graphic_Load(object sender, EventArgs e)
        {
            DrawGraphic();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DrawGraphic();
        }

        /* private void DrawGraphic()
         {
             GraphPane pane = ZDc.GraphPane;
             pane.Title.Text = "Погрешность идеализации"; //Название графика
             pane.XAxis.Title.Text = "Относительная магнитная проницаемость"; //Подписи к осям XY
             pane.YAxis.Title.Text = "Погрешность";

             PointPairList f0_list = new PointPairList();
             PointPairList f1_list = new PointPairList(); //Это объект, в котором сохраняются координаты точек.
             PointPairList f2_list = new PointPairList();
             PointPairList f3_list = new PointPairList();
             PointPairList f4_list = new PointPairList();
             PointPairList f5_list = new PointPairList();


             f0_list.Add(0, 0); //Задача одной точки
                                //f1_list.Add(0, 0);
             f1_list.Add(0, 0);
             f2_list.Add(0, 0);
             f3_list.Add(0, 0);
             f4_list.Add(0, 0);


             double xmin;
             int n = mu.Length;
             double xmax = 2 * mu[n - 1] - mu[n - 2];
             for (int i = 0; i < n; i++)
             {
                 f0_list.Add(mu[i], error[0][i]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
                 f1_list.Add(mu[i], error[1][i]);
                 f2_list.Add(mu[i], error[2][i]);
                 f3_list.Add(mu[i], error[3][i]);
                 f4_list.Add(mu[i], error[4][i]);
                 f5_list.Add(mu[i], 0.01);
             }
             for (int i = 0; i < n; i++)
             {
                 f1_list.Add(mu, error[1]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
             }
             for (int i = 0; i < n; i++)
             {
                 f2_list.Add(mu, error[2]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
             }
             for (int i = 0; i < n; i++)
             {
                 f3_list.Add(mu, error[3]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
             }
             for (int i = 0; i < n; i++)
             {
                 f4_list.Add(mu, error[4]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
             }


             // !!!
             // Включаем отображение сетки напротив крупных рисок по оси X
             pane.XAxis.MajorGrid.IsVisible = true;

             // Задаем вид пунктирной линии для крупных рисок по оси X:
             // Длина штрихов равна 10 пикселям, ... 
             pane.XAxis.MajorGrid.DashOn = 10;

             // затем 5 пикселей - пропуск
             pane.XAxis.MajorGrid.DashOff = 10;


             // Включаем отображение сетки напротив крупных рисок по оси Y
             pane.YAxis.MajorGrid.IsVisible = true;

             // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
             pane.YAxis.MajorGrid.DashOn = 10;
             pane.YAxis.MajorGrid.DashOff = 10;


             // Включаем отображение сетки напротив мелких рисок по оси X
             //pane.YAxis.MinorGrid.IsVisible = true;

             // Задаем вид пунктирной линии для крупных рисок по оси Y: 
             // Длина штрихов равна одному пикселю, ... 
             //pane.YAxis.MinorGrid.DashOn = 2;

             // затем 2 пикселя - пропуск
            // pane.YAxis.MinorGrid.DashOff = 6;

             // Включаем отображение сетки напротив мелких рисок по оси Y
             //pane.XAxis.MinorGrid.IsVisible = true;

             // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            // pane.XAxis.MinorGrid.DashOn = 2;
             //pane.XAxis.MinorGrid.DashOff = 6;

             // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
             pane.CurveList.Clear();

             //Создание курвы, то бишь линии. (подпись, объект с точками, Цвет, не помню, что это, но и НЕВАЖНО)
             LineItem myCurve0 = pane.AddCurve("r = 0,2 см", f0_list, Color.Blue, SymbolType.None);
             myCurve0.Line.Width = 2.0f; // толщина линии курвы
             myCurve0.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve0.Line.IsAntiAlias = true;
             myCurve0.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve0.Label.IsVisible = true;
             //myCurve0.Label.FontSpec.Size = FontSpec.schema;

             LineItem myCurve1 = pane.AddCurve("r = 0,4 см", f1_list, Color.Green, SymbolType.None);
             myCurve1.Line.Width = 2.0f; // толщина линии курвы
             myCurve1.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve1.Line.IsAntiAlias = true;
             myCurve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve1.Label.IsVisible = true;
             //myCurve1.Label.FontSpec.Size = FontSpec.schema;

             LineItem myCurve2 = pane.AddCurve("r = 0,6 см", f2_list, Color.Yellow, SymbolType.None);
             myCurve2.Line.Width = 2.0f; // толщина линии курвы
             myCurve2.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve2.Line.IsAntiAlias = true;
             myCurve2.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve2.Label.IsVisible = true;
             //myCurve2.Label.FontSpec.Size = FontSpec.schema;

             LineItem myCurve3 = pane.AddCurve("r = 0,8 см", f3_list, Color.Red, SymbolType.None);
             myCurve3.Line.Width = 2.0f; // толщина линии курвы
             myCurve3.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve3.Line.IsAntiAlias = true;
             myCurve3.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve3.Label.IsVisible = true;
             //myCurve3.Label.FontSpec.Size = FontSpec.schema;

             //Создание курвы, то бишь линии. (подпись, объект с точками, Цвет, не помню, что это, но и НЕВАЖНО)
             LineItem myCurve4 = pane.AddCurve("r = 1 см", f4_list, Color.Purple, SymbolType.None);
             myCurve4.Line.Width = 2.0f; // толщина линии курвы
             myCurve4.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve4.Line.IsAntiAlias = true;
             myCurve4.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve4.Label.IsVisible = true;
             //myCurve4.Label.FontSpec.Size = FontSpec.schema;

             LineItem myCurve5 = pane.AddCurve("", f5_list, Color.Black, SymbolType.None);
             myCurve5.Line.Width = 3.0f; // толщина линии курвы
             myCurve5.Line.IsSmooth = true; //Сглаживание курвы.
             myCurve5.Line.IsAntiAlias = true;
             myCurve5.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
             myCurve5.Label.IsVisible = false;
             //myCurve5.Label.FontSpec.Size = FontSpec.schema;



             double[] maxArr = new double[5];
             maxArr[0] = error[0].Max();
             maxArr[1] = error[1].Max();
             maxArr[2] = error[2].Max();
             maxArr[3] = error[3].Max();
             maxArr[4] = error[4].Max();
             double max = maxArr.Max();
             double[] minArr = new double[5];
             maxArr[0] = error[0].Min();
             maxArr[1] = error[1].Min();
             maxArr[2] = error[2].Min();
             maxArr[3] = error[3].Min();
             maxArr[4] = error[4].Min();
             double min = maxArr.Min();
             double xMax = 0;
             double yMax = 0;
             double xMin = 0;
             // double yMin = 0;
             double yMin0 = 0;
             myCurve0.GetRange(out xMin, out xMax, out yMin0, out yMax, false, true, pane); // Подстройка масштаба чертежа под размеры графика
             myCurve1.GetRange(out xMin, out xMax, out yMin0, out yMax, false, true, pane);
             myCurve2.GetRange(out xMin, out xMax, out yMin0, out yMax, false, true, pane);
             myCurve3.GetRange(out xMin, out xMax, out yMin0, out yMax, false, true, pane);
             myCurve4.GetRange(out xMin, out xMax, out yMin0, out yMax, false, true, pane);
             xmin = mu[0];
             xmax = mu.Last();
             pane.XAxis.Scale.Min = xmin;//Применение этих чудесных ограничений
             pane.XAxis.Scale.Max = xmax;
             pane.YAxis.Scale.Min = 0;
             pane.YAxis.Scale.Max = max;
             ZDc.AxisChange();
             ZDc.Refresh();
         }*/



        private void DrawGraphic()
        {
            if (fInf != null)
            {
                pane = ZDc.GraphPane;
                pane.Title.Text = "Зависимость силы от проницаемости"; //Название графика
                pane.Title.IsVisible = true;
                pane.XAxis.Title.Text = "Относительная магнитная проницаемость"; //Подписи к осям XY
                pane.YAxis.Title.Text = "Сила F, H";
            }
            else
            {
                pane = ZDc.GraphPane;
                pane.Title.Text = "Погрешность идеализации"; //Название графика
                pane.Title.IsVisible = true;
                pane.XAxis.Title.Text = "Относительная магнитная проницаемость"; //Подписи к осям XY
                pane.YAxis.Title.Text = "Погрешность";
            }

            PointPairList f0_list = new PointPairList();
            PointPairList f1_list = new PointPairList(); //Это объект, в котором сохраняются координаты точек.
            PointPairList f2_list = new PointPairList();
            PointPairList f3_list = new PointPairList();
            PointPairList f4_list = new PointPairList();
            PointPairList f5_list = new PointPairList();

            PointPairList[] Levels_fInf;
            LineItem[] Levels_fInf_Lines;


            double xmin;
            int n = points1D.Length;
            double xmax = 2 * points1D[n - 1] - points1D[n - 2];
            for (int i = 0; i < n; i++)
            {
                f0_list.Add(points1D[i], function[0][i]); //Тут задаются остальные точки через функцию. Типа задаем изменение одной из переменных и считаем при каждом ее значении функцию
                f1_list.Add(points1D[i], function[1][i]);
                f2_list.Add(points1D[i], function[2][i]);
                f3_list.Add(points1D[i], function[3][i]);
                f4_list.Add(points1D[i], function[4][i]);
                f5_list.Add(points1D[i], 0.01);
            }

            #region
            // !!!
            // Включаем отображение сетки напротив крупных рисок по оси X
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ... 
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 10;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 10;


            // Включаем отображение сетки напротив мелких рисок по оси X
            //pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y: 
            // Длина штрихов равна одному пикселю, ... 
            //pane.YAxis.MinorGrid.DashOn = 2;

            // затем 2 пикселя - пропуск
            // pane.YAxis.MinorGrid.DashOff = 6;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            //pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            // pane.XAxis.MinorGrid.DashOn = 2;
            //pane.XAxis.MinorGrid.DashOff = 6;
            #endregion

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            /*int n_fLevels;
            if (fInf != null)
            {
                n_fLevels = fInf.Length;
                Levels_fInf = new PointPairList[n_fLevels];
                Levels_fInf_Lines = new LineItem[n_fLevels];
                for (int i = 0; i < n_fLevels; i++)
                    Levels_fInf[i] = new PointPairList();

                for (int i = 0; i < n_fLevels; i++)
                    for (int j = 0; j < mu.Length; j++)
                        Levels_fInf[i].Add(mu[j], fInf[i]);
                /*Levels_fInf_Lines[0] = pane.AddCurve("", Levels_fInf[0], Color.Blue, SymbolType.None);
                Levels_fInf_Lines[1] = pane.AddCurve("", Levels_fInf[1], Color.Green, SymbolType.None);
                Levels_fInf_Lines[2] = pane.AddCurve("", Levels_fInf[2], Color.Yellow, SymbolType.None);
                Levels_fInf_Lines[3] = pane.AddCurve("", Levels_fInf[3], Color.Red, SymbolType.None);
                Levels_fInf_Lines[4] = pane.AddCurve("", Levels_fInf[4], Color.Purple, SymbolType.None);
                Levels_fInf_Lines[0] = pane.AddCurve("", Levels_fInf[0], Color.Black, SymbolType.None);
                Levels_fInf_Lines[1] = pane.AddCurve("", Levels_fInf[1], Color.Black, SymbolType.None);
                Levels_fInf_Lines[2] = pane.AddCurve("", Levels_fInf[2], Color.Black, SymbolType.None);
                Levels_fInf_Lines[3] = pane.AddCurve("", Levels_fInf[3], Color.Black, SymbolType.None);
                Levels_fInf_Lines[4] = pane.AddCurve("", Levels_fInf[4], Color.Black, SymbolType.None);
                for (int i = 0; i < n_fLevels; i++)
                    for (int j = 0; j < mu.Length; j++)
                        Levels_fInf[i].Add(mu[j], fInf[i]);
                for (int i = 0; i < n_fLevels; i++)
                {
                    Levels_fInf_Lines[i].Line.Width = 1.5f;
                    Levels_fInf_Lines[i].Line.IsSmooth = true;
                    Levels_fInf_Lines[i].Line.IsAntiAlias = true;
                    Levels_fInf_Lines[i].Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                    Levels_fInf_Lines[i].Label.IsVisible = false;
                }

            }*/

            #region
            LineItem myCurve0 = pane.AddCurve("r = 0,2 см", f0_list, Color.Blue, SymbolType.None);
            myCurve0.Line.Width = 2.0f; // толщина линии курвы
            myCurve0.Line.IsSmooth = true; //Сглаживание курвы.
            myCurve0.Line.IsAntiAlias = true;
            myCurve0.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve0.Label.IsVisible = true;

            LineItem myCurve1 = pane.AddCurve("r = 0,4 см", f1_list, Color.Green, SymbolType.None);
            myCurve1.Line.Width = 2.0f; // толщина линии курвы
            myCurve1.Line.IsSmooth = true; //Сглаживание курвы.
            myCurve1.Line.IsAntiAlias = true;
            myCurve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve1.Label.IsVisible = true;

            LineItem myCurve2 = pane.AddCurve("r = 0,6 см", f2_list, Color.Yellow, SymbolType.None);
            myCurve2.Line.Width = 2.0f; // толщина линии курвы
            myCurve2.Line.IsSmooth = true; //Сглаживание курвы.
            myCurve2.Line.IsAntiAlias = true;
            myCurve2.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve2.Label.IsVisible = true;
            //myCurve2.Label.FontSpec.Size = FontSpec.schema;

            LineItem myCurve3 = pane.AddCurve("r = 0,8 см", f3_list, Color.Red, SymbolType.None);
            myCurve3.Line.Width = 2.0f; // толщина линии курвы
            myCurve3.Line.IsSmooth = true; //Сглаживание курвы.
            myCurve3.Line.IsAntiAlias = true;
            myCurve3.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve3.Label.IsVisible = true;

            //Создание курвы, то бишь линии. (подпись, объект с точками, Цвет, не помню, что это, но и НЕВАЖНО)
            LineItem myCurve4 = pane.AddCurve("r = 1 см", f4_list, Color.Purple, SymbolType.None);
            myCurve4.Line.Width = 2.0f; // толщина линии курвы
            myCurve4.Line.IsSmooth = true; //Сглаживание курвы.
            myCurve4.Line.IsAntiAlias = true;
            myCurve4.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve4.Label.IsVisible = true;

            if (fInf == null)
            {
                LineItem myCurve5 = pane.AddCurve("", f5_list, Color.Black, SymbolType.None);
                myCurve5.Line.Width = 3.0f; // толщина линии курвы
                myCurve5.Line.IsSmooth = true; //Сглаживание курвы.
                myCurve5.Line.IsAntiAlias = true;
                myCurve5.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
                myCurve5.Label.IsVisible = false;
            }



            double[] maxArr = new double[5];
            maxArr[0] = function[0].Max();
            maxArr[1] = function[1].Max();
            maxArr[2] = function[2].Max();
            maxArr[3] = function[3].Max();
            maxArr[4] = function[4].Max();
            double max = maxArr.Max();
            double[] minArr = new double[5];
            maxArr[0] = function[0].Min();
            maxArr[1] = function[1].Min();
            maxArr[2] = function[2].Min();
            maxArr[3] = function[3].Min();
            maxArr[4] = function[4].Min();
            double min = maxArr.Min();
            #endregion
            xmin = points1D[0];
            xmax = points1D.Last();
            pane.XAxis.Scale.Min = xmin;//Применение этих чудесных ограничений
            pane.XAxis.Scale.Max = xmax;
            pane.YAxis.Scale.Min = 0;
            pane.YAxis.Scale.Max = max;
            ZDc.AxisChange();
            ZDc.Refresh();
        }
    }

}
