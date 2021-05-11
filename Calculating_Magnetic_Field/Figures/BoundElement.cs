using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{
    public class ElementBasisFunction
    {

        Bound_Rib Rib { get; set; }

        PointD Point1 { get; set; }
        PointD Point2 { get; set; }

        double P1;
        double P2;
        //Коэффициенты i-й базисной функции
        double a1, b1;
        //Коэффициенты (i+1)-й базисной функции
        double a2, b2;
        double determinant;
        public ElementBasisFunction(Bound_Rib rib)
        {
            determinant = rib.Point1.X * rib.Point2.Y - rib.Point2.X * rib.Point1.Y;
            a1 = rib.Point2.Y / determinant;
            b1 = -rib.Point2.X / determinant;
            a2 = -rib.Point1.Y / determinant;
            b2 = rib.Point1.X / determinant;
        }

        public ElementBasisFunction(PointD point1, PointD point2)
        {
            determinant = point1.X * point2.Y - point2.X * point1.Y;
            a1 = point2.Y / determinant;
            b1 = -point2.X / determinant;
            a2 = -point1.Y / determinant;
            b2 = point1.X / determinant;
        }

        public double Psi_i_1(double x, double y)
        {
            return a1 * x + b1 * y;
        }

        public double Psi_i_2(double x, double y)
        {
            return a2 * x + b2 * y;
        }

        public double Psi(double x, double y)
        {
            return Psi_i_1(x, y) + Psi_i_2(x, y);
        }

        public void SetPotencail(double P1, double P2)
        {
            this.P1 = P1;
            this.P2 = P2;
        }

        public Vector2D CalculateInductionFromScalarPotencial()
        {
            Vector2D result;
            result.X_component = -(P1 * a1 + P2 * a2);
            result.Y_component = -(P1 * b1 + P2 * b2);
            return result;
        }

        public Vector2D CalculateInductionFromVectorPotencial()
        {
            Vector2D result;
            result.X_component = (P1 * b1 + P2 * b2);
            result.Y_component = -(P1 * a1 + P2 * a2);
            return result;
        }

    }
}

