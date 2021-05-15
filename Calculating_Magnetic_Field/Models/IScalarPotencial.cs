using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Models
{
    interface IScalarPotencial
    {
        double[] Arr_Dencities { get; set; }
        int Sign { get; }
        double Calculate_Potencial_from_Element(PointD pointM, Bound_Rib ribN);

        Vector2D Calculate_Gradient_from_Element(PointD pointM, Bound_Rib ribN);

        Vector2D Calculate_Induction_from_Element(PointD pointM, Bound_Rib ribN);

        double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM);

    }
}
