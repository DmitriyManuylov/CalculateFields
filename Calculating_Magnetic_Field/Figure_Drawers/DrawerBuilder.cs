using Calculating_Magnetic_Field.Figures;
using Calculating_Magnetic_Field.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculating_Magnetic_Field.Figure_Drawers
{
    public static class DrawerBuilder
    {
        public static IDrawable BuildDrawer(PictureBox pictureBox1, object obj, float DrawingScale)
        {
            IDrawable result = null;
            if (obj is ISource source)
            {
                switch (source.GetSourceType())
                {
                    case SourceTypes.VolumeSource:
                        {
                            switch (source.GetFigureType())
                            {
                                case FigureType.Rectangle:
                                    {
                                        result = new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle((Bound_Rectangle)source.GetFigure(), DrawingScale));
                                        break;
                                    }
                                case FigureType.Circle:
                                    {
                                        result = new CircleObjectDrawer(pictureBox1, new Bound_Circle((Bound_Circle)source.GetFigure(), DrawingScale));
                                        break;
                                    }

                            }
                            break;
                        }
                    case SourceTypes.ResidualIntensitySource:
                        {
                            switch (source.GetFigureType())
                            {
                                case FigureType.Rectangle:
                                    {
                                        result = new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle((Bound_Rectangle)source.GetFigure(), DrawingScale));
                                        break;
                                    }
                                case FigureType.Circle:
                                    {
                                        result = new CircleObjectDrawer(pictureBox1, new Bound_Circle((Bound_Circle)source.GetFigure(), DrawingScale));
                                        break;
                                    }

                            }
                            break;
                        }
                    case SourceTypes.LinearSource:
                        {
                            result = new LineDrawer(pictureBox1, ((ILinearSource)source).Rib, DrawingScale);
                            break;
                        }
                    case SourceTypes.PointSource:
                        {
                            result = new PointDrawer(pictureBox1, ((IPointSource)source).Location, DrawingScale);
                            break;
                        }
                }

            }
            if(obj is Bound bound)
            {
                switch (bound.FigureType)
                {
                    case FigureType.Rectangle:
                        {
                            result = new RectangleObjectDrawer(pictureBox1, new Bound_Rectangle((Bound_Rectangle)bound.ThisFigure, DrawingScale));
                            break;
                        }
                    case FigureType.Circle:
                        {
                            result = new CircleObjectDrawer(pictureBox1, new Bound_Circle((Bound_Circle)bound.ThisFigure, DrawingScale));
                            break;
                        }
                    case FigureType.Stadium:
                        {
                            result = new StadiumBorderDrawer(pictureBox1, new Bound_Stadium((Bound_Stadium)bound.ThisFigure, DrawingScale));
                            break;
                        }

                }
            }
            return result;
        }
    }
}
