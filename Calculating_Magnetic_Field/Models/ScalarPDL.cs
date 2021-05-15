using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Models
{
    /// <summary>
    /// Скалярный потенциал двойного слоя
    /// </summary>
    class ScalarPDL : IScalarPotencial
    {
        private double[] arr_dencities;

        public double[] Arr_Dencities
        {
            get
            {
                return arr_dencities;
            }
            set
            {
                arr_dencities = value;
            }
        }

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
            return ribN.LenthElement * result * 0.5 / Math.PI;
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            throw new NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN)
        {
            PointD midP = ribN.GetMiddleOfRib();

            throw new NotImplementedException();
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            return ribN.LenthElement * ((pointM.X - MiddleOFRib.X) * ribN.Normal.CosAlpha + (pointM.Y - MiddleOFRib.Y) * ribN.Normal.CosBeta) /
               ((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) + (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
        }
    }
}

