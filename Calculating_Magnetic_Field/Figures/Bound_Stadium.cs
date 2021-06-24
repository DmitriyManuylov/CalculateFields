using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calculating_Magnetic_Field.Figures
{
    public class Bound_Stadium : IFigure
    {
        public float Radius { get; set; }

        public StadiumOrientation Orientation { get; set; }

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

        Rib[] ribs = new Rib[2];

        public Bound_Stadium(PointF location, float length, float width, StadiumOrientation orientation, float Scale)
        {
            Location = new PointF(location.X * Scale, location.Y * Scale);
            Width = length * Scale;
            Height = width * Scale;
            Orientation = orientation;
            SetRadius();
            BuildRibs();
        }

        public Bound_Stadium(Bound_Stadium bound_Stadium, float Scale)
        {
            Location = new PointF(bound_Stadium.Location.X * Scale, bound_Stadium.Location.Y * Scale);
            Width = bound_Stadium.Width * Scale;
            Height = bound_Stadium.Height * Scale;
            Radius = bound_Stadium.Radius * Scale;
            Orientation = bound_Stadium.Orientation;
            BuildRibs();
        }

        public Bound_Stadium(PointF location, float lenth, float width, StadiumOrientation orientation)
        {
            Location = location;
            Width = lenth;
            Height = width;
            Orientation = orientation;
            SetRadius();
            BuildRibs();
        }

        private void BuildRibs()
        {
            PointD p1, p2;
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    {
                        p1 = new PointD(Location.X, Location.Y - Height);
                        p2 = new PointD(Location.X + Width, Location.Y - Height);
                        ribs[0] = new Rib(p1, p2);
                        p1.X = Location.X + Width;
                        p1.Y = Location.Y;
                        p2.X = Location.X;
                        p2.Y = Location.Y;
                        ribs[1] = new Rib(p1, p2);
                        break;
                    }
                case StadiumOrientation.VerticalHalfRings:
                    {
                        p1 = new PointD(Location.X, Location.Y);
                        p2 = new PointD(Location.X, Location.Y - Height);
                        ribs[0] = new Rib(p1, p2);
                        p1.X = Location.X + Width;
                        p1.Y = Location.Y - Height;
                        p2.X = Location.X + Width;
                        p2.Y = Location.Y;
                        ribs[1] = new Rib(p1, p2);
                        break;
                    }
            }
        }

        public double GetPerimeter()
        {
            double perimeter = 0;
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    perimeter = 2 * Width + Math.PI * Height;
                    break;
                case StadiumOrientation.VerticalHalfRings:
                    perimeter = 2 * Height + Math.PI * Width;
                    break;
            }
            return perimeter;
        }

        public double GetSquare()
        {
            return Width * Height + Math.PI * Radius * Radius;
        }

        public FigureType GetFigureType()
        {
            return FigureType.Stadium;
        }

        public bool IsContaisPoint(PointD point)
        {
            PointD p = point;
            PointD c = new PointD();
            bool res = false;
            /*res = ((p.X >= Location.X + eps) && (p.Y <= Location.Y - eps) && 
                   (p.X <= (Location.X + Length - eps)) && (p.Y >= (Location.Y - Width + eps)));*/
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    {
                        res = res || ((p.X >= Location.X) && (p.Y <= Location.Y - eps) &&
                               (p.X <= (Location.X + Width)) && (p.Y >= (Location.Y - Height + eps)));
                        c.X = Location.X;
                        c.Y = Location.Y - Height / 2;
                        res = res ||
                        // ***********************************************************************************
                        // левый полукруг 
                        (((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius - eps)
                        && p.X <= Location.X);
                        //************************************************************************************

                        // ***********************************************************************************
                        // правый полукруг 
                        c.X = Location.X + Width;
                        c.Y = Location.Y - Height / 2;
                        res = res ||
                        (((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius - eps)
                        && p.X >= Location.X + Width);
                        //***********************************************************************************
                        break;
                    }


                case StadiumOrientation.VerticalHalfRings:
                    {
                        res = res || ((p.X >= Location.X + eps) && (p.Y <= Location.Y) &&
                               (p.X <= (Location.X + Width - eps)) && (p.Y >= (Location.Y - Height)));
                        c.X = Location.X + Width / 2;
                        c.Y = Location.Y;
                        res = res ||
                        // ***********************************************************************************
                        // верхний полукруг 
                        (((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius - eps)
                        && p.Y >= Location.Y);
                        //***********************************************************************************

                        // ***********************************************************************************
                        // нижний полукруг 
                        c.X = Location.X + Width / 2;
                        c.Y = Location.Y - Height;
                        res = res ||
                        (((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius - eps)
                        && p.Y <= Location.Y - Height);
                        //***********************************************************************************
                        break;
                    }
            }
            return res;
        }

        private void SetRadius()
        {
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    Radius = Height / 2;
                    break;
                case StadiumOrientation.VerticalHalfRings:
                    Radius = Width / 2;
                    break;
            }
        }

        public bool IsPointOnBorder(PointD point)
        {
            PointD p = point;
            PointD c = new PointD();
            bool res = false;
            PointPosition pointPosition;
            for (int i = 0; i < 2; i++)
            {
                pointPosition = ribs[i].Classify(point);
                if (pointPosition == PointPosition.ORIGIN ||
                    pointPosition == PointPosition.DESTINATION ||
                    pointPosition == PointPosition.BETWEEN)
                    return true;
            }
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    {
                        c.X = Location.X;
                        c.Y = Location.Y - Height / 2;
                        // ***********************************************************************************
                        // левая полуокружность 
                        if ((((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius + eps)
                            && ((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) >= Radius * Radius - eps))
                        && p.X <= Location.X)
                            return true;
                        //************************************************************************************

                        // ***********************************************************************************
                        // правая полуокружность
                        c.X = Location.X + Width;
                        c.Y = Location.Y - Height / 2;

                        if ((((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius + eps)
                            && ((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) >= Radius * Radius - eps))
                        && p.X >= Location.X + Width)
                            return true;
                        //************************************************************************************
                        break;
                    }


                case StadiumOrientation.VerticalHalfRings:
                    {
                        c.X = Location.X + Width / 2;
                        c.Y = Location.Y;
                        // ***********************************************************************************
                        // верхняя полуокружность
                        if ((((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius + eps)
                            && ((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) >= Radius * Radius - eps))
                        && (p.Y >= Location.Y))
                            return true;
                        //************************************************************************************

                        // ***********************************************************************************
                        // нижняя полуокружность
                        c.X = Location.X + Width / 2;
                        c.Y = Location.Y - Height;
                        if ((((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) <= Radius * Radius + eps)
                            && ((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y) >= Radius * Radius - eps))
                        && (p.Y <= Location.Y - Height))
                            return true;
                        //************************************************************************************
                        break;
                    }
            }
            return res;
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
            string orientation = "";
            switch (Orientation)
            {
                case StadiumOrientation.HorizontalHalfRings:
                    {
                        orientation = "горизонтальная";
                        break;
                    }
                case StadiumOrientation.VerticalHalfRings:
                    {
                        orientation = "вертикальная";
                        break;
                    }
            }

            res += "Тип фигуры: стадион" + "\n";
            res += $"ЛВУ: X = {Location.X}, Y = {Location.Y}" + "\n";
            res += $"Ширина: {Height}" + "\n";
            res += $"Высота: {Height}" + "\n";
            res += $"Ориентация: {orientation}" + "\n";
            return res;
        }

        public List<Rib> SplitBorder(int n)
        {

            List<PointD> points = new List<PointD>(n);
            int n1, n2, n3, n4;
            double dl, dAlpha, Alpha = 0;
            double center_x, center_y;
            double x, y;
            double perimeter = GetPerimeter();
            double r = Radius;
            if (Orientation == StadiumOrientation.HorizontalHalfRings)
            {
                n1 = Convert.ToInt32(n * Math.PI * Radius / perimeter);
                dAlpha = Math.PI / n1;
                n2 = Convert.ToInt32(n * Width / perimeter);
                dl = Width / n2;
                n3 = n1;
                n4 = n - n1 - n2 - n3;
                center_x = Location.X;
                center_y = Location.Y - Height / 2;
                Alpha = Math.PI / 2;


                points.Add(new PointD(Location.X, Location.Y));

                for (int i = 0; i < n1; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }

                x = Location.X + dl;
                y = Location.Y - Height;
                points.Add(new PointD(x, y));

                for (int i = 0; i < n2 - 1; i++)
                {
                    x += dl;
                    points.Add(new PointD(x, y));
                }

                center_x = Location.X + Width;
                center_y = Location.Y - Height / 2;
                Alpha = Math.PI * 3d / 2;

                for (int i = 0; i < n3; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }

                x = Location.X + Width - dl;
                y = Location.Y;
                points.Add(new PointD(x, y));
                for (int i = 0; i < n4 - 2; i++)
                {
                    x -= dl;
                    points.Add(new PointD(x, y));
                }
            }

            if (Orientation == StadiumOrientation.VerticalHalfRings)
            {
                n1 = Convert.ToInt32(n * Height / perimeter);
                dl = Height / n1;
                n2 = Convert.ToInt32(n * Math.PI * Radius / perimeter);
                dAlpha = Math.PI / n2;
                n3 = n1;
                n4 = n - n1 - n2 - n3;
                center_x = Location.X + Width / 2;
                center_y = Location.Y - Height;
                Alpha = Math.PI;

                x = Location.X;
                y = Location.Y;
                points.Add(new PointD(x, y));

                for (int i = 0; i < n1; i++)
                {
                    y -= dl;
                    points.Add(new PointD(x, y));
                }

                for (int i = 0; i < n2; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }

                x = Location.X + Width;
                y = Location.Y - Height;
                for (int i = 0; i < n3; i++)
                {
                    y += dl;
                    points.Add(new PointD(x, y));
                }
                center_x = Location.X + Width / 2;
                center_y = Location.Y;
                Alpha = 0;

                for (int i = 0; i < n4 - 1; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }
            }
            List<Rib> ribs = new List<Rib>(points.Count);
            for (int i = 0; i < n - 1; i++)
            {
                ribs.Add(new Rib(points[i], points[i + 1]));
            }
            ribs.Add(new Rib(points[n - 1], points[0]));
            return ribs;
        }
    }
}
