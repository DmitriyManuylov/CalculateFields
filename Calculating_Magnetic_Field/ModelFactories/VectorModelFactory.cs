using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class VectorModelFactory : ModelFactory
    {
        private PhysicalField physicalField;
        public VectorModelFactory(PhysicalField physicalField) 
        {
            this.physicalField = physicalField;
        }
        public override ILinearSource CreateLinearSource(PointD point1, PointD point2, double density, int n)
        {
            ILinearSource result;
            result = new LineCurrent(point1, point2, density, n);
            result.PhysicalField = physicalField;
            return result;
        }

        public override IModel CreateModel(double depth)
        {
            return new VectorPotencialModel(depth, physicalField);
        }

        public override IPointSource CreatePointSource(PointD location, double SourcePower)
        {
            IPointSource result;
            result = new ThreadCurrent(location, SourcePower);
            result.PhysicalField = physicalField;
            return result;
        }

        public override IResidualIntensitySource CreateResidualIntensitySource(IFigure figure, SimpleDirections direction, double Intensity, int N)
        {
            IResidualIntensitySource result = null;
            switch (figure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        result = new ConstantMagnetVectorPot(physicalField, (Bound_Rectangle)figure, direction, Intensity, N);
                        break;
                    }
            }

            return result;
        }

        public override IVolumeSource CreateVolumeSource(IFigure figure, double SourcePower, int n, int m)
        {
            IVolumeSource result = null;
            switch (figure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        result = new Coil((Bound_Rectangle)figure, SourcePower, n, m);
                        result.PhysicalField = physicalField;
                        break;
                    }
            }
            return result;
        }
    }
}
