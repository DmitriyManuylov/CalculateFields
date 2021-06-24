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

        public PotencialTypes TypeOFPotencialsLayer
        {
            get
            {
                return PotencialTypes.PDL;
            }
        }

        private const float eps = 2e-8f;

        public double Calculate_Potencial_from_Element(PointD pointM, Rib ribN)
        {
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            if (pointM.X == ribN.Point1.X && pointM.Y == ribN.Point1.Y ||
                pointM.X == ribN.Point2.X && pointM.Y == ribN.Point2.Y)
                return 0;
            if (pointM.X == pointN.X && pointM.Y == pointN.Y)
                return 0;
            double result = ((pointN.X - pointM.X) * ribN.Normal.CosAlpha + (pointN.Y - pointM.Y) * ribN.Normal.CosAlpha) / r2;
            return ribN.LengthOfElement * result;
        }

        public Vector2D Calculate_Intensity_from_Element(PointD pointM, Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Rib ribN)
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
            return  ribN.LengthOfElement * result;
        }

        
        public double KernelOfIntegral(Rib ribN, Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);

            if (ribN == ribM) return 0;
            if (ribM.IsHorizontalStrictly() && ribN.IsHorizontalStrictly() && ribM.Point1.Y == ribN.Point1.Y) return 0;
            if (ribM.IsVerticalStrictly() && ribN.IsVerticalStrictly() && ribM.Point1.X == ribN.Point1.X) return 0;

            return ((pointM.X - pointN.X) * ribN.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribN.Normal.CosBeta) / r2;
        }

        public double Integral_dAdn(Rib ribN, Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);

            if (ribN == ribM) return 0;
            if (ribM.IsHorizontalStrictly() && ribN.IsHorizontalStrictly() && ribM.Point1.Y == ribN.Point1.Y) return 0;
            if (ribM.IsVerticalStrictly() && ribN.IsVerticalStrictly() && ribM.Point1.X == ribN.Point1.X) return 0;

            return ribN.LengthOfElement * ((pointM.X - pointN.X) * ribN.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribN.Normal.CosBeta) / r2;
        }
    }
}

