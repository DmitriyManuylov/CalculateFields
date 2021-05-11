using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field.Forms
{
    public partial class PicBox : UserControl
    {

        bool Is_Ctrl;

        public PicBox()
        {
            InitializeComponent();
        }

        private void PicBox_Load(object sender, EventArgs e)
        {
            this.KeyDown += PicBox_KeyDown;
            Pb.Left = 0;
            Pb.Top = 0;

            Pb.Location = new Point(0, 0);
            Pb.Width = this.Width;
            Pb.Height = this.Height;
        }

        private void PicBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Modifiers == Keys.Shift) ;
        }
    }
}
