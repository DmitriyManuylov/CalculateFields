﻿using System;
using System.Collections.Generic;
using Calculating_Magnetic_Field.Figures;

namespace Calculating_Magnetic_Field
{

    public enum PointPosition
    {
        LEFT,
        RIGHT,
        BEYOND,
        BEHIND,
        BETWEEN,
        ORIGIN,
        DESTINATION
    }


    public class Rib
    {


        /// <summary>
        /// Базисная функция элемента
        /// </summary>
        public ElementBasisFunction BasisFunction;
        /// <summary>
        /// Нормаль к ребру
        /// </summary>
        public Normal Normal { get; private set; }

        public double LengthOfElement { get; set; }

        /// <summary>
        /// Наклон ребра(вертикальное, горизонтальное, наклонное)
        /// </summary>
        public Rib_Position Rib_Position { get; private set; }

        /// <summary>
        /// Первая точка ребра
        /// </summary>
        public PointD Point1 { get; set; }

        /// <summary>
        /// Вторая точка ребра
        /// </summary>
        public PointD Point2 { get; set; }

        private PointD middleOfRib;

        private double eps = 1e-8;



        public Rib(PointD point1, PointD point2)
        {
            BasisFunction = new ElementBasisFunction(point1, point2);
            this.Point1 = point1;
            this.Point2 = point2;
            middleOfRib = new PointD((Point1.X + Point2.X) / 2, (Point1.Y + Point2.Y) / 2);
            LengthOfElement =  Math.Sqrt((point2.X - point1.X) * (point2.X - point1.X) + (point2.Y - point1.Y) * (point2.Y - point1.Y));
            Normal = new Normal(point1, point2, LengthOfElement);
            Rib_Position = Rib_Position.Sloping;
            if (IsVertical()) Rib_Position = Rib_Position.Vertical;
            if (IsHorizontal()) Rib_Position = Rib_Position.Horizontal;
        }

        public PointD GetMiddleOfRib()
        {
            return middleOfRib;
        }

        public bool IsVertical()
        {
            if (Math.Abs((Point1.X - Point2.X)/LengthOfElement)< eps) return true;
            return false;
        }

        public bool IsHorizontal()
        {
            if (Math.Abs((Point1.Y - Point2.Y) / LengthOfElement) < eps) return true;
            return false;
        }

        public bool IsVerticalStrictly()
        {
            if (Point1.X == Point2.X) return true;
            return false;
        }

        public bool IsHorizontalStrictly()
        {
            if (Point1.Y == Point2.Y) return true;
            return false;
        }

        public bool IsPointOnLine(PointD point)
        {
            if (DistanceFromPointToLine(point) < eps) return true;
            return false;
        }

        public bool IsPointOnRib(PointD point)
        {
            double p = (point.X - Point2.X) / (Point1.X - Point2.X);
            if (Math.Abs(p - (point.Y - Point2.Y) / (Point1.Y - Point2.Y)) < eps && p >= 0 && p <= 1) return true;
            return false;
        }

        public double DistanceFromPointToLine(PointD pointM)
        {
            return Math.Abs((Point2.Y - Point1.Y) * pointM.X - (Point2.X - Point1.X) * pointM.Y + Point2.X * Point1.Y - Point2.Y * Point1.X) / LengthOfElement;
        }

        public PointPosition Classify(PointD point)
        {
            PointD a = new PointD(Point2.X - Point1.X, Point2.Y - Point1.Y);
            PointD b = new PointD(point.X - Point1.X, point.Y - Point1.Y);
            double D = (point.X - Point1.X) * (Point2.Y - Point1.Y) - (point.Y - Point1.Y) * (Point2.X - Point1.X);
            if (D > eps)
                return PointPosition.LEFT;
            if (D < -eps)
                return PointPosition.RIGHT;
            if ((a.X * b.X < 0.0) || (a.Y * b.Y < 0.0))
                return PointPosition.BEHIND;
            if (LengthOfElement < Point1.DistanceToOtherPoint(point))
                return PointPosition.BEYOND;
            if (Math.Abs(Point1.X - point.X) < eps && Math.Abs(Point1.Y - point.Y) < eps)
                return PointPosition.ORIGIN;
            if (Math.Abs(Point2.X - point.X) < eps && Math.Abs(Point2.Y - point.Y) < eps)
                return PointPosition.DESTINATION;
            return PointPosition.BETWEEN;
        }

