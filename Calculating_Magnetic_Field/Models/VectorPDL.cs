

namespace Calculating_Magnetic_Field.Models
{
    class VectorPDL : IPotencial
    {
        public int Sign => throw new System.NotImplementedException();

        public PotencialTypes TypeOFPotencialsLayer
        {
            get
            {
                return PotencialTypes.PDL;
            }
        }

        public Vector2D Calculate_Intensity_from_Element(PointD pointM, Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public Vector2D Calculate_Induction_from_Element(PointD pointM, Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public double Calculate_Potencial_from_Element(PointD pointM, Rib ribN)
        {
            throw new System.NotImplementedException();
        }

        public double Integral_dAdn(Rib ribN, Rib ribM)
        {
            throw new System.NotImplementedException();
        }

        public double KernelOfIntegral(Rib ribN, Rib ribM)
        {
            throw new System.NotImplementedException();
        }
    }
}
