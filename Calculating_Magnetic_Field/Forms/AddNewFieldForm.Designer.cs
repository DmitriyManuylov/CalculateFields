namespace Calculating_Magnetic_Field
{
    partial class AddNewFieldForm
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
            this.selectFieldTypeCB = new System.Windows.Forms.ComboBox();
            this.fieldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.addFerrButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectFieldTypeCB
            // 
            this.selectFieldTypeCB.FormattingEnabled = true;
            this.selectFieldTypeCB.Items.AddRange(new object[] {
            "Прямоугольник",
            "Круг",
            "Сектор круга"});
            this.selectFieldTypeCB.Location = new System.Drawing.Point(12, 32);
            this.selectFieldTypeCB.Name = "selectFieldTypeCB";
            this.selectFieldTypeCB.Size = new System.Drawing.Size(235, 21);
            this.selectFieldTypeCB.TabIndex = 0;
            this.selectFieldTypeCB.SelectedIndexChanged += new System.EventHandler(this.SelectFieldTypeCB_SelectedIndexChanged);
            // 
            // fieldInfoGroupBox
            // 
            this.fieldInfoGroupBox.AutoSize = true;
            this.fieldInfoGroupBox.Location = new System.Drawing.Point(12, 83);
            this.fieldInfoGroupBox.Name = "fieldInfoGroupBox";
            this.fieldInfoGroupBox.Size = new System.Drawing.Size(235, 99);
            this.fieldInfoGroupBox.TabIndex = 1;
            this.fieldInfoGroupBox.TabStop = false;
            this.fieldInfoGroupBox.Text = "Параметры";
            // 
            // addFerrButton
            // 
            this.addFerrButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addFerrButton.Location = new System.Drawing.Point(0, 251);
            this.addFerrButton.Name = "addFerrButton";
            this.addFerrButton.Size = new System.Drawing.Size(318, 41);
            this.addFerrButton.TabIndex = 2;
            this.addFerrButton.Text = "Добавить ферромагнетик";
            this.addFerrButton.UseVisualStyleBackColor = true;
            this.addFerrButton.Click += new System.EventHandler(this.AddFerrButton_Click);
            // 
            // AddNewFieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(318, 292);
            this.Controls.Add(this.addFerrButton);
            this.Controls.Add(this.fieldInfoGroupBox);
            this.Controls.Add(this.selectFieldTypeCB);
            this.Name = "AddNewFieldForm";
            this.Text = "Добавление ферромагнетика";
            this.Load += new System.EventHandler(this.AddNewField_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectFieldTypeCB;
        private System.Windows.Forms.GroupBox fieldInfoGroupBox;
        private System.Windows.Forms.Button addFerrButton;
    }
}