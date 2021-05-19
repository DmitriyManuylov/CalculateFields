

namespace Calculating_Magnetic_Field.Models
{
    class VectorPDL : IPotencial
    {
        public int Sign => throw new System.NotImplementedException();

        public PotencialTypes TypeOFPotencial
        {
            get
            {
                return PotencialTypes.PDL;
            }
        }

        public Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            throw new System.NotImplementedException();
        }
    }
}
