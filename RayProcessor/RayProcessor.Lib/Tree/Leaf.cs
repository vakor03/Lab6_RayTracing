

namespace RayProcessor.Lib
{
    public class Leaf : Node
    {
        public Triangle Triangle => _triangle;

        private Branch _mother;
        private Triangle _triangle;

        public Leaf(Branch mother, Triangle triangle)
        {
            _mother = mother;
            _triangle = triangle;
        }

        public override string GetNodeType()
        {
            return "Leaf";
        }
    }
}