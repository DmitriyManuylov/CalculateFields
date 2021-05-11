using System.Drawing;

namespace Calculating_Magnetic_Field.Figures
{
    /// <summary>
    /// Задает метод рисования
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Метод рисования фигуры
        /// </summary>
        /// <param name="graphics">Ссылка на поверхность для рисования</param>
        void Draw(Graphics graphics);
    }
}
