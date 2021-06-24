using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Figures
{
    public class Poligon : IFigure
    {

        
        private List<Rib> ribs;
        public List<Rib> Ribs
        {
            get
            {
                return ribs;
            }
            set
            {
                ribs = value;
            }
        }
        double perimeter;
        public Poligon(List<Rib> ribs)
        {
            this.ribs = ribs;
            perimeter = GetPerimeter();
        }


        private float eps = 1e-9f;

        public FigureType GetFigureType()
        {
            return FigureType.Line;
        }

        public double GetPerimeter()
        {
            double length = 0;
            for (int i = 0; i < ribs.Count; i++)
            {
                length += ribs[i].LengthOfElement;
            }
            return length;
        }

        public double GetSquare()
        {
            throw new NotImplementedException();
        }

        public bool IsContaisPoint(PointD point)
        {
            PointPosition pointPosition;
            for (int i = 0; i < ribs.Count; i++)
            {
                pointPosition = ribs[i].Classify(point);
                if (pointPosition == PointPosition.LEFT)
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        public bool IsPointOnBorder(PointD point)
        {
            PointPosition pointPosition;
            for (int i = 0; i < ribs.Count; i++)
            {
                pointPosition = ribs[i].Classify(point);
                if (pointPosition == PointPosition.ORIGIN ||
                    pointPosition == PointPosition.DESTINATION ||
                    pointPosition == PointPosition.BETWEEN)
                    return true;
            }
            return false;
        }

        public bool IsPointOnBorder(PointD point, float epsilon)
        {
            float old_eps = eps;
            eps = epsilon;
            bool result = IsPointOnBorder(point);
            eps = old_eps;
            return result;
        }

        public List<Rib> SplitBorder(int n)
        {
            Rib rib;
            List<Rib> smallRibs = new List<Rib>(n);
            int currentNum = 0;
            for(int i = 0; i < ribs.Count - 1; i++)
            {
                rib = ribs[i];
                currentNum = n * Convert.ToInt32((rib.LengthOfElement / perimeter));
                smallRibs.AddRange(rib.GetSubRibs(currentNum));
            }
            currentNum = n - smallRibs.Count;
            smallRibs.AddRange(ribs.Last().GetSubRibs(currentNum));
            return smallRibs;
        }
    }
}
