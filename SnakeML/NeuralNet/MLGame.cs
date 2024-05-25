using SnakeML.GameEngine;

namespace SnakeML.NeuralNet
{
    internal class MLGame : Game
    {
        public long Fitness => _fitness;

        private readonly NeuralNet _brain;
        private long _fitness;

        public MLGame()
        {
            _brain = new NeuralNet(24, 18, 4);
        }
        public MLGame(NeuralNet brain)
        {
            _brain = brain;
        }

        public MLGame Clone()
        {
            var game = new MLGame(_brain);
            return game;
        }

        public void Mutate(double mutateRate)
        {
            _brain.Mutate(mutateRate);
        }

        internal void CalculateFitness()
        {
            if (Snake.Length < 10)
                _fitness = (long)Math.Floor(LifeTime * LifeTime * Math.Pow(Snake.Length * 100, 2));
            else
            {
                _fitness = (long)(LifeTime * LifeTime *
                    Math.Pow(1000, 2) * (Snake.Length - 9) * 100);
            }
        }

        public string Save()
        {
            return _brain.NetToTable();
        }
        public void Load(string brainData)
        {
             _brain.TableToNet(brainData);
        }
        internal MLGame CrossOver(MLGame partner)
        {
            var newBrain = _brain.Crossover(partner._brain);
            var child = new MLGame(newBrain);
            return child;
        }

        public double[] GetVision()
        {
            double[] vision = new double[24];

            double[] tempValues = LookInDirection(-1,0);
            vision[0] = tempValues[0];
            vision[1] = tempValues[1];
            vision[2] = tempValues[2];
            //look left/up  
            tempValues = LookInDirection(-1,-1);
            vision[3] = tempValues[0];
            vision[4] = tempValues[1];
            vision[5] = tempValues[2];
            //look up
            tempValues = LookInDirection(0,-1);
            vision[6] = tempValues[0];
            vision[7] = tempValues[1];
            vision[8] = tempValues[2];
            //look up/right
            tempValues = LookInDirection(1,-1);
            vision[9] = tempValues[0];
            vision[10] = tempValues[1];
            vision[11] = tempValues[2];
            //look right
            tempValues = LookInDirection(1,0);
            vision[12] = tempValues[0];
            vision[13] = tempValues[1];
            vision[14] = tempValues[2];
            //look right/down
            tempValues = LookInDirection(1,1);
            vision[15] = tempValues[0];
            vision[16] = tempValues[1];
            vision[17] = tempValues[2];
            //look down
            tempValues = LookInDirection(0,1);
            vision[18] = tempValues[0];
            vision[19] = tempValues[1];
            vision[20] = tempValues[2];
            //look down/left
            tempValues = LookInDirection(-1,1);
            vision[21] = tempValues[0];
            vision[22] = tempValues[1];
            vision[23] = tempValues[2];

            return vision;

            //double[] vision = new double[(Width + 2) * (Height + 2)];
            //int index = 0;
            //for (int i = -1; i < Width + 1; i++)
            //{
            //    for (int j = -1; j < Height + 1; j++)
            //    {
            //        if(i == -1 || j == -1 || i == Width || j == Height)
            //            vision[index] = -1;
            //        else if (Snake.TailIsAt(i, j))
            //            vision[index] = -1;
            //        else if (Snake.HeadIsAt(i, j))
            //            vision[index] = 1;
            //        else if (Food.IsAt(i, j))
            //            vision[index] = 2;
            //        else
            //            vision[index] = 0;
            //        index++;
            //    }
            //}
            //return vision;
        }

        private double[] LookInDirection(int x, int y)
        {
            double[] visionInDirection = new double[3];
            Vector position = new Vector(Snake.HeadPosition.X, Snake.HeadPosition.Y);
            bool  foodIsFound = false;
            bool  tailIsFound = false; 
            double distance = 0;

            position.Add(new Vector(x,y));
            distance += 1;

            //look in the direction until you reach a wall
            while (!(position.X < 0 || position.Y < 0 || position.X >= Width || position.Y >= Height))
            {

                //check for food at the position
                if (!foodIsFound && position.X == Food.X && position.Y == Food.Y)
                {
                    visionInDirection[0] = 1;
                    foodIsFound = true; // dont check if food is already found
                }

                //check for tail at the position
                if (!tailIsFound && Snake.TailIsAt(position.X, position.Y))
                {
                    visionInDirection[1] = 1 / distance;
                    tailIsFound = true; // dont check if tail is already found
                }

                //look further in the direction
                position.Add(new Vector(x, y));
                distance += 1;
            }

            //set the distance to the wall
            visionInDirection[2] = 1 / distance;

            return visionInDirection;
        }

        public void Decide()
        {
            var vision = GetVision();
            var decision = _brain.Output(vision);

            double max = 0;
            int maxIndex = 0;
            for (int i = 0; i < decision.Length; i++)
            {
                if (max < decision[i])
                {
                    max = decision[i];
                    maxIndex = i;
                }
            }

            if (maxIndex == 0)
                Snake.SetDirection(Direction.Left);
            else if (maxIndex == 1)
                Snake.SetDirection(Direction.Down);
            else if (maxIndex == 2)
                Snake.SetDirection(Direction.Right);
            else if (maxIndex == 3)
                Snake.SetDirection(Direction.Up);
        }
    }
}