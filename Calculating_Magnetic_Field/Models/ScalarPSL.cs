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

        public PotencialTypes TypeOFPotencial
        {
            get
            {
                return PotencialTypes.PSL;
            }
        }

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(MiddleOFRib);
            /*if (r < ribN.LenthElement * 1e-5)
                return ribN.LenthElement * (Math.Log(1.0 / Math.Sqrt((ribN.Point1.X - pointM.X) * (ribN.Point1.X - pointM.X) + (ribN.Point1.Y - pointM.Y) * (ribN.Point1.Y - pointM.Y))) +
                                                     Math.Log(1.0 / Math.Sqrt((ribN.Point2.X - pointM.X) * (ribN.Point2.X - pointM.X) + (ribN.Point2.Y - pointM.Y) * (ribN.Point2.Y - pointM.Y)))) / 2;*/
            return - ribN.LenthElement * Math.Log(r2) * 0.5;
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN)
        {
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN)
        {
            double r2;
            double lenth;
            Vector2D result;
        
            PointD pointN;
            pointN = new PointD(ribN.GetMiddleOfRib());
            r2 = pointM.SquareOfDistanceToOtherPoint(pointN);
            lenth = ribN.LenthElement;
         
            result.X_component = (pointM.X - pointN.X) / r2 * lenth;
            result.Y_component = (pointM.Y - pointN.Y) / r2 * lenth;
            return result;
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD pointN = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(pointN);

            if (ribM == ribN) return 0;
            PointPosition pointPosition = ribN.Classify(pointM);
            if (pointPosition != PointPosition.LEFT && pointPosition != PointPosition.RIGHT) return 0;

            return ribN.LenthElement * ((pointM.X - pointN.X) * ribM.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribM.Normal.CosBeta) / r2;
        }

    }
}
