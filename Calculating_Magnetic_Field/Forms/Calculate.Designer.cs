﻿namespace Calculating_Magnetic_Field
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
            this.addFerromagneticBut = new System.Windows.Forms.Button();
            this.addCoilButton = new System.Windows.Forms.Button();
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
            this.butCulcInduction = new System.Windows.Forms.Button();
            this.butCulcForces = new System.Windows.Forms.Button();
            this.butCalculatePower = new System.Windows.Forms.Button();
            this.groupBoxPhysicsElements = new System.Windows.Forms.GroupBox();
            this.butAddMagnet = new System.Windows.Forms.Button();
            this.groupBoxForces = new System.Windows.Forms.GroupBox();
            this.groupBoxPowerLines = new System.Windows.Forms.GroupBox();
            this.butBuildPowerLines = new System.Windows.Forms.Button();
            this.groupBoxGraphicsCalc = new System.Windows.Forms.GroupBox();
            this.butCulcNormInduction = new System.Windows.Forms.Button();
            this.butCalcPotencialForGraphic = new System.Windows.Forms.Button();
            this.cbChooseGraphicType = new System.Windows.Forms.ComboBox();
            this.butBuildGraphic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBoxPhysicsElements.SuspendLayout();
            this.groupBoxForces.SuspendLayout();
            this.groupBoxPowerLines.SuspendLayout();
            this.groupBoxGraphicsCalc.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(231, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(758, 629);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // butCalculate
            // 
            this.butCalculate.Location = new System.Drawing.Point(12, 160);
            this.butCalculate.Name = "butCalculate";
            this.butCalculate.Size = new System.Drawing.Size(204, 23);
            this.butCalculate.TabIndex = 1;
            this.butCalculate.Text = "Рассчитать плотность";
            this.butCalculate.UseVisualStyleBackColor = true;
            this.butCalculate.Click += new System.EventHandler(this.ButCalculate_Click);
            // 
            // addFerromagneticBut
            // 
            this.addFerromagneticBut.Location = new System.Drawing.Point(9, 48);
            this.addFerromagneticBut.Name = "addFerromagneticBut";
            this.addFerromagneticBut.Size = new System.Drawing.Size(206, 23);
            this.addFerromagneticBut.TabIndex = 2;
            this.addFerromagneticBut.Text = "Добавить ферромагнетик";
            this.addFerromagneticBut.UseVisualStyleBackColor = true;
            this.addFerromagneticBut.Click += new System.EventHandler(this.AddFerromagneticBut_Click);
            // 
            // addCoilButton
            // 
            this.addCoilButton.Location = new System.Drawing.Point(9, 19);
            this.addCoilButton.Name = "addCoilButton";
            this.addCoilButton.Size = new System.Drawing.Size(206, 23);
            this.addCoilButton.TabIndex = 3;
            this.addCoilButton.Text = "Добавить катушку";
            this.addCoilButton.UseVisualStyleBackColor = true;
            this.addCoilButton.Click += new System.EventHandler(this.AddCoilButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.ProblemDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
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
            // butCulcInduction
            // 
            this.butCulcInduction.Location = new System.Drawing.Point(16, 477);
            this.butCulcInduction.Name = "butCulcInduction";
            this.butCulcInduction.Size = new System.Drawing.Size(200, 34);
            this.butCulcInduction.TabIndex = 11;
            this.butCulcInduction.Text = "Рассчитать модуль индукции";
            this.butCulcInduction.UseVisualStyleBackColor = true;
            this.butCulcInduction.Click += new System.EventHandler(this.Button6_Click);
            // 
            // butCulcForces
            // 
            this.butCulcForces.Location = new System.Drawing.Point(3, 68);
            this.butCulcForces.Name = "butCulcForces";
            this.butCulcForces.Size = new System.Drawing.Size(206, 37);
            this.butCulcForces.TabIndex = 12;
            this.butCulcForces.Text = "Рассчитать силы при разных проницаемостях";
            this.butCulcForces.UseVisualStyleBackColor = true;
            this.butCulcForces.Click += new System.EventHandler(this.ButCulcForces_Click);
            // 
            // butCalculatePower
            // 
            this.butCalculatePower.Location = new System.Drawing.Point(3, 19);
            this.butCalculatePower.Name = "butCalculatePower";
            this.butCalculatePower.Size = new System.Drawing.Size(206, 38);
            this.butCalculatePower.TabIndex = 10;
            this.butCalculatePower.Text = "Рассчитать силу действия на груз";
            this.butCalculatePower.UseVisualStyleBackColor = true;
            this.butCalculatePower.Click += new System.EventHandler(this.CalculatePower_Click);
            // 
            // groupBoxPhysicsElements
            // 
            this.groupBoxPhysicsElements.Controls.Add(this.butAddMagnet);
            this.groupBoxPhysicsElements.Controls.Add(this.addCoilButton);
            this.groupBoxPhysicsElements.Controls.Add(this.addFerromagneticBut);
            this.groupBoxPhysicsElements.Location = new System.Drawing.Point(1, 41);
            this.groupBoxPhysicsElements.Name = "groupBoxPhysicsElements";
            this.groupBoxPhysicsElements.Size = new System.Drawing.Size(215, 104);
            this.groupBoxPhysicsElements.TabIndex = 14;
            this.groupBoxPhysicsElements.TabStop = false;
            this.groupBoxPhysicsElements.Text = "Добавить элементы";
            // 
            // butAddMagnet
            // 
            this.butAddMagnet.Location = new System.Drawing.Point(9, 78);
            this.butAddMagnet.Name = "butAddMagnet";
            this.butAddMagnet.Size = new System.Drawing.Size(206, 23);
            this.butAddMagnet.TabIndex = 4;
            this.butAddMagnet.Text = "Добавить магнит";
            this.butAddMagnet.UseVisualStyleBackColor = true;
            this.butAddMagnet.Click += new System.EventHandler(this.butAddMagnet_Click);
            // 
            // groupBoxForces
            // 
            this.groupBoxForces.Controls.Add(this.butCalculatePower);
            this.groupBoxForces.Controls.Add(this.butCulcForces);
            this.groupBoxForces.Location = new System.Drawing.Point(7, 517);
            this.groupBoxForces.Name = "groupBoxForces";
            this.groupBoxForces.Size = new System.Drawing.Size(209, 124);
            this.groupBoxForces.TabIndex = 15;
            this.groupBoxForces.TabStop = false;
            this.groupBoxForces.Text = "Расчёт сил";
            // 
            // groupBoxPowerLines
            // 
            this.groupBoxPowerLines.Controls.Add(this.butBuildPowerLines);
            this.groupBoxPowerLines.Controls.Add(this.butGetNodes);
            this.groupBoxPowerLines.Controls.Add(this.butCalcPotencial);
            this.groupBoxPowerLines.Location = new System.Drawing.Point(10, 189);
            this.groupBoxPowerLines.Name = "groupBoxPowerLines";
            this.groupBoxPowerLines.Size = new System.Drawing.Size(206, 125);
            this.groupBoxPowerLines.TabIndex = 16;
            this.groupBoxPowerLines.TabStop = false;
            this.groupBoxPowerLines.Text = "Расчёт картины поля";
            // 
            // butBuildPowerLines
            // 
            this.butBuildPowerLines.Location = new System.Drawing.Point(2, 96);
            this.butBuildPowerLines.Name = "butBuildPowerLines";
            this.butBuildPowerLines.Size = new System.Drawing.Size(204, 23);
            this.butBuildPowerLines.TabIndex = 12;
            this.butBuildPowerLines.Text = "Построить силовые линии";
            this.butBuildPowerLines.UseVisualStyleBackColor = true;
            this.butBuildPowerLines.Click += new System.EventHandler(this.ButBuildPowerLines_Click);
            // 
            // groupBoxGraphicsCalc
            // 
            this.groupBoxGraphicsCalc.Controls.Add(this.butBuildGraphic);
            this.groupBoxGraphicsCalc.Controls.Add(this.cbChooseGraphicType);
            this.groupBoxGraphicsCalc.Location = new System.Drawing.Point(10, 320);
            this.groupBoxGraphicsCalc.Name = "groupBoxGraphicsCalc";
            this.groupBoxGraphicsCalc.Size = new System.Drawing.Size(206, 77);
            this.groupBoxGraphicsCalc.TabIndex = 13;
            this.groupBoxGraphicsCalc.TabStop = false;
            this.groupBoxGraphicsCalc.Text = "Графики";
            // 
            // butCulcNormInduction
            // 
            this.butCulcNormInduction.Location = new System.Drawing.Point(16, 409);
            this.butCulcNormInduction.Name = "butCulcNormInduction";
            this.butCulcNormInduction.Size = new System.Drawing.Size(200, 34);
            this.butCulcNormInduction.TabIndex = 18;
            this.butCulcNormInduction.Text = "Рассчитать норм.сост. индукции";
            this.butCulcNormInduction.UseVisualStyleBackColor = true;
            this.butCulcNormInduction.Click += new System.EventHandler(this.ButCulcNormInduction_Click);
            // 
            // butCalcPotencialForGraphic
            // 
            this.butCalcPotencialForGraphic.Location = new System.Drawing.Point(16, 449);
            this.butCalcPotencialForGraphic.Name = "butCalcPotencialForGraphic";
            this.butCalcPotencialForGraphic.Size = new System.Drawing.Size(200, 23);
            this.butCalcPotencialForGraphic.TabIndex = 12;
            this.butCalcPotencialForGraphic.Text = "Рассчитать потенциал";
            this.butCalcPotencialForGraphic.UseVisualStyleBackColor = true;
            this.butCalcPotencialForGraphic.Click += new System.EventHandler(this.ButCalcPotencialForGraphic_Click);
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
            // butBuildGraphic
            // 
            this.butBuildGraphic.Location = new System.Drawing.Point(6, 48);
            this.butBuildGraphic.Name = "butBuildGraphic";
            this.butBuildGraphic.Size = new System.Drawing.Size(200, 23);
            this.butBuildGraphic.TabIndex = 1;
            this.butBuildGraphic.Text = "Построить график";
            this.butBuildGraphic.UseVisualStyleBackColor = true;
            this.butBuildGraphic.Click += new System.EventHandler(this.butBuildGraphic_Click);
            // 
            // CalculateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 653);
            this.Controls.Add(this.butCulcNormInduction);
            this.Controls.Add(this.butCalculate);
            this.Controls.Add(this.butCulcInduction);
            this.Controls.Add(this.butCalcPotencialForGraphic);
            this.Controls.Add(this.groupBoxGraphicsCalc);
            this.Controls.Add(this.groupBoxPowerLines);
            this.Controls.Add(this.groupBoxForces);
            this.Controls.Add(this.groupBoxPhysicsElements);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CalculateForm";
            this.Text = "Расчёт магнитного поля";
            this.Load += new System.EventHandler(this.CalculateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxPhysicsElements.ResumeLayout(false);
            this.groupBoxForces.ResumeLayout(false);
            this.groupBoxPowerLines.ResumeLayout(false);
            this.groupBoxGraphicsCalc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butCalculate;
        private System.Windows.Forms.Button addFerromagneticBut;
        private System.Windows.Forms.Button addCoilButton;
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
        private System.Windows.Forms.Button butCulcInduction;
        private System.Windows.Forms.Button butCulcForces;
        private System.Windows.Forms.ToolStripMenuItem CreateFileGeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateMshBasedOnGeoToolStripMenuItem;
        private System.Windows.Forms.Button butCalculatePower;
        private System.Windows.Forms.GroupBox groupBoxPhysicsElements;
        private System.Windows.Forms.GroupBox groupBoxForces;
        private System.Windows.Forms.GroupBox groupBoxPowerLines;
        private System.Windows.Forms.ToolStripMenuItem WriteNodesAndElementsToolStripMenuItem;
        private System.Windows.Forms.Button butBuildPowerLines;
        private System.Windows.Forms.GroupBox groupBoxGraphicsCalc;
        private System.Windows.Forms.Button butCalcPotencialForGraphic;
        private System.Windows.Forms.ToolStripMenuItem closeProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDirToolStripMenuItem;
        private System.Windows.Forms.Button butAddMagnet;
        private System.Windows.Forms.Button butCulcNormInduction;
        private System.Windows.Forms.Button butBuildGraphic;
        private System.Windows.Forms.ComboBox cbChooseGraphicType;
    }
}

