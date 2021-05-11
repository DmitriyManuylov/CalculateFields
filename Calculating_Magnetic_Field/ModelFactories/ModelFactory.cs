using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public abstract class ModelFactory
    {
        public abstract IModel CreateModel(double depth);
        public abstract IVolumeSource CreateVolumeSource(IFigure figure, double SourcePower, int n, int m);

        public abstract ILinearSource CreateLinearSource(PointD point1, PointD point2, double density, int n);

        public abstract IPointSource CreatePointSource(PointD location, double SourcePower);

        public abstract IResidualIntensitySource CreateResidualIntensitySource(IFigure figure, SimpleDirections direction, double Intensity, int N);


    }
}
