using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calculating_Magnetic_Field
{
    public enum FigureType
    {
        Rectangle,
        Circle,
        Stadium,
        C_image,
        Line,
        Point
    }
    /// <summary>
    /// Содержит данные о положении и размерах прямоугольника
    /// </summary>
    public class Bound_Rectangle: IFigure
    {

        /// <summary>
        /// Левый верхний угол прямоугольника
        /// </summary>
        public PointF Location { get; set; }

        /// <summary>
        /// Размер по оси Ox
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Размер по оси Oy
        /// </summary>
        public float Height { get; set; }

        private float eps = 1e-9f;

        private Rib[] ribs = new Rib[4];

        public Bound_Rectangle(PointF location, float lenth, float width, float Scale)
        {
            Location = new PointF(location.X * Scale, location.Y * Scale);
            Width = lenth * Scale;
            Height = width * Scale;
            BuildRibs();
        }

        private void BuildRibs()
        {
            PointD p1, p2;
            p1 = new PointD(Location.X, Location.Y);
            p2 = new PointD(Location.X, Location.Y - Height);
            ribs[0] = new Rib(p1, p2);
            p1 = p2;
            p2.X = Location.X + Width;
            ribs[1] = new Rib(p1, p2);
            p1 = p2;
            p2.Y = Location.Y;
            ribs[2] = new Rib(p1, p2);
            p1 = p2;
            p2.X = Location.X;
            ribs[3] = new Rib(p1, p2);
        }

        public Bound_Rectangle(Bound_Rectangle bound_Rectangle, float Scale)
        {
            Location = new PointF(bound_Rectangle.Location.X * Scale, bound_Rectangle.Location.Y * Scale);
            Width = bound_Rectangle.Width * Scale;
            Height = bound_Rectangle.Height * Scale;
            BuildRibs();
        }

        public Bound_Rectangle(PointF location, float lenth, float width)
        {
            Location = location;
            Width = lenth;
            Height = width;
            BuildRibs();
        }

        public double GetPerimeter()
        {
            return 2 * (Width + Height);
        }

        public double GetSquare()
        {
            return Width * Height;
        }

        public FigureType GetFigureType()
        {
            return FigureType.Rectangle;
        }

        public bool IsContaisPoint(PointD point)
        {
            return (point.X >= Location.X + eps) && (point.Y <= Location.Y - eps) && (point.X <= (Location.X + Width - eps)) && (point.Y >= (Location.Y - Height + eps));
        }

        public bool IsPointOnBorder(PointD point)
        {
            PointPosition pointPosition;
            for(int i = 0; i < 4; i++)
            {
                pointPosition = ribs[i].Classify(point);
                if (pointPosition == PointPosition.ORIGIN || 
                    pointPosition == PointPosition.DESTINATION || 
                    pointPosition == PointPosition.BETWEEN)
                    return true;
            }
            return false;
        }

        public bool IsPointOnBorder(PointD point, float epsilon)
        {
            float old_eps = eps;
            eps = epsilon;
            bool result = IsPointOnBorder(point);
            eps = old_eps;
            return result;
        }

        public override string ToString()
        {
            string res = "";
            res += "Тип фигуры: прямоугольник" + "\n";
            res += $"ЛВУ: X = {Location.X}, Y = {Location.Y}" + "\n";
            res += $"Ширина: {Width}" + "\n";
            res += $"Высота: {Height}" + "\n";
            return res;
        }

        public List<Rib> SplitBorder(int n)
        {
            throw new NotImplementedException();
        }
    }
}
