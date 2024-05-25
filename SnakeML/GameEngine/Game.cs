namespace SnakeML.GameEngine
{
    //https://github.com/Code-Bullet/SnakeFusion/blob/master/SmartSnakesCombine/Food.pde
    internal class Game
    {
        public const int Width = 20;
        public const int Height = 10;

        public Snake Snake { get; }
        public Food Food { get; private set; }
        public Board Board { get; }
        public int LifeTime { get; private set; }
        public int TicksLeft { get; private set; } = Width * Height;


        public Game()
        {
            Snake = new Snake(Width / 2, Height / 2);
            Board = new Board(Width, Height);
            var foodPositon = GetRandomEmpty();
            Food = new Food(foodPositon.x, foodPositon.y);
            Board.Add(Food);
            Board.Add(Snake);
        }

        private Game(Game game)
        {
            Snake = game.Snake.Clone();
            Board = game.Board.Clone();
            Food = game.Food.Clone();
        }

        private (int x, int y) GetRandomEmpty()
        {
            var random = new Random();
            while (true)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (Board.IsFree(x, y))
                    return (x, y);
            }
        }

        public void Tick()
        {
            if (!Snake.Alive)
                return;
            LifeTime++;
            TicksLeft--;
            if (FoodCollide())
                EatFood();
            if (SnakeCollide())
                Snake.Alive = false;
            else
                Snake.Move();

            if (TicksLeft < 0)
                Snake.Alive = false;

        }

        private bool SnakeCollide()
        {
            return Snake.TailPositions.Any(tp => tp.X == Snake.HeadPosition.X + Snake.DirectionVector.X &&
             tp.Y == Snake.HeadPosition.Y + Snake.DirectionVector.Y) || 
             Snake.HeadPosition.X > Width -1 || Snake.HeadPosition.X < 0 ||
             Snake.HeadPosition.Y > Height - 1 || Snake.HeadPosition.Y < 0;
        }

        private void EatFood()
        {
            TicksLeft += Width * Height ;
            Snake.Grow();
            var foodPositon = GetRandomEmpty();
            Food = new Food(foodPositon.x, foodPositon.y);
        }

        private bool FoodCollide()
        {
            return Snake.HeadPosition.X + Snake.DirectionVector.X == Food.X &&
                 Snake.HeadPosition.Y + Snake.DirectionVector.Y == Food.Y;
        }

        public Game Clone(Game game)
        {
            return new Game(game);
        }
    }
}
