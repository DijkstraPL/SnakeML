namespace SnakeML.GameEngine
{
    internal class Vector
    {
        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        internal void Add(Vector directionVector)
        {
            X += directionVector.X;
            Y += directionVector.Y;
        }
    }
}
