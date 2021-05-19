using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculating_Magnetic_Field.Figures;
/*using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;*/
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers.Preconditioners;
using Calculating_Magnetic_Field.ModelFactories;


namespace Calculating_Magnetic_Field.Models
{
    public class ScalarPotencialModel: IModel
    {

        #region Поля
        //Коэффициенты для квадратуры Гаусса
        public double[] tau;
        public double[] d;

        #region Для ППС
        Matrix<double> matrix;
        Vector<double> vectorB;
        BiConjugateGradientSolver<double> BiCgStabSolve;
        public List<double> Dens;
        public List<PointD> Points { get; set; }
        
        public IFigure figure;
        double n;
        double[,] MatrixA;
        double[] VectorB;
        double[] ArrDencities;
        Vector<double> Densities;


        private double[,] kernelValues;
        private double[,] middleKernelValues;

        private List<Bound> bounds;
        public List<Bound> Bounds
        {
            get
            {
                return bounds;
            }
        }
        private List<ISource> sources;
        public List<ISource> Sources
        {
            get
            {
                return sources;
            }
        }


        double[] MidValuesBn;
        Vector2D[] CoilsPotencialGradientOnBound;
        double[] FreeMemberFunOnBound;

        #endregion


        #region для Интегральной формулы Грина
        public List<ElementBasisFunction> boundElements;

        #endregion


        //double 

        //глубина модели по оси Oz
        private double depth;

        public double Depth
        {
            get { return depth; }

            set { depth = value; }
        }

        public double[,] PotencialValues { get; private set; }


        public double midV;

        double standart_field_property;



        private PhysicalField physicalField;
        public PhysicalField PhysicalField
        {
            get
            {
                return physicalField;
            }
            set
            {
                switch (value)
                {
                    case PhysicalField.Electric:
                        {
                            standart_field_property = PhysicalConstants.epsilon_0;
                            break;
                        }
                    case PhysicalField.Magnetic:
                        {
                            standart_field_property = PhysicalConstants.mu_0;
                            break;
                        }
                    case PhysicalField.Current:
                        {
                            standart_field_property = PhysicalConstants.conductivity;
                            break;
                        }
                }
                physicalField = value;
            }
        }

        private IPotencialFactoryMethod potencialFactoryMethod;

        public IPotencialFactoryMethod PotencialFactoryMethod
        {
            get
            {
                return potencialFactoryMethod;
            }
            set
            {
                potencialFactoryMethod = value;
            }
        }

        private IPotencial scalarPotencial;

        public IPotencial Potencial
        {
            get
            {
                return scalarPotencial;
            }
            set
            {
                scalarPotencial = value;
            }
        }

        #endregion

        public ScalarPotencialModel(double depth, PhysicalField physicalField)
        {
            #region Коэффициенты для квадратуры Гаусса
            tau = new double[3];
            d = new double[3];
            tau[0] = -Math.Sqrt(3.0 / 5);
            tau[1] = 0;
            tau[2] = Math.Sqrt(3.0 / 5);
            d[0] = 5.0 / 9;
            d[1] = 8.0 / 9;
            d[2] = 5.0 / 9;
            #endregion

            this.PhysicalField = physicalField;
            this.depth = depth;
            //BiCgStabSolve = new BiCgStab();
            bounds = new List<Bound>();
            sources = new List<ISource>();
        }


        public void AddBorderOfEnvironments(IFigure figure, int n, double right_mu, double left_mu)
        {
            switch (figure.GetFigureType())
            {
                case FigureType.Rectangle:
                    {
                        bounds.Add(new Bound((Bound_Rectangle)figure, n, right_mu, left_mu));
                        break;
                    }

                case FigureType.Circle:
                    {
                        bounds.Add(new Bound((Bound_Circle)figure, n, right_mu, left_mu));
                        break;
                    }
                case FigureType.Stadium:
                    {
                        bounds.Add(new Bound((Bound_Stadium)figure, n, right_mu, left_mu));
                        break;
                    }
            }

        }

