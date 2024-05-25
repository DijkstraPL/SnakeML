using SnakeMLDesktop.Misc;

namespace SnakeMLDesktop.NeuralNet
{

    namespace GameEngine
    {
        // DrawableVision class
        public class DrawableVision
        {
            public Point wall_location;
            public Point apple_location;
            public Point self_location;

            public DrawableVision(Point wall_location, Point apple_location, Point self_location)
            {
                this.wall_location = wall_location;
                this.apple_location = apple_location;
                this.self_location = self_location;
            }
        }
    }
}
