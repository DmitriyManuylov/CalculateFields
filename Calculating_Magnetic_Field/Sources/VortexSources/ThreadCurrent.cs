using Calculating_Magnetic_Field.Sources;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{
    public class ThreadCurrent : IPointSource, ISource
    {
        private double standart_field_property;

        private PointD location;

        private double current;

        public double SourcePower
        {
            get { return current; }
            set { current = value; }
        }

        public PointD Location
        {
            get { return location; }
            set { location = value; }
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

        public ThreadCurrent(PointD location, double current)
        {
            this.location = location;

            this.current = current;
        }

        public void ChangeSourcePower(double value)
        {
            current = value;
        }

        public Vector2D GetGradientValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = -(location.X - pointM.X) / (r * r);
            result.Y_component = -(location.Y - pointM.Y) / (r * r);
            result *= standart_field_property / (2 * Math.PI) * current;
            return result;
        }

        public Vector2D GetInductionValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = (pointM.Y - location.Y) / (r * r);
            result.Y_component = -(pointM.X - location.X) / (r * r);
            result *= standart_field_property / (2 * Math.PI) * current;
            return -result;
        }

        public double GetPotencialValue(PointD PointM)
        {
            return -0.25 * standart_field_property * current / Math.PI * Math.Log((location.X - PointM.X) * (location.X - PointM.X) + (location.Y - PointM.Y) * (location.Y - PointM.Y));
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.PointSource;
        }

        public FigureType GetFigureType()
        {
            return FigureType.Point;
        }

        public IFigure GetFigure()
        {
            throw new NotImplementedException();
        }

        public Vector2D GetIntensityValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = (pointM.Y - location.Y) / (r * r);
            result.Y_component = -(pointM.X - location.X) / (r * r);
            result *= 1d / (2 * Math.PI) * current;
            return -result;
        }
    }
}
