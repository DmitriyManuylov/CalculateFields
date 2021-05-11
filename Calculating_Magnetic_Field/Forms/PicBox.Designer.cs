namespace Calculating_Magnetic_Field.Forms
{
    partial class PicBox
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pb)).BeginInit();
            this.SuspendLayout();
            // 
            // Pb
            // 
            this.Pb.Location = new System.Drawing.Point(90, 42);
            this.Pb.Name = "Pb";
            this.Pb.Size = new System.Drawing.Size(480, 219);
            this.Pb.TabIndex = 0;
            this.Pb.TabStop = false;
            // 
            // PicBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Pb);
            this.Name = "PicBox";
            this.Size = new System.Drawing.Size(658, 314);
            this.Load += new System.EventHandler(this.PicBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Pb;
    }
}
