using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculating_Magnetic_Field;

namespace Calculating_Magnetic_Field.Forms
{
    public partial class AddMagnetForm : Form
    {
        public double coerciveForce { get; set; }

        public Bound_Rectangle Rectangle { get; set; }

        /// <summary>
        /// Число делений по оси Oy
        /// </summary>
        public int M { get; set; }

        /// <summary>
        /// Число делений по оси Ox
        /// </summary>
        public int N { get; set; }

        public SimpleDirections Directions { get; set; }

        public AddMagnetForm()
        {
            InitializeComponent();
        }


        private bool CheckData()
        {
            float R_x, R_y, lenth, width;
            double Curr;
            if (!float.TryParse(tbLenth.Text, out lenth)) return false;
            if (!float.TryParse(tbWidth.Text, out width)) return false;
            if (!float.TryParse(tbCoordinate_X.Text, out R_x)) return false;
            if (!float.TryParse(tbCoordinate_Y.Text, out R_y)) return false;
            if (!double.TryParse(tbCoerciveForce.Text, out Curr)) return false;
            


            Rectangle = new Bound_Rectangle(new PointF(R_x, R_y), lenth, width);
            coerciveForce = Curr;
            Directions = (SimpleDirections)cbDirections.SelectedItem;

            return true;
        }
        private void SelectFieldTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            fieldInfoGroupBox.Controls.Clear();
            Size labelSize = new Size(130, 20);
            Size tbSize = new Size(50, 20);
            Size cbSize = new Size(150, 20);
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

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 95), labelSize, "M ="));
                        FormTextBox(new Point(140, 95), tbSize, "0", 4, ref tbCoerciveForce);
                        fieldInfoGroupBox.Controls.Add(tbCoerciveForce);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 120), labelSize, "Направление"));
                        fieldInfoGroupBox.Controls.Add(AddCB(new Point(140, 120), cbSize, 5));
                        cbDirections.SelectedIndex = 0;
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

        private ComboBox AddCB(Point location, Size size, int tabIndex)
        {
            cbDirections = new ComboBox();
            cbDirections.Location = location;
            cbDirections.Size = size;
            cbDirections.TabIndex = tabIndex;
            cbDirections.Items.Add(SimpleDirections.FromBottomToTop);
            cbDirections.Items.Add(SimpleDirections.FromTopToBottom);
            cbDirections.Items.Add(SimpleDirections.FromLeftToRight);
            cbDirections.Items.Add(SimpleDirections.FromRightToLeft);
            return cbDirections;
        }


        private TextBox tbLenth;
        private TextBox tbWidth;
        private TextBox tbCoordinate_X;
        private TextBox tbCoordinate_Y;
        private TextBox tbCoerciveForce;
        private ComboBox cbDirections;

        private void addMagnetButton_Click(object sender, EventArgs e)
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

        private void AddMagnetForm_Load(object sender, EventArgs e)
        {
            selectFieldTypeCB.SelectedIndex = 0;
        }
    }
}
