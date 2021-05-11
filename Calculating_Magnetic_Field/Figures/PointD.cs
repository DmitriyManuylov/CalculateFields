using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calculating_Magnetic_Field
{
    /// <summary>
    /// Точка разбиения границы
    /// </summary>
    public struct PointD
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y { get; set; }

        public PointD(double x, double y)
        { this.X = x; this.Y = y; }

        public PointD(PointD point)
        { this.X = point.X; this.Y = point.Y; }

        public void GetFrom_PointF(PointF pointF)
        {
            X = Convert.ToDouble(pointF.X);
            Y = Convert.ToDouble(pointF.Y);
        }

        public double DistanceToOtherPoint(PointD point)
        {
            return Math.Sqrt((point.X - X) * (point.X - X) + (point.Y - Y) * (point.Y - Y));
        }
    }
}