        public PointPosition StrictClassify(PointD point)
        {
            PointD a = new PointD(Point2.X - Point1.X, Point2.Y - Point1.Y);
            PointD b = new PointD(point.X - Point1.X, point.Y - Point1.Y);
            double D = (point.X - Point1.X) * (Point2.Y - Point1.Y) - (point.Y - Point1.Y) * (Point2.X - Point1.X);
            if (D > 0)
                return PointPosition.LEFT;
            if (D < 0)
                return PointPosition.RIGHT;
            if ((a.X * b.X < 0.0) || (a.Y * b.Y < 0.0))
                return PointPosition.BEHIND;
            if (LengthOfElement < Point1.DistanceToOtherPoint(point))
                return PointPosition.BEYOND;
            if (Point1.X == point.X && Point1.Y == point.Y)
                return PointPosition.ORIGIN;
            if (Point2.X == point.X && Point2.Y == point.Y)
                return PointPosition.DESTINATION;
            return PointPosition.BETWEEN;
        }

        public List<Rib> GetSubRibs(int n)
        {
            List<Rib> ribs = new List<Rib>(n);
            double xx = Point1.X;
            double yy = Point1.Y;
            double dx = (Point2.X - xx) / n ;
            double dy = (Point2.Y - yy) / n;
            for (int i = 0; i < n - 1; i++)
            {
                ribs.Add(new Rib(new PointD(xx, yy), new PointD(xx + dx, yy + dy)));
                xx += dx;
                yy += dy;
            }
            ribs.Add(new Rib(new PointD(xx, yy), Point2));
            return ribs;
        }


        public LinesIntersection Intersect(Rib rib, ref double t)
        {
            PointD a = Point1;
            PointD b = Point2;
            PointD c = rib.Point1;
            PointD d = rib.Point2;
            Vector2D n = new Vector2D { X_component = Normal.CosAlpha, Y_component = Normal.CosBeta };
            double denom = Vector2D.ScalarProduct(n, new Vector2D { X_component = b.X - a.X, Y_component = b.Y - a.Y });
            if(Math.Abs(denom) < eps)
            {
                PointPosition aclass = rib.Classify(Point1);
                if ((aclass == PointPosition.LEFT) || (aclass == PointPosition.RIGHT))
                    return LinesIntersection.Parallel;
                else return LinesIntersection.Collinear;
            }
            double num = Vector2D.ScalarProduct(n, new Vector2D { X_component = a.X - c.X, Y_component = a.Y - c.Y });
            t = -num / denom;
            return LinesIntersection.Skew;
        }
       
        public LinesIntersection Cross(Rib rib, ref double t)
        {
            double s = 0;
            LinesIntersection crossType = rib.Intersect(this, ref s);
            if ((crossType == LinesIntersection.Collinear) || (crossType == LinesIntersection.Parallel))
                return crossType;
            if ((Math.Abs(s) < eps))
                return LinesIntersection.Skew_Cross;
            Intersect(rib, ref t);
            if ((t >= -eps) && (t <= 1 + eps))
                return LinesIntersection.Skew_Cross;
            else
                return LinesIntersection.Skew_No_Cross;
        }

        public double Distance(PointD point)
        {
            Rib f = new Rib(point, new PointD(point.X - Normal.CosAlpha, point.Y - Normal.CosBeta));
            double t = 0;
            f.Intersect(this, ref t);
            return t;
        }

        public PointD PointOnLine(double t)
        {
            return new PointD(Point1.X + t * (Point2.X - Point1.X), Point1.Y + t * (Point2.Y - Point1.Y));
        }
    }
}
