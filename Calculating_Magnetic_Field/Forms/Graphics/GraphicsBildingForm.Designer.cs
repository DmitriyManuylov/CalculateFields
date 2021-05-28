namespace Calculating_Magnetic_Field
{
    partial class GraphicsBildingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ZDc = new ZedGraph.ZedGraphControl();
            this.butRedraw = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.добавитьГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьРисунокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьГрафикИзElcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGraphic = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteGraphic = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZDc
            // 
            this.ZDc.Location = new System.Drawing.Point(12, 35);
            this.ZDc.Name = "ZDc";
            this.ZDc.ScrollGrace = 0D;
            this.ZDc.ScrollMaxX = 0D;
            this.ZDc.ScrollMaxY = 0D;
            this.ZDc.ScrollMaxY2 = 0D;
            this.ZDc.ScrollMinX = 0D;
            this.ZDc.ScrollMinY = 0D;
            this.ZDc.ScrollMinY2 = 0D;
            this.ZDc.Size = new System.Drawing.Size(1190, 716);
            this.ZDc.TabIndex = 0;
            this.ZDc.UseExtendedPrintDialog = true;
            // 
            // butRedraw
            // 
            this.butRedraw.Location = new System.Drawing.Point(12, 791);
            this.butRedraw.Name = "butRedraw";
            this.butRedraw.Size = new System.Drawing.Size(903, 10);
            this.butRedraw.TabIndex = 2;
            this.butRedraw.Text = "Перерисовать";
            this.butRedraw.UseVisualStyleBackColor = true;
            this.butRedraw.Click += new System.EventHandler(this.ButRedraw_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьГрафикToolStripMenuItem,
            this.сохранитьРисунокToolStripMenuItem,
            this.добавитьГрафикИзElcutToolStripMenuItem,
            this.AddGraphic,
            this.DeleteGraphic});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1214, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // добавитьГрафикToolStripMenuItem
            // 
            this.добавитьГрафикToolStripMenuItem.Name = "добавитьГрафикToolStripMenuItem";
            this.добавитьГрафикToolStripMenuItem.Size = new System.Drawing.Size(166, 20);
            this.добавитьГрафикToolStripMenuItem.Text = "Добавить график из Femm";
            this.добавитьГрафикToolStripMenuItem.Click += new System.EventHandler(this.добавитьГрафикToolStripMenuItem_Click);
            // 
            // сохранитьРисунокToolStripMenuItem
            // 
            this.сохранитьРисунокToolStripMenuItem.Name = "сохранитьРисунокToolStripMenuItem";
            this.сохранитьРисунокToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.сохранитьРисунокToolStripMenuItem.Text = "Сохранить рисунок";
            this.сохранитьРисунокToolStripMenuItem.Click += new System.EventHandler(this.сохранитьРисунокToolStripMenuItem_Click);
            // 
            // добавитьГрафикИзElcutToolStripMenuItem
            // 
            this.добавитьГрафикИзElcutToolStripMenuItem.Name = "добавитьГрафикИзElcutToolStripMenuItem";
            this.добавитьГрафикИзElcutToolStripMenuItem.Size = new System.Drawing.Size(158, 20);
            this.добавитьГрафикИзElcutToolStripMenuItem.Text = "Добавить график из Elcut";
            this.добавитьГрафикИзElcutToolStripMenuItem.Click += new System.EventHandler(this.добавитьГрафикИзElcutToolStripMenuItem_Click);
            // 
            // AddGraphic
            // 
            this.AddGraphic.Name = "AddGraphic";
            this.AddGraphic.Size = new System.Drawing.Size(141, 20);
            this.AddGraphic.Text = "Добавить график МГЭ";
            this.AddGraphic.Click += new System.EventHandler(this.AddGraphic_Click);
            // 
            // DeleteGraphic
            // 
            this.DeleteGraphic.Name = "DeleteGraphic";
            this.DeleteGraphic.Size = new System.Drawing.Size(106, 20);
            this.DeleteGraphic.Text = "Удалить график";
            this.DeleteGraphic.Click += new System.EventHandler(this.DeleteGraphic_Click);
            // 
            // GraphicsBildingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 763);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.butRedraw);
            this.Controls.Add(this.ZDc);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GraphicsBildingForm";
            this.Text = "Построитель графика";
            this.Load += new System.EventHandler(this.BuilderPotencialGraphic_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl ZDc;
        private System.Windows.Forms.Button butRedraw;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьГрафикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьРисунокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьГрафикИзElcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGraphic;
        private System.Windows.Forms.ToolStripMenuItem DeleteGraphic;
    }
}