using System.Linq;
using System;

namespace SnakeMLDesktop.GeneticAlgorithm
{
    public static class CrossoverFunctions
    {
        public static (double[], double[]) SimulatedBinaryCrossover(double[] parent1, double[] parent2, double eta)
        {
            var rand = GenerateRandomArray(parent1.Length);

            var gamma = new double[parent1.Length];

            for (int i = 0; i < parent1.Length; i++)
            {
                if (rand[i] <= 0.5)
                {
                    gamma[i] = Math.Pow(2 * rand[i], 1.0 / (eta + 1));
                }
                else
                {
                    gamma[i] = Math.Pow(1.0 / (2.0 * (1.0 - rand[i])), 1.0 / (eta + 1));
                }
            }

            var chromosome1 = new double[parent1.Length];
            var chromosome2 = new double[parent1.Length];

            for (int i = 0; i < parent1.Length; i++)
            {
                chromosome1[i] = 0.5 * ((1 + gamma[i]) * parent1[i] + (1 - gamma[i]) * parent2[i]);
                chromosome2[i] = 0.5 * ((1 - gamma[i]) * parent1[i] + (1 + gamma[i]) * parent2[i]);
            }

            return (chromosome1, chromosome2);
        }

        public static (double[], double[]) UniformBinaryCrossover(double[] parent1, double[] parent2)
        {
            var offspring1 = new double[parent1.Length];
            var offspring2 = new double[parent1.Length];

            var mask = GenerateRandomArray(parent1.Length);

            for (int i = 0; i < parent1.Length; i++)
            {
                if (mask[i] > 0.5)
                {
                    offspring1[i] = parent2[i];
                    offspring2[i] = parent1[i];
                }
                else
                {
                    offspring1[i] = parent1[i];
                    offspring2[i] = parent2[i];
                }
            }

            return (offspring1, offspring2);
        }

        public static (double[], double[]) SinglePointBinaryCrossover(double[] parent1, double[] parent2, char major = 'r')
        {
            var offspring1 = new double[parent1.Length];
            var offspring2 = new double[parent1.Length];

            var rows = parent1.Length;
            var cols = parent2.Length;
            var row = GenerateRandomInteger(0, rows);
            var col = GenerateRandomInteger(0, cols);

            if (major.ToString().ToLower() == "r")
            {
                for (int i = 0; i < rows; i++)
                {
                    if (i < row)
                    {
                        offspring1[i] = parent2[i];
                        offspring2[i] = parent1[i];
                    }
                    else if (i == row)
                    {
                        for (int j = 0; j <= col; j++)
                        {
                            offspring1[j] = parent2[j];
                            offspring2[j] = parent1[j];
                        }
                    }
                }
            }
            else if (major.ToString().ToLower() == "c")
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j < col)
                    {
                        offspring1[j] = parent2[j];
                        offspring2[j] = parent1[j];
                    }
                    else if (j == col)
                    {
                        for (int i = 0; i <= row; i++)
                        {
                            offspring1[i] = parent2[i];
                            offspring2[i] = parent1[i];
                        }
                    }
                }
            }

            return (offspring1, offspring2);
        }

        private static double[] GenerateRandomArray(int length)
        {
            var random = new Random();
            return Enumerable.Range(0, length).Select(_ => random.NextDouble()).ToArray();
        }

        private static int GenerateRandomInteger(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
