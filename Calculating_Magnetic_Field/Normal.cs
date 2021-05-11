using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{
    /// <summary>
    /// Хранит данные о нормали к граничному элементу
    /// </summary>
    public class Normal
    {
        /// <summary>
        /// Направляющий косинус угла с осью Ox
        /// </summary>
        public double CosAlpha { get; set; }
        /// <summary>
        /// Направляющий косинус угла с осью Oy
        /// </summary>
        public double CosBeta { get; set; }

        /// <summary>
        /// Расчёт проекций единичного вектора нормали, внешней к ограниченной области, на координатные оси
        /// </summary>
        /// <param name="bound_Point1">Первая точка граничного элемента</param>
        /// <param name="bound_Point2"></param>
        public Normal(PointD bound_Point1, PointD bound_Point2, double lenth)
        {
            CosAlpha = (bound_Point2.Y - bound_Point1.Y) / lenth;
            CosBeta = (bound_Point1.X - bound_Point2 .X) / lenth;
        }
    }
}
