using System;

namespace Calculating_Magnetic_Field
{
    public class Trigonometry
    {
        /// <summary>
        /// Вычисление полярного угла точки относительно начала координат
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Возвращает угол A: -pi/2 <= A <= 3*pi/2</returns>
        public static double Angle(double x, double y)
        {
            double r = Math.Sqrt(x * x + y * y);
            double result = Math.Asin(y / r);
            if (x < 0)
            {
                return Math.PI - result;
            }
            return result;
        }



        /// <summary>
        /// Вычисление полярного угла точки относительно точки (x0, y0)
        /// </summary>
        /// <param name="x0">Координата X точки отсчета</param>
        /// <param name="y0">Координата Y точки отсчета</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Возвращает угол A: -pi/2 <= A <= 3*pi/2</returns>
        public static double Angle(double x0, double y0, double x, double y)
        {
            double dx = x - x0;
            double dy = y - y0;
            double r = Math.Sqrt(dx * dx + dy * dy);
            double result = Math.Asin(dy / r);
            if (dx < 0)
            {
                return Math.PI - result;
            }
            return result;
        }


        /// <summary>
        /// Вычисление полярного угла точки относительно точки
        /// </summary>
        /// <param name="p0">Точка начала отсчета</param>
        /// <param name="point">Точка</param>
        /// <returns>Возвращает угол A: -pi/2 <= A <= 3*pi/2</returns>
        public static double Angle(PointD p0, PointD point)
        {
            double dx = point.X - p0.X;
            double dy = point.Y - p0.Y;
            double r = Math.Sqrt(dx * dx + dy * dy);
            double result = Math.Asin(dy / r);
            if (dx < 0)
            {
                return Math.PI - result;
            }
            return result;
        }
    }
}
