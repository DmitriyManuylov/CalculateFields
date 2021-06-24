
namespace Calculating_Magnetic_Field.Models
{
    public interface IPotencial
    {
        int Sign { get; }

        PotencialTypes TypeOFPotencialsLayer { get; }
        double Calculate_Potencial_from_Element(PointD pointM, Rib ribN);

        Vector2D Calculate_Intensity_from_Element(PointD pointM, Rib ribN);

        Vector2D Calculate_Induction_from_Element(PointD pointM, Rib ribN);

        double KernelOfIntegral(Rib ribN, Rib ribM);

        double Integral_dAdn(Rib ribN, Rib ribM);

    }
}
