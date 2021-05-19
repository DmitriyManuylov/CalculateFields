
namespace Calculating_Magnetic_Field.Models
{
    public interface IPotencial
    {
        int Sign { get; }

        PotencialTypes TypeOFPotencial { get; }
        double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN);

        Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN);

        Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN);

        double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM);

    }
}
