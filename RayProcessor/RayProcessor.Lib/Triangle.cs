namespace RayProcessor.Lib
{
    public class Triangle
    {
        private Point vertex1;
        private Point vertex2;
        private Point vertex3;
        public Point Normal { get; private set; }

        public Triangle(Point v1, Point v2, Point v3)
        {
            vertex1 = v1;
            vertex2 = v2;
            vertex2 = v3;
        }

        public bool IsCrossesTriangle(Ray ray)
        {
            return true;
        }

        private Point CrossProduct(Point vector1, Point vector2) //векторний добуток
        {
            double x = vector1.y * vector2.z - vector2.z * vector1.y;
            double y = (-1)*(vector1.x * vector2.z - vector2.z * vector1.x);
            double z = vector1.x * vector2.y - vector2.y * vector1.x;
            return new Point(x, y, z);
        }
    }
}