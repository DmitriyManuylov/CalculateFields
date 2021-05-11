namespace Calculating_Magnetic_Field
{
    public struct Vector2D
    {
        public double X_component;
        public double Y_component;

        public static Vector2D operator +(Vector2D b1, Vector2D b2)
        {
            Vector2D ind;
            ind.X_component = b1.X_component + b2.X_component;
            ind.Y_component = b1.Y_component + b2.Y_component;
            return ind;
        }

        public static Vector2D operator -(Vector2D b1)
        {
            Vector2D ind;
            ind.X_component = -b1.X_component;
            ind.Y_component = -b1.Y_component;
            return ind;
        }
        public static Vector2D operator -(Vector2D b1, Vector2D b2)
        {
            Vector2D ind;
            ind.X_component = b1.X_component - b2.X_component;
            ind.Y_component = b1.Y_component - b2.Y_component;
            return ind;
        }

        public static Vector2D operator *(double c, Vector2D b2)
        {
            Vector2D ind;
            ind.X_component = c * b2.X_component;
            ind.Y_component = c * b2.Y_component;
            return ind;
        }

        public static Vector2D operator *(Vector2D b2, double c)
        {
            Vector2D ind;
            ind.X_component = c * b2.X_component;
            ind.Y_component = c * b2.Y_component;
            return ind;
        }

        public static Vector2D operator /(Vector2D b2, double c)
        {
            Vector2D ind;
            ind.X_component = b2.X_component / c;
            ind.Y_component = b2.Y_component / c;
            return ind;
        }

        public static double operator * (Vector2D vec1, Vector2D vec2)
        {
            return vec1.X_component * vec2.X_component + vec1.Y_component * vec2.Y_component;
        }

        public Vector2D GetMiddleValue(Vector2D b1, Vector2D b2)
        {
            return (b1 + b2) / 2;
        }

        public static double ScalarProduct(Vector2D vec1, Vector2D vec2)
        {
            return vec1.X_component * vec2.X_component + vec1.Y_component * vec2.Y_component;
        }
    }
}
