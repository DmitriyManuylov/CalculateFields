﻿using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.Sources;
using Calculating_Magnetic_Field.Sources.PotencialSources;
using System;

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
                }
            }
            owner_bound = null;
            return false;
        }
    }

}
