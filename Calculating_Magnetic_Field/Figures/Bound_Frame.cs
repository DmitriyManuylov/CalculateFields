using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Calculating_Magnetic_Field
{
    public class Bound_Frame: IFigure
    {

        /// <summary>
        /// Левый верхний угол внешнего прямоугольника
        /// </summary>
        public PointF Location1 { get; set; }

        /// <summary>
        /// Размер внешнего прямоугольника по оси Ox
        /// </summary>
        public float Lenth1 { get; set; }

        /// <summary>
        /// Размер внешнего прямоугольника по оси Oy
        /// </summary>
        public float Width1 { get; set; }

        /// <summary>
        /// Левый верхний угол внутреннего прямоугольника
        /// </summary>
        public PointF Location2 { get; set; }

        /// <summary>
        /// Размер внутреннего прямоугольника по оси Ox
        /// </summary>
        public float Lenth2 { get; set; }

        /// <summary>
        /// Размер внутреннего прямоугольника по оси Oy
        /// </summary>
        public float Width2 { get; set; }

        public Bound_Rectangle Outer_Rectangle;

        public Bound_Rectangle Inner_Rectangle;


        public Bound_Frame(PointF location1, 
                           PointF location2, 
                           float lenth1,
                           float lenth2,
                           float width1,
                           float width2)
        {
            Location1 = location1;
            Location2 = location2;
            Lenth1 = lenth1;
            Lenth2 = lenth2;
            Width1 = width1;
            Width2 = width2;
            Outer_Rectangle = new Bound_Rectangle(Location1, Lenth1, Width1);
            Inner_Rectangle = new Bound_Rectangle(Location2, Lenth2, Width2);
        }

        private double GetOuterPerimeter()
        {
            return 2 * (Lenth1 + Width1);
        }

        private double GetInnerPerimeter()
        {
            return 2 * (Lenth2 + Width2);
        }

        private double GetBigSquare()
        {
            return Lenth1 * Width1;
        }

        private double GetSmallSquare()
        {
            return Lenth2 * Width2;
        }

        public double GetPerimeter()
        {
            return GetInnerPerimeter() + GetOuterPerimeter();
        }

        public double GetSquare()
        {
            return GetBigSquare() - GetSmallSquare();
        }

        public FigureType GetFigureType()
        {
            throw new NotImplementedException();
        }

        public bool IsContaisPoint(PointD point)
        {
            throw new NotImplementedException();
        }

        public bool IsPointOnBorder(PointD point)
        {
            throw new NotImplementedException();
        }

        public bool IsPointOnBorder(PointD point, float eps)
        {
            throw new NotImplementedException();
        }
    }
}
