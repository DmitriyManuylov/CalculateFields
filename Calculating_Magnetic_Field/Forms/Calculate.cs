using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Calculating_Magnetic_Field.Figures;
using Calculating_Magnetic_Field.Figure_Drawers;
using System.Diagnostics;
using Calculating_Magnetic_Field.Forms;
using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.Sources;
using Calculating_Magnetic_Field.ModelFactories;

namespace Calculating_Magnetic_Field
{
    public partial class CalculateForm : Form
    {
        #region Переменные
        string path_file = "\\path file.txt";

        string path_to_directory_of_power_lines_data;//директория с файлами для построения изолиний
        string path_to_data_files; // файл задачи ?

        bool isDensCalc = false, isNodesGot = false;

        //коэффициент перевода геометрических размеров в метры
        float SizeScale;

        float DrawingScale;

        //Глубина модели(размер вдоль оси Oz) 
        double Depth;

        GraphicTypes choosenGraphic;


        public PointD[] PointsForCulc;
        public double[] ReactionPotencial;
        public double[] SourcesPotencial;
        public double[] ResultPotencial;

        public ModelFactory modelFactory;
        public Models.IModel model;
        public GraphicsCalculation graphicsCalculating;

        public List<PointsPair> pointsPairs = new List<PointsPair>();
        PointsPair? selectedPair = null;


        PointD[] points;
        List<IDrawable> figuresForDrawing;
        List<Bound_Rectangle> bound_Rectangles;
        Pen pen;
        SolidBrush brush;
        double mu_left;
        #endregion
        public CalculateForm()
        {
            InitializeComponent();
            changeDirToolStripMenuItem.Enabled = false;
            //butBuildPowerLines.Enabled = false;
            groupBoxGraphicsCalc.Enabled = false;
            groupBoxPowerLines.Enabled = false;
            ProblemDataToolStripMenuItem.Enabled = false;
            
            //points = new List<PointD>();
            bound_Rectangles = new List<Bound_Rectangle>();

            figuresForDrawing = new List<IDrawable>();


            Depth = 1000;
            SizeScale = 0.001f;
            DrawingScale = 10f / SizeScale;
            Work_With_Files.SizeScale = this.SizeScale;
            cbChooseGraphicType.DataSource = Enum.GetValues(typeof(GraphicTypes));
            cbChooseGraphicType.SelectedIndex = 0;


        }

