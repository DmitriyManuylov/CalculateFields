namespace Calculating_Magnetic_Field
{
    partial class AddCoilForm
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
            this.fieldInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.addCoilButton = new System.Windows.Forms.Button();
            this.selectFieldTypeCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // fieldInfoGroupBox
            // 
            this.fieldInfoGroupBox.AutoSize = true;
            this.fieldInfoGroupBox.Location = new System.Drawing.Point(12, 69);
            this.fieldInfoGroupBox.Name = "fieldInfoGroupBox";
            this.fieldInfoGroupBox.Size = new System.Drawing.Size(235, 99);
            this.fieldInfoGroupBox.TabIndex = 6;
            this.fieldInfoGroupBox.TabStop = false;
            this.fieldInfoGroupBox.Text = "Параметры";
            // 
            // addCoilButton
            // 
            this.addCoilButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCoilButton.Location = new System.Drawing.Point(0, 293);
            this.addCoilButton.Name = "addCoilButton";
            this.addCoilButton.Size = new System.Drawing.Size(414, 41);
            this.addCoilButton.TabIndex = 8;
            this.addCoilButton.Text = "Добавить катушку";
            this.addCoilButton.UseVisualStyleBackColor = true;
            this.addCoilButton.Click += new System.EventHandler(this.AddCoilButton_Click);
            // 
            // selectFieldTypeCB
            // 
            this.selectFieldTypeCB.FormattingEnabled = true;
            this.selectFieldTypeCB.Items.AddRange(new object[] {
            "Прямоугольник"});
            this.selectFieldTypeCB.Location = new System.Drawing.Point(12, 24);
            this.selectFieldTypeCB.Name = "selectFieldTypeCB";
            this.selectFieldTypeCB.Size = new System.Drawing.Size(235, 21);
            this.selectFieldTypeCB.TabIndex = 7;
            this.selectFieldTypeCB.SelectedIndexChanged += new System.EventHandler(this.SelectFieldTypeCB_SelectedIndexChanged);
            // 
            // AddCoilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(414, 334);
            this.Controls.Add(this.addCoilButton);
            this.Controls.Add(this.selectFieldTypeCB);
            this.Controls.Add(this.fieldInfoGroupBox);
            this.Name = "AddCoilForm";
            this.Text = "Добавление катушки";
            this.Load += new System.EventHandler(this.AddCoilForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox fieldInfoGroupBox;
        private System.Windows.Forms.Button addCoilButton;
        private System.Windows.Forms.ComboBox selectFieldTypeCB;
    }
}