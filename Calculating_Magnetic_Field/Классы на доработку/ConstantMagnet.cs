using MathNet.Numerics.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Расчёт_магнитного_поля
{

    public enum SimpleDirections
    {
        FromBottomToTop,
        FromTopToBottom,
        FromLeftToRight,
        FromRightToLeft
    }
    class ConstantMagnet : ISource
    {
        int n;

        const double mu_0 = 4.0 * Math.PI / (10 * 10 * 10 * 10 * 10 * 10 * 10);

        double coerciveForce;

        SimpleDirections direction;

        List<double> densities;

        List<Bound_Rib> bigRibs = new List<Bound_Rib>(2);

        List<Bound_Rib> ribs;

        IFigure thisFigure;

        Vector2D directionVector;

        Vector2D DirectionVector 
        {
            get
            {
                return directionVector;

            } 

            set
            {
                double r = value.X_component * value.X_component + value.Y_component * value.Y_component;
                directionVector = value / r;
            }
        }

        public ConstantMagnet(Bound_Rectangle rectangle, SimpleDirections direction, double M, int N)
        {
            this.direction = direction;
            thisFigure = rectangle;
            coerciveForce = M;
            ribs = new List<Bound_Rib>(2 * N);
            densities = new List<double>(2 * N);
            
            switch (direction)
            {
                case SimpleDirections.FromBottomToTop:
                    {
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width)));
                        ribs.AddRange(bigRibs[0].GetSubRibs(N));
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y)));
                        ribs.AddRange(bigRibs[1].GetSubRibs(N));
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[0].LenthElement);
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[1].LenthElement);
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = 0;
                        directionVector.Y_component = 1;
                        break;
                    }
                case SimpleDirections.FromTopToBottom:
                    {
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y)));
                        ribs.AddRange(bigRibs[0].GetSubRibs(N));
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width)));
                        ribs.AddRange(bigRibs[1].GetSubRibs(N));
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[0].LenthElement);
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[1].LenthElement);
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = 0;
                        directionVector.Y_component = -1;
                        break;
                    }
                case SimpleDirections.FromLeftToRight:
                    {
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width)));
                        ribs.AddRange(bigRibs[0].GetSubRibs(N));
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y)));
                        ribs.AddRange(bigRibs[1].GetSubRibs(N));
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[0].LenthElement);
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[1].LenthElement);
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = 1;
                        directionVector.Y_component = 0;
                        break;
                    }
                case SimpleDirections.FromRightToLeft:
                    {
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y - rectangle.Width), new PointD(rectangle.Location.X + rectangle.Lenth, rectangle.Location.Y)));
                        ribs.AddRange(bigRibs[0].GetSubRibs(N));
                        bigRibs.Add(new Bound_Rib(new PointD(rectangle.Location.X, rectangle.Location.Y), new PointD(rectangle.Location.X, rectangle.Location.Y - rectangle.Width)));
                        ribs.AddRange(bigRibs[1].GetSubRibs(N));
                        for(int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[0].LenthElement);
                        for (int i = 0; i < N; i++)
                            densities.Add(mu_0 * coerciveForce / bigRibs[1].LenthElement);
                        /*densities[0] /= ribs[0].LenthElement;
                        densities[1] /= ribs[1].LenthElement;*/
                        directionVector.X_component = -1;
                        directionVector.Y_component = 0;
                        break;
                    }
            }
        }
        public void ChangeSourcePower(double value)
        {
            coerciveForce = value;
            densities.Clear();
            densities.Add(-mu_0 * coerciveForce);
            densities.Add(mu_0 * coerciveForce);
        }

        public IFigure GetFigure()
        {
            return thisFigure;
        }

        public FigureType GetFigureType()
        {
            return thisFigure.GetFigureType();
        }

        public Vector2D GetGradientValue(PointD pointM)
        {
            double lenth;
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;
            if (thisFigure.IsContaisPoint(pointM))
            {
                result.X_component = -mu_0 * mu_0 * coerciveForce * directionVector.Y_component;
                result.X_component = mu_0 * mu_0 * coerciveForce * directionVector.X_component;
                return result / (2d * Math.PI);
            }
            double coef = 0.0000000001;
            PointD midP;
            double r;
            for (int j = 0; j < ribs.Count; j++)
            {
                midP = new PointD(ribs[j].GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth * coef)
                {
                    PointD[] FalsePoints = { new PointD(pointM.X + lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y - lenth * coef), new PointD(pointM.X + lenth * coef, pointM.Y - lenth * coef) };

                    for (int k = 0; k < 4; k++)
                    {
                        midP = FalsePoints[k];
                        r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                        result.X_component -= ribs[j].Normal.CosAlpha * lenth * densities[j] * (r * r - 2 * (pointM.X * pointM.X - midP.X * midP.X)) / (r * r * r * r);
                        result.Y_component -= ribs[j].Normal.CosBeta * lenth * densities[j] * (r * r - 2 * (pointM.Y * pointM.Y - midP.Y * midP.Y)) / (r * r * r * r);
                    }
                    result /= 4;
                    continue;
                }

                result.X_component -= ribs[j].Normal.CosAlpha * lenth * densities[j] * (r * r - 2 * (pointM.X * pointM.X - midP.X * midP.X)) / (r * r * r * r);
                result.Y_component -= ribs[j].Normal.CosBeta * lenth * densities[j] * (r * r - 2 * (pointM.Y * pointM.Y - midP.Y * midP.Y)) / (r * r * r * r);
            }
            result *= mu_0  / (2d * Math.PI);
            return result;
        }

        public Vector2D GetInductionValue(PointD pointM)
        {
            if (thisFigure.IsContaisPoint(pointM)) return mu_0 * mu_0 * coerciveForce * directionVector;

            /*Vector2D result = GetGradientValue(pointM);
            Vector2D res;
            res.X_component = result.Y_component;
            res.Y_component = -result.X_component;
            return res;*/
            double lenth;
            Vector2D result;
            double coef = 0.000000001;
            result.X_component = 0;
            result.Y_component = 0;
            PointD midP;
            double r;
            for (int j = 0; j < ribs.Count; j++)
            {
                midP = new PointD(ribs[j].GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth * coef)
                {
                    PointD[] FalsePoints = { new PointD(pointM.X + lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y - lenth * coef), new PointD(pointM.X + lenth * coef, pointM.Y - lenth * coef) };

                    for (int k = 0; k < 4; k++)
                    {
                        midP = FalsePoints[k];
                        r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                        result.X_component += ribs[j].Normal.CosBeta * lenth * densities[j] * (r * r - 2 * (pointM.Y * pointM.Y - midP.Y * midP.Y)) / (r * r * r * r);
                        result.Y_component -= ribs[j].Normal.CosAlpha * lenth * densities[j] * (r * r - 2 * (pointM.X * pointM.X - midP.X * midP.X)) / (r * r * r * r);
                    }
                    result /= 4;
                    continue;
                }

                result.X_component += ribs[j].Normal.CosBeta * lenth * densities[j] * (r * r - 2 * (pointM.Y * pointM.Y - midP.Y * midP.Y)) / (r * r * r * r);
                result.Y_component -= ribs[j].Normal.CosAlpha * lenth * densities[j] * (r * r - 2 * (pointM.X * pointM.X - midP.X * midP.X)) / (r * r * r * r);
            }
            result *= mu_0  / (2 * Math.PI);
            return result;
        }


        double Integral_Potencial(Bound_Rib rib, PointD pointM, double Density)
        {

            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < rib.LenthElement * 1e-2)
                return rib.LenthElement * Density * (Math.Log(1.0 / Math.Sqrt((rib.Point1.X - pointM.X) * (rib.Point1.X - pointM.X) + (rib.Point1.Y - pointM.Y) * (rib.Point1.Y - pointM.Y))) +
                                                     Math.Log(1.0 / Math.Sqrt((rib.Point2.X - pointM.X) * (rib.Point2.X - pointM.X) + (rib.Point2.Y - pointM.Y) * (rib.Point2.Y - pointM.Y)))) / 2;
            return rib.LenthElement * Density * Math.Log(1.0 / r);
        }
        public double GetPotencialValue(PointD pointM)
        {
           /* if (thisFigure.IsContaisPoint(pointM))
            { 

                return mu_0 * coerciveForce * (directionVector.X_component * pointM.Y - directionVector.Y_component * pointM.X) / (2 * Math.PI); 
            }

            double lenth;
            Vector2D result;
            double res = 0;
            double coef = 0.000000001;
            result.X_component = 0;
            result.Y_component = 0;
            PointD midP;
            double r = 0;
            for (int j = 0; j < ribs.Count; j++)
            {
                midP = new PointD(ribs[j].GetMiddleOfRib());
                r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                lenth = ribs[j].LenthElement;
                if (r < lenth * coef)
                {
                    PointD[] FalsePoints = { new PointD(pointM.X + lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y + lenth * coef), new PointD(pointM.X - lenth * coef, pointM.Y - lenth * coef), new PointD(pointM.X + lenth * coef, pointM.Y - lenth * coef) };

                    for (int k = 0; k < 4; k++)
                    {
                        midP = FalsePoints[k];
                        result.X_component = lenth * densities[j] * ((pointM.Y - midP.Y) / ((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y)));
                        result.Y_component = -lenth * densities[j] * ((pointM.X - midP.X) / ((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y)));
                        res += result.X_component * ribs[j].Normal.CosAlpha + result.Y_component * ribs[j].Normal.CosBeta;
                    }
                    res /= 4;
                    continue;
                }

                result.X_component = (pointM.Y - midP.Y) / (r * r) * densities[j] * lenth;
                result.Y_component = -(pointM.X - midP.X) / (r * r) * densities[j] * lenth;
                res += result.X_component * ribs[j].Normal.CosAlpha + result.Y_component * ribs[j].Normal.CosBeta;
            }
            res *= mu_0 / (2d * Math.PI);
            return res;*/
            double result = 0;
                for (int j = 0; j < ribs.Count; j++)
                    result += Integral_Potencial(ribs[j], pointM, densities[j]);
            result *= mu_0 * mu_0 / (2.0 * Math.PI);
            return -result;
        }

        public SourceType GetSourceType()
        {
            return SourceType.ConstantMagnet;
        }
    }
}
