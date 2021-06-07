using System.Drawing;
using System.Windows.Forms;
using Calculating_Magnetic_Field.Figures;

namespace Calculating_Magnetic_Field.Figure_Drawers
{
    public class StadiumBorderDrawer: IDrawable
    {
        Brush brush;
        Pen pen;
        Bound_Stadium bound_Stadium;

        /// <summary>
        /// Конструктор обертки треугольника для рисования
        /// </summary>
        /// <param name="box">Ссылка на PictureBox</param>
        /// <param name="rectangle">Прямоугольник</param>
        public StadiumBorderDrawer(PictureBox box, Bound_Stadium bound)
        {
            PBox = box;
            bound_Stadium = bound;
        }
        public PictureBox PBox { get; set; }

        public void Draw(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 2);
            if (bound_Stadium.Orientation == StadiumOrientation.VerticalHalfRings)
            {
                graphics.DrawLine(pen,
                                  new PointF(bound_Stadium.Location.X + PBox.Width / 2, -bound_Stadium.Location.Y + PBox.Height / 2),
                                  new PointF(bound_Stadium.Location.X + PBox.Width / 2, -(bound_Stadium.Location.Y - bound_Stadium.Height) + PBox.Height / 2));
                graphics.DrawLine(pen,
                                  new PointF(bound_Stadium.Location.X + bound_Stadium.Width + PBox.Width / 2, -(bound_Stadium.Location.Y - bound_Stadium.Height) + PBox.Height / 2),
                                  new PointF(bound_Stadium.Location.X + bound_Stadium.Width + PBox.Width / 2, - bound_Stadium.Location.Y + PBox.Height / 2));
                graphics.DrawArc(pen, bound_Stadium.Location.X + PBox.Width / 2, -bound_Stadium.Location.Y + bound_Stadium.Height - bound_Stadium.Radius + PBox.Height / 2, bound_Stadium.Width, bound_Stadium.Width, 0, 180);
                graphics.DrawArc(pen, bound_Stadium.Location.X + PBox.Width / 2, -bound_Stadium.Location.Y - bound_Stadium.Radius + PBox.Height / 2, bound_Stadium.Width, bound_Stadium.Width, 180, 180);
            }
            if (bound_Stadium.Orientation == StadiumOrientation.HorizontalHalfRings)
            {
                graphics.DrawLine(pen,
                                  new PointF(bound_Stadium.Location.X + PBox.Width / 2,                        -bound_Stadium.Location.Y + PBox.Height / 2),
                                  new PointF(bound_Stadium.Location.X + bound_Stadium.Width + PBox.Width / 2, -bound_Stadium.Location.Y + PBox.Height / 2));
                graphics.DrawLine(pen,
                                  new PointF(bound_Stadium.Location.X + PBox.Width / 2,                        -bound_Stadium.Location.Y + bound_Stadium.Height + PBox.Height / 2),
                                  new PointF(bound_Stadium.Location.X + bound_Stadium.Width + PBox.Width / 2, -bound_Stadium.Location.Y + bound_Stadium.Height + PBox.Height / 2));
                graphics.DrawArc(pen, bound_Stadium.Location.X - bound_Stadium.Radius + PBox.Width / 2, -bound_Stadium.Location.Y + PBox.Height / 2, bound_Stadium.Height, bound_Stadium.Height, 90, 180);
                graphics.DrawArc(pen, bound_Stadium.Location.X + bound_Stadium.Width - bound_Stadium.Radius + PBox.Width / 2, -bound_Stadium.Location.Y + PBox.Height / 2, bound_Stadium.Height, bound_Stadium.Height, 270, 180);
            }

            brush.Dispose();
            pen.Dispose();
        }
    }
}
