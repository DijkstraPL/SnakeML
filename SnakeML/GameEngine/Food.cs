namespace SnakeML.GameEngine
{
    internal class Food : BoardObject
    {
        public int X { get; }
        public int Y { get; }

        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal override bool IsAt(int x, int y)
        {
            bool isTakenByFood = X == x && Y == y;
            return isTakenByFood;
        }

        internal override Food Clone()
        {
            return new Food(X, Y);
        }
    }
}
