using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field.Forms
{
    public partial class AddCommentToGraphic : Form
    {
        public string Comment { get; private set; }
        public AddCommentToGraphic()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if(tbComment.Text == "")
            {
                MessageBox.Show("Введите комментарий");
                return;
            }
            DialogResult = DialogResult.OK;
            Comment = tbComment.Text;
            Close();
        }
    }
}
