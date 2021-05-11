using System.Drawing;
using System.Windows.Forms;
using Calculating_Magnetic_Field.Figures;

namespace Calculating_Magnetic_Field.Figure_Drawers
{

    class RectangleObjectDrawer: IDrawable
    {
        Brush brush;
        Pen pen;
        RectangleF rect;

        /// <summary>
        /// Конструктор обертки треугольника для рисования
        /// </summary>
        /// <param name="box">Ссылка на PictureBox</param>
        /// <param name="rectangle">Прямоугольник</param>
        public RectangleObjectDrawer(PictureBox box, Bound_Rectangle rectangle)
        {
            PBox = box;
            rect = new RectangleF(rectangle.Location, new SizeF(rectangle.Lenth, rectangle.Width));
        }
        public PictureBox PBox { get; set; }

        public void Draw(Graphics graphics)
        {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 2);
            graphics.DrawRectangle(pen, rect.Location.X + PBox.Width / 2, -rect.Location.Y + PBox.Height / 2, rect.Width, rect.Height);
            brush.Dispose();
            pen.Dispose();
        }
    }

}
