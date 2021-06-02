using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Drawing.Drawing2D;

namespace Calculating_Magnetic_Field
{
    public partial class GraphicsBildingForm : Form
    {
        List<double> points1D;
        List<double> function;
        List<List<double>> functions = new List<List<double>>();
        GraphPane pane;
        // double[] fInf;
        GraphicTypes graphicType;
        PhysicalField physicalField;
        string name_in_legend;
        Color color;

        List<double> femm_elcut_function;

        List<List<double>> femm_elcut_functions_list = new List<List<double>>();
        int current_femm_elcut_function = 0;

        List<LineItem> curves = new List<LineItem>();
        LineItem myCurve0;
        LineItem myCurve1;
        LineItem myCurve2;

        List<PointPairList> pointPairs = new List<PointPairList>();
        PointPairList f0_list;
        PointPairList f1_list;
        PointPairList f2_list;

        public GraphicsBildingForm(List<double> points, List<double> function, GraphicTypes graphicType, PhysicalField physicalField)
        {
            InitializeComponent();
            this.points1D = points;
            this.function = function;
            this.graphicType = graphicType;
            this.physicalField = physicalField;        
            Init(graphicType, physicalField);
        }
        public GraphicsBildingForm(GraphicTypes graphicType, PhysicalField physicalField)
        {
            InitializeComponent();
            this.graphicType = graphicType;
            this.physicalField = physicalField;
            Init(graphicType, physicalField);
        }

        private void Init(GraphicTypes graphicType, PhysicalField physicalField)
        {
            pane = ZDc.GraphPane;
            switch (graphicType)
            {
                case GraphicTypes.Potencial:
                    {
                        switch (physicalField)
                        {
                            case PhysicalField.Current:
                                {
                                    pane.Title.Text = "График скалярного потенциала";
                                    pane.YAxis.Title.Text = "f, V";
                                    break;
                                }
                            case PhysicalField.Electric:
                                {
                                    pane.Title.Text = "График скалярного потенциала";
                                    pane.YAxis.Title.Text = "f, V";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.Title.Text = "График потенциала";
                                    pane.YAxis.Title.Text = "A, Wb/m";
                                    break;
                                }
                        }
                        break;
                    }
                case GraphicTypes.InductionModul:
                    {
                        pane.Title.Text = "График модуля индукции";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "|D|, C/m2";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "|B|, Т";
                                    break;
                                }
                        }
                        break;
                    }
                case GraphicTypes.Induction_Normal_component:
                    {
                        pane.Title.Text = "График нормальной составляющей индукции";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "D.n, C/m2";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "B.n, Т";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Induction_Tangencial_component:
                    {
                        pane.Title.Text = "График тангенциальной составляющей индукции";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "D.t, C/m2";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "B.t, Т";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Induction_X_component:
                    {
                        pane.Title.Text = "График X составляющей индукции";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "D.x, C/m2";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "B.x, Т";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Induction_Y_component:
                    {
                        pane.Title.Text = "График Y составляющей индукции";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "D.y, C/m2";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "B.y, Т";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Intensity_Normal_component:
                    {
                        pane.Title.Text = "График нормальной составляющей напряжённости";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "E.n, V/m";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "H.n, A/m";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Intensity_Tangencial_component:
                    {
                        pane.Title.Text = "График тангенциальной составляющей напряжённости";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "E.t, V/m";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "H.t, A/m";
                                    break;
                                }

                        }
                        break;
                    }
                case GraphicTypes.Intensity_X_component:
                    {
                        pane.Title.Text = "График X составляющей напряжённости";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "E.x, V/m";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "H.x, A/m";
                                    break;
                                }
                            case PhysicalField.Current:
                                {
                                    pane.YAxis.Title.Text = "E.x, V/m";
                                    break;
                                }
                        }
                        break;
                    }
                case GraphicTypes.Intensity_Y_component:
                    {
                        pane.Title.Text = "График Y составляющей напряжённости";
                        switch (physicalField)
                        {
                            case PhysicalField.Electric:
                                {
                                    pane.YAxis.Title.Text = "E.y, V/m";
                                    break;
                                }
                            case PhysicalField.Magnetic:
                                {
                                    pane.YAxis.Title.Text = "H.y, A/m";
                                    break;
                                }
                            case PhysicalField.Current:
                                {
                                    pane.YAxis.Title.Text = "E.x, V/m";
                                    break;
                                }
                        }
                        break;
                    }
                case GraphicTypes.CurrentModul:
                    {
                        pane.Title.Text = "График модуля тока";
                        pane.YAxis.Title.Text = "|i|, A/m^2";
                        break;
                    }
                case GraphicTypes.Current_X_component:
                    {
                        pane.Title.Text = "График X составляющей тока";
                        pane.YAxis.Title.Text = "i.x, A/m^2";
                        break;
                    }
                case GraphicTypes.Current_Y_component:
                    {
                        pane.Title.Text = "График Y составляющей тока";
                        pane.YAxis.Title.Text = "i.y, A/m^2";
                        break;
                    }
            }


