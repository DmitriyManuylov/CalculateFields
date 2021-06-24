using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources.PotencialSources
{
    public class ChargedThread : IPointSource, ISource
    {
        private double standart_field_property;

        private PointD location;

        private double charge;

        public double SourcePower
        {
            get { return charge; }
            set { charge = value; }
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

        private double fieldProperty;
        public double FieldProperty
        {
            get
            {
                return fieldProperty;
            }
            set
            {
                fieldProperty = value;
            }
        }

        public ChargedThread(PointD location, double charge)
        {
            this.location = location;

            this.charge = charge;
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
            return FigureType.Point;
        }

        public Vector2D GetGradientValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = (pointM.X - location.X) / (r * r);
            result.Y_component = (pointM.Y - location.Y) / (r * r);
            result *= 1d / (2 * Math.PI * standart_field_property) * charge;
            return result;
        }

        public Vector2D GetInductionValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = (pointM.X - location.X) / (r * r);
            result.Y_component = (pointM.Y - location.Y) / (r * r);
            result *= 1d / (2 * Math.PI) * charge;
            return result;
        }

        public double GetPotencialValue(PointD PointM)
        {
            return -0.5 * charge / (2 * Math.PI * standart_field_property) * Math.Log((location.X - PointM.X) * (location.X - PointM.X) + (location.Y - PointM.Y) * (location.Y - PointM.Y));
        }

        public Vector2D GetIntensityValue(PointD pointM)
        {
            Vector2D result;
            double r = Math.Sqrt((location.X - pointM.X) * (location.X - pointM.X) + (location.Y - pointM.Y) * (location.Y - pointM.Y));
            result.X_component = (pointM.X - location.X) / (r * r);
            result.Y_component = (pointM.Y - location.Y) / (r * r);
            result *= 1d / (2 * Math.PI * standart_field_property) * charge;
            return result;
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.PointSource;
        }

        public override string ToString()
        {
            string power = "";
            switch (physicalField)
            {
                case PhysicalField.Current:
                    {
                        power = "A/m";
                        break;
                    }
            }
            string result = "";
            result += $"Тип источника: точечный" + "\n";
            result += $"Позиция: X = {location.X}, Y = {location.Y}" + "\n";
            result += $"Мощность: {SourcePower} " + power + "\n";
            return result;
        }
    }
}