        public void AddMagnetic(Bound_Rectangle rectangle, int n, double right_mu, double left_mu)
        {
            bounds.Add(new Bound(rectangle, n, right_mu, left_mu));
        }

        public void AddMagnetic(Bound_Rectangle rectangleEx, Bound_Rectangle rectangleIm, int n, double right_mu, double left_mu)
        {
            bounds.Add(new Bound(rectangleEx, rectangleIm, n, right_mu, left_mu));
        }

        public void AddMagnetic(Bound_Circle circle, int n, double right_mu, double left_mu)
        {
            bounds.Add(new Bound(circle, n, right_mu, left_mu));
        }

        public void AddMagnetic(Bound_Frame bound_Frame, int n, double right_mu, double left_mu)
        {
            bounds.Add(new Bound(bound_Frame, n, right_mu, left_mu));
        }


        public void AddSource(ISource source)
        {
            sources.Add(source);
        }

        public void AddCoil(Bound_Rectangle rectangle, double Current, int n, int m)
        {
            sources.Add(new Coil(rectangle, Current, n, m));
        }

        public void AddMagnet(Bound_Rectangle rectangle, double M, SimpleDirections direction, int N)
        {
            //sources.Add(new ConstantMagnetScalarPot(rectangle, direction, M, N));
        }


