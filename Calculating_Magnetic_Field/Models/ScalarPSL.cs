using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Models
{
    /// <summary>
    /// Скалярный потенциал простого слоя
    /// </summary>
    public class ScalarPSL : IPotencial
    {

        public int Sign
        {
            get
            {
                return 1;
            }

        }

        public PotencialTypes TypeOFPotencialsLayer
        {
            get
            {
                return PotencialTypes.PSL;
            }
        }

        public double Calculate_Potencial_from_Element(PointD pointM, Rib ribN)
        {
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            double length = ribN.LengthElement;

            if (pointM.X == ribN.Point1.X && pointM.Y == ribN.Point1.Y ||
                pointM.X == ribN.Point2.X && pointM.Y == ribN.Point2.Y)
                return length * (1 - Math.Log(length));
            if (pointM.X == pointN.X && pointM.Y == pointN.Y)
                return length * (1 - Math.Log(length / 2));
            return - length * Math.Log(r2) * 0.5;
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Rib ribN)
        {
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Rib ribN)
        {
            double r2;
            double lenth;
            Vector2D result;
        
            PointD pointN;
            pointN = new PointD(ribN.GetMiddleOfRib());
            r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            lenth = ribN.LengthElement;
         
            result.X_component = (pointM.X - pointN.X) / r2 * lenth;
            result.Y_component = (pointM.Y - pointN.Y) / r2 * lenth;
            return result;
        }

        public double Integral_dAdn(Rib ribN, Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);

            if (ribM == ribN) return 0;
            if (ribM.IsHorizontalStrictly() && ribN.IsHorizontalStrictly() && ribM.Point1.Y == ribN.Point1.Y) return 0;
            if (ribM.IsVerticalStrictly() && ribN.IsVerticalStrictly() && ribM.Point1.X == ribN.Point1.X) return 0;

            return ribN.LengthElement * ((pointM.X - pointN.X) * ribM.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribM.Normal.CosBeta) / r2;
        }

    }
}
