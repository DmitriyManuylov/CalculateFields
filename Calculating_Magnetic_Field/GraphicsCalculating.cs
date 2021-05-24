using Calculating_Magnetic_Field.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field
{
    public class GraphicsCalculating
    {
        private Bound_Rib rib;

        private Vector2D normal;

        private Vector2D tangent;
        private int n;
        private IModel model;

        private List<PointD> points;
        private PointD startPoint;

        private List<double> function;

        List<double> L;
        public GraphicsCalculating(IModel model)
        {
            this.model = model;
        }

        public List<PointD> GetPoints()
        {
            return points;
        }

        public List<double> GetLenth()
        {
            return L;
        }

        float eps = 1e-9f;
        
        public List<double> Calculate(PointD p1, PointD p2, int n, GraphicTypes graphicTypes)
        {
            rib = new Bound_Rib(p1, p2);
            normal = new Vector2D
            {
                X_component = -rib.Normal.CosAlpha,
                Y_component = -rib.Normal.CosBeta
            };

            tangent = new Vector2D
            {
                X_component = (p2.X - p1.X) / rib.LengthElement,
                Y_component = (p2.Y - p1.Y) / rib.LengthElement
            };

            this.n = n;
            double dx = (p2.X - p1.X) / (n - 1);
            double dy = (p2.Y - p1.Y) / (n - 1);

            points = new List<PointD>(n);
            function = new List<double>(n);
            L = new List<double>(n);
            for (int i = 0; i < n; i++)
            {
                points.Add(new PointD(p1.X + i * dx, p1.Y + i * dy));
            }
            startPoint = points[0];
            for(int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < model.Bounds.Count; j++)
                {
                    if (model.Bounds[j].IsPointOnBorder(points[i], eps))
                    {
                        points.RemoveAt(i);
                        break;
                    }
                }
            }
            n = points.Count;
            this.n = n;
            for (int i = 0; i < n; i++)
            {
                L.Add(points[i].DistanceToOtherPoint(startPoint));
            }
            function = new List<double>(n);
            switch (graphicTypes)
            {
                case GraphicTypes.InductionModul:
                    {
                        CalculateInductionModul();
                        break;
                    }
                case GraphicTypes.Induction_Normal_component:
                    {
                        CalculateInduction_Normal_component();
                        break;
                    }
                case GraphicTypes.Induction_Tangencial_component:
                    {
                        CalculateInduction_Tangencial_component();
                        break;
                    }
                case GraphicTypes.Induction_X_component:
                    {
                        CalculateInduction_X_component();
                        break;
                    }
                case GraphicTypes.Induction_Y_component:
                    {
                        CalculateInduction_Y_component();
                        break;
                    }
                case GraphicTypes.IntensityModul:
                    {
                        CalculateIntensityModul();
                        break;
                    }
                case GraphicTypes.Intensity_Normal_component:
                    {
                        CalculateIntensity_Normal_component();
                        break;
                    }
                case GraphicTypes.Intensity_Tangencial_component:
                    {
                        CalculateIntensity_Tangencial_component();
                        break;
                    }
                case GraphicTypes.Intensity_X_component:
                    {
                        CalculateIntensity_X_component();
                        break;
                    }
                case GraphicTypes.Intensity_Y_component:
                    {
                        CalculateIntensity_Y_component();
                        break;
                    }
                case GraphicTypes.Potencial:
                    {
                        CalculatePotencial();
                        break;
                    }
                case GraphicTypes.CurrentModul:
                    {
                        CalculateInductionModul();
                        break;
                    }
                case GraphicTypes.Current_X_component:
                    {
                        CalculateInduction_X_component();
                        break;
                    }
                case GraphicTypes.Current_Y_component:
                    {
                        CalculateInduction_Y_component();
                        break;
                    }
            }
            return function;
        }

        private List<double> CalculateInductionModul()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateInduction(points[i]);
                function.Add(Math.Sqrt(vec.X_component * vec.X_component + vec.Y_component * vec.Y_component));
            }
            return function;
        }

        private List<double> CalculateInduction_Normal_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateInduction(points[i]);
                function.Add(vec.X_component*normal.X_component+vec.Y_component*normal.Y_component);
            }
            return function;
        }

        private List<double> CalculateInduction_Tangencial_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateInduction(points[i]);
                function.Add(vec.X_component * tangent.X_component + vec.Y_component * tangent.Y_component);
            }
            return function;
        }

        private List<double> CalculateInduction_X_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateInduction(points[i]);
                function.Add(vec.X_component);
            }
            return function;
        }

        private List<double> CalculateInduction_Y_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateInduction(points[i]);
                function.Add(vec.Y_component);
            }
            return function;
        }

        private List<double> CalculateIntensityModul()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateIntensity(points[i]);
                function.Add(Math.Sqrt(vec.X_component * vec.X_component + vec.Y_component * vec.Y_component));
            }
            return function;
        }

        private List<double> CalculateIntensity_Normal_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateIntensity(points[i]);
                function.Add(vec.X_component * normal.X_component + vec.Y_component * normal.Y_component);
            }
            return function;
        }

        private List<double> CalculateIntensity_Tangencial_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateIntensity(points[i]);
                function.Add(vec.X_component * tangent.X_component + vec.Y_component * tangent.Y_component);
            }
            return function;
        }

        private List<double> CalculateIntensity_X_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateIntensity(points[i]);
                function.Add(vec.X_component);
            }
            return function;
        }

        private List<double> CalculateIntensity_Y_component()
        {
            Vector2D vec;
            for (int i = 0; i < n; i++)
            {
                vec = model.CalculateIntensity(points[i]);
                function.Add(vec.Y_component);
            }
            return function;
        }

        private List<double> CalculatePotencial()
        {
            for (int i = 0; i < n; i++)
            {
                function.Add(model.CalculatePotencial(points[i]));
            }
            return function;
        }
    }
}