        #region Потенциал простого слоя
        public double Integral_dAdy_M(PointD pointM)
        {
            double r2;
            double result = 0;
            PointD MiddleOFRib;
            for (int i = 0; i < bounds[0].Bound_Ribs.Count; i++)
            {
                /* MiddleOFRib = new PointD(bounds[0].Bound_Ribs[i].GetMiddleOfRib().X, bounds[0].Bound_Ribs[i].GetMiddleOfRib().Y);
                 r2 = (MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) + (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y);
                 result += bounds[0].Bound_Ribs[i].LenthElement * ArrDencities[i] * (MiddleOFRib.Y - pointM.Y) / r2;*/
                result += GaussIntegral(bounds[0].Bound_Ribs[i], pointM, dAdy_M) * ArrDencities[i];
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointM"></param>
        /// <returns></returns>
        public double Integral_dAdx_M(PointD pointM)
        {
            double r2;
            double result = 0;
            PointD MiddleOFRib;
            for (int i = 0; i < bounds[0].Bound_Ribs.Count; i++)
            {
                MiddleOFRib = new PointD(bounds[0].Bound_Ribs[i].GetMiddleOfRib().X, bounds[0].Bound_Ribs[i].GetMiddleOfRib().Y);
                r2 = (MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) + (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y);
                result += GaussIntegral(bounds[0].Bound_Ribs[i], pointM, dAdx_M) * ArrDencities[i];
            }
            return result;
        }

        public double dAdx_M (PointD pointN, PointD pointM)
        {
            double r2 = (pointN.X - pointM.X) * (pointN.X - pointM.X) + (pointN.Y - pointM.Y) * (pointN.Y - pointM.Y);
            return (pointN.X - pointM.X) / r2;
        }

        public double dAdy_M (PointD pointN, PointD pointM)
        {
            double r2 = (pointN.X - pointM.X) * (pointN.X - pointM.X) + (pointN.Y - pointM.Y) * (pointN.Y - pointM.Y);
            return (pointN.Y - pointM.Y) / r2;
        }

        #region Силы
        /*public Vector2D Calculate_Power()
        {

            Vector2D result;
            double resultX = 0;
            double resultY = 0;
            int n = bounds[1].Bound_Ribs.Count;
            int ni = bounds[0].Bound_Ribs.Count;
            double magnet_dAdy_On_carg;
            double magnet_dAdx_On_carg;

            for (int j = 0; j < n; j++)
            {
                magnet_dAdx_On_carg = Integral_dAdx_M(bounds[1].MiddlePoints[j]) + CoilsPotencialGradientOnBound[ni + j].X_component;
                magnet_dAdy_On_carg = Integral_dAdy_M(bounds[1].MiddlePoints[j]) + CoilsPotencialGradientOnBound[ni + j].Y_component;
                resultX += magnet_dAdx_On_carg * ArrDencities[ni + j] * bounds[1].Bound_Ribs[j].LenthElement;
                resultY += magnet_dAdy_On_carg * ArrDencities[ni + j] * bounds[1].Bound_Ribs[j].LenthElement;
            }
            result.X_component = resultX;
            result.Y_component = resultY;
            return depth * depth * result / (2 * Math.PI * standart_field_property);
        }*/
        public double Calculate_Power()
        {
            switch (bounds.Count)
            {
                case 1:
                    {
                        return Calculate_PowerK();
                    }
                case 2:
                    {
                        return Calculate_PowerKS();
                    }
            }
            return 0;
        }
        public double Calculate_PowerKS()
        {

            double result;
            double resultY = 0;
            int n = bounds[1].Bound_Ribs.Count;
            int ni = bounds[0].Bound_Ribs.Count;
            double magnet_dAdy_On_carg;

            for (int j = 0; j < n; j++)
            {
                magnet_dAdy_On_carg = Integral_dAdy_M(bounds[1].MiddlePoints[j]) + CoilsPotencialGradientOnBound[ni + j].Y_component;
                resultY += magnet_dAdy_On_carg * ArrDencities[ni + j] * bounds[1].Bound_Ribs[j].LenthElement;
            }
            result = resultY;
            return depth * depth * result / (2 * Math.PI * standart_field_property);
        }
        public double Calculate_PowerK()
        {

            double result;
            double resultY = 0;
            int n = bounds[0].Bound_Ribs.Count;
            double magnet_dAdy_On_carg;

            for (int j = 0; j < n; j++)
            {
                magnet_dAdy_On_carg = CoilsPotencialGradientOnBound[j].Y_component;
                resultY += magnet_dAdy_On_carg * ArrDencities[j] * bounds[0].Bound_Ribs[j].LenthElement;
            }
            result = resultY;
            return depth * depth * result / (2 * Math.PI * standart_field_property);
        }
        #endregion
        /// <summary>
        /// Вычисляет потенциал элемента в точке наблюдения
        /// </summary>
        /// <param name="rib">Элементарный носитель плотности</param>
        /// <param name="pointM">Точка наблюдения</param>
        /// <param name="Density">Плотность тока на носителе</param>
        /// <returns></returns>
        public double Integral_Potencial(PointD pointM, Bound_Rib rib)
        {
            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < rib.LenthElement * 1e-5)
                return rib.LenthElement * (Math.Log(1.0 / Math.Sqrt((rib.Point1.X - pointM.X) * (rib.Point1.X - pointM.X) + (rib.Point1.Y - pointM.Y) * (rib.Point1.Y - pointM.Y))) +
                                                     Math.Log(1.0 / Math.Sqrt((rib.Point2.X - pointM.X) * (rib.Point2.X - pointM.X) + (rib.Point2.Y - pointM.Y) * (rib.Point2.Y - pointM.Y)))) / 2;
            return rib.LenthElement * Math.Log(1.0 / r);
        }

        /*public double Integral_Potencial(Bound_Rib rib, PointD pointM, double Density)
        {
            PointD MiddleOFRib = rib.GetMiddleOfRib();
            double r = Math.Sqrt((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) +
                                 (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y));
            if (r < rib.LenthElement)
                return Density * (-2*Math.PI/ rib.LenthElement + rib.LenthElement*(1-Math.Log(rib.LenthElement/2)));
            return rib.LenthElement * Density * Math.Log(1.0 / r);
        }*/
        #region Методы


        private void CalculateKernels()
        {
            int n, ni = 0;
            int m, nk = 0;
            PointD pointM;
            PointD pointN;
            Bound_Rib ribM;
            for (int i = 0; i < bounds.Count; i++)
            {
                n = bounds[i].Bound_Ribs.Count;
                for (int j = 0; j < n; j++)
                {
                    ribM = bounds[i].Bound_Ribs[j];
                    pointM = ribM.GetMiddleOfRib();
                    for (int k = 0; k < bounds.Count; k++)
                    {
                        m = bounds[k].Bound_Ribs.Count;
                        for (int p = 0; p < m; p++)
                        {
                            if (i == k && j == p) { kernelValues[ni + j, nk + p] = 0; continue; }

                            pointN = bounds[k].Bound_Ribs[p].GetMiddleOfRib();
                            kernelValues[ni + j, nk + p] = ((pointM.X - pointN.X) * ribM.Normal.CosAlpha + (pointM.Y - pointN.Y) * ribM.Normal.CosBeta) /
                                                           ((pointN.X - pointM.X) * (pointN.X - pointM.X) + (pointN.Y - pointM.Y) * (pointN.Y - pointM.Y));
                        }
                        nk += m;
                    }
                    nk = 0;
                }
                ni += n;
            }
        }


        private void CalculateMiddleValuesOfKernels()
        {
            int n, ni = 0;
            int m, nk = 0;
            Bound_Rib ribN;
            for (int i = 0; i < bounds.Count; i++)
            {
                n = bounds[i].Bound_Ribs.Count;
                for (int k = 0; k < bounds.Count; k++)
                {
                    m = bounds[k].Bound_Ribs.Count;
                    for (int p = 0; p < m; p++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            ribN = bounds[i].Bound_Ribs[j];
                            middleKernelValues[i, nk + p] += kernelValues[ni + j, nk + p] * ribN.LenthElement;
                        }
                        middleKernelValues[i, nk + p] /= bounds[i].BoundLenth;
                    }
                    nk += m;

                }
                nk = 0;
                ni += n;
            }
        }

