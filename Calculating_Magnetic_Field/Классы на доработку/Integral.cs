using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Расчёт_магнитного_поля
{
    public static class Integral
    {
        #region С измельчением шага
        /// <summary>
        /// Квадратурная формула Симпсона интегрирования непрерывной функции с параметром M c измельчением шага интегрирования(для нормальной производной)
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureParametered(Bound_Rib rib, PointD pointM, Function function, Interrupt_Conditions interrupt, double a)
        {
            Interrupt_Conditions interrupt_Conditions;

            int j = interrupt.CountOfIterations;
            int i = interrupt.i;
            double epsilon = interrupt.Epsilon;
            double max_epsilon = interrupt.MaxEpsilon;

            PointD MiddlePoint = rib.GetMiddleOfRib();
            Bound_Rib rib1, rib2, rib3, rib4, rib5, rib6;
            double result1, result;
            PointD rib1Mid, rib2Mid;

            result1 = rib.LenthElement / 6 * (function(rib.Point1, pointM, rib, a) + 4 * function(MiddlePoint, pointM, rib, a) + function(rib.Point2, pointM, rib, a));
            rib1 = new Bound_Rib(rib.Point1, MiddlePoint);
            rib2 = new Bound_Rib(MiddlePoint, rib.Point2);
            
            result = rib1.LenthElement / 6 * (function(rib1.Point1, pointM, rib, a) + 4 * function(rib1.GetMiddleOfRib(), pointM, rib, a) + function(rib1.Point2, pointM, rib, a)) +
                      rib2.LenthElement / 6 * (function(rib2.Point1, pointM, rib, a) + 4 * function(rib2.GetMiddleOfRib(), pointM, rib, a) + function(rib2.Point2, pointM, rib, a));

            if (Math.Abs((result1 - result) / result) < epsilon)
                return result;

            rib1Mid = rib1.GetMiddleOfRib();
            rib2Mid = rib2.GetMiddleOfRib();
            i--;
            if (i > 0)
            {
                interrupt_Conditions.i = i;
                interrupt_Conditions.CountOfIterations = j;
                interrupt_Conditions.Epsilon = epsilon;
                interrupt_Conditions.MaxEpsilon = max_epsilon;
                rib3 = new Bound_Rib(rib.Point1, rib1Mid);
                rib4 = new Bound_Rib(rib1Mid, MiddlePoint);
                rib5 = new Bound_Rib(MiddlePoint, rib2Mid);
                rib6 = new Bound_Rib(rib2Mid,rib.Point2);
                result = SimpsonQuadratureParametered(rib3, pointM, function, interrupt_Conditions, a) + SimpsonQuadratureParametered(rib4, pointM, function, interrupt_Conditions, a) +
                          SimpsonQuadratureParametered(rib5, pointM, function, interrupt_Conditions, a) + SimpsonQuadratureParametered(rib6, pointM, function, interrupt_Conditions, a);
                if (Math.Abs((result1 - result) / result1) < epsilon)
                    return result;
            }
            epsilon += 0.01;
            i = j;
            if (epsilon <= max_epsilon)
            {
                interrupt_Conditions.i = i;
                interrupt_Conditions.CountOfIterations = j;
                interrupt_Conditions.Epsilon = epsilon;
                interrupt_Conditions.MaxEpsilon = max_epsilon;
                result = SimpsonQuadratureParametered(rib, pointM, function, interrupt_Conditions, a);
            }

            return result;
        }
        /// <summary>
        /// Квадратурная формула Симпсона интегрирования на отрезке с измельчением шага, содержащем особую точку(для нормальной производной потенциала)
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(особая точка)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="specialPoint">Расположение особой точки на отрезке</param>
        /// <returns></returns>
        public static double SimpsonImproperQuadrature(Bound_Rib rib, PointD pointM, Function function, SpecialPoint specialPoint, Interrupt_Conditions interrupt, double a)
        {
            if (rib.Rib_Position == Rib_Position.Vertical && pointM.X == rib.Point1.X && pointM.X == rib.Point2.X) return 0;
            PointD MiddlePoint;
            Bound_Rib rib1, rib2;
            double result = 0;
            switch (specialPoint)
            {
                case SpecialPoint.Point_1:
                    {
                        rib1 = new Bound_Rib(new PointD(rib.Point1.X * 1.1, rib.Point1.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt, a);
                        break;
                    }
                case SpecialPoint.Point_Middle:
                    {
                        MiddlePoint = rib.GetMiddleOfRib();
                        rib1 = new Bound_Rib(rib.Point1, new PointD(MiddlePoint.X * 0.9, MiddlePoint.Y * 0.9));
                        rib2 = new Bound_Rib(new PointD(MiddlePoint.X * 1.1, MiddlePoint.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt, a) + SimpsonQuadratureParametered(rib2, pointM, function, interrupt, a);
                        break;
                    }
                case SpecialPoint.Point_2:
                    {
                        rib1 = new Bound_Rib(rib.Point1, new PointD(rib.Point2.X * 0.9, rib.Point2.Y * 0.9));
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt, a);
                        break;
                    }
            }
            return result;
        }

#endregion

        /// <summary>
        /// Квадратурная формула Симпсона интегрирования непрерывной функции
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureForMatrix(Bound_Rib rib, Function_ForMatrix function, Bound_Rib ribL_i, Bound_Rib ribL_j, BoundDataMatrix boundData)
        {
            PointD MiddlePoint = rib.GetMiddleOfRib();
            double result1;

            result1 = rib.LenthElement / 6 * (function(rib.Point1, ribL_i, ribL_j, boundData) + 4 * function(MiddlePoint, ribL_i, ribL_j, boundData) + function(rib.Point2, ribL_i, ribL_j, boundData));

            return result1;
        }

        /// <summary>
        /// Квадратурная формула Симпсона интегрирования правой части уравнения
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureForVector(Bound_Rib rib, double ribP1_BnValue, double ribP2_BnValue, Function_ForVector function, Bound_Rib ribL_i, BoundDataVector boundData)
        {

            PointD MiddlePoint = rib.GetMiddleOfRib();
            double result;

            double mid = boundData.BnMiddleValue;
            double Bn_MiddleValue;

            Bn_MiddleValue = (ribP1_BnValue + ribP2_BnValue) / 2;

            result = rib.LenthElement / 6 * (function(rib.Point1, ribL_i, boundData)  * (ribP1_BnValue-mid) + 
                                         4 * function(MiddlePoint, ribL_i, boundData) * (Bn_MiddleValue - mid) + 
                                             function(rib.Point2, ribL_i, boundData) * (ribP2_BnValue - mid));
            return result;
        }


        /// <summary>
        /// Квадратурная формула Симпсона интегрирования непрерывной функции с параметром M
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureParametered(Bound_Rib rib, PointD pointM, Function function, double a)
        {
            if (rib.Rib_Position == Rib_Position.Vertical && pointM.X == rib.Point1.X && pointM.X == rib.Point2.X) return 0;
            PointD MiddlePoint = rib.GetMiddleOfRib();
            double result;
            result = rib.LenthElement / 6 * (function(rib.Point1, pointM, rib, a) + 4 * function(MiddlePoint, pointM, rib, a) + function(rib.Point2, pointM, rib, a));
            return result;
        }
        /// <summary>
        /// Квадратурная формула Симпсона интегрирования на отрезке, содержащем особую точку
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(особая точка)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="specialPoint">Расположение особой точки на отрезке</param>
        /// <returns></returns>
        public static double SimpsonImproperQuadrature(Bound_Rib rib, PointD pointM, Function function, SpecialPoint specialPoint, double a)
        {
            PointD MiddlePoint;
            Bound_Rib rib1, rib2;
            double result = 0;
            switch (specialPoint)
            {
                case SpecialPoint.Point_1:
                    {
                        rib1 = new Bound_Rib(new PointD(rib.Point1.X * 1.1, rib.Point1.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, a);
                        break;
                    }
                case SpecialPoint.Point_Middle:
                    {
                        MiddlePoint = rib.GetMiddleOfRib();
                        rib1 = new Bound_Rib(rib.Point1, new PointD(MiddlePoint.X * 0.9, MiddlePoint.Y * 0.9));
                        rib2 = new Bound_Rib(new PointD(MiddlePoint.X * 1.1, MiddlePoint.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, a) + SimpsonQuadratureParametered(rib2, pointM, function, a);
                        break;
                    }
                case SpecialPoint.Point_2:
                    {
                        rib1 = new Bound_Rib(rib.Point1, new PointD(rib.Point2.X * 0.9, rib.Point2.Y * 0.9));
                        result = SimpsonQuadratureParametered(rib1, pointM, function, a);
                        break;
                    }
            }
            return result;
        }
        

            /// <summary>
            /// Квадратурная формула Симпсона интегрирования на отрезке с измельчением шага, содержащем особую точку(для потенциала)
            /// </summary>
            /// <param name="rib">Отрезок интегрирования</param>
            /// <param name="pointM">Точка наблюдения(особая точка)</param>
            /// <param name="function">Подынтегральная функция</param>
            /// <param name="specialPoint">Расположение особой точки на отрезке</param>
            /// <returns></returns>
        public static double SimpsonImproperQuadrature(Bound_Rib rib, PointD pointM, UnderIntegralFunction function, SpecialPoint specialPoint, Interrupt_Conditions interrupt)
        {
            PointD MiddlePoint;
            Bound_Rib rib1, rib2;
            double result = 0;
            switch (specialPoint)
            {
                case SpecialPoint.Point_1:
                    {
                        rib1 = new Bound_Rib(new PointD(rib.Point1.X * 1.1, rib.Point1.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt);
                        break;
                    }
                case SpecialPoint.Point_Middle:
                    {
                        MiddlePoint = rib.GetMiddleOfRib();
                        rib1 = new Bound_Rib(rib.Point1, new PointD(MiddlePoint.X * 0.9, MiddlePoint.Y * 0.9));
                        rib2 = new Bound_Rib(new PointD(MiddlePoint.X * 1.1, MiddlePoint.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt) + SimpsonQuadratureParametered(rib2, pointM, function, interrupt);
                        break;
                    }
                case SpecialPoint.Point_2:
                    {
                        rib1 = new Bound_Rib(rib.Point1, new PointD(rib.Point2.X * 0.9, rib.Point2.Y * 0.9));
                        result = SimpsonQuadratureParametered(rib1, pointM, function, interrupt);
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// Квадратурная формула Симпсона интегрирования непрерывной функции с параметром M c измельчением шага интегрирования(для искомых величин: потенциала, индукции)
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureParametered(Bound_Rib rib, PointD pointM, UnderIntegralFunction function, Interrupt_Conditions interrupt)
        {
            Interrupt_Conditions interrupt_Conditions;

            int j = interrupt.CountOfIterations;
            int i = interrupt.i;
            double epsilon = interrupt.Epsilon;
            double max_epsilon = interrupt.MaxEpsilon;

            PointD MiddlePoint = rib.GetMiddleOfRib();
            Bound_Rib rib1, rib2, rib3, rib4, rib5, rib6;
            double result1, result;
            PointD rib1Mid, rib2Mid;

            result1 = rib.LenthElement / 6 * (function(rib.Point1, pointM, rib) + 4 * function(MiddlePoint, pointM, rib) + function(rib.Point2, pointM, rib));
            rib1 = new Bound_Rib(rib.Point1, MiddlePoint);
            rib2 = new Bound_Rib(MiddlePoint, rib.Point2);

            result = rib1.LenthElement / 6 * (function(rib1.Point1, pointM, rib) + 4 * function(rib1.GetMiddleOfRib(), pointM, rib) + function(rib1.Point2, pointM, rib)) +
                      rib2.LenthElement / 6 * (function(rib2.Point1, pointM, rib) + 4 * function(rib2.GetMiddleOfRib(), pointM, rib) + function(rib2.Point2, pointM, rib));

            if (Math.Abs((result1 - result) / result) < epsilon)
                return result;

            rib1Mid = rib1.GetMiddleOfRib();
            rib2Mid = rib2.GetMiddleOfRib();
            i--;
            if (i > 0)
            {
                interrupt_Conditions.i = i;
                interrupt_Conditions.CountOfIterations = j;
                interrupt_Conditions.Epsilon = epsilon;
                interrupt_Conditions.MaxEpsilon = max_epsilon;
                rib3 = new Bound_Rib(rib.Point1, rib1Mid);
                rib4 = new Bound_Rib(rib1Mid, MiddlePoint);
                rib5 = new Bound_Rib(MiddlePoint, rib2Mid);
                rib6 = new Bound_Rib(rib2Mid, rib.Point2);
                result = SimpsonQuadratureParametered(rib3, pointM, function, interrupt_Conditions) + SimpsonQuadratureParametered(rib4, pointM, function, interrupt_Conditions) +
                          SimpsonQuadratureParametered(rib5, pointM, function, interrupt_Conditions) + SimpsonQuadratureParametered(rib6, pointM, function, interrupt_Conditions);
            }
            epsilon += 0.01;
            i = j;
            if (epsilon <= max_epsilon)
            {
                interrupt_Conditions.i = i;
                interrupt_Conditions.CountOfIterations = j;
                interrupt_Conditions.Epsilon = epsilon;
                interrupt_Conditions.MaxEpsilon = max_epsilon;
                result = SimpsonQuadratureParametered(rib, pointM, function, interrupt_Conditions);
            }

            return result;
        }

        /// <summary>
        /// Квадратурная формула Симпсона интегрирования на отрезке с измельчением шага, содержащем особую точку(для потенциала)
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(особая точка)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="specialPoint">Расположение особой точки на отрезке</param>
        /// <returns></returns>
        public static double SimpsonImproperQuadrature(Bound_Rib rib, PointD pointM, UnderIntegralFunctionDesired function, SpecialPoint specialPoint, double Density)
        {
            PointD MiddlePoint;
            Bound_Rib rib1, rib2;
            double result = 0;
            switch (specialPoint)
            {
                case SpecialPoint.Point_1:
                    {
                        rib1 = new Bound_Rib(new PointD(rib.Point1.X * 1.1, rib.Point1.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, Density);
                        break;
                    }
                case SpecialPoint.Point_Middle:
                    {
                        MiddlePoint = rib.GetMiddleOfRib();
                        rib1 = new Bound_Rib(rib.Point1, new PointD(MiddlePoint.X * 0.9, MiddlePoint.Y * 0.9));
                        rib2 = new Bound_Rib(new PointD(MiddlePoint.X * 1.1, MiddlePoint.Y * 1.1), rib.Point2);
                        result = SimpsonQuadratureParametered(rib1, pointM, function, Density) + SimpsonQuadratureParametered(rib2, pointM, function, Density);
                        break;
                    }
                case SpecialPoint.Point_2:
                    {
                        rib1 = new Bound_Rib(rib.Point1, new PointD(rib.Point2.X * 0.9, rib.Point2.Y * 0.9));
                        result = SimpsonQuadratureParametered(rib1, pointM, function, Density);
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// Квадратурная формула Симпсона интегрирования непрерывной функции с параметром M интегрирования(для искомых величин: потенциала, индукции)
        /// </summary>
        /// <param name="rib">Отрезок интегрирования</param>
        /// <param name="pointM">Точка наблюдения(параметр)</param>
        /// <param name="function">Подынтегральная функция</param>
        /// <param name="interrupt">Условия прерывания </param>
        /// <returns></returns>
        public static double SimpsonQuadratureParametered(Bound_Rib rib, PointD pointM, UnderIntegralFunctionDesired function, double Density)
        {
            PointD MiddlePoint = rib.GetMiddleOfRib();
            double result;
            result = rib.LenthElement / 6 * (function(rib.Point1, pointM, rib, Density) + 4 * function(MiddlePoint, pointM, rib, Density) + function(rib.Point2, pointM, rib, Density));
           
            return result;
        }
    }

    
}
