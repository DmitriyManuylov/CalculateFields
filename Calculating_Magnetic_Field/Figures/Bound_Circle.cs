using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calculating_Magnetic_Field
{
    /// <summary>
    /// Представляет окружность
    /// </summary>
    public class Bound_Circle : IFigure
    {
        /// <summary>
        /// Центр окружности
        /// </summary>
        public PointF Location { get; set; }

        /// <summary>
        /// Радиус окружности
        /// </summary>
        public float Radius { get; set; }

        private float eps = 1e-9F;

        public Bound_Circle(PointF location, float radius, float Scale = 1)
        {
            Location = new PointF(location.X * Scale, location.Y * Scale);
            Radius = radius * Scale;
        }

        public Bound_Circle(Bound_Circle bound_Circle, float Scale = 1)
        {
            Location = new PointF(bound_Circle.Location.X * Scale, bound_Circle.Location.Y * Scale);
            Radius = bound_Circle.Radius * Scale;
        }

        public Bound_Circle(PointF location, float radius)
        {
            Location = location;
            Radius = radius;
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public double GetSquare()
        {
            return Math.PI * Radius * Radius;
        }

        public FigureType GetFigureType()
        {
            return FigureType.Circle;
        }

        public bool IsPointOnBorder(PointD point)
        {
            return ((Location.X - point.X) * (Location.X - point.X) + (Location.Y - point.Y) * (Location.Y - point.Y)) <= ((Radius * Radius) + eps)
                    &&
                   ((Location.X - point.X) * (Location.X - point.X) + (Location.Y - point.Y) * (Location.Y - point.Y)) >= ((Radius * Radius) - eps);
        }
        public bool IsContaisPoint(PointD point)
        {
            return ((Location.X - point.X) * (Location.X - point.X) + (Location.Y - point.Y) * (Location.Y - point.Y)) < (Radius * Radius) - eps;
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
            res += "Тип фигуры: окружность" + "\n";
            res += $"Центр окружности: X = {Location.X}, Y = {Location.Y}" + "\n";
            res += $"Радиус: {Radius}" + "\n";
            return res;
        }

        public List<Rib> SplitBorder(int n)
        {
            throw new NotImplementedException();
        }
    }
}