        public double Integral_dAdn(Bound_Rib ribN, Bound_Rib ribM)
        {
            PointD pointM = ribM.GetMiddleOfRib();
            PointD MiddleOFRib = ribN.GetMiddleOfRib();
            //if (ribN.Rib_Position == Rib_Position.Vertical && pointM.X == ribN.Point1.X) return 0;
            //if (ribN.Rib_Position == Rib_Position.Horizontal && pointM.Y == ribN.Point1.Y) return 0;

            //if (ribN.Rib_Position == Rib_Position.Horizontal && pointM.DistanceToOtherPoint(MiddleOFRib) < ribN.LenthElement)
            return ribN.LenthElement * ((((pointM.X - MiddleOFRib.X) * ribM.Normal.CosAlpha + (pointM.Y - MiddleOFRib.Y) * ribM.Normal.CosBeta) /
               ((MiddleOFRib.X - pointM.X) * (MiddleOFRib.X - pointM.X) + (MiddleOFRib.Y - pointM.Y) * (MiddleOFRib.Y - pointM.Y))));
            //return GaussIntegralDn(ribN, ribM, dAdn_pps);
        }

        #region Расчет коэффициентов
        /// <summary>
        /// Расчёт коэффициентов матрицы коэффициентов и столбца свободных членов
        /// </summary>
        void CalculateCoefficientsForVectorPotencialWithoutRegularization()
        {
            double lambdaI, lambdaJ;
            int ni = 0;
            int nk = 0;
            int n = 0;
            for (int i = 0; i < bounds.Count; i++)
                n += bounds[i].Bound_Ribs.Count;

            //Вычисление коэффициентов матрицы
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    for (int k = 0; k < bounds.Count; k++)
                    {
                        lambdaJ = Potencial.Sign * (bounds[k].Left_Mu - bounds[k].Right_Mu) / (bounds[k].Left_Mu + bounds[k].Right_Mu);
                        for (int p = 0; p < bounds[k].Bound_Ribs.Count; p++)
                        {
                            if (i == k && j == p)
                                MatrixA[ni + j, nk + p] = 1;
                            else
                                MatrixA[ni + j, nk + p] = -lambdaJ / Math.PI * Potencial.Integral_dAdn(bounds[k].Bound_Ribs[p], bounds[i].Bound_Ribs[j]);
                        }
                        nk += bounds[k].Bound_Ribs.Count;

                    }
                    nk = 0;
                }
                ni += bounds[i].Bound_Ribs.Count;
            }
            ni = 0;
            //Вычисление столбца свободных членов
            for (int i = 0; i < bounds.Count; i++)
            {
                lambdaI = Potencial.Sign * (bounds[i].Left_Mu - bounds[i].Right_Mu) / (bounds[i].Right_Mu + bounds[i].Left_Mu);
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    VectorB[ni + j] = 2 * standart_field_property * lambdaI * FreeMemberFunOnBound[ni + j];
                }
                ni += bounds[i].Bound_Ribs.Count;
            }
        }


        private void CalculateCoefficientsForVectorPotencialWithRegularization()
        {
            double lambdaI, lambdaJ;
            int ni = 0;
            int nk = 0;
            int n = 0;
            for (int i = 0; i < bounds.Count; i++)
                n += bounds[i].Bound_Ribs.Count;

            //Вычисление коэффициентов матрицы
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    for (int k = 0; k < bounds.Count; k++)
                    {
                        lambdaJ = Potencial.Sign * (bounds[k].Left_Mu - bounds[k].Right_Mu) / (bounds[k].Left_Mu + bounds[k].Right_Mu);
                        for (int p = 0; p < bounds[k].Bound_Ribs.Count; p++)
                        {
                            if (i == k && j == p)
                                MatrixA[ni + j, nk + p] = 1 + lambdaJ / Math.PI * bounds[k].Bound_Ribs[p].LenthElement * middleKernelValues[i, nk + p];
                            else
                                MatrixA[ni + j, nk + p] = -lambdaJ / Math.PI * bounds[k].Bound_Ribs[p].LenthElement * (kernelValues[ni + j, nk + p] - middleKernelValues[i, nk + p]);
                        }
                        nk += bounds[k].Bound_Ribs.Count;

                    }
                    nk = 0;
                }
                ni += bounds[i].Bound_Ribs.Count;
            }
            ni = 0;
            //Вычисление столбца свободных членов
            for (int i = 0; i < bounds.Count; i++)
            {
                lambdaI = (bounds[i].Left_Mu - bounds[i].Right_Mu) / (bounds[i].Right_Mu + bounds[i].Left_Mu);
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    VectorB[ni + j] = 2 * standart_field_property * lambdaI * (FreeMemberFunOnBound[ni + j] - MidValuesBn[i]);
                }
                ni += bounds[i].Bound_Ribs.Count;
            }
        }

        #endregion




        public bool IsThereAre2Magnetics()
        {
            if (bounds.Count == 2) return true;
            return false;
        }


        /// <summary>
        /// Вычисление значения градиента потенциала первичных источников в точке наблюдения
        /// </summary>
        /// <param name="pointM">Точка наблюдения</param>
        /// <returns></returns>
        public Vector2D CalculateCoisPotencialGradient(PointD pointM)
        {
            Vector2D res;
            res.X_component = 0;
            res.Y_component = 0;
            for (int i = 0; i < sources.Count; i++)
            {
                res += sources[i].GetGradientValue(pointM);
            }
            return res;
        }

        /// <summary>
        /// Расчёт первичного поля, создаваемого первичными источниками на границе
        /// </summary>
        void CalculateSourcesFieldInboundsPoints()
        {

            int nj = 0;
            Vector2D grad;
            switch (Potencial.TypeOFPotencial)
            {
                case PotencialTypes.PSL:
                    {
                        for (int i = 0; i < bounds.Count; i++)
                        {
                            for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                                for (int k = 0; k < sources.Count; k++)
                                {
                                    grad = sources[k].GetGradientValue(bounds[i].Bound_Ribs[j].GetMiddleOfRib());
                                    CoilsPotencialGradientOnBound[nj + j] += grad;
                                    FreeMemberFunOnBound[nj + j] += bounds[i].Bound_Ribs[j].Normal.CosAlpha * grad.X_component + bounds[i].Bound_Ribs[j].Normal.CosBeta * grad.Y_component;
                                }
                            nj += bounds[i].Bound_Ribs.Count;
                        }
                        break;
                    }
                case PotencialTypes.PDL:
                    {
                        for (int i = 0; i < bounds.Count; i++)
                        {
                            for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                                for (int k = 0; k < sources.Count; k++)
                                {
                                    grad = sources[k].GetGradientValue(bounds[i].Bound_Ribs[j].GetMiddleOfRib());
                                    CoilsPotencialGradientOnBound[nj + j] += grad;
                                    FreeMemberFunOnBound[nj + j] += sources[k].GetPotencialValue(bounds[i].Bound_Ribs[j].GetMiddleOfRib());
                                }
                            nj += bounds[i].Bound_Ribs.Count;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Расчёт средних значений свободного члена на границе каждого намагничиваемого тела
        /// </summary>
        void CalculateMiddleValuesOf_Bn()
        {
            int ni = 0;
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count - 1; j++)
                {
                    MidValuesBn[i] += bounds[i].Bound_Ribs[j].LenthElement * FreeMemberFunOnBound[ni + j];
                }
                MidValuesBn[i] /= bounds[i].BoundLenth;
                ni += bounds[i].Bound_Ribs.Count;
            }

        }

        /// <summary>
        /// Квадратурная формула Гаусса
        /// </summary>
        /// <param name="rib">Элемен</param>
        /// <param name="pointM"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public double GaussIntegral(Bound_Rib rib, PointD pointM, Func<PointD, PointD, double> func)
        {
            double result = 0;
            PointD PointN;
            double x, y;
            for (int i = 0; i < 3; i++)
            {
                x = (rib.Point2.X + rib.Point1.X) / 2 + (rib.Point2.X - rib.Point1.X) / 2 * tau[i];
                y = (rib.Point2.Y + rib.Point1.Y) / 2 + (rib.Point2.Y - rib.Point1.Y) / 2 * tau[i];
                PointN = new PointD(x, y);
                result += d[i] * func(PointN, pointM);
            }
            result *= rib.LenthElement / 2;
            return result;
        }
        public double GaussIntegralDn(Bound_Rib ribN, Bound_Rib ribM, Func<PointD, Bound_Rib, double> func)
        {
            
            double result = 0;
            PointD PointN;
            double x, y;
            for (int i = 0; i < 3; i++)
            {
                x = (ribN.Point2.X + ribN.Point1.X) / 2 + (ribN.Point2.X - ribN.Point1.X) / 2 * tau[i];
                y = (ribN.Point2.Y + ribN.Point1.Y) / 2 + (ribN.Point2.Y - ribN.Point1.Y) / 2 * tau[i];
                PointN = new PointD(x, y);
                result += d[i] * func(PointN, ribM);
            }
            result *= ribN.LenthElement / 2;
            return result;
        }



        #region Расчёт величин
        public Vector2D CalculateInduction(PointD pointM)
        {
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;
            Bound bound = GetOwnerField(pointM);
            double fieldProperty;

            for (int i = 0; i < sources.Count; i++)
            {
                result += depth * sources[i].GetInductionValue(pointM);
            }
            result += CalculateReactionInduction(pointM);

            if (Potencial.TypeOFPotencial == PotencialTypes.PSL)
            {
                if (bound == null)
                {
                    fieldProperty = Bounds[0].Right_Mu;
                }
                else fieldProperty = bound.Left_Mu;
                result *= fieldProperty;
            }
            

            return result;
            //return result;
        }

        private Vector2D CalculateReactionInduction(PointD pointM)
        {
            Bound_Rib rib;
            double lenth;
            Vector2D result;

            double eps = 1e-6;

            result.X_component = 0;
            result.Y_component = 0;
            PointD midP;
            int ni = 0;
            double r = 0;
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    rib = bounds[i].Bound_Ribs[j];
                    /*midP = new PointD(rib.GetMiddleOfRib());
                    r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                    
                    lenth = rib.LenthElement;
                    
                    result.X_component += (pointM.X - midP.X) / (r * r) * ArrDencities[ni + j] * lenth;
                    result.Y_component += (pointM.Y - midP.Y) / (r * r) * ArrDencities[ni + j] * lenth;*/
                    result += Potencial.Calculate_Induction_from_Element(pointM, rib) * ArrDencities[ni + j];
                }
                ni += bounds[i].Bound_Ribs.Count;
            }

            return depth * 0.5 / Math.PI * result;
        }

        private Vector2D CalculateReactionIntensity(PointD pointM)
        {
            Bound_Rib rib;
            double lenth;
            Vector2D result;

            double eps = 1e-6;

            result.X_component = 0;
            result.Y_component = 0;
            PointD midP;
            int ni = 0;
            double r = 0;
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                {
                    rib = bounds[i].Bound_Ribs[j];
                    midP = new PointD(bounds[i].Bound_Ribs[j].GetMiddleOfRib());
                    r = Math.Sqrt((midP.X - pointM.X) * (midP.X - pointM.X) + (midP.Y - pointM.Y) * (midP.Y - pointM.Y));
                    lenth = bounds[i].Bound_Ribs[j].LenthElement;

                    result.X_component += (pointM.X - midP.X) / (r * r) * ArrDencities[ni + j] * lenth;
                    result.Y_component += (pointM.Y - midP.Y) / (r * r) * ArrDencities[ni + j] * lenth;
                }
                ni += bounds[i].Bound_Ribs.Count;
            }

            return depth * result / (2 * Math.PI * standart_field_property);
        }

        /// <summary>
        /// Вычисление потенциала поля реакции среды в точке pointM
        /// </summary>
        /// <param name="points">Точка, в которой вычисляется потенциал</param>
        /// /// <param name="Potencial">Значение потенциала в заданной точке</param>
        public double Calculate_Sources_Potencial(PointD pointM)
        {

            double result = 0;

            for (int i = 0; i < sources.Count; i++)
            {
                result += sources[i].GetPotencialValue(pointM);
            }

            return depth * result;
        }

        public void Calculate_Sources_Potencial(PointD[] points, out double[] Potencial)
        {
            Potencial = new double[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                Potencial[i] = Calculate_Sources_Potencial(points[i]);
                //Calculate_CoilsPotencial(points[i], out Potencial[i]);
            }
        }

        /// <summary>
        /// Вычисление потенциала поля реакции среды в точке pointM
        /// </summary>
        /// <param name="points">Точка, в которой вычисляется потенциал</param>
        /// /// <param name="Potencial">Значение потенциала в заданной точке</param>
        public double Calculate_ReactionPotencial(PointD pointM)
        {
            double result = 0;
            int ni = 0;
            for (int i = 0; i < bounds.Count; i++)
            {
                for (int j = 0; j < bounds[i].Bound_Ribs.Count; j++)
                    result += Integral_Potencial(pointM, bounds[i].Bound_Ribs[j]) * ArrDencities[ni + j];
                ni += bounds[i].Bound_Ribs.Count;
            }
            return depth / (2.0 * Math.PI * standart_field_property) * result;
        }

        /// <summary>
        /// Вычисление потенциала реакции среды в наборе точек points
        /// </summary>
        /// <param name="points">Точки, в которых вычисляется потенциал</param>
        /// /// <param name="Potencial">Значения потенциала в заданных точках</param>
        public void Calculate_ReactionPotencial(PointD[] points, out double[] Potencial)
        {
            Potencial = new double[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                Potencial[i] = Calculate_ReactionPotencial(points[i]);

            }
        }

        public Vector2D CalculateIntensity(PointD pointM)
        {
            Vector2D result;
            result.X_component = 0;
            result.Y_component = 0;
            result = CalculateReactionIntensity(pointM);
            for (int i = 0; i < sources.Count; i++)
                result += depth * sources[i].GetIntensityValue(pointM);
            return result;
        }

        public double CalculatePotencial(PointD pointM)
        {
            return Calculate_ReactionPotencial(pointM) + Calculate_Sources_Potencial(pointM);
        }

        #endregion

        #endregion
        public void SolveProblem()
        {
            
            int n = 0;
            for (int i = 0; i < bounds.Count; i++)
                n += bounds[i].Bound_Ribs.Count;
            MatrixA = new double[n, n];
            VectorB = new double[n];
            kernelValues = new double[n, n];
            middleKernelValues = new double[bounds.Count, n];
            ArrDencities = new double[n];
            CoilsPotencialGradientOnBound = new Vector2D[n];
            FreeMemberFunOnBound = new double[n];
            MidValuesBn = new double[bounds.Count];

            if (sources.Count < 1) return;

            CalculateKernels();
            CalculateMiddleValuesOfKernels();

            CalculateSourcesFieldInboundsPoints();
            CalculateMiddleValuesOf_Bn();

            CalculateCoefficientsForVectorPotencialWithoutRegularization();

            matrix = Matrix.Create(MatrixA);
            /*for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = MatrixA[i, j];
            for (int i = 0; i < n; i++)
                vectorB[i] = VectorB[i];*/
            vectorB = Vector.Create(VectorB);

            BiCgStabSolve = new BiConjugateGradientSolver<double>(matrix);
            BiCgStabSolve.Preconditioner = new Extreme.Mathematics.LinearAlgebra.IterativeSolvers.Preconditioners.IdentityPreconditioner<double>(matrix);
            Vector<double> res = BiCgStabSolve.Solve(vectorB);
            for (int i = 0; i < n; i++)
                ArrDencities[i] = res[i];

            //SolveEq(MatrixA, VectorB, out ArrDencities, n);

        }
        #endregion


        /// <summary>
        /// Решение СЛАУ
        /// </summary>
        /// <param name="matrix">Матрица коэффициентов</param>
        /// <param name="freeF">Столбец  свободных членов</param>
        /// <param name="n">Размерность</param>
        /// <returns></returns>
        public void SolveEq(double[,] matrix, double[] freeF, out double[] result, int n)
        {
            double[,] matr = (double[,])matrix.Clone();
            result = new double[freeF.Length];
            int count, i, j, i_2, j_2;
            double elem;
            double[] vector = new double[n];
            double changingEl_F;

            for (count = n - 1; count >= 0; count--)
            {
                // перестановка строк в случае,
                // если элемент matrix[count,count]=0
                if (matr[count, count] == 0)
                {
                    i_2 = count;
                    while (matr[i_2, count] == 0)
                    {
                        i_2 -= 1;
                    }
                    changingEl_F = freeF[i_2];
                    freeF[i_2] = freeF[count];
                    freeF[count] = changingEl_F;
                    for (j_2 = 0; j_2 <= count; j_2++)
                    {
                        vector[j_2] = matr[count, j_2];
                        matr[count, j_2] = matr[i_2, j_2];
                        matr[i_2, j_2] = vector[j_2];
                    }
                }

                //фиксирование элемента matrix[count,count]
                elem = matr[count, count];
                //деление строки count на зафиксированный элемент
                freeF[count] /= elem;
                for (i = 0; i <= n - 1; i++)
                {
                    matr[count, i] = matr[count, i] / elem;
                }

                for (i = 0; i <= count - 1; i++)
                {
                    //Фиксирование элемента i-й строки(выше строки count) столбца count
                    elem = matr[i, count];
                    freeF[i] = freeF[i] - freeF[count] * elem;
                    //Вычитание строки count из верхних строк
                    for (j = 0; j <= n - 1; j++)
                    {
                        matr[i, j] = matr[i, j] - matr[count, j] * elem;
                    }
                }
            }
            for (count = 0; count <= n - 1; count++)
            {
                for (i = count + 1; i <= n - 1; i++)
                {
                    freeF[i] -= freeF[count] * matr[i, count];
                    matr[i, count] = 0;
                }
            }
            result = freeF;
        }

        public DimensionsOfPotencial GetPotencialDimensionType()
        {
            return DimensionsOfPotencial.Scalar;
        }

        public Bound GetOwnerField(PointD point)
        {
            for (int i = 1; i < Bounds.Count; i++)
            {
                if (Bounds[i].IsContaisPoint(point))
                {
                    return Bounds[i];
                }
            }
            if (Bounds[0].IsContaisPoint(point))
            {
                return Bounds[0];
            }
            return null;
        }
        
    }
}
