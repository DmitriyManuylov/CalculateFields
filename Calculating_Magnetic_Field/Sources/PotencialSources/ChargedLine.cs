using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources.PotencialSources
{
    class ChargedLine : ILinearSource, ISource
    {
        private double standart_field_property;


        private int n;
        public int N
        {
            get { return n; }
            set { n = value; }
        }
        
        private List<Bound_Rib> ribs;

        private Bound_Rib rib;
        public Bound_Rib Rib
        {
            get { return rib; }
            set { rib = value; }
        }

        private double density;
        public double Density
        {
            get { return density; }
            set { this.density = value; }
        }

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


        public ChargedLine(PointD point1, PointD point2, double density, int n)
        {
            this.n = n;
            rib = new Bound_Rib(point1, point2);
            ribs = rib.GetSubRibs(n);
            this.density = density;
        }

        public void ChangeSourcePower(double value)
        {
            throw new NotImplementedException();
        }

        public IFigure GetFigure()
        {
            throw new NotImplementedException();
        }

        public FigureType GetFigureType()
        {
            return FigureType.Line;
        }

        public Vector2D GetGradientValue(PointD pointM)
        {
            PointD p1, p2;
            Vector2D f1, f2, d_fun;
            f1.X_component = 0;
            f1.Y_component = 0;
            f2.X_component = 0;
            f2.Y_component = 0;
            Bound_Rib rib;

            double lenth;
            Vector2D result;
            double coef = 0.05;
            double coef2 = coef;
            result.X_component = 0;
            result.Y_component = 0;

            PointD midP;
            double r;
            for (int j = 0; j < ribs.Count; j++)
            {
                rib = ribs[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth)
                {

                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component += (pointM.X - midP.X) / (r * r) * density * lenth;
                result.Y_component += (pointM.Y - midP.Y) / (r * r) * density * lenth;
            }
            result *= 1d / (2 * Math.PI * standart_field_property);
            return result;
        }

        public Vector2D GetInductionValue(PointD pointM)
        {
            PointD p1, p2;
            Vector2D f1, f2, d_fun;
            f1.X_component = 0;
            f1.Y_component = 0;
            f2.X_component = 0;
            f2.Y_component = 0;
            Bound_Rib rib;

            double lenth;
            Vector2D result;
            double coef = 0.05;
            double coef2 = coef;
            result.X_component = 0;
            result.Y_component = 0;

            PointD midP;
            double r;
            for (int j = 0; j < ribs.Count; j++)
            {
                rib = ribs[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth)
                {

                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component += (pointM.X - midP.X) / (r * r) * density * lenth;
                result.Y_component += (pointM.Y - midP.Y) / (r * r) * density * lenth;
            }
            result *= 1d / (2 * Math.PI);
            return result;
        }


        double Integral_Potencial(Bound_Rib rib, PointD pointM, double Density)
        {
            PointD p1, p2;
            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double dx, dy;
            double f1 = 0, f2 = 0, d_fun;
            double lenth = rib.LenthElement;
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < lenth * 0.5)
            {
                if (rib.Classify(pointM) == PointPosition.LEFT)
                {
                    p1 = new PointD(pointM.X - rib.Normal.CosAlpha * lenth, pointM.Y - rib.Normal.CosBeta * lenth);
                    p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * lenth, pointM.Y - 2 * rib.Normal.CosBeta * lenth);
                    r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                    f1 = Math.Log(1.0 / r);
                    r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                    f2 = Math.Log(1.0 / r);
                }
                if (rib.Classify(pointM) == PointPosition.RIGHT)
                {
                    p1 = new PointD(pointM.X + rib.Normal.CosAlpha * lenth, pointM.Y + rib.Normal.CosBeta * lenth);
                    p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * lenth, pointM.Y + 2 * rib.Normal.CosBeta * lenth);
                    r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                    f1 = Math.Log(1.0 / r);
                    r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                    f2 = Math.Log(1.0 / r);
                }
                d_fun = f1 - f2;

                return lenth * Density * (f1 + 2 * d_fun);
            }
            return lenth * Density * (Math.Log(1.0 / r));
        }

        public Vector2D GetIntensityValue(PointD pointM)
        {
            PointD p1, p2;
            Vector2D f1, f2, d_fun;
            f1.X_component = 0;
            f1.Y_component = 0;
            f2.X_component = 0;
            f2.Y_component = 0;
            Bound_Rib rib;

            double lenth;
            Vector2D result;
            double coef = 0.05;
            double coef2 = coef;
            result.X_component = 0;
            result.Y_component = 0;

            PointD midP;
            double r;
            for (int j = 0; j < ribs.Count; j++)
            {
                rib = ribs[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth)
                {

                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + rib.Normal.CosBeta * rib.LenthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * rib.LenthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * rib.LenthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * density * ((pointM.X - p1.X) / (r * r));
                        f1.Y_component += lenth * density * ((pointM.Y - p1.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * density * ((pointM.X - p2.X) / (r * r));
                        f2.Y_component += lenth * density * ((pointM.Y - p2.Y) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component += (pointM.X - midP.X) / (r * r) * density * lenth;
                result.Y_component += (pointM.Y - midP.Y) / (r * r) * density * lenth;
            }
            result *= 1d / (2 * Math.PI * standart_field_property);
            return result;
        }

        public double GetPotencialValue(PointD pointM)
        {
            double result = 0;
            for (int j = 0; j < ribs.Count; j++)
                result += Integral_Potencial(ribs[j], pointM, density);
            result *= 1d / (2.0 * Math.PI * standart_field_property);
            return result;
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.LinearSource;
        }
    }
}
