using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources
{
    class LineCurrent : ILinearSource, ISource
    {
        private double standart_field_property;

        private int n;
        public int N
        {
            get { return n; }
            set { n = value; }
        }
        
        private List<Rib> ribs;

        private Rib rib;
        public Rib Rib
        {
            get { return rib; }
            set { rib = value; }
        }

        private double density;
        public double Density
        {
            get { return density; }
            set { density = value; }
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

        public LineCurrent(PointD point1, PointD point2, double density, int n)
        {
            this.n = n;
            rib = new Rib(point1, point2);
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
            Rib rib;

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
                lenth = ribs[j].LengthOfElement;
                

                result.X_component -= (midP.X - pointM.X) / (r * r) * density * lenth;
                result.Y_component -= (midP.Y - pointM.Y) / (r * r) * density * lenth;
            }
            result *= standart_field_property / (2 * Math.PI);
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
            Rib rib;

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
                lenth = ribs[j].LengthOfElement;
                

                result.X_component += (pointM.Y - midP.Y) / (r * r) * density * lenth;
                result.Y_component -= (pointM.X - midP.X) / (r * r) * density * lenth;
            }
            result *= standart_field_property / (2 * Math.PI);
            return -result;
        }

        double Integral_Potencial(Rib rib, PointD pointM, double Density)
        {
            PointD p1, p2;
            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double f1 = 0, f2 = 0, d_fun;
            double lenth = rib.LengthOfElement;
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
           
            return lenth * Density * (Math.Log(1.0 / r));
        }
        public double GetPotencialValue(PointD pointM)
        {
            double result = 0;
            for (int j = 0; j < ribs.Count; j++)
                result += Integral_Potencial(ribs[j], pointM, density);
            result *= standart_field_property / (2.0 * Math.PI);
            return result;
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.LinearSource;
        }

        public Vector2D GetIntensityValue(PointD pointM)
        {
            PointD p1, p2;
            Vector2D f1, f2;

            Rib rib;

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
                lenth = ribs[j].LengthOfElement;
                

                result.X_component += (pointM.Y - midP.Y) / (r * r) * density * lenth;
                result.Y_component -= (pointM.X - midP.X) / (r * r) * density * lenth;
            }
            result *= 1d / (2 * Math.PI);
            return -result;
        }
    }
}
