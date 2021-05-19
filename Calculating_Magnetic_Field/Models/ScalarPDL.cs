using System;

namespace Calculating_Magnetic_Field.Models
{
    /// <summary>
    /// Скалярный потенциал двойного слоя
    /// </summary>
    public class ScalarPDL : IPotencial
    {

        public int Sign
        {
            get
            {
                return -1;
            }
        }

        public PotencialTypes TypeOFPotencial
        {
            get
            {
                return PotencialTypes.PDL;
            }
        }

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            double result = ((pointN.X - pointM.X) * ribN.Normal.CosAlpha + (pointN.Y - pointM.Y) * ribN.Normal.CosAlpha) / r2;
            return ribN.LenthElement * result;
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN)
        {
            Vector2D result;
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            
            double cosA = ribN.Normal.CosAlpha;
            double cosB = ribN.Normal.CosBeta;

            result.X_component = (2 * (pointN.X - pointM.X) * ((pointN.X - pointM.X) * cosA + (pointN.Y - pointM.Y) * cosB) - r2 * cosA) / (r2 * r2);
            result.Y_component = (2 * (pointN.Y - pointM.Y) * ((pointN.X - pointM.X) * cosA + (pointN.Y - pointM.Y) * cosB) - r2 * cosB) / (r2 * r2);
            return  ribN.LenthElement * result;
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            return ribN.LenthElement * ((pointM.X - pointN.X) * ribN.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribN.Normal.CosBeta) / r2;
        }
    }
}

