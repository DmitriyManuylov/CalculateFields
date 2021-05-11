namespace Calculating_Magnetic_Field.Forms
{
    partial class AddMagnetForm
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
            this.addMagnetButton = new System.Windows.Forms.Button();
            this.selectFieldTypeCB = new System.Windows.Forms.ComboBox();
            this.fieldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // addMagnetButton
            // 
            this.addMagnetButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addMagnetButton.Location = new System.Drawing.Point(0, 364);
            this.addMagnetButton.Name = "addMagnetButton";
            this.addMagnetButton.Size = new System.Drawing.Size(405, 41);
            this.addMagnetButton.TabIndex = 11;
            this.addMagnetButton.Text = "Добавить магнит";
            this.addMagnetButton.UseVisualStyleBackColor = true;
            this.addMagnetButton.Click += new System.EventHandler(this.addMagnetButton_Click);
            // 
            // selectFieldTypeCB
            // 
            this.selectFieldTypeCB.FormattingEnabled = true;
            this.selectFieldTypeCB.Items.AddRange(new object[] {
            "Прямоугольник"});
            this.selectFieldTypeCB.Location = new System.Drawing.Point(12, 12);
            this.selectFieldTypeCB.Name = "selectFieldTypeCB";
            this.selectFieldTypeCB.Size = new System.Drawing.Size(235, 21);
            this.selectFieldTypeCB.TabIndex = 10;
            this.selectFieldTypeCB.SelectedIndexChanged += new System.EventHandler(this.SelectFieldTypeCB_SelectedIndexChanged);
            // 
            // fieldInfoGroupBox
            // 
            this.fieldInfoGroupBox.AutoSize = true;
            this.fieldInfoGroupBox.Location = new System.Drawing.Point(12, 57);
            this.fieldInfoGroupBox.Name = "fieldInfoGroupBox";
            this.fieldInfoGroupBox.Size = new System.Drawing.Size(235, 99);
            this.fieldInfoGroupBox.TabIndex = 9;
            this.fieldInfoGroupBox.TabStop = false;
            this.fieldInfoGroupBox.Text = "Параметры";
            // 
            // AddMagnetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 405);
            this.Controls.Add(this.addMagnetButton);
            this.Controls.Add(this.selectFieldTypeCB);
            this.Controls.Add(this.fieldInfoGroupBox);
            this.Name = "AddMagnetForm";
            this.Text = "AddMagnetForm";
            this.Load += new System.EventHandler(this.AddMagnetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addMagnetButton;
        private System.Windows.Forms.ComboBox selectFieldTypeCB;
        private System.Windows.Forms.GroupBox fieldInfoGroupBox;
    }
}