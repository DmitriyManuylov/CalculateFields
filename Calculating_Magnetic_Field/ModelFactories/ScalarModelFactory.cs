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
            if(model.Potencial.TypeOFPotencial == PotencialTypes.PDL)
            {
                return result;
            }    

            if(isPointSourceOnBound(location, model, out bound))
            {
                result.SourcePower /= (bound.Left_Mu + bound.Right_Mu) / 2;
                return result;
            }
            if (!model.Bounds[0].IsContaisPoint(location))
            {
                result.SourcePower /= model.Bounds[0].Right_Mu;
                return result;
            }
            for(int i = 1; i < model.Bounds.Count; i++)
            {
                if (model.Bounds[i].IsContaisPoint(location))
                {
                    result.SourcePower /= model.Bounds[i].Left_Mu;
                    return result;
                }
            }
            if (model.Bounds[0].IsContaisPoint(location))
            {
                result.SourcePower /= model.Bounds[0].Left_Mu;
                return result;
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
                        
                        if (r_c < bound.Bound_Ribs[i].LengthElement / 2)
                        {
                            Bound_Rib rib = bound.Bound_Ribs[i];
                            double length = rib.LengthElement;
                            r1 = rib.Point1.DistanceToOtherPoint(location);
                            r2 = rib.Point2.DistanceToOtherPoint(location);
                            using (StreamWriter writer = new StreamWriter($"C:\\Users\\Димка\\Desktop\\Анализ\\{bound.Bound_Ribs.Count}.{model.Sources.Count}.txt"))
                            {
                                writer.WriteLine($"Длина элемента: {length}");
                                writer.WriteLine($"r_c: {r_c}");
                                writer.WriteLine($"r1: {r1}");
                                writer.WriteLine($"r2: {r2}");
                            }


                            if (r1 < length / 4)
                            {
                                bound.Bound_Ribs[(i - 1 + count) % count].Point2 = location;
                                bound.Bound_Ribs[i].Point1 = location;
                            }
                            else if (r2 < length / 4)
                            {
                                bound.Bound_Ribs[i].Point2 = location;
                                bound.Bound_Ribs[(i + 1) % count].Point1 = location;
                            }
                            else
                            {
                                bound.Bound_Ribs.Insert((i + 1) % count, new Bound_Rib(location, rib.Point2));
                                rib.Point2 = location;
                            }
                            return true;
                        }
                    }
                    //for (int i = 0; i < bound.Bound_Ribs.Count; i++)
                    //{
                    //    PointPosition pointPosition = bound.Bound_Ribs[i].Classify(location);
                    //    switch (pointPosition)
                    //    {
                    //        case PointPosition.BETWEEN:
                    //            {
                    //                bound.Bound_Ribs.RemoveAt(i);
                    //                return true;
                    //            }
                    //        case PointPosition.ORIGIN:
                    //            {
                    //                if (i == 0)
                    //                {
                    //                    bound.Bound_Ribs.RemoveAt(i);
                    //                    bound.Bound_Ribs.RemoveAt(bound.Bound_Ribs.Count - 1);
                    //                }
                    //                else
                    //                {
                    //                    bound.Bound_Ribs.RemoveAt(i);
                    //                    bound.Bound_Ribs.RemoveAt(i-1);
                    //                }
                    //                return true;
                    //            }
                    //        case PointPosition.DESTINATION:
                    //            {
                    //                bound.Bound_Ribs.RemoveRange(i, 2);
                    //                return true;
                    //            }
                    //    }

                    //}
                    return true;
                }
            }
            owner_bound = null;
            return false;
        }
    }

}
