using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Models
{
    public interface IIntegral
    {
        double Integrate(Rib rib, PointD pointM, Func<PointD, PointD, double> func);

        double Integrate(Rib ribN, Rib ribM, Func<Rib, Rib, double> func);

        Vector2D Integrate(Rib rib, PointD pointM, Func<PointD, PointD, Vector2D> func);

        Vector2D Integrate(Rib ribN, Rib ribM, Func<Rib, Rib, Vector2D> func);
    }
}
