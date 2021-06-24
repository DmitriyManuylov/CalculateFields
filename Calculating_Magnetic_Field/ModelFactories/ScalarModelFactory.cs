using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.Sources;
using Calculating_Magnetic_Field.Sources.PotencialSources;
using System;
using System.IO;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class ScalarModelFactory : ModelFactory
    {
        private PhysicalField physicalField;

        public ScalarModelFactory(PhysicalField physicalField)
        {
            this.physicalField = physicalField;
        }
        public override ILinearSource CreateLinearSource(PointD point1, PointD point2, double density, int n)
        {
            ILinearSource result;
            result = new ChargedLine(point1, point2, density, n);
            result.PhysicalField = physicalField;
            return result;
        }

        public override IModel CreateModel(double depth)
        {
            return new ScalarPotencialModel(depth, physicalField);
        }

        public override IPointSource CreatePointSource(PointD location, double SourcePower, IModel model)
        {
            IPointSource result;
            result = new ChargedThread(location, SourcePower);
            result.PhysicalField = physicalField;
            Bound bound;

            if (isPointSourceOnBound(location, model, out bound))
            {
                result.FieldProperty = (bound.Left_Property + bound.Right_Property) / 2;
            }
            else if (!model.Bounds[0].IsContaisPoint(location))
            {
                result.FieldProperty = model.Bounds[0].Right_Property;
            }
            else if (model.Bounds[0].IsContaisPoint(location))
            {
                result.FieldProperty = model.Bounds[0].Left_Property;
            }
            else
            {
                for (int i = 1; i < model.Bounds.Count; i++)
                {
                    if (model.Bounds[i].IsContaisPoint(location))
                    {
                        result.FieldProperty = model.Bounds[i].Left_Property;
                        break;
                    }
                }
            }

            if (physicalField == PhysicalField.Current)
            {
                if (model.Potencial.TypeOFPotencialsLayer == PotencialTypes.PSL)
                {
                    result.SourcePower /= result.FieldProperty;
                }
            }
            return result;
        }

        public override IResidualIntensitySource CreateResidualIntensitySource(IFigure figure, SimpleDirections direction, double Intensity, int N)
        {

            IResidualIntensitySource result = null;
            switch (figure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        result = new ConstantMagnetScalarPot(physicalField, (Bound_Rectangle)figure, direction, Intensity, N);
                        break;
                    }
            }

            return result;
        }

        public override IVolumeSource CreateVolumeSource(IFigure figure, double SourcePower, int n, int m)
        {
            throw new NotImplementedException();
            /*switch (figure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        return new 
                        break;
                    }
            }*/

        }

        private bool isPointSourceOnBound(PointD location, IModel model, out Bound owner_bound)
        {
            foreach(Bound bound in model.Bounds)
            {
                if (bound.IsPointOnBorder(location))
                {
                    owner_bound = bound;
                    double r_c;
                    double r1;
                    double r2;
                    int count = bound.Bound_Ribs.Count;
                    for (int i = 0; i < bound.Bound_Ribs.Count; i++)
                    {
                        r_c = bound.Bound_Ribs[i].GetMiddleOfRib().DistanceToOtherPoint(location);

                        if (r_c < bound.Bound_Ribs[i].LengthOfElement / 2)
                        {
                            Rib rib = bound.Bound_Ribs[i];
                            double length = rib.LengthOfElement;
                            r1 = rib.Point1.DistanceToOtherPoint(location);
                            r2 = rib.Point2.DistanceToOtherPoint(location);

                            if (r1 < length / 10)
                            {
                                bound.Bound_Ribs[(i - 1 + count) % count].Point2 = location;
                                bound.Bound_Ribs[i].Point1 = location;
                            }
                            else if (r2 < length / 10)
                            {
                                bound.Bound_Ribs[i].Point2 = location;
                                bound.Bound_Ribs[(i + 1) % count].Point1 = location;
                            }
                            else if (r2 < r1)
                            {
                                bound.Bound_Ribs.Insert((i + 1) % count, new Rib(location, rib.Point2));
                                rib.Point2 = location;
                            }
                            else
                            {
                                bound.Bound_Ribs.Insert(i, new Rib(rib.Point1, location));
                                rib.Point1 = location;
                            }

                            return true;
                        }
                    }
                    return true;
                }
            }
            owner_bound = null;
            return false;
        }
    }

}
