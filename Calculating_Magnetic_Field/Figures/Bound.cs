using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculating_Magnetic_Field.Figures;

namespace Calculating_Magnetic_Field
{
    public class Bound : IFigure
    {
        public event EventHandler FindedInternalField;

        public double BoundLenth { get; private set; }


        public IFigure ThisFigure { get; private set; }

        public FigureType FigureType { get; private set; }

        List<PointD> points;

        private Bound_Rectangle rectangle;

        private Bound_Rectangle rectangleEx;

        private Bound_Rectangle rectangleIm;

        public Bound ExternalBound { get; set; }

        public List<Bound> InnerBounds { get; set; }

        private Bound_Circle circle;

        public PointD[] MiddlePoints { get; set; }


        public List<PointD> Points
        {
            get
            {
                return points;
            }
            private set
            {
                points = value;
            }
        }

        public List<Rib> Bound_Ribs { get; set; }

        public double Right_Property { get; set; }

        public double Left_Property { get; set; }

        public Bound(Bound_Rectangle rectangle, int n, double right_mu, double left_mu)
        {
            BoundLenth = rectangle.GetPerimeter();
            Right_Property = right_mu;
            Left_Property = left_mu;

            ThisFigure = rectangle;
            FigureType = FigureType.Rectangle;

            SplitOnPoints(rectangle, n, out points);
            Bound_Ribs = new List<Rib>(n);
            for (int i = 0; i < n - 1; i++)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i + 1]));
            }
            Bound_Ribs.Add(new Rib(points[n - 1], points[0]));
            MiddlePoints = new PointD[n];
            for (int i = 0; i < n; i++)
                MiddlePoints[i] = Bound_Ribs[i].GetMiddleOfRib();

        }

        public Bound(Bound_Rectangle rectangleEx, Bound_Rectangle rectangleIm, int n, double right_mu, double left_mu)
        {
            BoundLenth = 2 * (rectangleEx.Width + rectangleEx.Height + rectangleIm.Height);
            Right_Property = right_mu;
            Left_Property = left_mu;
            this.rectangleEx = rectangleEx;
            this.rectangleIm = rectangleIm;
            circle = null;
            rectangle = null;
            SplitOnPoints(rectangleEx, rectangleIm, n, out points);
            Bound_Ribs = new List<Rib>(n);
            for (int i = 0; i < n - 1; i++)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i + 1]));
            }
            Bound_Ribs.Add(new Rib(points[n - 1], points[0]));
            MiddlePoints = new PointD[n];
            for (int i = 0; i < n; i++)
                MiddlePoints[i] = Bound_Ribs[i].GetMiddleOfRib();

        }

        public Bound(Bound_Circle circle, int n, double right_mu, double left_mu)
        {
            BoundLenth = circle.GetPerimeter();
            Right_Property = right_mu;
            Left_Property = left_mu;

            ThisFigure = circle;
            FigureType = FigureType.Circle;

            rectangleEx = null;
            rectangleIm = null;
            SplitOnPoints(circle, n, out points);
            Bound_Ribs = new List<Rib>(points.Count);
            for (int i = 0; i < n - 1; i++)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i + 1]));
            }
            Bound_Ribs.Add(new Rib(points[n - 1], points[0]));
        }

        public Bound(Bound_Stadium bound_Stadium, int n, double right_mu, double left_mu)
        {
            BoundLenth = bound_Stadium.GetPerimeter();
            Right_Property = right_mu;
            Left_Property = left_mu;

            ThisFigure = bound_Stadium;
            FigureType = FigureType.Stadium;

            rectangleEx = null;
            rectangleIm = null;
            SplitOnPoints(bound_Stadium, n, out points);
            Bound_Ribs = new List<Rib>(points.Count);
            for (int i = 0; i < n - 1; i++)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i + 1]));
            }
            Bound_Ribs.Add(new Rib(points[n - 1], points[0]));
        }


        public Bound(Bound_Frame bound_Frame, int n, double right_mu, double left_mu)
        {
            int num_of_out_points, num_of_in_points;
            num_of_in_points = n / 2;
            num_of_out_points = n - num_of_in_points;
            BoundLenth = circle.GetPerimeter();
            Right_Property = right_mu;
            Left_Property = left_mu;
            this.circle = null;
            rectangle = null;
            rectangleEx = null;
            rectangleIm = null;

            SplitOnPoints(bound_Frame, n, out points);
            Bound_Ribs = new List<Rib>(points.Count);
            for (int i = 0; i < num_of_out_points - 1; i++)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i + 1]));
            }
            Bound_Ribs.Add(new Rib(points[num_of_out_points - 1], points[0]));
            for (int i = n - 1; i > num_of_out_points; i--)
            {
                Bound_Ribs.Add(new Rib(points[i], points[i - 1]));
            }
            Bound_Ribs.Add(new Rib(points[num_of_out_points], points[n - 1]));
        }

        public void RaiseEvent()
        {
            if (FindedInternalField != null)
            {
                EventArgs e = new EventArgs();
                FindedInternalField(this, e);
            }
        }

        public void SplitOnPoints(Bound_Frame bound_Frame, int n, out List<PointD> points)
        {
            int num_of_out_points, num_of_in_points;
            num_of_in_points = n / 2;
            num_of_out_points = n - num_of_in_points;
            points = new List<PointD>(n);
            List<PointD> outer_points = new List<PointD>(num_of_out_points);
            List<PointD> inner_points = new List<PointD>(num_of_in_points);
            SplitOnPoints(bound_Frame.Outer_Rectangle, num_of_out_points, out outer_points);
            SplitOnPoints(bound_Frame.Outer_Rectangle, num_of_out_points, out outer_points);
            for (int i = 0; i < num_of_out_points; i++)
                points[i] = outer_points[i];
            for (int i = num_of_out_points; i < n; i++)
                points[i] = inner_points[i - num_of_out_points];
        }


        public void SplitOnPoints(Bound_Rectangle rectangle, int n, out List<PointD> points)
        {
            points = new List<PointD>(n);
            int n1, n2, n3, n4;

            double perimeter = 2 * (rectangle.Width + rectangle.Height);
            n1 = Convert.ToInt32(n * rectangle.Height / perimeter);
            n2 = Convert.ToInt32(n * rectangle.Width / perimeter);
            n3 = n1;
            n4 = n - n1 - n2 - n3;

            double dl1, dl2, dl3, dl4;
            dl1 = rectangle.Height / n1;
            dl2 = rectangle.Width / n2;
            dl3 = rectangle.Height / n3;
            dl4 = rectangle.Width / n4;
            double x, y;
            x = rectangle.Location.X;
            y = rectangle.Location.Y;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n1; i++)
            {
                y -= dl1;
                points.Add(new PointD(x, y));
            }
            x += dl2;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n2 - 1; i++)
            {
                x += dl2;
                points.Add(new PointD(x, y));
            }
            y += dl3;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n3 - 1; i++)
            {
                y += dl3;
                points.Add(new PointD(x, y));
            }
            x -= dl4;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n4 - 2; i++)
            {
                x -= dl4;
                points.Add(new PointD(x, y));
            }
        }

        public void SplitOnPoints(Bound_Stadium bound_stadium, int n, out List<PointD> points)
        {
            points = new List<PointD>(n);
            int n1, n2, n3, n4;
            double dl, dAlpha, Alpha = 0;
            double center_x, center_y;
            double x, y;
            double perimeter = bound_stadium.GetPerimeter();
            double r = bound_stadium.Radius;
            if (bound_stadium.Orientation == StadiumOrientation.HorizontalHalfRings)
            {
                n1 = Convert.ToInt32(n * Math.PI * bound_stadium.Radius / perimeter);
                dAlpha = Math.PI / n1;
                n2 = Convert.ToInt32(n * bound_stadium.Width / perimeter);
                dl = bound_stadium.Width / n2;
                n3 = n1;
                n4 = n - n1 - n2 - n3;
                center_x = bound_stadium.Location.X;
                center_y = bound_stadium.Location.Y - bound_stadium.Height / 2;
                Alpha = Math.PI / 2;


                points.Add(new PointD(bound_stadium.Location.X, bound_stadium.Location.Y));

                for (int i = 0; i < n1; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }

                x = bound_stadium.Location.X + dl;
                y = bound_stadium.Location.Y - bound_stadium.Height;
                points.Add(new PointD(x, y));

                for (int i = 0; i < n2 - 1; i++)
                {
                    x += dl;
                    points.Add(new PointD(x, y));
                }

                center_x = bound_stadium.Location.X + bound_stadium.Width;
                center_y = bound_stadium.Location.Y - bound_stadium.Height / 2;
                Alpha = Math.PI * 3d / 2;

                for (int i = 0; i < n3; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }

                x = bound_stadium.Location.X + bound_stadium.Width - dl;
                y = bound_stadium.Location.Y;
                points.Add(new PointD(x, y));
                for (int i = 0; i < n4 - 2; i++)
                {
                    x -= dl;
                    points.Add(new PointD(x, y));
                }
            }

            if (bound_stadium.Orientation == StadiumOrientation.VerticalHalfRings)
            {
                n1 = Convert.ToInt32(n * bound_stadium.Height / perimeter);
                dl = bound_stadium.Height / n1;
                n2 = Convert.ToInt32(n * Math.PI * bound_stadium.Radius / perimeter);
                dAlpha = Math.PI / n2;
                n3 = n1;
                n4 = n - n1 - n2 - n3;
                center_x = bound_stadium.Location.X + bound_stadium.Width / 2;
                center_y = bound_stadium.Location.Y - bound_stadium.Height;
                Alpha = Math.PI;

                x = bound_stadium.Location.X;
                y = bound_stadium.Location.Y;
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

                x = bound_stadium.Location.X + bound_stadium.Width;
                y = bound_stadium.Location.Y - bound_stadium.Height;
                for (int i = 0; i < n3; i++)
                {
                    y += dl;
                    points.Add(new PointD(x, y));
                }
                center_x = bound_stadium.Location.X + bound_stadium.Width / 2;
                center_y = bound_stadium.Location.Y;
                Alpha = 0;

                for (int i = 0; i < n4 - 1; i++)
                {
                    Alpha += dAlpha;
                    points.Add(new PointD(center_x + r * Math.Cos(Alpha), center_y + r * Math.Sin(Alpha)));
                }
            }
        }

        /// <summary>
        /// П-образная форма
        /// </summary>
        /// <param name="rectangleEx">Внешний прямоугольник</param>
        /// <param name="rectangleIm">Внутреннийпрямоугольник</param>
        /// <param name="n"></param>
        /// <param name="points"></param>
        public void SplitOnPoints(Bound_Rectangle rectangleEx, Bound_Rectangle rectangleIm, int n, out List<PointD> points)
        {
            points = new List<PointD>(n);
            int n1, n2, n3, n4, n5, n6, n7, n8;

            double perimeter = 2 * (rectangleEx.Width + rectangleEx.Height + rectangleIm.Height);
            n1 = Convert.ToInt32(n * rectangleEx.Height / perimeter);
            n2 = Convert.ToInt32(n * (rectangleEx.Width - rectangleIm.Width) / 2);
            n3 = Convert.ToInt32(n * rectangleIm.Height / perimeter);

            n4 = Convert.ToInt32(n * rectangleIm.Width / perimeter);
            n5 = n3;
            n6 = n2;
            n7 = n1;
            n8 = n - n1 - n2 - n3 - n4 - n5 - n6 - n7;

            double dl1, dl2, dl3, dl4, dl5, dl6, dl7, dl8;
            dl1 = rectangleEx.Height / n1;
            dl2 = (rectangleEx.Width - rectangleIm.Width) / (2 * n2);
            dl3 = rectangleIm.Height / n3;
            dl4 = rectangleIm.Width / n4;
            dl5 = dl3;
            dl6 = dl2;
            dl7 = dl1;
            dl8 = rectangleEx.Width / n8;
            double x, y;
            x = rectangleEx.Location.X;
            y = rectangleEx.Location.Y;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n1; i++)
            {
                y -= dl1;
                points.Add(new PointD(x, y));
            }
            x += dl2;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n2 - 1; i++)
            {
                x += dl2;
                points.Add(new PointD(x, y));
            }
            y += dl3;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n3 - 1; i++)
            {
                y += dl3;
                points.Add(new PointD(x, y));
            }
            x += dl4;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n4 - 1; i++)
            {
                x += dl4;
                points.Add(new PointD(x, y));
            }
            y -= dl5;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n5 - 1; i++)
            {
                y -= dl5;
                points.Add(new PointD(x, y));
            }
            x += dl6;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n6 - 1; i++)
            {
                x += dl6;
                points.Add(new PointD(x, y));
            }
            y += dl7;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n7 - 1; i++)
            {
                y += dl7;
                points.Add(new PointD(x, y));
            }
            x -= dl8;
            points.Add(new PointD(x, y));
            for (int i = 0; i < n8 - 2; i++)
            {
                x -= dl8;
                points.Add(new PointD(x, y));
            }
        }


        public void SplitOnPoints(Bound_Circle circle, int n, out List<PointD> points)
        {
            points = new List<PointD>(n);
            double x = circle.Location.X;
            double y = circle.Location.Y;
            double dAlpha = 2 * Math.PI / n;
            double Alpha = 0;
            double r = circle.Radius;
            for (int i = 0; i < n; i++)
            {
                points.Add(new PointD(x + r * Math.Cos(Alpha), y + r * Math.Sin(Alpha)));
                Alpha += dAlpha;
            }
        }

        public bool FieldContainsPoint(PointD point)
        {
            switch (ThisFigure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        rectangle = (Bound_Rectangle)ThisFigure;
                        if (point.X > rectangle.Location.X && point.X < (rectangle.Location.X + rectangle.Width) && point.Y < rectangle.Location.Y && (point.Y > rectangle.Location.Y - rectangle.Height))
                            return true;
                        break;
                    }
                case FigureType.Circle:
                    {
                        if (((point.X - circle.Location.X) * (point.X - circle.Location.X) + (point.Y - circle.Location.Y) * (point.Y - circle.Location.Y)) < circle.Radius * circle.Radius)
                            return true;
                        break;
                    }
                case FigureType.C_image:
                    {
                        if ((point.X > rectangleEx.Location.X && point.X < (rectangleEx.Location.X + rectangleEx.Width) && point.Y < rectangleEx.Location.Y && (point.Y > rectangleEx.Location.Y - rectangleEx.Height))
                            && (point.X < rectangleIm.Location.X && point.X > (rectangleIm.Location.X + rectangleIm.Width) && point.Y > rectangleIm.Location.Y && (point.Y < rectangleIm.Location.Y - rectangleEx.Height)))
                            return true;
                        break;
                    }
            }
            return false;
        }

        public double GetPerimeter()
        {
            throw new NotImplementedException();
        }

        public double GetSquare()
        {
            throw new NotImplementedException();
        }

        public bool IsContaisPoint(PointD point)
        {
            return ThisFigure.IsContaisPoint(point);
        }

        public FigureType GetFigureType()
        {
            return this.FigureType;
        }

        public void Check_is_Contains_Field(Bound bound)
        {

        }


        public bool IsPointOnBorder(PointD point)
        {
            return ThisFigure.IsPointOnBorder(point);
        }

        public bool IsPointOnBorder(PointD point, float epsilon)
        {
            return ThisFigure.IsPointOnBorder(point, epsilon);
        }

        public override string ToString()
        {
            string result = ThisFigure.ToString();
            result += $"Свойства среды:" + "\n";
            result += $"Слева — {Left_Property}" + "\n";
            result += $"Справа — {Right_Property}" + "\n";
            return result;
        }
    }
}
