using System.Collections.Generic;
using Calculating_Magnetic_Field.ModelFactories;

namespace Calculating_Magnetic_Field.Models
{
    public interface IModel
    {
        PhysicalField PhysicalField { get; set; } 
        double Depth { get; set; }
        List<Bound> Bounds { get; }

        List<ISource> Sources { get; }

        IPotencialFactoryMethod PotencialFactoryMethod { get; set; }

        IPotencial Potencial { get; set; }

        void AddSource(ISource source);
        void AddBorderOfEnvironments(IFigure figure, int n, double right_invironment_property, double left_invironment_property);

        TypeOfPotencial GetPotencialType();

        Vector2D CalculateInduction(PointD pointM);

        Vector2D CalculateIntensity(PointD pointM);

        double CalculatePotencial(PointD pointM);

        double GetFieldPropertyInPoint(PointD point);

        void SolveProblem();

        void SolveProblemWithRegularization(double parameter);
    }
}
