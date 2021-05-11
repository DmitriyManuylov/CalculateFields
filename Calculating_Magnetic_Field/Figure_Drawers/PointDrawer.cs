using Calculating_Magnetic_Field.Figures;
using System.Windows.Forms;
using System.Drawing;


namespace Calculating_Magnetic_Field.Figure_Drawers
{
    public class PointDrawer : IDrawable
    {
        Brush brush;
        Pen pen;
        PointD location;
        PictureBox pBox;
        public PointDrawer(PictureBox pictureBox, PointD location, float DrawingScale)
        {
            pBox = pictureBox;
            this.location = new PointD(location.X * DrawingScale,
                                       location.Y * DrawingScale);
        }
        public void Draw(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 2);
            graphics.DrawRectangle(pen, (float)location.X - 2.5f + pBox.Width / 2,
                                       -(float)location.Y - 2.5f + pBox.Height / 2,
                                        5f, 
                                        5f);
            brush.Dispose();
            pen.Dispose();
        }
    }
}
