

namespace RayProcessor.Lib
{
    public class Leaf : Node
    {
        public Triangle Triangle => _triangle;

        private readonly Branch mother;
        private Triangle _triangle;

        public Leaf(Branch mother, Triangle triangle)
        {
            this.mother = mother;
            _triangle = triangle;
        }

        public override string GetNodeType()
        {
            return "Leaf";
        }
    }
}