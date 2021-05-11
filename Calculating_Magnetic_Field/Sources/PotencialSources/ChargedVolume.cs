using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources.PotencialSources
{
    class ChargedVolume : IVolumeSource, ISource
    {
        double chargeDensity;

        double standart_field_property;

        PointD[,] points;

        int n, m;

        double dx;

        double dy;


        public int N
        {
            get { return n; }
        }

        public int M
        {
            get { return m; }
        }
        public double SourcePower { get; private set; }


        public Bound_Rectangle Bound_Rectangle { get; private set; }
        public IFigure ThisFigure { get; private set; }
        public FigureType FigureType { get; set; }
        private PhysicalField physicalField;
        public PhysicalField PhysicalField
        {
            get
            {
                return physicalField;
            }
            set
            {
                switch (value)
                {
                    case PhysicalField.Electric:
                        {
                            standart_field_property = PhysicalConstants.epsilon_0;
                            break;
                        }
                    case PhysicalField.Magnetic:
                        {
                            standart_field_property = PhysicalConstants.mu_0;
                            break;
                        }
                    case PhysicalField.Current:
                        {
                            standart_field_property = PhysicalConstants.conductivity;
                            break;
                        }
                }
                physicalField = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle">Прямоугольник, ограничивающий катушку</param>
        /// <param name="charge"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        public ChargedVolume(Bound_Rectangle rectangle, double charge, int n, int m)
        {
            this.n = n;
            this.m = m;
            //coef 
            if (n % 2 == 0) n++;
            if (m % 2 == 0) m++;
            points = new PointD[n, m];
            ThisFigure = rectangle;
            FigureType = FigureType.Rectangle;

            Bound_Rectangle = rectangle;
            SourcePower = charge;
            chargeDensity = SourcePower / rectangle.GetSquare();
            double startX = rectangle.Location.X;
            double startY = rectangle.Location.Y - rectangle.Width;

            dx = rectangle.Lenth / n;
            dy = rectangle.Width / m;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    points[i, j] = new PointD(startX + i * dx + dx / 2, startY + j * dy + dy / 2);
        }


        public Vector2D GetInductionValue(PointD PointM)
        {
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;

            double midAfif = dx + dy / 2;
            double coef = midAfif * 0.0001;

            for (int i = 0; i < points.GetLength(0); i++)
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    if (PointM.DistanceToOtherPoint(points[i, j]) < 0.05 * Math.Sqrt(dx * dx + dy * dy))
                    {
                        PointD[] FalsePoints = { new PointD(PointM.X + coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y - coef), new PointD(PointM.X + coef, PointM.Y - coef) };
                        for (int k = 0; k < 4; k++)
                        {
                            result.X_component += dy * dx * ((PointM.Y - FalsePoints[k].Y) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                            result.Y_component += dy * dx * ((PointM.X - FalsePoints[k].X) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                        }
                        result /= 4;
                        continue;
                    }
                    result.X_component += dy * dx * ((PointM.Y - points[i, j].Y) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                    result.Y_component += dy * dx * ((PointM.X - points[i, j].X) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                }
            result *= 1d / (2 * Math.PI) * chargeDensity;
            return result;
        }

        public Vector2D GetGradientValue(PointD PointM)
        {
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;

            double midAfif = dx + dy / 2;
            double coef = midAfif * 0.0001;
            for (int i = 0; i < points.GetLength(1); i++)
                for (int j = 0; j < points.GetLength(0); j++)
                {
                    if (PointM.DistanceToOtherPoint(points[i, j]) < 0.00005 * Math.Sqrt(dx * dx + dy * dy))
                    {
                        PointD[] FalsePoints = { new PointD(PointM.X + coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y - coef), new PointD(PointM.X + coef, PointM.Y - coef) };
                        for (int k = 0; k < 4; k++)
                        {
                            result.X_component += dy * dx * ((PointM.X - FalsePoints[k].X) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                            result.Y_component += dy * dx * ((PointM.Y - FalsePoints[k].Y) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                        }
                        result /= 4;
                        continue;
                    }
                    result.X_component += dy * dx * ((PointM.X - points[i, j].X) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                    result.Y_component += dy * dx * ((PointM.Y - points[i, j].Y) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                }
            result *= 1d / (2 * Math.PI * standart_field_property) * chargeDensity;
            return result;
        }

        public double GetPotencialValue(PointD PointM)
        {
            double result;
            result = 0;
            for (int i = 0; i < points.GetLength(1); i++)
                for (int j = 0; j < points.GetLength(0); j++)
                {
                    if (PointM.DistanceToOtherPoint(points[i, j]) < Math.Sqrt(dx * dx + dy * dy))
                        result += dy * dx * (1 - Math.Log((dx + dy) / 4));
                    else
                        result += dy * dx * Math.Log(1.0 / Math.Sqrt((PointM.X - points[i, j].X) * (PointM.X - points[i, j].X) + (PointM.Y - points[i, j].Y) * (PointM.Y - points[i, j].Y)));
                }
            result *= 1d / (2 * Math.PI * standart_field_property) * chargeDensity;
            return result;
        }

        public void ChangeSourcePower(double current)
        {
            SourcePower = current;
            chargeDensity = SourcePower / Bound_Rectangle.GetSquare();
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.VolumeSource;
        }

        public FigureType GetFigureType()
        {
            return this.FigureType;
        }

        public IFigure GetFigure()
        {
            return ThisFigure;
        }

        public Vector2D GetIntensityValue(PointD PointM)
        {
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;

            double midAfif = dx + dy / 2;
            double coef = midAfif * 0.0001;

            for (int i = 0; i < points.GetLength(0); i++)
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    if (PointM.DistanceToOtherPoint(points[i, j]) < 0.05 * Math.Sqrt(dx * dx + dy * dy))
                    {
                        PointD[] FalsePoints = { new PointD(PointM.X + coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y + coef), new PointD(PointM.X - coef, PointM.Y - coef), new PointD(PointM.X + coef, PointM.Y - coef) };
                        for (int k = 0; k < 4; k++)
                        {
                            result.X_component += dy * dx * ((PointM.Y - FalsePoints[k].Y) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                            result.Y_component += dy * dx * ((PointM.X - FalsePoints[k].X) / ((FalsePoints[k].X - PointM.X) * (FalsePoints[k].X - PointM.X) + (FalsePoints[k].Y - PointM.Y) * (FalsePoints[k].Y - PointM.Y)));
                        }
                        result /= 4;
                        continue;
                    }
                    result.X_component += dy * dx * ((PointM.Y - points[i, j].Y) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                    result.Y_component += dy * dx * ((PointM.X - points[i, j].X) / ((points[i, j].X - PointM.X) * (points[i, j].X - PointM.X) + (points[i, j].Y - PointM.Y) * (points[i, j].Y - PointM.Y)));
                }
            result *= 1d / (2d * Math.PI * standart_field_property) * chargeDensity;
            return result;
        }
    }
}
