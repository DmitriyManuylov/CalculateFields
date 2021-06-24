namespace Calculating_Magnetic_Field
{
    partial class CalculateForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butCalculate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProblemDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.единицыИзмеренияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размерыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.метрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сантиметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.миллиметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.глубинаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateFileGeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateMshBasedOnGeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WriteNodesAndElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.butGetNodes = new System.Windows.Forms.Button();
            this.butCalcPotencial = new System.Windows.Forms.Button();
            this.groupBoxPowerLines = new System.Windows.Forms.GroupBox();
            this.groupBoxGraphicsCalc = new System.Windows.Forms.GroupBox();
            this.groupBoxLinePoints = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNumOfGraphicPoints = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbX2 = new System.Windows.Forms.TextBox();
            this.tbY2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbX1 = new System.Windows.Forms.TextBox();
            this.tbY1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSelectGraphicLine = new System.Windows.Forms.ComboBox();
            this.butBuildGraphic = new System.Windows.Forms.Button();
            this.cbChooseGraphicType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbFields = new System.Windows.Forms.GroupBox();
            this.rtbFields = new System.Windows.Forms.RichTextBox();
            this.gbSources = new System.Windows.Forms.GroupBox();
            this.rtbSources = new System.Windows.Forms.RichTextBox();
            this.gbGeneralModelData = new System.Windows.Forms.GroupBox();
            this.tbDepth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLayerType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPotencialType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFieldType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbCursorY = new System.Windows.Forms.TextBox();
            this.tbCursorX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxUseRegularization = new System.Windows.Forms.CheckBox();
            this.tbRegularizationParameter = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.откалиброватьПотенциалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBoxPowerLines.SuspendLayout();
            this.groupBoxGraphicsCalc.SuspendLayout();
            this.groupBoxLinePoints.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbFields.SuspendLayout();
            this.gbSources.SuspendLayout();
            this.gbGeneralModelData.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(231, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(599, 716);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // butCalculate
            // 
            this.butCalculate.Location = new System.Drawing.Point(7, 100);
            this.butCalculate.Name = "butCalculate";
            this.butCalculate.Size = new System.Drawing.Size(204, 23);
            this.butCalculate.TabIndex = 1;
            this.butCalculate.Text = "Рассчитать плотность";
            this.butCalculate.UseVisualStyleBackColor = true;
            this.butCalculate.Click += new System.EventHandler(this.ButCalculate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.ProblemDataToolStripMenuItem,
            this.откалиброватьПотенциалToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1222, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.closeProblemToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.OpenToolStripMenuItem.Text = "Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // closeProblemToolStripMenuItem
            // 
            this.closeProblemToolStripMenuItem.Name = "closeProblemToolStripMenuItem";
            this.closeProblemToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.closeProblemToolStripMenuItem.Text = "Закрыть задачу";
            this.closeProblemToolStripMenuItem.Click += new System.EventHandler(this.CloseProblemToolStripMenuItem_Click);
            // 
            // ProblemDataToolStripMenuItem
            // 
            this.ProblemDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.единицыИзмеренияToolStripMenuItem,
            this.глубинаToolStripMenuItem,
            this.CreateFileGeoToolStripMenuItem,
            this.CreateMshBasedOnGeoToolStripMenuItem,
            this.WriteNodesAndElementsToolStripMenuItem,
            this.changeDirToolStripMenuItem});
            this.ProblemDataToolStripMenuItem.Name = "ProblemDataToolStripMenuItem";
            this.ProblemDataToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.ProblemDataToolStripMenuItem.Text = "Данные задачи";
            // 
            // единицыИзмеренияToolStripMenuItem
            // 
            this.единицыИзмеренияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.размерыToolStripMenuItem});
            this.единицыИзмеренияToolStripMenuItem.Name = "единицыИзмеренияToolStripMenuItem";
            this.единицыИзмеренияToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.единицыИзмеренияToolStripMenuItem.Text = "Единицы измерения";
            // 
            // размерыToolStripMenuItem
            // 
            this.размерыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.метрыToolStripMenuItem,
            this.сантиметрыToolStripMenuItem,
            this.миллиметрыToolStripMenuItem});
            this.размерыToolStripMenuItem.Name = "размерыToolStripMenuItem";
            this.размерыToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.размерыToolStripMenuItem.Text = "Размеры";
            // 
            // метрыToolStripMenuItem
            // 
            this.метрыToolStripMenuItem.Name = "метрыToolStripMenuItem";
            this.метрыToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.метрыToolStripMenuItem.Text = "Метры";
            this.метрыToolStripMenuItem.Click += new System.EventHandler(this.МетрыToolStripMenuItem_Click);
            // 
            // сантиметрыToolStripMenuItem
            // 
            this.сантиметрыToolStripMenuItem.Name = "сантиметрыToolStripMenuItem";
            this.сантиметрыToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.сантиметрыToolStripMenuItem.Text = "Сантиметры";
            this.сантиметрыToolStripMenuItem.Click += new System.EventHandler(this.СантиметрыToolStripMenuItem_Click);
            // 
            // миллиметрыToolStripMenuItem
            // 
            this.миллиметрыToolStripMenuItem.Name = "миллиметрыToolStripMenuItem";
            this.миллиметрыToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.миллиметрыToolStripMenuItem.Text = "Миллиметры";
            this.миллиметрыToolStripMenuItem.Click += new System.EventHandler(this.МиллиметрыToolStripMenuItem_Click);
            // 
            // глубинаToolStripMenuItem
            // 
            this.глубинаToolStripMenuItem.Name = "глубинаToolStripMenuItem";
            this.глубинаToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.глубинаToolStripMenuItem.Text = "Глубина";
            this.глубинаToolStripMenuItem.Click += new System.EventHandler(this.ГлубинаToolStripMenuItem_Click);
            // 
            // CreateFileGeoToolStripMenuItem
            // 
            this.CreateFileGeoToolStripMenuItem.Name = "CreateFileGeoToolStripMenuItem";
            this.CreateFileGeoToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.CreateFileGeoToolStripMenuItem.Text = "Создать файл geo";
            this.CreateFileGeoToolStripMenuItem.Click += new System.EventHandler(this.CreateFileGeoToolStripMenuItem_Click);
            // 
            // CreateMshBasedOnGeoToolStripMenuItem
            // 
            this.CreateMshBasedOnGeoToolStripMenuItem.Name = "CreateMshBasedOnGeoToolStripMenuItem";
            this.CreateMshBasedOnGeoToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.CreateMshBasedOnGeoToolStripMenuItem.Text = "Создать Msh на основе geo";
            this.CreateMshBasedOnGeoToolStripMenuItem.Click += new System.EventHandler(this.CreateMshBasedOnGeoToolStripMenuItem_Click);
            // 
            // WriteNodesAndElementsToolStripMenuItem
            // 
            this.WriteNodesAndElementsToolStripMenuItem.Name = "WriteNodesAndElementsToolStripMenuItem";
            this.WriteNodesAndElementsToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.WriteNodesAndElementsToolStripMenuItem.Text = "Записать узлы и элементы в файлы";
            this.WriteNodesAndElementsToolStripMenuItem.Click += new System.EventHandler(this.WriteNodesAndElementsToolStripMenuItem_Click);
            // 
            // changeDirToolStripMenuItem
            // 
            this.changeDirToolStripMenuItem.Name = "changeDirToolStripMenuItem";
            this.changeDirToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.changeDirToolStripMenuItem.Text = "Указать директорию файлов задачи";
            this.changeDirToolStripMenuItem.Click += new System.EventHandler(this.ChangeDirToolStripMenuItem_Click_1);
            // 
            // butGetNodes
            // 
            this.butGetNodes.Location = new System.Drawing.Point(3, 19);
            this.butGetNodes.Name = "butGetNodes";
            this.butGetNodes.Size = new System.Drawing.Size(203, 23);
            this.butGetNodes.TabIndex = 5;
            this.butGetNodes.Text = "Извлечь узлы";
            this.butGetNodes.UseVisualStyleBackColor = true;
            this.butGetNodes.Click += new System.EventHandler(this.ButGetNodes_Click);
            // 
            // butCalcPotencial
            // 
            this.butCalcPotencial.Location = new System.Drawing.Point(3, 48);
            this.butCalcPotencial.Name = "butCalcPotencial";
            this.butCalcPotencial.Size = new System.Drawing.Size(203, 42);
            this.butCalcPotencial.TabIndex = 6;
            this.butCalcPotencial.Text = "Рассчитать потенциал для силовых линий";
            this.butCalcPotencial.UseVisualStyleBackColor = true;
            this.butCalcPotencial.Click += new System.EventHandler(this.ButCalcPotencial_Click);
            // 
            // groupBoxPowerLines
            // 
            this.groupBoxPowerLines.Controls.Add(this.butGetNodes);
            this.groupBoxPowerLines.Controls.Add(this.butCalcPotencial);
            this.groupBoxPowerLines.Location = new System.Drawing.Point(4, 129);
            this.groupBoxPowerLines.Name = "groupBoxPowerLines";
            this.groupBoxPowerLines.Size = new System.Drawing.Size(217, 98);
            this.groupBoxPowerLines.TabIndex = 16;
            this.groupBoxPowerLines.TabStop = false;
            this.groupBoxPowerLines.Text = "Расчёт картины поля";
            // 
            // groupBoxGraphicsCalc
            // 
            this.groupBoxGraphicsCalc.Controls.Add(this.groupBoxLinePoints);
            this.groupBoxGraphicsCalc.Controls.Add(this.cbSelectGraphicLine);
            this.groupBoxGraphicsCalc.Controls.Add(this.butBuildGraphic);
            this.groupBoxGraphicsCalc.Controls.Add(this.cbChooseGraphicType);
            this.groupBoxGraphicsCalc.Location = new System.Drawing.Point(6, 233);
            this.groupBoxGraphicsCalc.Name = "groupBoxGraphicsCalc";
            this.groupBoxGraphicsCalc.Size = new System.Drawing.Size(215, 370);
            this.groupBoxGraphicsCalc.TabIndex = 13;
            this.groupBoxGraphicsCalc.TabStop = false;
            this.groupBoxGraphicsCalc.Text = "Графики";
            // 
            // groupBoxLinePoints
            // 
            this.groupBoxLinePoints.Controls.Add(this.label5);
            this.groupBoxLinePoints.Controls.Add(this.tbNumOfGraphicPoints);
            this.groupBoxLinePoints.Controls.Add(this.groupBox2);
            this.groupBoxLinePoints.Controls.Add(this.groupBox1);
            this.groupBoxLinePoints.Location = new System.Drawing.Point(6, 73);
            this.groupBoxLinePoints.Name = "groupBoxLinePoints";
            this.groupBoxLinePoints.Size = new System.Drawing.Size(200, 252);
            this.groupBoxLinePoints.TabIndex = 3;
            this.groupBoxLinePoints.TabStop = false;
            this.groupBoxLinePoints.Text = "Граничные точки линии";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Количество точек:";
            // 
            // tbNumOfGraphicPoints
            // 
            this.tbNumOfGraphicPoints.Location = new System.Drawing.Point(6, 221);
            this.tbNumOfGraphicPoints.Name = "tbNumOfGraphicPoints";
            this.tbNumOfGraphicPoints.Size = new System.Drawing.Size(182, 20);
            this.tbNumOfGraphicPoints.TabIndex = 5;
            this.tbNumOfGraphicPoints.Text = "100";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbX2);
            this.groupBox2.Controls.Add(this.tbY2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 75);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Точка 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "X:";
            // 
            // tbX2
            // 
            this.tbX2.Location = new System.Drawing.Point(29, 18);
            this.tbX2.Name = "tbX2";
            this.tbX2.ReadOnly = true;
            this.tbX2.Size = new System.Drawing.Size(156, 20);
            this.tbX2.TabIndex = 3;
            // 
            // tbY2
            // 
            this.tbY2.Location = new System.Drawing.Point(29, 41);
            this.tbY2.Name = "tbY2";
            this.tbY2.ReadOnly = true;
            this.tbY2.Size = new System.Drawing.Size(156, 20);
            this.tbY2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbX1);
            this.groupBox1.Controls.Add(this.tbY1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Точка 1";
            // 
            // tbX1
            // 
            this.tbX1.Location = new System.Drawing.Point(29, 19);
            this.tbX1.Name = "tbX1";
            this.tbX1.ReadOnly = true;
            this.tbX1.Size = new System.Drawing.Size(156, 20);
            this.tbX1.TabIndex = 5;
            // 
            // tbY1
            // 
            this.tbY1.Location = new System.Drawing.Point(29, 45);
            this.tbY1.Name = "tbY1";
            this.tbY1.ReadOnly = true;
            this.tbY1.Size = new System.Drawing.Size(156, 20);
            this.tbY1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // cbSelectGraphicLine
            // 
            this.cbSelectGraphicLine.FormattingEnabled = true;
            this.cbSelectGraphicLine.Location = new System.Drawing.Point(6, 46);
            this.cbSelectGraphicLine.Name = "cbSelectGraphicLine";
            this.cbSelectGraphicLine.Size = new System.Drawing.Size(200, 21);
            this.cbSelectGraphicLine.TabIndex = 2;
            this.cbSelectGraphicLine.SelectedIndexChanged += new System.EventHandler(this.cbSelectGraphicLine_SelectedIndexChanged);
            // 
            // butBuildGraphic
            // 
            this.butBuildGraphic.Location = new System.Drawing.Point(6, 331);
            this.butBuildGraphic.Name = "butBuildGraphic";
            this.butBuildGraphic.Size = new System.Drawing.Size(200, 23);
            this.butBuildGraphic.TabIndex = 1;
            this.butBuildGraphic.Text = "Построить график";
            this.butBuildGraphic.UseVisualStyleBackColor = true;
            this.butBuildGraphic.Click += new System.EventHandler(this.butBuildGraphic_Click);
            // 
            // cbChooseGraphicType
            // 
            this.cbChooseGraphicType.FormattingEnabled = true;
            this.cbChooseGraphicType.Location = new System.Drawing.Point(6, 19);
            this.cbChooseGraphicType.Name = "cbChooseGraphicType";
            this.cbChooseGraphicType.Size = new System.Drawing.Size(200, 21);
            this.cbChooseGraphicType.TabIndex = 0;
            this.cbChooseGraphicType.SelectedIndexChanged += new System.EventHandler(this.cbChooseGraphicType_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gbFields);
            this.groupBox3.Controls.Add(this.gbSources);
            this.groupBox3.Controls.Add(this.gbGeneralModelData);
            this.groupBox3.Location = new System.Drawing.Point(836, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(378, 716);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Данные модели";
            // 
            // gbFields
            // 
            this.gbFields.Controls.Add(this.rtbFields);
            this.gbFields.Location = new System.Drawing.Point(6, 136);
            this.gbFields.Name = "gbFields";
            this.gbFields.Size = new System.Drawing.Size(366, 350);
            this.gbFields.TabIndex = 0;
            this.gbFields.TabStop = false;
            this.gbFields.Text = "Области";
            // 
            // rtbFields
            // 
            this.rtbFields.Location = new System.Drawing.Point(3, 16);
            this.rtbFields.Name = "rtbFields";
            this.rtbFields.ReadOnly = true;
            this.rtbFields.Size = new System.Drawing.Size(357, 328);
            this.rtbFields.TabIndex = 0;
            this.rtbFields.Text = "";
            // 
            // gbSources
            // 
            this.gbSources.Controls.Add(this.rtbSources);
            this.gbSources.Location = new System.Drawing.Point(6, 495);
            this.gbSources.Name = "gbSources";
            this.gbSources.Size = new System.Drawing.Size(366, 215);
            this.gbSources.TabIndex = 0;
            this.gbSources.TabStop = false;
            this.gbSources.Text = "Источники";
            // 
            // rtbSources
            // 
            this.rtbSources.Location = new System.Drawing.Point(5, 21);
            this.rtbSources.Name = "rtbSources";
            this.rtbSources.ReadOnly = true;
            this.rtbSources.Size = new System.Drawing.Size(355, 188);
            this.rtbSources.TabIndex = 1;
            this.rtbSources.Text = "";
            // 
            // gbGeneralModelData
            // 
            this.gbGeneralModelData.Controls.Add(this.tbDepth);
            this.gbGeneralModelData.Controls.Add(this.label9);
            this.gbGeneralModelData.Controls.Add(this.tbLayerType);
            this.gbGeneralModelData.Controls.Add(this.label8);
            this.gbGeneralModelData.Controls.Add(this.tbPotencialType);
            this.gbGeneralModelData.Controls.Add(this.label7);
            this.gbGeneralModelData.Controls.Add(this.tbFieldType);
            this.gbGeneralModelData.Controls.Add(this.label6);
            this.gbGeneralModelData.Location = new System.Drawing.Point(3, 16);
            this.gbGeneralModelData.Name = "gbGeneralModelData";
            this.gbGeneralModelData.Size = new System.Drawing.Size(369, 114);
            this.gbGeneralModelData.TabIndex = 0;
            this.gbGeneralModelData.TabStop = false;
            this.gbGeneralModelData.Text = "Общие данные";
            // 
            // tbDepth
            // 
            this.tbDepth.Location = new System.Drawing.Point(104, 90);
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.ReadOnly = true;
            this.tbDepth.ShortcutsEnabled = false;
            this.tbDepth.Size = new System.Drawing.Size(259, 20);
            this.tbDepth.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Глубина модели:";
            // 
            // tbLayerType
            // 
            this.tbLayerType.Location = new System.Drawing.Point(185, 61);
            this.tbLayerType.Name = "tbLayerType";
            this.tbLayerType.ReadOnly = true;
            this.tbLayerType.ShortcutsEnabled = false;
            this.tbLayerType.Size = new System.Drawing.Size(178, 20);
            this.tbLayerType.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Тип слоя вторичных источников:";
            // 
            // tbPotencialType
            // 
            this.tbPotencialType.Location = new System.Drawing.Point(103, 37);
            this.tbPotencialType.Name = "tbPotencialType";
            this.tbPotencialType.ReadOnly = true;
            this.tbPotencialType.ShortcutsEnabled = false;
            this.tbPotencialType.Size = new System.Drawing.Size(260, 20);
            this.tbPotencialType.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Тип потенциала:";
            // 
            // tbFieldType
            // 
            this.tbFieldType.Location = new System.Drawing.Point(65, 13);
            this.tbFieldType.Name = "tbFieldType";
            this.tbFieldType.ReadOnly = true;
            this.tbFieldType.ShortcutsEnabled = false;
            this.tbFieldType.Size = new System.Drawing.Size(298, 20);
            this.tbFieldType.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Тип поля:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbCursorY);
            this.groupBox4.Controls.Add(this.tbCursorX);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(15, 640);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Курсор";
            // 
            // tbCursorY
            // 
            this.tbCursorY.Location = new System.Drawing.Point(29, 62);
            this.tbCursorY.Name = "tbCursorY";
            this.tbCursorY.Size = new System.Drawing.Size(165, 20);
            this.tbCursorY.TabIndex = 3;
            // 
            // tbCursorX
            // 
            this.tbCursorX.Location = new System.Drawing.Point(29, 31);
            this.tbCursorX.Name = "tbCursorX";
            this.tbCursorX.Size = new System.Drawing.Size(165, 20);
            this.tbCursorX.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Y: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "X: ";
            // 
            // checkBoxUseRegularization
            // 
            this.checkBoxUseRegularization.AutoSize = true;
            this.checkBoxUseRegularization.Location = new System.Drawing.Point(12, 37);
            this.checkBoxUseRegularization.Name = "checkBoxUseRegularization";
            this.checkBoxUseRegularization.Size = new System.Drawing.Size(180, 17);
            this.checkBoxUseRegularization.TabIndex = 19;
            this.checkBoxUseRegularization.Text = "Использовать регуляризацию";
            this.checkBoxUseRegularization.UseVisualStyleBackColor = true;
            // 
            // tbRegularizationParameter
            // 
            this.tbRegularizationParameter.Location = new System.Drawing.Point(76, 74);
            this.tbRegularizationParameter.Name = "tbRegularizationParameter";
            this.tbRegularizationParameter.Size = new System.Drawing.Size(134, 20);
            this.tbRegularizationParameter.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Параметр:";
            // 
            // откалиброватьПотенциалToolStripMenuItem
            // 
            this.откалиброватьПотенциалToolStripMenuItem.Name = "откалиброватьПотенциалToolStripMenuItem";
            this.откалиброватьПотенциалToolStripMenuItem.Size = new System.Drawing.Size(165, 20);
            this.откалиброватьПотенциалToolStripMenuItem.Text = "Откалибровать потенциал";
            this.откалиброватьПотенциалToolStripMenuItem.Click += new System.EventHandler(this.откалиброватьПотенциалToolStripMenuItem_Click);
            // 
            // CalculateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 761);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbRegularizationParameter);
            this.Controls.Add(this.checkBoxUseRegularization);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.butCalculate);
            this.Controls.Add(this.groupBoxGraphicsCalc);
            this.Controls.Add(this.groupBoxPowerLines);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CalculateForm";
            this.Text = "Расчёт магнитного поля";
            this.Load += new System.EventHandler(this.CalculateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxPowerLines.ResumeLayout(false);
            this.groupBoxGraphicsCalc.ResumeLayout(false);
            this.groupBoxLinePoints.ResumeLayout(false);
            this.groupBoxLinePoints.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.gbFields.ResumeLayout(false);
            this.gbSources.ResumeLayout(false);
            this.gbGeneralModelData.ResumeLayout(false);
            this.gbGeneralModelData.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butCalculate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProblemDataToolStripMenuItem;
        private System.Windows.Forms.Button butGetNodes;
        private System.Windows.Forms.Button butCalcPotencial;
        private System.Windows.Forms.ToolStripMenuItem единицыИзмеренияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размерыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem метрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сантиметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem миллиметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem глубинаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateFileGeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateMshBasedOnGeoToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxPowerLines;
        private System.Windows.Forms.ToolStripMenuItem WriteNodesAndElementsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxGraphicsCalc;
        private System.Windows.Forms.ToolStripMenuItem closeProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDirToolStripMenuItem;
        private System.Windows.Forms.Button butBuildGraphic;
        private System.Windows.Forms.ComboBox cbChooseGraphicType;
        private System.Windows.Forms.ComboBox cbSelectGraphicLine;
        private System.Windows.Forms.GroupBox groupBoxLinePoints;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbX2;
        private System.Windows.Forms.TextBox tbY2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbX1;
        private System.Windows.Forms.TextBox tbY1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNumOfGraphicPoints;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbFields;
        private System.Windows.Forms.RichTextBox rtbFields;
        private System.Windows.Forms.GroupBox gbSources;
        private System.Windows.Forms.GroupBox gbGeneralModelData;
        private System.Windows.Forms.TextBox tbDepth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbLayerType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPotencialType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFieldType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbSources;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbCursorY;
        private System.Windows.Forms.TextBox tbCursorX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxUseRegularization;
        private System.Windows.Forms.TextBox tbRegularizationParameter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem откалиброватьПотенциалToolStripMenuItem;
    }
}

