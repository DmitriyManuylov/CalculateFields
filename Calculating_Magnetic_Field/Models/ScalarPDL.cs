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

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD midP = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(midP);
            double result = ((pointM.X - midP.X) * ribN.Normal.CosAlpha + (pointM.X - midP.X) * ribN.Normal.CosAlpha) / r2;
            return 0.5 / Math.PI * ribN.LenthElement * result;
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN)
        {
            Vector2D result;
            PointD midP = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(midP);
            
            double cosA = ribN.Normal.CosAlpha;
            double cosB = ribN.Normal.CosBeta;

            result.X_component = (r2 * cosA - 2 * (pointM.X - midP.X) * ((pointM.X - midP.X) * cosA + (pointM.X - midP.X) * cosB)) / (r2 * r2);
            result.Y_component = (r2 * cosB - 2 * (pointM.Y - midP.Y) * ((pointM.X - midP.X) * cosA + (pointM.X - midP.X) * cosB)) / (r2 * r2);
            return  0.5 / Math.PI * ribN.LenthElement * result;
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD midP = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(midP);
            return ribN.LenthElement * ((pointM.X - midP.X) * ribN.Normal.CosAlpha + (pointM.Y - midP.Y) * ribN.Normal.CosBeta) / r2;
        }
    }
}

