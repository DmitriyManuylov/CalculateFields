using System.Drawing;
using System.Windows.Forms;
using Calculating_Magnetic_Field.Figures;

namespace Calculating_Magnetic_Field.Figure_Drawers
{
    class CircleObjectDrawer: IDrawable
    {
        Brush brush;
        Pen pen;
        Bound_Circle Circle { get; set; }

        /// <summary>
        /// Конструктор обертки окружности для рисования
        /// </summary>
        /// <param name="box">Ссылка на PictureBox</param>
        /// <param name="circle">Окружность</param>
        public CircleObjectDrawer(PictureBox box, Bound_Circle circle)
        {
            PBox = box;
            Circle = circle;
            
        }
        public PictureBox PBox { get; set; }
        public void Draw(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 2);
            RectangleF rect = new RectangleF(new PointF(Circle.Location.X - Circle.Radius + PBox.Width / 2, -Circle.Location.Y - Circle.Radius + PBox.Height / 2),
                                                  new SizeF(2 * Circle.Radius, 2 * Circle.Radius));
            graphics.DrawEllipse(pen, rect);
            brush.Dispose();
            pen.Dispose();
        }
    }
}
