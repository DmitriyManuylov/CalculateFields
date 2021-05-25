using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field.Forms.Graphics
{
    public partial class SelectGraphic : Form
    {
        public string Label { get; private set; }
        public SelectGraphic(List<string> labels)
        {
            InitializeComponent();
            labelsList.Items.AddRange(labels.ToArray());
        }


        private void butOK_Click(object sender, EventArgs e)
        {
            Label = labelsList.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
