using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Models
{
    public enum PotencialTypes
    {
        Scalar,
        Vector
    }
    public interface IModel
    {
        PhysicalField PhysicalField { get; set; } 
        double Depth { get; set; }
        List<Bound> Bounds { get; }

        List<ISource> Sources { get; }

        void AddSource(ISource source);
        void AddBorderOfEnvironments(IFigure figure, int n, double right_invironment_property, double left_invironment_property);
        PotencialTypes GetPotencialType();

        Vector2D CalculateInduction(PointD pointM);

        Vector2D CalculateIntensity(PointD pointM);

        double CalculatePotencial(PointD pointM);

        void SolveProblem();
    }
}
