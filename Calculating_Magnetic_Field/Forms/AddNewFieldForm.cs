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
    public partial class AddNewFieldForm : Form
    {
        public AddNewFieldForm()
        {
            InitializeComponent();
        }

        private void AddNewField_Load(object sender, EventArgs e)
        {
            selectFieldTypeCB.SelectedIndex = 0;
            //Circle = new Bound_Circle();
        }

        public int SelectedFigure { get; private set; }

        public Bound_Rectangle Rectangle { get; set; }

        public Bound_Circle Circle { get; set; }

        public double MagneticPermittivity { get; set; }

        public int PointsNumber { get; set; }

        private bool CheckData(int cb_ind)
        {
            double value0;
            switch (cb_ind)
            {
                case 0:
                    {
                        float value1, value2, value3, value4;
                        if (!float.TryParse(tbLenth.Text, out value1)) return false;
                        if (!float.TryParse(tbWidth.Text, out value2)) return false;
                        if (!float.TryParse(tbCoordinate_X.Text, out value3)) return false;
                        if (!float.TryParse(tbCoordinate_Y.Text, out value4)) return false;
                        Rectangle = new Bound_Rectangle(new PointF(value3, value4), value1, value2);
                        break;
                    }
                case 1:
                    {
                        float value1, value2, value3;
                        if (!float.TryParse(tbRadius.Text, out value1)) return false;
                        if (!float.TryParse(tbCenter_X.Text, out value2)) return false;
                        if (!float.TryParse(tbCenter_Y.Text, out value3)) return false;
                        Circle = new Bound_Circle(new PointF(value2, value3), value1);
                        break;
                    }
            }
            if (!int.TryParse(tbPointsNumber.Text, out int n)) return false;
            if (!double.TryParse(tbMagneticPermittivity.Text, out value0)) return false;
            PointsNumber = n;
            MagneticPermittivity = value0;

            return true;
        }

        private void SelectFieldTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*tbLenth.Text = "0";
            tbWidth.Text = "0";
            tbCoordinate_X.Text = "0";
            tbCoordinate_Y.Text = "0";
            tbCenter_X.Text = "0";
            tbCenter_Y.Text = "0";
            tbRadius.Text = "0";
            tbMagneticPermittivity.Text = "0";*/
            fieldInfoGroupBox.Controls.Clear();
            Size labelSize = new Size(230, 20);
            Size tbSize = new Size(50, 20);
            switch(selectFieldTypeCB.SelectedIndex)
            {
                case 0:
                    {
                        this.Controls.Add(fieldInfoGroupBox);
                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 20), labelSize, "Размер по оси Ox"));
                        FormTextBox(new Point(240, 20), tbSize, "0", 0, ref tbLenth);
                        fieldInfoGroupBox.Controls.Add(tbLenth);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 45), labelSize, "Размер по оси Oy"));
                        FormTextBox(new Point(240, 45), tbSize, "0", 1, ref tbWidth);
                        fieldInfoGroupBox.Controls.Add(tbWidth);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 70), labelSize, "Координаты л.в.у."));
                        FormTextBox(new Point(240, 70), tbSize, "0", 2, ref tbCoordinate_X);
                        FormTextBox(new Point(300, 70), tbSize, "0", 3, ref tbCoordinate_Y);
                        fieldInfoGroupBox.Controls.Add(tbCoordinate_X);
                        fieldInfoGroupBox.Controls.Add(tbCoordinate_Y);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 95), labelSize, "Количество точек границы"));
                        FormTextBox(new Point(240, 95), tbSize, "0", 4, ref tbPointsNumber);
                        fieldInfoGroupBox.Controls.Add(tbPointsNumber);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 120), labelSize, "Относительная магнитная проницаемость"));
                        FormTextBox(new Point(240, 120), tbSize, "0", 4, ref tbMagneticPermittivity);
                        fieldInfoGroupBox.Controls.Add(tbMagneticPermittivity);
                        SelectedFigure = 0;
                        break;

                    }
                case 1:
                    {
                        this.Controls.Add(fieldInfoGroupBox);
                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 20), labelSize, "Радиус"));
                        FormTextBox(new Point(240, 20), tbSize, "0", 0, ref tbRadius);
                        fieldInfoGroupBox.Controls.Add(tbRadius);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 45), labelSize, "Координата центра"));
                        FormTextBox(new Point(240, 45), tbSize, "0", 1, ref tbCenter_X);
                        FormTextBox(new Point(300, 45), tbSize, "0", 2, ref tbCenter_Y);
                        fieldInfoGroupBox.Controls.Add(tbCenter_X);
                        fieldInfoGroupBox.Controls.Add(tbCenter_Y);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 70), labelSize, "Количество точек границы"));
                        FormTextBox(new Point(240, 70), tbSize, "0", 4, ref tbPointsNumber);
                        fieldInfoGroupBox.Controls.Add(tbPointsNumber);

                        fieldInfoGroupBox.Controls.Add(AddLabel(new Point(5, 95), labelSize, "Относительная магнитная проницаемость"));
                        FormTextBox(new Point(240, 95), tbSize, "0", 3, ref tbMagneticPermittivity);
                        fieldInfoGroupBox.Controls.Add(tbMagneticPermittivity);
                        SelectedFigure = 1;
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
        #region 
        // Текстовые поля
        private TextBox tbLenth;
        private TextBox tbWidth;
        private TextBox tbCoordinate_X;
        private TextBox tbCoordinate_Y;
        private TextBox tbRadius;
        private TextBox tbCenter_X;
        private TextBox tbCenter_Y;
        private TextBox tbPointsNumber;
        private TextBox tbMagneticPermittivity;
        #endregion

        private void AddFerrButton_Click(object sender, EventArgs e)
        {
            if (CheckData(selectFieldTypeCB.SelectedIndex))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных", "Ошибка!");
            }
        }
    }
}
