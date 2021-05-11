namespace Calculating_Magnetic_Field
{
    partial class FormGraphic
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ZDc
            // 
            this.ZDc.Location = new System.Drawing.Point(12, 12);
            this.ZDc.Name = "ZDc";
            this.ZDc.ScrollGrace = 0D;
            this.ZDc.ScrollMaxX = 0D;
            this.ZDc.ScrollMaxY = 0D;
            this.ZDc.ScrollMaxY2 = 0D;
            this.ZDc.ScrollMinX = 0D;
            this.ZDc.ScrollMinY = 0D;
            this.ZDc.ScrollMinY2 = 0D;
            this.ZDc.Size = new System.Drawing.Size(1433, 769);
            this.ZDc.TabIndex = 0;
            this.ZDc.UseExtendedPrintDialog = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 787);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1433, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Перерисовать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // FormGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 829);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ZDc);
            this.Name = "FormGraphic";
            this.Text = "Graphic";
            this.Load += new System.EventHandler(this.Graphic_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl ZDc;
        private System.Windows.Forms.Button button1;
    }
}