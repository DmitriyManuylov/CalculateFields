namespace Calculating_Magnetic_Field
{
    public interface IFigure
    {
        double GetPerimeter();

        double GetSquare();

        bool IsPointOnBorder(PointD point);

        bool IsContaisPoint(PointD point);


        FigureType GetFigureType();
    }
}
