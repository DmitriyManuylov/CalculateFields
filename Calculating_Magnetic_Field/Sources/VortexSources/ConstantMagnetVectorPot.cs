using Calculating_Magnetic_Field.Sources;
using MathNet.Numerics.Financial;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{

    public enum SimpleDirections
    {
        FromBottomToTop,
        FromTopToBottom,
        FromLeftToRight,
        FromRightToLeft
    }
    class ConstantMagnetVectorPot : IResidualIntensitySource, ISource
    {
        int n;

        
        double standart_field_property;

        double coerciveForce;

        double l1, l2;

        SimpleDirections direction;

        List<double> densities = new List<double>(2);

        List<Bound_Rib> bigRibsForVect = new List<Bound_Rib>(2);

        List<Bound_Rib> ribsForVect;

        IFigure thisFigure;

        Vector2D directionVector;

        public Vector2D DirectionVector 
        {
            get
            {
                return directionVector;

            } 

            set
            {
                double r = value.X_component * value.X_component + value.Y_component * value.Y_component;
                directionVector = value / r;
            }
        }

        public double ResidualIntensity
        {
            get
            {
                return coerciveForce;
            }
            set
            { 
                coerciveForce = value; 
            }
        }

        public SimpleDirections Direction
        {
            get
            { 
                return direction; 
            }

            set
            {
                direction = value;
            }
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

        public int N
        {
            get { return n; }
        }

        public ConstantMagnetVectorPot(PhysicalField physicalField, Bound_Rectangle rectangle, SimpleDirections direction, double M, int N)
        {
            this.n = N;
            this.direction = direction;
            thisFigure = rectangle;
            coerciveForce = M;
            ribsForVect = new List<Bound_Rib>(2 * n);
            densities = new List<double>(2 * n);
            this.PhysicalField = physicalField;
            
            switch (direction)
            {
                case SimpleDirections.FromBottomToTop:
                    {
                        
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y)));
                        ribsForVect.AddRange(bigRibsForVect[0].GetSubRibs(n));
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width)));
                        ribsForVect.AddRange(bigRibsForVect[1].GetSubRibs(n));
                        for (int i = 0; i < n; i++)
                            densities.Add(-standart_field_property * coerciveForce);
                        for (int i = 0; i < n; i++)
                            densities.Add(standart_field_property * coerciveForce);

                        directionVector.X_component = 0;
                        directionVector.Y_component = 1;
                        break;
                    }
                case SimpleDirections.FromTopToBottom:
                    {
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y)));
                        ribsForVect.AddRange(bigRibsForVect[0].GetSubRibs(n));
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width)));
                        ribsForVect.AddRange(bigRibsForVect[1].GetSubRibs(n));
                        /*for (int i = 0; i < n; i++)
                            densities.Add(-standart_field_property * coerciveForce);
                        for (int i = 0; i < n; i++)
                            densities.Add(standart_field_property * coerciveForce);*/
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = 0;
                        directionVector.Y_component = -1;
                        break;
                    }
                case SimpleDirections.FromLeftToRight:
                    {
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width)));
                        ribsForVect.AddRange(bigRibsForVect[0].GetSubRibs(n));
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y)));
                        ribsForVect.AddRange(bigRibsForVect[1].GetSubRibs(n));
                        /*for (int i = 0; i < n; i++)
                            densities.Add(-standart_field_property * coerciveForce);
                        for (int i = 0; i < n; i++)
                            densities.Add(standart_field_property * coerciveForce);*/
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = 1;
                        directionVector.Y_component = 0;
                        break;
                    }
                case SimpleDirections.FromRightToLeft:
                    {
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y)));
                        ribsForVect.AddRange(bigRibsForVect[0].GetSubRibs(n));
                        bigRibsForVect.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width)));
                        ribsForVect.AddRange(bigRibsForVect[1].GetSubRibs(n));
                        /*for(int i = 0; i < n; i++)
                            densities.Add(standart_field_property * coerciveForce);
                        for (int i = 0; i < n; i++)
                            densities.Add(standart_field_property * coerciveForce);*/
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = -1;
                        directionVector.Y_component = 0;
                        break;
                    }
            }
        }
        public void ChangeSourcePower(double value)
        {
            coerciveForce = value;
            densities.Clear();
            densities.Add(-standart_field_property * coerciveForce);
            densities.Add(standart_field_property * coerciveForce);
        }

        public IFigure GetFigure()
        {
            return thisFigure;
        }

        public FigureType GetFigureType()
        {
            return thisFigure.GetFigureType();
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
            for (int j = 0; j < ribsForVect.Count; j++)
            {
                rib = ribsForVect[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribsForVect[j].LengthElement;
                if (r < lenth)
                {

                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component -= lenth * densities[j] * ((p1.X - pointM.X) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((p1.Y - pointM.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component -= lenth * densities[j] * ((p2.X - pointM.X) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((p2.Y - pointM.Y) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component -= lenth * densities[j] * ((p1.X - pointM.X) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((p1.Y - pointM.Y) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component -= lenth * densities[j] * ((p2.X - pointM.X) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((p2.Y - pointM.Y) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component -= (midP.X - pointM.X) / (r * r) * densities[j] * lenth;
                result.Y_component -= (midP.Y - pointM.Y) / (r * r) * densities[j] * lenth;
            }
            result *= 1d / (2 * Math.PI);
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
            for (int j = 0; j < ribsForVect.Count; j++)
            {
                rib = ribsForVect[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribsForVect[j].LengthElement;
                if (r < lenth)
                {
                    
                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * densities[j] * ((pointM.Y - p1.Y) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((pointM.X - p1.X) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * densities[j] * ((pointM.Y - p2.Y) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((pointM.X - p2.X) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * densities[j] * ((pointM.Y - p1.Y) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((pointM.X - p1.X) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * densities[j] * ((pointM.Y - p2.Y) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((pointM.X - p2.X) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component += (pointM.Y - midP.Y) / (r * r) * densities[j] * lenth;
                result.Y_component -= (pointM.X - midP.X) / (r * r) * densities[j] * lenth;
            }
            result *= 1d  / (2 * Math.PI);
            return -result;
        }


        double Integral_Potencial(Bound_Rib rib, PointD pointM, double Density)
        {
            PointD p1, p2;
            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double dx, dy;
            double f1 = 0, f2 = 0, d_fun;
            double lenth = rib.LengthElement;
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < lenth * 0.5)
            {
                if (rib.Classify(pointM)== PointPosition.LEFT)
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

        /*double Integral_Potencial(Bound_Rib rib, PointD pointM, double Density)
        {

            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < rib.LenthElement * 1)
                return rib.LenthElement * Density * (1 - Math.Log(rib.LenthElement / 2));
            return rib.LenthElement * Density * Math.Log(1.0 / r);
        }*/

        public double GetScalarPotencialValue(PointD pointM)
        {
            double result = 0;
            for (int j = 0; j < ribsForVect.Count; j++)
                result += Integral_Potencial(ribsForVect[j], pointM, densities[j]);
            result *= 1d / (2.0 * Math.PI);
            return result;
        }
        public double GetPotencialValue(PointD pointM)
        {
            double result = 0;
                for (int j = 0; j < ribsForVect.Count; j++)
                    result += Integral_Potencial(ribsForVect[j], pointM, densities[j]);
            result *= 1d / (2.0  * Math.PI);
            return result;
        }

        public SourceTypes GetSourceType()
        {
            return SourceTypes.ResidualIntensitySource;
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
            for (int j = 0; j < ribsForVect.Count; j++)
            {
                rib = ribsForVect[j];
                midP = new PointD(rib.GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribsForVect[j].LengthElement;
                if (r < lenth)
                {

                    if (rib.Classify(pointM) == PointPosition.LEFT)
                    {
                        p1 = new PointD(pointM.X - rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X - 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y - 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * densities[j] * ((pointM.Y - p1.Y) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((pointM.X - p1.X) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * densities[j] * ((pointM.Y - p2.Y) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((pointM.X - p2.X) / (r * r));
                    }
                    if (rib.Classify(pointM) == PointPosition.RIGHT)
                    {
                        p1 = new PointD(pointM.X + rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        p2 = new PointD(pointM.X + 2 * rib.Normal.CosAlpha * bigRibsForVect[0].LengthElement * coef, pointM.Y + 2 * rib.Normal.CosBeta * bigRibsForVect[0].LengthElement * coef);
                        r = Math.Sqrt((p1.X - pointM.X) * (p1.X - pointM.X) + (p1.Y - pointM.Y) * (p1.Y - pointM.Y));
                        f1.X_component += lenth * densities[j] * ((pointM.Y - p1.Y) / (r * r));
                        f1.Y_component -= lenth * densities[j] * ((pointM.X - p1.X) / (r * r));
                        r = Math.Sqrt((p2.X - pointM.X) * (p2.X - pointM.X) + (p2.Y - pointM.Y) * (p2.Y - pointM.Y));
                        f2.X_component += lenth * densities[j] * ((pointM.Y - p2.Y) / (r * r));
                        f2.Y_component -= lenth * densities[j] * ((pointM.X - p2.X) / (r * r));
                    }
                    d_fun = f1 - f2;

                    result += f1 + 2 * d_fun;
                    continue;
                }

                result.X_component += (pointM.Y - midP.Y) / (r * r) * densities[j] * lenth;
                result.Y_component -= (pointM.X - midP.X) / (r * r) * densities[j] * lenth;
            }
            result *= 1d / (2 * Math.PI * standart_field_property);
            return -result;
        }
    }
}
