namespace Calculating_Magnetic_Field
{
    public interface IFigure
    {
        double GetPerimeter();

        double GetSquare();

        bool IsPointOnBorder(PointD point);

        bool IsPointOnBorder(PointD point, float eps);

        bool IsContaisPoint(PointD point);


        FigureType GetFigureType();
    }
}
