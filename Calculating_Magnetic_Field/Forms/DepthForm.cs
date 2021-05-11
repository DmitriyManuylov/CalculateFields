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
    public partial class DepthForm : Form
    {
        public DepthForm()
        {
            InitializeComponent();
        }

        private double depth;
        public double Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public bool IsCorrect()
        {
            return double.TryParse(tbDepth.Text, out depth);
        }

        private void ButSetDepth_Click(object sender, EventArgs e)
        {
            if (!IsCorrect()) MessageBox.Show("Проверьте правильность ввода", "Ошибка ввода данных!");
            else Close();
        }
    }
}
