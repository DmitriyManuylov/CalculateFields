using Calculating_Magnetic_Field.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field.Figure_Drawers
{
    public class LineDrawer : IDrawable
    {
        Brush brush;
        Pen pen;
        Bound_Rib rib;
        PictureBox pBox;
        public LineDrawer(PictureBox pictureBox, Bound_Rib rib, float DrawingScale)
        {
            pBox = pictureBox;
            this.rib = new Bound_Rib(new PointD(rib.Point1.X * DrawingScale,
                                                rib.Point1.Y * DrawingScale),
                                     new PointD(rib.Point2.X * DrawingScale,
                                                rib.Point2.Y * DrawingScale));
        }
        public void Draw(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 2);
            graphics.DrawLine(pen, (float)rib.Point1.X + pBox.Width / 2, 
                                  -(float)rib.Point1.Y + pBox.Height / 2, 
                                   (float)rib.Point2.X + pBox.Width / 2, 
                                  -(float)rib.Point2.Y + pBox.Height / 2);
            brush.Dispose();
            pen.Dispose();
        }
    }
}
