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

        private const float eps = 2e-8f;

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            if (pointM.X == ribN.Point1.X && pointM.Y == ribN.Point1.Y ||
                pointM.X == ribN.Point2.X && pointM.Y == ribN.Point2.Y)
                return 0;
            if (pointM.X == pointN.X && pointM.Y == pointN.Y)
                return 0;
            double result = ((pointN.X - pointM.X) * ribN.Normal.CosAlpha + (pointN.Y - pointM.Y) * ribN.Normal.CosAlpha) / r2;
            return ribN.LengthElement * result;
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

            //if (Math.Sqrt(r2) < eps) return new Vector2D { X_component = 0, Y_component = 0 };
            //PointPosition pointPosition = ribN.Classify(pointM);
            //if (pointPosition != PointPosition.LEFT && pointPosition != PointPosition.RIGHT) return new Vector2D { X_component = 0, Y_component = 0 };

            result.X_component = (2 * (pointN.X - pointM.X) * ((pointN.X - pointM.X) * cosA + (pointN.Y - pointM.Y) * cosB) - r2 * cosA) / (r2 * r2);
            result.Y_component = (2 * (pointN.Y - pointM.Y) * ((pointN.X - pointM.X) * cosA + (pointN.Y - pointM.Y) * cosB) - r2 * cosB) / (r2 * r2);
            return  ribN.LengthElement * result;
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);

            if (ribN == ribM) return 0;
            //PointPosition pointPosition = ribN.Classify(pointM);
            //if (pointPosition != PointPosition.LEFT && pointPosition != PointPosition.RIGHT) return 0;

            return ribN.LengthElement * ((pointM.X - pointN.X) * ribN.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribN.Normal.CosBeta) / r2;
        }
    }
}

