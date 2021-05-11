namespace Calculating_Magnetic_Field
{
    partial class DepthForm
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
            this.tbDepth = new System.Windows.Forms.TextBox();
            this.butSetDepth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDepth
            // 
            this.tbDepth.Location = new System.Drawing.Point(12, 33);
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(170, 20);
            this.tbDepth.TabIndex = 0;
            // 
            // butSetDepth
            // 
            this.butSetDepth.Location = new System.Drawing.Point(12, 68);
            this.butSetDepth.Name = "butSetDepth";
            this.butSetDepth.Size = new System.Drawing.Size(170, 23);
            this.butSetDepth.TabIndex = 1;
            this.butSetDepth.Text = "Установить";
            this.butSetDepth.UseVisualStyleBackColor = true;
            this.butSetDepth.Click += new System.EventHandler(this.ButSetDepth_Click);
            // 
            // DepthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 103);
            this.Controls.Add(this.butSetDepth);
            this.Controls.Add(this.tbDepth);
            this.Name = "DepthForm";
            this.Text = "Depth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDepth;
        private System.Windows.Forms.Button butSetDepth;
    }
}