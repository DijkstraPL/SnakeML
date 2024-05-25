namespace SnakeML.GameEngine
{
    internal class Snake : BoardObject
    {
        public int Length => TailPositions.Count + 1;
        public Vector HeadPosition { get; private set; }
        public List<Vector> TailPositions { get; private set; }
        public Vector DirectionVector { get; private set; }

        public bool Alive { get; set; } = true;

        public Snake(int x, int y)
        {
            HeadPosition = new Vector(x, y);
            DirectionVector = new Vector(1, 0);
            TailPositions = new List<Vector>();
            TailPositions.Add(new Vector(x - 1, y));
        }

        private Snake(Snake snake)
        {
            HeadPosition = new Vector(snake.HeadPosition.X, snake.HeadPosition.Y);
            DirectionVector = new Vector(snake.DirectionVector.X, snake.DirectionVector.Y);
            TailPositions = new List<Vector>();
            foreach (var tailPosition in snake.TailPositions)
                TailPositions.Add(new Vector(tailPosition.X, tailPosition.Y));

            Alive = snake.Alive;
        }

        public void SetDirection(Direction direction)
        {


            DirectionVector = direction switch
            {
                Direction.Up when DirectionVector.Y != -1 => new Vector(0, 1),
                Direction.Down when DirectionVector.Y != 1 => new Vector(0, -1),
                Direction.Left when DirectionVector.X != 1 => new Vector(-1, 0),
                Direction.Right when DirectionVector.X != -1 => new Vector(1, 0),
                _ => DirectionVector
            };
        }

        public void Move()
        {
            int x = HeadPosition.X;
            int y = HeadPosition.Y;

            foreach (var tailPosition in TailPositions)
            {
                int tempX = tailPosition.X;
                int tempY = tailPosition.Y;
                tailPosition.X = x;
                tailPosition.Y = y;
                x = tempX;
                y = tempY;
            }
            HeadPosition.Add(DirectionVector);
        }

        internal override bool IsAt(int x, int y)
        {
            bool isTakenBySnake = HeadPosition.X == x && HeadPosition.Y == y
                || TailPositions.Any(tp => tp.X == x && tp.Y == y);
            return isTakenBySnake;
        }
        internal bool TailIsAt(int x, int y)
        {
            bool isTakenBySnake = TailPositions.Any(tp => tp.X == x && tp.Y == y);
            return isTakenBySnake;
        }
        internal bool HeadIsAt(int x, int y)
        {
            bool isTakenBySnake = HeadPosition.X == x && HeadPosition.Y == y;
            return isTakenBySnake;
        }

        internal void Grow()
        {
            int x = HeadPosition.X;
            int y = HeadPosition.Y;

            TailPositions.Insert(0, new Vector(x, y));
            HeadPosition.Add(DirectionVector);
        }

        internal override Snake Clone()
        {
            return new Snake(this);
        }
    }
}