            #region Настройки полотна 
            pane = ZDc.GraphPane;
            pane.Title.IsVisible = true;
            pane.XAxis.Title.Text = "Координата"; //Подписи к осям XY

            // !!!
            // Включаем отображение сетки напротив крупных рисок по оси X
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ... 
            //pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            //pane.XAxis.MajorGrid.DashOff = 10;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 10;
            pane.CurveList.Clear();
            #endregion

            #region Настройки первого графика МГЭ
            f0_list = new PointPairList();
            double xmin;
            int n = points1D.Count;
            double xmax = 2 * points1D[n - 1] - points1D[n - 2];
            for (int i = 0; i < n; i++)
            {
                f0_list.Add(points1D[i], function[i]);
            }


            string comment = SetComment("МГЭ");
            myCurve0 = pane.AddCurve(comment, f0_list, Color.Gray, SymbolType.None);
            myCurve0.Line.Width = 2.0f; // толщина линии курвы
            myCurve0.Line.IsSmooth = false; //Сглаживание курвы.
            myCurve0.Line.IsAntiAlias = true;
            myCurve0.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve0.Label.IsVisible = true;
            #endregion

            #region Настройка осей
            double max = function.Max() + 0.1 * Math.Abs(function.Max());
            double min = function.Min() - 0.1 * Math.Abs(function.Min());
            xmin = points1D[0] - 0.1 * Math.Abs(points1D[0]);
            xmax = points1D.Last() + 0.1 * Math.Abs(points1D.Last());

            pane.XAxis.Scale.Min = xmin;
            pane.XAxis.Scale.Max = xmax;
            pane.YAxis.Scale.Min = min;
            pane.YAxis.Scale.Max = max;
            ZDc.AxisChange();
            #endregion

            ZDc.Refresh();
        }

        private void BuilderPotencialGraphic_Load(object sender, EventArgs e)
        {
            ZDc.Refresh();
        }


        private void ButRedraw_Click(object sender, EventArgs e)
        {
            ZDc.Refresh();
        }


        private void добавитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string comment = SetComment("Femm");
            List<double> points = new List<double>();
            int n;
            f1_list = new PointPairList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            StreamReader reader;
            string path;
            string str;
            string[] str2;
            string[] str3;
            femm_elcut_functions_list.Add(new List<double>());

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                using (reader = new StreamReader(path))
                {
                    /*reader.ReadLine();
                    reader.ReadLine();*/
                    while (!reader.EndOfStream)
                    {
                        str = reader.ReadLine();
                        str2 = str.Split('\t');
                        str3 = str2[0].Split('e');
                        points.Add(double.Parse(str3[0].Replace('.', ',')) * Math.Pow(10, Int32.Parse(str3[1])) / 1000);
                        str3 = str2[2].Split('e');
                        femm_elcut_functions_list[current_femm_elcut_function].Add(double.Parse(str3[0].Replace('.', ',')) * Math.Pow(10, Int32.Parse(str3[1])));
                    }

                }
                n = points.Count;
                for (int i = 0; i < n; i++)
                {
                    f1_list.Add(points[i], femm_elcut_functions_list[current_femm_elcut_function][i]);
                }

