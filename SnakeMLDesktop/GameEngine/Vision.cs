namespace SnakeMLDesktop.NeuralNet
{

    namespace GameEngine
    {
        // Vision class
        public class Vision
        {
            public float dist_to_wall;
            public float dist_to_apple;
            public float dist_to_self;

            public Vision(float dist_to_wall, float dist_to_apple, float dist_to_self)
            {
                this.dist_to_wall = dist_to_wall;
                this.dist_to_apple = dist_to_apple;
                this.dist_to_self = dist_to_self;
            }
        }
    }
}