        #region Добавление элементов
        private void AddFerromagneticBut_Click(object sender, EventArgs e)
        {
            int n;       
            AddNewFieldForm form = new AddNewFieldForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                n = form.PointsNumber;
                mu_left = form.MagneticPermittivity;
                switch (form.SelectedFigure)
                {
                    case 0:
                        {
                            figuresForDrawing.Add(new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle(form.Rectangle, DrawingScale)));
                            model.AddBorderOfEnvironments(new Bound_Rectangle(form.Rectangle, SizeScale), n, 1, mu_left);
                            break;
                        }
                    case 1:
                        {
                            figuresForDrawing.Add(new CircleObjectDrawer(pictureBox1, new Bound_Circle(form.Circle, DrawingScale)));
                            model.AddBorderOfEnvironments(new Bound_Circle(form.Circle, SizeScale), n, 1, mu_left);
                           break;
                        }
                }
            }
            pictureBox1.Invalidate();
            
        }


        
        private void AddCoilButton_Click(object sender, EventArgs e)
        {         
            AddCoilForm form = new AddCoilForm();
            int n, m;
            double Current;
            if (form.ShowDialog() == DialogResult.OK)
            {
                n = form.N;
                m = form.M;
                Current = form.Current;

                figuresForDrawing.Add(new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle(form.Rectangle, DrawingScale)));
                model.AddSource(modelFactory.CreateVolumeSource(new Bound_Rectangle(form.Rectangle, SizeScale), Current, n, m));
            }
            pictureBox1.Invalidate();
        }
        #endregion

        #region Рисование
        private void CalculateForm_Load(object sender, EventArgs e)
        {
            Work_With_Files.Set_Paths_To_Problem_Files();
            path_to_directory_of_power_lines_data = Work_With_Files.path_to_directory_of_power_lines_data;
            path_to_data_files = Work_With_Files.path_to_data_files;
            pictureBox1.Paint += PictureBox1_Paint;
            InitPointsForGraphic();
            //selectedPair = pointsPairs[10];
            FillComboBoxOfLineSelecting();
            cbChooseGraphicType.SelectedIndex = 13;
           // cbSelectGraphicLine.SelectedIndex = 10;
            pictureBox1.Invalidate();
        }

        private void FillComboBoxOfLineSelecting()
        {
            for (int i = 0; i < pointsPairs.Count; i++)
            {
                cbSelectGraphicLine.Items.Add($"Line {i + 1}");
            }
        }

        private void Redraw(Graphics graphics)
        {

            DrawAxes(graphics);
            DrawFigures(graphics);
            //DrawLines(graphics);
            DrawSelectedLine(graphics);

        }

        private void DrawFigures(Graphics graphics)
        {

            if (figuresForDrawing.Count == 0) return;
            foreach (var x in figuresForDrawing)
                x.Draw(graphics);

        }

        private void DrawAxes(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 1);

            graphics.DrawLine(pen, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            graphics.DrawLine(pen, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
            graphics.DrawLine(pen, pictureBox1.Width - 20, pictureBox1.Height / 2 - 3, pictureBox1.Width - 2, pictureBox1.Height / 2);
            graphics.DrawLine(pen, pictureBox1.Width - 20, pictureBox1.Height / 2 + 3, pictureBox1.Width - 2, pictureBox1.Height / 2);
            graphics.DrawLine(pen, pictureBox1.Width / 2 - 3, 20, pictureBox1.Width / 2, 2);
            graphics.DrawLine(pen, pictureBox1.Width / 2 + 3, 20, pictureBox1.Width / 2, 2);

            brush.Dispose();
            pen.Dispose();

        }

        private void DrawSelectedLine(Graphics graphics)
        {
            if (selectedPair == null)
                return;
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            PointD p1 = selectedPair.Value.Point1;
            PointD p2 = selectedPair.Value.Point2;
            graphics.DrawLine(pen, ModifyModelPointToPicBoxPoint(new PointF((float)p1.X, (float)p1.Y)), ModifyModelPointToPicBoxPoint(new PointF((float)p2.X, (float)p2.Y)));

            brush.Dispose();
            pen.Dispose();
        }

        private void DrawLines(Graphics graphics)
        {
            if (model == null)
            {
                return;
            }
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 1);
            foreach (Bound bound in model.Bounds)
            {
                foreach (Rib rib in bound.Bound_Ribs)
                {
                    graphics.DrawLine(pen, ModifyModelPointToPicBoxPoint(new PointF((float)rib.Point1.X, (float)rib.Point1.Y)), ModifyModelPointToPicBoxPoint(new PointF((float)rib.Point2.X, (float)rib.Point2.Y)));
                }
            }
        
        }

        private PointF ModifyModelPointToPicBoxPoint(PointF point)
        {
            PointF result = new PointF(point.X * DrawingScale, -point.Y * DrawingScale);
            result.X += pictureBox1.Width / 2;
            result.Y += pictureBox1.Height / 2;
            return result;
        }
        private PointF ModifyPicBoxPointToModelPoint(Point point)
        {
            PointF result = new PointF(point.X, -point.Y);
            result.X -= pictureBox1.Width / 2;
            result.Y += pictureBox1.Height / 2;
            result.X /= DrawingScale;
            result.Y /= DrawingScale;

            return result;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Redraw(e.Graphics);
        }
        #endregion

        private void ButCalculate_Click(object sender, EventArgs e)
        {
            if (model == null) { MessageBox.Show("Модель не создана"); return; }
            if (model.Bounds.Count < 1 && model.Sources.Count < 1) { MessageBox.Show("Не добавлен источник или намагничиваемое тело"); return; }
            if (checkBoxUseRegularization.Checked)
            {
                if (double.TryParse(tbRegularizationParameter.Text, out double result))
                {
                    model.SolveProblemWithRegularization(result);
                }
                else
                {
                    MessageBox.Show("Неверный ввод параметра"); return;
                }
            }
            else
            {
                model.SolveProblem();
            }

            groupBoxGraphicsCalc.Enabled = true;
            groupBoxPowerLines.Enabled = true;

        }

        #region Масштаб и глубина
        private void МетрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeScale = 1;
            Work_With_Files.SizeScale = this.SizeScale;
        }

        private void СантиметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeScale = 0.01f;
            Work_With_Files.SizeScale = this.SizeScale;
        }

        private void МиллиметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeScale = 0.001f;
            Work_With_Files.SizeScale = this.SizeScale;
        }

        private void ГлубинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepthForm depthForm = new DepthForm();
            if(depthForm.ShowDialog() == DialogResult.OK)
            {
                Depth = depthForm.Depth;
                try
                {
                    model.Depth = depthForm.Depth;
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
           
        }
        #endregion 

        #region Силы
        double[] force;
        double[] mu;
        double[] error;
        double[] fInf;
        double forceMuInf;

        private void ButCulcForces_Click(object sender, EventArgs e)
        {
           /* FormGraphic graphic, graphicForce;
            float startWidht, startHeight;
            if (model.Bounds.Count == 1)
            {
                startWidht = bound_Rectangles[0].Lenth;
                startHeight = bound_Rectangles[0].Width;
            }
            if (model.Bounds.Count == 2)
            {
                startWidht = bound_Rectangles[1].Lenth;
                startHeight = bound_Rectangles[1].Width;
            }

            Bound_Rectangle startrect, rect;
            if (model.Bounds.Count == 1)
            {
                startrect = new Bound_Rectangle(bound_Rectangles[0].Location, bound_Rectangles[0].Lenth, bound_Rectangles[0].Width);
            }
            if (model.Bounds.Count == 2)
            {
                startrect = new Bound_Rectangle(bound_Rectangles[1].Location, bound_Rectangles[1].Lenth, bound_Rectangles[1].Width);
            }

            Bound_Rectangle bound_Rectangle;
            List<double[]> force_s = new List<double[]>();
            List<double[]> mu_s = new List<double[]>();
            List<double[]> error_s = new List<double[]>();*/

            
            
            #region Вариации области //меняется ширина якоря
            /*for(int k = 0; k < 5; k++)
            {
                if (model.Bounds.Count == 1)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[0].Location.X - k * bound_Rectangles[0].Lenth / 2, bound_Rectangles[0].Location.Y), 
                                                                            (k + 1) * bound_Rectangles[0].Lenth, bound_Rectangles[0].Width);
                    model.Bounds[0] = new Bound(rect, model.Bounds[0].Bound_Ribs.Count, model.Bounds[0].Right_Mu, model.Bounds[0].Left_Mu);
                }
                if (model.Bounds.Count == 2)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[1].Location.X - k * bound_Rectangles[1].Lenth / 2, bound_Rectangles[1].Location.Y),
                                                                            (k + 1) * bound_Rectangles[1].Lenth, bound_Rectangles[1].Width);
                    model.Bounds[1] = new Bound(rect, model.Bounds[1].Bound_Ribs.Count, model.Bounds[1].Right_Mu, model.Bounds[1].Left_Mu);
                }
                CalcGraph();
                error_s.Add(error);
                
            }*/
            //Меняется толщина якоря
            /*for (int k = 0; k < 5; k++)
            {
                if (model.Bounds.Count == 1)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[0].Location.X, bound_Rectangles[0].Location.Y),
                                                                            bound_Rectangles[0].Lenth, (k + 1) * bound_Rectangles[0].Width);
                    model.Bounds[0] = new Bound(rect, model.Bounds[0].Bound_Ribs.Count, model.Bounds[0].Right_Mu, model.Bounds[0].Left_Mu);
                }
                if (model.Bounds.Count == 2)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[1].Location.X, bound_Rectangles[1].Location.Y),
                                                                            bound_Rectangles[1].Lenth, (k + 1) * bound_Rectangles[1].Width);
                    model.Bounds[1] = new Bound(rect, model.Bounds[1].Bound_Ribs.Count, model.Bounds[1].Right_Mu, model.Bounds[1].Left_Mu);
                }
                CalcGraph();
                error_s.Add(error);

            }
            graphic = new FormGraphic(mu, error_s);
            graphic.ShowDialog();*/

            //Меняются оба размера
            /*for (int k = 0; k < 5; k++)
            {
                if (model.Bounds.Count == 1)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[0].Location.X - k  * bound_Rectangles[0].Lenth / 2, bound_Rectangles[0].Location.Y),
                                                                            (k + 1) * bound_Rectangles[0].Lenth, (k + 1) * bound_Rectangles[0].Width);
                    model.Bounds[0] = new Bound(rect, model.Bounds[0].Bound_Ribs.Count, model.Bounds[0].Right_Mu, model.Bounds[0].Left_Mu);
                }
                if (model.Bounds.Count == 2)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[1].Location.X - k * bound_Rectangles[1].Lenth / 2, bound_Rectangles[1].Location.Y),
                                                                            (k + 1) * bound_Rectangles[1].Lenth, (k + 1) * bound_Rectangles[1].Width);
                    model.Bounds[1] = new Bound(rect, model.Bounds[1].Bound_Ribs.Count, model.Bounds[1].Right_Mu, model.Bounds[1].Left_Mu);
                }
                CalcGraph();
                error_s.Add(error);

            }
            graphic = new FormGraphic(mu, error_s);
            graphic.ShowDialog();*/
            //fInf = new double[5];
            //Меняется величина зазора
            /*for (int k = 0; k < 5; k++)
            {
                if (model.Bounds.Count == 1)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[0].Location.X, bound_Rectangles[0].Location.Y -  2 * k * SizeScale),
                                                                            bound_Rectangles[0].Lenth, bound_Rectangles[0].Width);
                    model.Bounds[0] = new Bound(rect, model.Bounds[0].Bound_Ribs.Count, model.Bounds[0].Right_Mu, model.Bounds[0].Left_Mu);
                }
                if (model.Bounds.Count == 2)
                {
                    rect = new Bound_Rectangle(new PointF(bound_Rectangles[1].Location.X, bound_Rectangles[1].Location.Y -  2 * k* SizeScale),
                                                                            bound_Rectangles[1].Lenth, bound_Rectangles[1].Width);
                    model.Bounds[1] = new Bound(rect, model.Bounds[1].Bound_Ribs.Count, model.Bounds[1].Right_Mu, model.Bounds[1].Left_Mu);
                }
                CalcGraph();
                force_s.Add(force);
                error_s.Add(error);
                fInf[k] = forceMuInf;
            }
            */
            #endregion

            /*fInf = new double[5];
            graphic = new FormGraphic(mu, error_s);
            graphic.ShowDialog();
            graphicForce = new FormGraphic(mu, force_s, fInf);
            graphicForce.ShowDialog();*/
        }

        private void CalcGraph ()
        {
            /*try
            {
                int n = 100;
                double start = 2;
                double step = 210;
                model.Bounds[0].Left_Mu = double.MaxValue;
                if (model.Bounds.Count == 2)
                    model.Bounds[1].Left_Mu = double.MaxValue;
                model.SolveProblem();
                forceMuInf = model.Calculate_Power();


                error = new double[n];
                mu = new double[n];
                force = new double[n];

                mu[0] = start;
                model.Bounds[0].Left_Mu = start;
                if (model.Bounds.Count == 2)
                    model.Bounds[1].Left_Mu = start;

                model.SolveProblem();
                force[0] = model.Calculate_Power();
                error[0] = Math.Abs(forceMuInf - force[0]) / Math.Abs(force[0]);


                for (int i = 1; i < mu.Length; i++)
                {
                    mu[i] = mu[i - 1] + step;
                    model.Bounds[0].Left_Mu = mu[i];
                    if (model.Bounds.Count == 2)
                        model.Bounds[1].Left_Mu = mu[i];
                    model.SolveProblem();
                    force[i] = model.Calculate_Power();
                    error[i] = Math.Abs(forceMuInf - force[i]) / Math.Abs(force[i]);
                }
            }

            catch
            {
                MessageBox.Show("Недостаточно данных", "Ошибка!");
            }*/
        }
        #endregion

        private void CalculatePower_Click(object sender, EventArgs e)
        {
            /*try
            {
                double Force = model.Calculate_Power();
                string str = String.Format("Fy={0} Н", Force);
                MessageBox.Show(str, "Сила действия на груз");
            }
            catch
            {
                MessageBox.Show("Недостаточно данных", "Ошибка!");
            }*/
        }

        

        private void ButGetNodes_Click(object sender, EventArgs e)
        {
           PointsForCulc = Work_With_Files.GetNodesFromFile(path_to_directory_of_power_lines_data + "\\xy.dat");
        }

        private void ButCalcPotencial_Click(object sender, EventArgs e)
        {
            if (PointsForCulc == null) { MessageBox.Show("Нет точек для расчёта"); return; } 
            CalculatePotencial();
        }


        #region Вычисление величин

        private void CalculateVectorPotencial_ForGraphic()
        {
            int n = 1000;
            int k = 1001;
            List<double> Potencial = new List<double>(k);
            double dx = 0.1 / n;
            PointD[] PointsBn = new PointD[k];
            double[] X = new double[k];
            for (int i = 0; i <= n; i++)
            {
                PointsBn[i] = new PointD(-0.05 + i * dx, 0.005);
                X[i] = PointsBn[i].X;
            }

            for (int i = 0; i <= n; i++)
            {
                Potencial.Add(model.CalculatePotencial(PointsBn[i]));
            }

           // BuilderPotencialGraphic graphic = new BuilderPotencialGraphic(X, Potencial, GraphicTypes.Potencial);
            //graphic.ShowDialog();
        }

        /// <summary>
        /// Переделать
        /// </summary>
        private void CalculateNormalPartOfInduction_ForGraphic()
        {
            //int n = 1000;
            
            int k = 1001;
            List<double> X = new List<double>(k);
            List<double> Bn = new List<double>(k);
            Bn = graphicsCalculating.Calculate(new PointD(-0.05, 0), new PointD(0.05, 0), k, GraphicTypes.Intensity_Y_component);
            X = graphicsCalculating.GetLenth();
            /*double dx = 0.1 / n;
            PointD[] PointsBn = new PointD[k];
            
            for (int i = 0; i <= n; i++)
            {
                //PointsBn[i] = new PointD(-0.05 + i * dx, 0.005);
                PointsBn[i] = new PointD(-0.05 + i * dx, 0);
                X[i] = PointsBn[i].X;
            }
            Vector2D vec;
            Bound_Rib rib = new Bound_Rib(new PointD(-0.1, 0.005), new PointD(0.1, 0.005));
            for (int i = 0; i <= n; i++)
            {
                vec = model.CalculateIntensity(PointsBn[i]);
                //Bn.Add(vec.X_component * rib.Normal.CosAlpha + vec.Y_component * rib.Normal.CosBeta);
                Bn.Add(vec.Y_component);
            }*/

            //BuilderPotencialGraphic graphic = new BuilderPotencialGraphic(X, Bn, GraphicTypes.Induction_Normal_component);
            //graphic.ShowDialog();
        }


        private void CalculateModulOfInduction_ForGraphic()
        {
            int n = 1000;
            int k = 1001;
            List<double> InductionModul = new List<double>(k);
            double dx = 0.1 / n;
            PointD[] PointsBn = new PointD[k];
            double[] X = new double[k];
            for (int i = 0; i <= n; i++)
            {
                PointsBn[i] = new PointD(-0.05 + i * dx, 0.005);
                X[i] = PointsBn[i].X;
            }
            Vector2D vec;
            for (int i = 0; i <= n; i++)
            {
                vec = model.CalculateInduction(PointsBn[i]);
                InductionModul.Add(Math.Sqrt(vec.X_component * vec.X_component + vec.Y_component * vec.Y_component));
            }

            //BuilderPotencialGraphic graphic = new BuilderPotencialGraphic(X, InductionModul, GraphicTypes.InductionModul);
            //graphic.ShowDialog();
        }


        private void CalculatePotencial()
        {
            try
            {
                /*model.Calculate_ReactionPotencial(PointsForCulc, out ReactionPotencial);
                model.Calculate_Sources_Potencial(PointsForCulc, out SourcesPotencial);*/
                ResultPotencial = new double[PointsForCulc.Length];
                for (int i = 0; i < PointsForCulc.Length; i++)
                    ResultPotencial[i] = model.CalculatePotencial(PointsForCulc[i]);

                string str;
                string pathF;
                pathF = path_to_directory_of_power_lines_data + @"\\c.dat";
                using (StreamWriter stream = new StreamWriter(pathF))
                {
                    for (int i = 0; i < ResultPotencial.Length; i++)
                    {
                        str = ResultPotencial[i].ToString().Replace(',', '.');
                        stream.WriteLine(str);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Недостаточно данных", "Ошибка!");
            }

        }

        #endregion

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figuresForDrawing.Clear();
            string file;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = path_to_data_files;
            openFileDialog.DefaultExt = ".dat";
            openFileDialog.Filter = "Data files (*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                Work_With_Files.ReadPhisicalObjectsInformationFromFile(out model, out modelFactory, file);
                graphicsCalculating = new GraphicsCalculation(model);
                foreach(var figure in model.Bounds)
                {
                    figuresForDrawing.Add(DrawerBuilder.BuildDrawer(pictureBox1, figure, DrawingScale));
  
                }
                foreach (var figure in model.Sources)
                {
                    figuresForDrawing.Add(DrawerBuilder.BuildDrawer(pictureBox1, figure, DrawingScale));

                }
                FillModelData();
                pictureBox1.Invalidate();
                groupBoxPowerLines.Enabled = false;
                groupBoxGraphicsCalc.Enabled = false;
            }
            
        }

        private void FillModelData()
        {
            rtbFields.Clear();
            rtbSources.Clear();
            string fieldType = "";
            string potencialType = "";
            string layerType = "";
            switch (model.PhysicalField)
            {
                case PhysicalField.Electric:
                    {
                        fieldType = "электрическое";
                        break;
                    }
                case PhysicalField.Magnetic:
                    {
                        fieldType = "магнитное";
                        break;
                    }
                case PhysicalField.Current:
                    {
                        fieldType = "поле постоянного тока";
                        break;
                    }
            }

            switch (model.GetPotencialType())
            {
                case TypeOfPotencial.Scalar:
                    {
                        potencialType = "скалярный";
                        break;
                    }
                case TypeOfPotencial.Vector:
                    {
                        potencialType = "векторный";
                        break;
                    }
            }

            switch (model.Potencial.TypeOFPotencialsLayer)
            {
                case PotencialTypes.PSL:
                    {
                        layerType = "простой слой";
                        break;
                    }
                case PotencialTypes.PDL:
                    {
                        layerType = "двойной слой";
                        break;
                    }
            }

            tbFieldType.Text = fieldType;
            tbPotencialType.Text = potencialType;
            tbLayerType.Text = layerType;
            tbDepth.Text = model.Depth.ToString();
            int i = 1;
            foreach(Bound bound in model.Bounds)
            {
                rtbFields.AppendText($"№ {i++}. ");
                rtbFields.AppendText(bound.ToString() + "\n");
            }

            i = 1;
            foreach(ISource source in model.Sources)
            {
                rtbSources.AppendText($"№ {i++}. ");
                rtbSources.AppendText(source.ToString() + "\n" );
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = path_to_data_files;
            saveFileDialog.DefaultExt = ".dat";
            saveFileDialog.Filter = "Data files (*.dat)|*.dat";
            if (saveFileDialog.ShowDialog()== DialogResult.OK)
            {
                file = saveFileDialog.FileName;
                Work_With_Files.WritePhisicalObjectsInformationToFile(model, "", file);
            }
            
        }

        private void CreateFileGeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = path_to_directory_of_power_lines_data;
            saveFileDialog.DefaultExt = ".geo";
            saveFileDialog.Filter = "Geo files (*.geo)|*geo";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = saveFileDialog.FileName;
                Work_With_Files.CreateGeoFile(new PointD(-0.025, 0.025), new PointD(-0.025, -0.025), new PointD(0.025, -0.025), new PointD(0.025, 0.025), 0.0005, file);
            }
        }

        private void CreateMshBasedOnGeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_geo = "";
            string file_msh = "";
            string format = "msh1";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = path_to_directory_of_power_lines_data;
            openFileDialog.DefaultExt = ".geo";
            openFileDialog.Filter = "Geo files (*.geo)|*geo";
            openFileDialog.Title = "Выбрать файл геометрии";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_geo = openFileDialog.FileName;
            }


            Work_With_Files.CreateMshFile(file_geo, file_msh, format);

        }

        private void WriteNodesAndElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_msh = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = path_to_directory_of_power_lines_data;
            openFileDialog.DefaultExt = ".geo";
            openFileDialog.Filter = "Msh files (*.msh)|*msh";
            openFileDialog.Title = "Выбрать файл сетки";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_msh = openFileDialog.FileName;
                Work_With_Files.WriteNodesAndElementsToFile(file_msh, "\\tri.dat", "\\xy.dat");
            }
        }

        private void ButBuildPowerLines_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Work_With_Files.path_to_data_files;
            process.StartInfo.FileName = path_to_directory_of_power_lines_data + "\\flux.exe";
            process.Start();
        }

        private void ButCalcPotencialForGraphic_Click(object sender, EventArgs e)
        {
            CalculateVectorPotencial_ForGraphic();
        }

        private void CloseProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(model != null && model.Bounds.Count > 0) model.Bounds.Clear();
            if(model != null && model.Sources.Count > 0) model.Sources.Clear();
            if(figuresForDrawing.Count > 0) figuresForDrawing.Clear();
            pictureBox1.Invalidate();
            groupBoxPowerLines.Enabled = false;
            groupBoxGraphicsCalc.Enabled = false;

        }

        private void ChangeDirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                path_to_directory_of_power_lines_data = folderBrowser.SelectedPath;
                DirectoryInfo appDirectory = new DirectoryInfo(".");
                using (StreamWriter streamWriter = new StreamWriter(appDirectory.FullName + path_file))
                {
                    streamWriter.WriteLine(path_to_directory_of_power_lines_data);
                }
                Work_With_Files.path_to_directory_of_power_lines_data = this.path_to_directory_of_power_lines_data;
            }
        }

        private void butAddMagnet_Click(object sender, EventArgs e)
        {
            AddMagnetForm form = new AddMagnetForm();
            double coersive;
            if (form.ShowDialog() == DialogResult.OK)
            {
                coersive = form.coerciveForce;


                figuresForDrawing.Add(new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle(form.Rectangle, DrawingScale)));
                model.AddSource(modelFactory.CreateResidualIntensitySource(new Bound_Rectangle(form.Rectangle, SizeScale), form.Directions, coersive, 100));
            }
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(PotencialTypes.Scalar.ToString());
            Vector2D vec = model.CalculateInduction(new PointD(0, 0.01));
            MessageBox.Show($"{vec.X_component}\n{vec.Y_component}\n{model.CalculatePotencial(new PointD(0, 0.01))}");
        }

        private void cbChooseGraphicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            choosenGraphic = (GraphicTypes)cbChooseGraphicType.SelectedIndex;
        }

        private void butBuildGraphic_Click(object sender, EventArgs e)
        {
            int k;
            if (!int.TryParse(tbNumOfGraphicPoints.Text, out k) || k <= 0)
            {
                MessageBox.Show("Некорректное число!");
                return;
            }

            List<double> X;
            List<double> function;
            //горизонтальная линия
            InitPointsForGraphic();

            function = graphicsCalculating.Calculate(selectedPair.Value.Point1, selectedPair.Value.Point2, k, (GraphicTypes)cbChooseGraphicType.SelectedItem);
            X = graphicsCalculating.GetLenth();

            Work_With_Files.SaveGraphicData(X, function);

            GraphicsBildingForm graphic = new GraphicsBildingForm(X, function, (GraphicTypes)cbChooseGraphicType.SelectedItem, model.PhysicalField);
            graphic.ShowDialog();
        }

        private void InitPointsForGraphic()
        {
            PointD Point1 = new PointD(-0.02, 0);
            PointD Point2 = new PointD(0.02, 0);
            pointsPairs.Add(new PointsPair { Point1 = Point1, Point2 = Point2 });

            //вертикальная линия
            PointD Point3 = new PointD(0, -0.02);
            PointD Point4 = new PointD(0, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point3, Point2 = Point4 });


            PointD Point5 = new PointD(-0.05, 0.005);
            PointD Point6 = new PointD(0.05, 0.005);
            pointsPairs.Add(new PointsPair { Point1 = Point5, Point2 = Point6 });
            //вертикальная линия 2
            PointD Point7 = new PointD(0.001, -0.02);
            PointD Point8 = new PointD(0.001, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point7, Point2 = Point8 });


            PointD Point9 = new PointD(-0.02, 0);
            PointD Point10 = new PointD(0.02, 0);
            pointsPairs.Add(new PointsPair { Point1 = Point9, Point2 = Point10 });

            PointD Point11 = new PointD(-0.02, 0.0021);
            PointD Point12 = new PointD(0.02, 0.0021);
            pointsPairs.Add(new PointsPair { Point1 = Point11, Point2 = Point12 });



            //горизонтальная линия 3
            PointD Point13 = new PointD(-0.02, 0.0045);
            PointD Point14 = new PointD(0.02, 0.0045);
            pointsPairs.Add(new PointsPair { Point1 = Point13, Point2 = Point14 });

            //вертикальная линия 3
            PointD Point15 = new PointD(-0.0071, -0.02);
            PointD Point16 = new PointD(-0.0071, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point15, Point2 = Point16 });

            //горизонтальная линия
            PointD Point17 = new PointD(-0.05, 0.00175);
            PointD Point18 = new PointD(0.05, 0.00175);
            pointsPairs.Add(new PointsPair { Point1 = Point17, Point2 = Point18 });

            //вертикальная линия 4
            PointD Point19 = new PointD(0, -0.02);
            PointD Point20 = new PointD(0, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point19, Point2 = Point20 });

            //вертикальная линия 5
            PointD Point21 = new PointD(0.0016, -0.02);
            PointD Point22 = new PointD(0.0016, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point21, Point2 = Point22 });

            //вертикальная линия 6
            PointD Point23 = new PointD(0.0016, -0.02993);
            PointD Point24 = new PointD(0.0016, 0.02993);
            pointsPairs.Add(new PointsPair { Point1 = Point23, Point2 = Point24 });

            //горизонтальная линия 5
            PointD Point25 = new PointD(-0.02, 0.0194);
            PointD Point26 = new PointD(0.02, 0.0194);
            pointsPairs.Add(new PointsPair { Point1 = Point25, Point2 = Point26 });

            //горизонтальная линия 6
            PointD Point27 = new PointD(-0.02, 0);
            PointD Point28 = new PointD(0.02, 0);
            pointsPairs.Add(new PointsPair { Point1 = Point27, Point2 = Point28 });

            //горизонтальная линия 7
            PointD Point29 = new PointD(-0.02, 0.0038);
            PointD Point30 = new PointD(0.02, 0.0038);
            pointsPairs.Add(new PointsPair { Point1 = Point29, Point2 = Point30 });

            //горизонтальная линия 7
            PointD Point31 = new PointD(-0.02, 0.0175);
            PointD Point32 = new PointD(0.02, 0.0175);
            pointsPairs.Add(new PointsPair { Point1 = Point31, Point2 = Point32 });

            //вертикальная линия 7
            PointD Point33 = new PointD(0.00175, -0.02);
            PointD Point34 = new PointD(0.00175, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point33, Point2 = Point34 });

            //вертикальная линия 8
            PointD Point35 = new PointD(0.0011, -0.02);
            PointD Point36 = new PointD(0.0011, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point35, Point2 = Point36 });

            //горизонтальная линия 8
            PointD Point37 = new PointD(-0.02, 0.003);
            PointD Point38 = new PointD(0.02, 0.003);
            pointsPairs.Add(new PointsPair { Point1 = Point37, Point2 = Point38 });

            //вертикальная линия 9
            PointD Point39 = new PointD(0.002, -0.02);
            PointD Point40 = new PointD(0.002, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point39, Point2 = Point40 });

            //вертикальная линия 10
            PointD Point41 = new PointD(0.004, -0.02);
            PointD Point42 = new PointD(0.004, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point41, Point2 = Point42 });
            pointsPairs.Add(new PointsPair { Point1 = Point42, Point2 = Point41 });

            //вертикальная линия 11
            PointD Point43 = new PointD(0.00151, -0.02);
            PointD Point44 = new PointD(0.00151, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point43, Point2 = Point44 });

            //вертикальная линия 12
            PointD Point45 = new PointD(0.02, -0.0195);
            PointD Point46 = new PointD(0.02, 0.0195);
            pointsPairs.Add(new PointsPair { Point1 = Point45, Point2 = Point46 });

            //вертикальная линия 14
            PointD Point47 = new PointD(0.0016, -0.02);
            PointD Point48 = new PointD(0.0016, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point48, Point2 = Point47 });

            //вертикальная линия 15
            PointD Point49 = new PointD(0.0005, -0.02);
            PointD Point50 = new PointD(0.0005, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point49, Point2 = Point50 });

            //вертикальная линия 16
            pointsPairs.Add(new PointsPair { Point1 = Point50, Point2 = Point49 });

            //вертикальная линия 17
            PointD Point51 = new PointD(0, -0.01);
            PointD Point52 = new PointD(0, 0.01);
            pointsPairs.Add(new PointsPair { Point1 = Point51, Point2 = Point52 });
            pointsPairs.Add(new PointsPair { Point1 = Point52, Point2 = Point51 });

            //вертикальная линия 18
            PointD Point53 = new PointD(0, -0.02);
            PointD Point54 = new PointD(0, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point53, Point2 = Point54 });
            pointsPairs.Add(new PointsPair { Point1 = Point54, Point2 = Point53 });

            //вертикальная линия 19
            PointD Point55 = new PointD(0.004, -0.02);
            PointD Point56 = new PointD(0.004, 0.02);
            pointsPairs.Add(new PointsPair { Point1 = Point55, Point2 = Point56 });
            pointsPairs.Add(new PointsPair { Point1 = Point56, Point2 = Point55 });

            //горизонтальная линия 9
            PointD Point57 = new PointD(-0.02, 0.011);
            PointD Point58 = new PointD(0.02, 0.011);
            pointsPairs.Add(new PointsPair { Point1 = Point57, Point2 = Point58 });
            pointsPairs.Add(new PointsPair { Point1 = Point58, Point2 = Point57 });

            //вертикальная линия 20
            PointD Point59 = new PointD(0.015, -0.012);
            PointD Point60 = new PointD(0.015, 0.012);
            pointsPairs.Add(new PointsPair { Point1 = Point59, Point2 = Point60 });
            pointsPairs.Add(new PointsPair { Point1 = Point60, Point2 = Point59 });
            //вертикальная линия 21
            PointD Point61 = new PointD(-0.015, -0.012);
            PointD Point62 = new PointD(-0.015, 0.012);
            pointsPairs.Add(new PointsPair { Point1 = Point61, Point2 = Point62 });
            pointsPairs.Add(new PointsPair { Point1 = Point62, Point2 = Point61 });
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = pictureBox1.PointToClient(Cursor.Position);
            PointF modifiedPointF = ModifyPicBoxPointToModelPoint(point);
            tbCursorX.Text = modifiedPointF.X.ToString();
            tbCursorY.Text = modifiedPointF.Y.ToString();
        }

        private void откалиброватьПотенциалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalibratePotencial();
        }

        private void CalibratePotencial()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл с потенциалом, рассчитанным МГЭ";
            openFileDialog.Filter = "Data files (*.dat)|*.dat|All files|*.*";
            double delta;
            StreamReader reader;
            string fileName = "";
            int kMBE;
            int kFemm;


            List<double> pointsMBE = new List<double>();
            List<double> functionMBE = new List<double>();
            List<double> pointsFemm = new List<double>();
            List<double> functionFemm = new List<double>();
            int n = 0;

            string path;
            string str;
            string[] str2;
            string[] str3;

            if (openFileDialog.ShowDialog()== DialogResult.OK)
            {
                path = openFileDialog.FileName;
                using(reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        str = reader.ReadLine();
                        str2 = str.Split('\t');
                        pointsMBE.Add(double.Parse(str2[0]));
                        functionMBE.Add(double.Parse(str2[1]));
                    }
                }

            }
            else
            {
                MessageBox.Show("Ошибка");
                return;
            }
            openFileDialog.Title = "Выберите файл с графиком из FEmm";
            openFileDialog.Filter = "Text files (*.txt)|*.dat|All files|*.*";
            

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                fileName = path + "fromFemmToMBE.dat";
                using (reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        n++;
                        str = reader.ReadLine();
                        str2 = str.Split('\t');
                        str3 = str2[0].Split('e');
                        pointsFemm.Add(double.Parse(str3[0].Replace('.', ',')) * Math.Pow(10, Int32.Parse(str3[1])) / 1000);
                        str3 = str2[2].Split('e');
                        functionFemm.Add(double.Parse(str3[0].Replace('.', ',')) * Math.Pow(10, Int32.Parse(str3[1])));
                    }

                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                return;
            }

            //kMBE = functionMBE.Count / 2;
            //kFemm = functionFemm.Count / 2;
            kMBE = 0;
            kFemm = 0;
            delta = functionFemm[kFemm] - functionMBE[kMBE];
            for(int i = 0; i < n; i++)
            {
                functionFemm[i] -= delta;
            }
            StreamWriter writer;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                using (writer = new StreamWriter(fileName))
                {
                    for (int i = 0; i < n; i++)
                    {
                        writer.WriteLine($"{pointsFemm[i]}\t{functionFemm[i]}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                return;
            }

        }

        private void cbSelectGraphicLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPair = pointsPairs[cbSelectGraphicLine.SelectedIndex];
            tbX1.Text = selectedPair.Value.Point1.X.ToString();
            tbY1.Text = selectedPair.Value.Point1.Y.ToString();
            tbX2.Text = selectedPair.Value.Point2.X.ToString();
            tbY2.Text = selectedPair.Value.Point2.Y.ToString();
            pictureBox1.Invalidate();
        }

    }
}

