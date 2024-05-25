namespace SnakeML.NeuralNet
{
    internal class Population
    {
        public int BestLength => _currentBestLength;
        public long BestFitness => _currentBestFitness;
        public int Generation => _generation;
        public MLGame[] Games => _games;
        private MLGame[] _games;
        private MLGame _globalBestGame;
        private readonly double _globalMutationRate = 0.01;
        private int _currentBestLength = 2;
        private long _currentBestFitness = 0;
        private int _generation = 1;

        public Population(int size)
        {
            _games = new MLGame[size];

            for (int i = 0; i < _games.Length; i++)
                _games[i] = new MLGame();

            _globalBestGame = _games[0].Clone();
        }

        public Population(int size, MLGame best)
        {
            _games = new MLGame[size];
            for (int i = 0; i < _games.Length; i++)
            {
                _games[i] = best.Clone();
                _games[i].Mutate(_globalMutationRate);
            }

            _globalBestGame = best.Clone();
        }

        public void TickAll()
        {
            Parallel.ForEach(_games, game =>
            {
                if (game.Snake.Alive)
                {
                    game.Decide();
                    game.Tick();
                }
            });
            SetBestGame();
        }

        public void Mutate()
        {
            foreach (var game in _games)
            {
                game.Mutate(_globalMutationRate);
            }
        }

        public bool IsDone()
        {
            foreach (var game in _games)
                if (game.Snake.Alive)
                    return false;
            return true;
        }

        public void CalculateFitness()
        {
            foreach (var game in _games)
                game.CalculateFitness();
        }

        public void NaturalSelection()
        {
            MLGame[] newGames = new MLGame[_games.Length];

            SetBestGame();

            newGames[0] = _globalBestGame.Clone();
            for (int i = 1; i < newGames.Length; i++)
            {
                MLGame parent1 = SelectGame();
                MLGame parent2 = SelectGame();

                MLGame child = parent1.CrossOver(parent2);

                child.Mutate(_globalMutationRate);

                newGames[i] = child;
            }

            _games = (MLGame[])newGames.Clone();

            _generation++;
        }

        private MLGame SelectGame()
        {
            long fitnessSum = 0;
            for (int i = 0; i < _games.Length; i++)
                fitnessSum += _games[i].Fitness;

            var random = new Random();
            long randomValue = random.NextInt64(0, fitnessSum);

            long runningSum = 0;

            for (int i = 0; i < _games.Length; i++)
            {
                runningSum += _games[i].Fitness;
                if (runningSum > randomValue)
                    return _games[i];
            }

            return _games[0];
        }

        private void SetBestGame()
        {
            if (!IsDone())
                return;

            long maxFitness = 0;
            int maxLength = 0;
            int maxIndex = 0;

            int index = 0;
            foreach (var game in _games)
            {
                if (game.Fitness > maxFitness)
                {
                    maxFitness = game.Fitness;
                    maxIndex = index;
                }
                if (game.Snake.Length > maxLength)
                    maxLength = game.Snake.Length;
                index++;
            }
            if (maxFitness > _currentBestFitness)
            {
                _currentBestFitness = maxFitness;
                _globalBestGame = _games[maxIndex];
            }
            if (maxLength > _currentBestLength)
                _currentBestLength = maxLength;
        }
        //public Population(MLGame best)
        //{
        //    _games = new MLGame[2000];
        //    for (int i = 0; i < _games.Length; i++)
        //    {
        //        _games[i] = best.Clone();
        //    }
        //}
    }
}