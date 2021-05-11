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

namespace Calculating_Magnetic_Field
{
    public partial class BuilderPotencialGraphic : Form
    {
        double[] points1D;
        List<double> function;
        GraphPane pane;
       // double[] fInf;
        GraphicTypes graphicType;
        PhysicalField physicalField;

        List<double> femm_function;

        LineItem myCurve0;
        LineItem myCurve1;

        PointPairList f0_list;
        PointPairList f1_list;

        public BuilderPotencialGraphic(double[] points, List<double> function, GraphicTypes graphicType, PhysicalField physicalField)
        {
            InitializeComponent();
            this.points1D = points;
            this.function = function;
            this.graphicType = graphicType;
            this.physicalField = physicalField;
        }


        private void DrawGraphic()
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
            
            
            pane = ZDc.GraphPane;
            pane.Title.IsVisible = true;
            pane.XAxis.Title.Text = "Координата"; //Подписи к осям XY


            f0_list = new PointPairList();
            f1_list = new PointPairList();


            double xmin;
            int n = points1D.Length;
            double xmax = 2 * points1D[n - 1] - points1D[n - 2];
            for (int i = 0; i < n; i++)
            {
                f0_list.Add(points1D[i], function[i]);
            }


            

            

            #region
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



            myCurve0 = pane.AddCurve("МГЭ", f0_list, Color.Gray, SymbolType.None);
            myCurve0.Line.Width = 2.0f; // толщина линии курвы
            myCurve0.Line.IsSmooth = false; //Сглаживание курвы.
            myCurve0.Line.IsAntiAlias = true;
            myCurve0.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid; // Тип линии курвы
            myCurve0.Label.IsVisible = true;

            if (femm_function != null)
            {
                for (int i = 0; i < n; i++)
                {
                    f1_list.Add(points1D[i], femm_function[i]);
                }
                myCurve1 = pane.AddCurve("Femm", f1_list, Color.Green, SymbolType.None);
                myCurve1.Line.Width = 4.0f; // толщина линии курвы
                myCurve1.Line.IsSmooth = false; //Сглаживание курвы.
                myCurve1.Line.IsAntiAlias = true;
                myCurve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash; // Тип линии курвы
                myCurve1.Label.IsVisible = true;
            }


            double max = function.Max() + 0.1 * Math.Abs(function.Max());
            double min = function.Min() - 0.1 * Math.Abs(function.Min());
            #endregion
            xmin = points1D[0] - 0.1 * Math.Abs(points1D[0]);
            xmax = points1D.Last() + 0.1 * Math.Abs(points1D.Last());

            pane.XAxis.Scale.Min = xmin;
            pane.XAxis.Scale.Max = xmax;
            pane.YAxis.Scale.Min = min;
            pane.YAxis.Scale.Max = max;
            ZDc.AxisChange();
            ZDc.Refresh();
        }

        private void BuilderPotencialGraphic_Load(object sender, EventArgs e)
        {
            DrawGraphic();
        }


        private void ButRedraw_Click(object sender, EventArgs e)
        {
            DrawGraphic();
        }

        private void BuilderPotencialGraphic_Resize(object sender, EventArgs e)
        {

        }

        private void добавитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int n = points1D.Length;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            StreamReader reader;
            string path;
            string str;
            string[] str2;
            string[] str3;
            femm_function = new List<double>(n);
            
            if(openFileDialog.ShowDialog() == DialogResult.OK)
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
                        str3 = str2[2].Split('e');
                        femm_function.Add(double.Parse(str3[0].Replace('.',',')) * Math.Pow(10, Int32.Parse(str3[1])));
                    }

                }

                for (int i = 0; i < n; i++)
                {
                    f1_list.Add(points1D[i], femm_function[i]);
                }

                myCurve1 = pane.AddCurve("Femm", f1_list, Color.Green, SymbolType.None);
                myCurve1.Line.Width = 4.0f; // толщина линии курвы
                myCurve1.Line.IsSmooth = false; //Сглаживание курвы.
                myCurve1.Line.IsAntiAlias = true;
                myCurve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash; // Тип линии курвы
                myCurve1.Label.IsVisible = true;
                ZDc.Refresh();
            }
        }

        private void сохранитьРисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZDc.SaveAsBitmap();
        }


        private void добавитьГрафикИзElcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = points1D.Length;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            StreamReader reader;
            string path;
            string str;
            string[] str2;
            string[] str3;
            femm_function = new List<double>(n);

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
                        femm_function.Add(double.Parse(str2[2].Replace('.', ',')));
                    }

                }

                for (int i = 0; i < n; i++)
                {
                    f1_list.Add(points1D[i], femm_function[i]);
                }

                myCurve1 = pane.AddCurve("Elcut", f1_list, Color.Black, SymbolType.None);
                myCurve1.Line.Width = 3.0f; // толщина линии курвы
                myCurve1.Line.IsSmooth = false; //Сглаживание курвы.
                myCurve1.Line.IsAntiAlias = true;
                myCurve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot; // Тип линии курвы
                myCurve1.Label.IsVisible = true;
                ZDc.Refresh();
            }
        }
    }
}

