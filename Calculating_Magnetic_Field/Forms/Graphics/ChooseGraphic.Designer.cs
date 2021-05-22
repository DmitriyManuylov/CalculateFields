
namespace Calculating_Magnetic_Field.Forms.Graphics
{
    partial class ChooseGraphic
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
            this.labelsList = new System.Windows.Forms.ListBox();
            this.butOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelsList
            // 
            this.labelsList.FormattingEnabled = true;
            this.labelsList.Location = new System.Drawing.Point(12, 12);
            this.labelsList.Name = "labelsList";
            this.labelsList.Size = new System.Drawing.Size(173, 199);
            this.labelsList.TabIndex = 0;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(12, 217);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(70, 23);
            this.butOK.TabIndex = 1;
            this.butOK.Text = "ОК";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChooseGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 258);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelsList);
            this.Name = "ChooseGraphic";
            this.Text = "ChooseGraphic";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox labelsList;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button button1;
    }
}