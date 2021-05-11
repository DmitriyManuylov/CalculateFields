using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field
{
    public partial class AddCoilForm : Form
    {
        public AddCoilForm()
        {
            InitializeComponent();
        }

        public double Current { get; set; }

        public Bound_Rectangle Rectangle { get; set; }

        /// <summary>
        /// Число делений по оси Oy
        /// </summary>
        public int M { get; set; }

        /// <summary>
        /// Число делений по оси Ox
        /// </summary>
        public int N { get; set; }

        private bool CheckData()
        {
            float R_x, R_y, lenth, width;
            double Curr;
            int m, n;
            if (!float.TryParse(tbLenth.Text, out lenth)) return false;
            if (!float.TryParse(tbWidth.Text, out width)) return false;
            if (!float.TryParse(tbCoordinate_X.Text, out R_x)) return false;
            if (!float.TryParse(tbCoordinate_Y.Text, out R_y)) return false;
            if (!double.TryParse(tbCurrent.Text, out Curr)) return false;
            if (!int.TryParse(tbN_x.Text, out n)) return false;
            if (!int.TryParse(tbN_y.Text, out m)) return false;


            Rectangle = new Bound_Rectangle(new PointF(R_x, R_y), lenth, width);
            Current = Curr;
            N = n;
            M = m;

            return true;
        }

        private void SelectFieldTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fieldInfoGroupBox.Controls.Clear();
            Size labelSize = new Size(130, 20);
            Size tbSize = new Size(50, 20);
            switch (selectFieldTypeCB.SelectedIndex)
            {
                case 0:
                    {
                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 20), labelSize, "Размер по оси Ox"));
                        FormTextBox(new Point(140, 20), tbSize, "0", 0, ref tbLenth);
                        fieldInfoGroupBox.Controls.Add(tbLenth);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 45), labelSize, "Размер по оси Oy"));
                        FormTextBox(new Point(140, 45), tbSize, "0", 1, ref tbWidth);
                        fieldInfoGroupBox.Controls.Add(tbWidth);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 70), labelSize, "Координаты л.в.у."));
                        FormTextBox(new Point(140, 70), tbSize, "0", 2, ref tbCoordinate_X);
                        FormTextBox(new Point(200, 70), tbSize, "0", 3, ref tbCoordinate_Y);
                        fieldInfoGroupBox.Controls.Add(tbCoordinate_X);
                        fieldInfoGroupBox.Controls.Add(tbCoordinate_Y);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 95), labelSize, "Ток I ="));
                        FormTextBox(new Point(140, 95), tbSize, "0", 4, ref tbCurrent);
                        fieldInfoGroupBox.Controls.Add(tbCurrent);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 120), labelSize, "Делений по Ox"));
                        FormTextBox(new Point(140, 120), tbSize, "0", 5, ref tbN_x);
                        fieldInfoGroupBox.Controls.Add(tbN_x);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 145), labelSize, "Делений по Oy"));
                        FormTextBox(new Point(140, 145), tbSize, "0", 6, ref tbN_y);
                        fieldInfoGroupBox.Controls.Add(tbN_y);
                        break;
                    }
            }
        }
        private Label AddLabel(Point location, Size size, string text)
        {
            Label creatingLabel = new Label();
            creatingLabel.Location = location;
            creatingLabel.Size = size;
            creatingLabel.Text = text;
            return creatingLabel;
        }

        private void FormTextBox(Point location, Size size, string text, int tabIndex, ref TextBox tb)
        {
            TextBox creatingTB = new TextBox();
            creatingTB.Location = location;
            creatingTB.Size = size;
            creatingTB.Text = text;
            creatingTB.TabIndex = tabIndex;
            tb = creatingTB;
        }
        private TextBox tbLenth;
        private TextBox tbWidth;
        private TextBox tbCoordinate_X;
        private TextBox tbCoordinate_Y;
        private TextBox tbCurrent;
        private TextBox tbN_x;
        private TextBox tbN_y;

        private void AddCoilForm_Load(object sender, EventArgs e)
        {
            selectFieldTypeCB.SelectedIndex = 0;
        }

        private void AddCoilButton_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных", "Ошибка!");
            }
        }
    }
}
