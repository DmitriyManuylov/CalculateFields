using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{

    public interface ISource
    {

        PhysicalField PhysicalField { get; set; }
        Vector2D GetInductionValue(PointD PointM);

        Vector2D GetIntensityValue(PointD PointM);

        Vector2D GetGradientValue(PointD PointM);

        double GetPotencialValue(PointD PointM);

        void ChangeSourcePower(double value);

        IFigure GetFigure();

        FigureType GetFigureType();

        SourceTypes GetSourceType();

    }
}