                myCurve1 = pane.AddCurve(comment, f1_list, Color.Black, SymbolType.None);
                myCurve1.Line.Width = 3.0f; // толщина линии курвы
                myCurve1.Line.IsSmooth = false; //Сглаживание курвы.
                myCurve1.Line.IsAntiAlias = true;
                myCurve1.Line.Style = DashStyle.Solid; // Тип линии курвы
                myCurve1.Label.IsVisible = true;
                ZDc.Refresh();
            }
            current_femm_elcut_function++;
        }

        private void сохранитьРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZDc.SaveAsBitmap();
        }


        private void добавитьГрафикИзElcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string comment = SetComment("Elcut");
            List<double> points = new List<double>();
            int n;
            f2_list = new PointPairList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            StreamReader reader;
            string path;
            string str;
            string[] str2;
            string[] str3;
            femm_elcut_functions_list.Add(new List<double>());

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                using (reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        str = reader.ReadLine();
                        str2 = str.Split('\t');
                        //str3 = str2[1].Split('e');
                        points.Add(double.Parse(str2[1].Replace('.', ',')));
                        femm_elcut_functions_list.Last().Add(double.Parse(str2[2].Replace('.', ',')));
                    }

                }
                n = points.Count;
                double p = points[0];
                for (int i = 0; i < n; i++)
                {
                    points[i] -= p;
                    points[i] /= 1000;
                }

                for (int i = 0; i < n; i++)
                {
                    f2_list.Add(points[i], femm_elcut_functions_list.Last()[i]);
                }

                myCurve2 = pane.AddCurve(comment, f2_list, Color.Black, SymbolType.None);
                myCurve2.Line.Width = 3.0f; // толщина линии курвы
                myCurve2.Line.IsSmooth = false; //Сглаживание курвы.
                myCurve2.Line.IsAntiAlias = true;
                myCurve2.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot; // Тип линии курвы
                myCurve2.Label.IsVisible = true;
                ZDc.Refresh();
            }
            current_femm_elcut_function++;
        }

        private void AddGraphic_Click(object sender, EventArgs e)
        {
            string comment = SetComment("МГЭ доп");
            List<double> points = new List<double>();
            int n;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            StreamReader reader;
            string path;
            string str;
            string[] str2;
            functions.Add(new List<double>());

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                using (reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        str = reader.ReadLine();
                        str2 = str.Split('\t');
                        points.Add(double.Parse(str2[0]));
                        functions.Last().Add(double.Parse(str2[1]));
                    }

                }
                n = points.Count;
                double p = points[0];
                pointPairs.Add(new PointPairList());
                for (int i = 0; i < n; i++)
                {
                    pointPairs.Last().Add(points[i], functions.Last()[i]);
                }

                DashStyle dashStyle;
                SymbolType symbolType;
                Color color = Color.Black;
                if (pane.CurveList.Count < 5)
                {
                    symbolType = SymbolType.None;
                    color = Color.Black;
                }
                else if (pane.CurveList.Count < 10)
                {
                    symbolType = SymbolType.None;
                    color = Color.DarkGray;
                }
                else
                {
                    symbolType = (SymbolType)((pane.CurveList.Count + 1) % 11);
                    color = Color.Black;
                }
                dashStyle = (DashStyle)(pane.CurveList.Count % 5);
                curves.Add(pane.AddCurve(comment, pointPairs.Last(), color, symbolType));
                curves.Last().Line.Width = 2f; // толщина линии курвы
                curves.Last().Line.IsSmooth = false; //Сглаживание курвы.
                curves.Last().Line.IsAntiAlias = true;
                curves.Last().Line.Style = dashStyle; // Тип линии курвы
                curves.Last().Label.IsVisible = true;
                curves.Last().Symbol.Size = 5f;


                //var graphics = ZDc.CreateGraphics();
                //var curve = pane.CurveList.Last();
                //var line = curve as LineItem;

                //string coords;
                //curve.GetCoords(pane, line.Points.Count / 3, out coords);
                //string[] coords_arr = coords.Split(',');
                //float[] arr = new float[4];
                //for (int i = 0; i < 4; i++)
                //{
                //    arr[i] = Single.Parse(coords_arr[i]);
                //}
                //PointF point1 = new PointF(arr[0], arr[1]);
                //float width = arr[2] - arr[0];
                //float height = arr[3] - arr[1];
                //RectangleF rectangleF = new RectangleF(point1, new SizeF(width, height));
                //pane.CurveList.Last().DrawLegendKey(graphics, pane, rectangleF, pane.CalcScaleFactor());
                ZDc.Refresh();
            }
        }

        private static string SetComment(string default_comment)
        {
            Forms.AddCommentToGraphic addCommentToGraphic = new Forms.AddCommentToGraphic();
            if (addCommentToGraphic.ShowDialog() == DialogResult.OK)
            {
                return addCommentToGraphic.Comment;

            }

            return default_comment;
        }

        private void DeleteGraphic_Click(object sender, EventArgs e)
        {
            int count = pane.CurveList.Count;
            List<string> labels = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                labels.Add(pane.CurveList[i].Label.Text);
            }
            string label;
            Forms.Graphics.SelectGraphic chooseGraphic = new Forms.Graphics.SelectGraphic(labels);
            switch (chooseGraphic.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        label = chooseGraphic.Label;
                        int index = pane.CurveList.FindIndex(c => c.Label.Text == label);
                        pane.CurveList.RemoveAt(index);

                        ZDc.Refresh();
                        break;
                    }
                case DialogResult.Cancel:
                    {
                        return;
                    }
            }
        }

    }
}


