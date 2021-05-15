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

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(MiddleOFRib);
            /*if (r < ribN.LenthElement * 1e-5)
                return ribN.LenthElement * (Math.Log(1.0 / Math.Sqrt((ribN.Point1.X - pointM.X) * (ribN.Point1.X - pointM.X) + (ribN.Point1.Y - pointM.Y) * (ribN.Point1.Y - pointM.Y))) +
                                                     Math.Log(1.0 / Math.Sqrt((ribN.Point2.X - pointM.X) * (ribN.Point2.X - pointM.X) + (ribN.Point2.Y - pointM.Y) * (ribN.Point2.Y - pointM.Y)))) / 2;*/
            return - ribN.LenthElement * Math.Log(r2) * 0.25 / Math.PI;
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
        
            PointD midP;
            midP = new PointD(ribN.GetMiddleOfRib());
            r2 = pointM.SquareOfDistanceToOtherPoint(midP);
            lenth = ribN.LenthElement;
         
            result.X_component = (pointM.X - midP.X) / r2 * lenth;
            result.Y_component = (pointM.Y - midP.Y) / r2 * lenth;
            return result;
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD midP = ribN.GetMiddleOfRib();
            double r2 = pointM.SquareOfDistanceToOtherPoint(midP);
            return ribN.LenthElement * ((pointM.X - midP.X) * ribM.Normal.CosAlpha + (pointM.Y - midP.Y) * ribM.Normal.CosBeta) / r2;
        }

    }
}
