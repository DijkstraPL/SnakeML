using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMLDesktop.GeneticAlgorithm
{
    public static class MutationFunctions
    {
        public static void GaussianMutation(double[] chromosome, double probMutation, List<double>? mu = null, List<double>? sigma = null, double? scale = null)
        {
            var mutationArray = GenerateMutationArray(chromosome.Length, probMutation);

            double[] gaussianMutation;

            if (mu != null && sigma != null)
            {
                gaussianMutation = GenerateGaussianDistribution(mu, sigma);
            }
            else
            {
                gaussianMutation = GenerateGaussianDistribution(chromosome.Length);
            }

            if (scale != null)
            {
                ApplyScaleToMutation(mutationArray, gaussianMutation, (double)scale);
            }

            UpdateChromosome(chromosome, mutationArray, gaussianMutation);
        }

        public static void RandomUniformMutation(double[] chromosome, double probMutation, double low, double high)
        {
            var mutationArray = GenerateMutationArray(chromosome.Length, probMutation);
            var uniformMutation = GenerateUniformDistribution(low, high, chromosome.Length);

            UpdateChromosome(chromosome, mutationArray, uniformMutation);
        }

        public static void UniformMutationWithRespectToBestIndividual(double[] chromosome, double[] bestChromosome, double probMutation)
        {
            var mutationArray = GenerateMutationArray(chromosome.Length, probMutation);
            var uniformMutation = GenerateUniformDistribution(chromosome.Length);

            for (int i = 0; i < chromosome.Length; i++)
            {
                chromosome[i] += mutationArray[i] * (bestChromosome[i] - chromosome[i]) * uniformMutation[i];
            }
        }

        public static void ExponentialMutation(double[] chromosome, double xi, double probMutation)
        {
            var mutationArray = GenerateMutationArray(chromosome.Length, probMutation);
            var xiArray = GenerateConstantArray(xi, chromosome.Length);

            // Eq 11.17
            var y = GenerateUniformDistribution(chromosome.Length);
            var x = new double[chromosome.Length];

            for (int i = 0; i < chromosome.Length; i++)
            {
                if (y[i] <= 0.5)
                {
                    x[i] = 1.0 / xiArray[i] * Math.Log(2 * y[i]);
                }
                else
                {
                    x[i] = -(1.0 / xiArray[i]) * Math.Log(2 * (1 - y[i]));
                }
            }

            // Eq 11.16
            var delta = new double[chromosome.Length];

            for (int i = 0; i < chromosome.Length; i++)
            {
                delta[i] = xiArray[i] / 2.0 * Math.Exp(-xiArray[i] * Math.Abs(x[i]));
            }

            // Update delta such that E(0, xi) = (1 / xi) * E(0 , 1)
            for (int i = 0; i < chromosome.Length; i++)
            {
                delta[i] *= 1.0 / xiArray[i];
            }

            // Update individual
            UpdateChromosome(chromosome, mutationArray, delta);
        }

        public static void MmoMutation(double[] chromosome, double probMutation)
        {
            var mutationArray = GenerateMutationArray(chromosome.Length, probMutation);
            var normal = GenerateNormalDistribution(chromosome.Length);
            var cauchy = GenerateCauchyDistribution(chromosome.Length);

            // Eq 11.20
            var delta = new double[chromosome.Length];

            for (int i = 0; i < chromosome.Length; i++)
            {
                delta[i] = normal[i] + cauchy[i];
            }

            // Update individual
            UpdateChromosome(chromosome, mutationArray, delta);
        }

        private static bool[] GenerateMutationArray(int length, double probability)
        {
            var random = new Random();
            return Enumerable.Range(0, length).Select(_ => random.NextDouble() < probability).ToArray();
        }

        private static double[] GenerateGaussianDistribution(List<double> mu, List<double> sigma)
        {
            return mu.Select((m, i) => Normal.Sample(m, sigma[i])).ToArray();
        }

        private static double[] GenerateGaussianDistribution(int length)
        {
            var normal = new Normal();
            return Enumerable.Range(0, length).Select(_ => normal.Sample()).ToArray();
        }

        private static void ApplyScaleToMutation(bool[] mutationArray, double[] mutation, double scale)
        {
            for (int i = 0; i < mutationArray.Length; i++)
            {
                if (mutationArray[i])
                {
                    mutation[i] *= scale;
                }
            }
        }

        private static double[] GenerateUniformDistribution(double low, double high, int length)
        {
            var continuousUniform = new ContinuousUniform(low, high);
            return Enumerable.Range(0, length).Select(_ => continuousUniform.Sample()).ToArray();
        }

        private static double[] GenerateUniformDistribution(int length)
        {
            var continuousUniform = new ContinuousUniform();
            return Enumerable.Range(0, length).Select(_ => continuousUniform.Sample()).ToArray();
        }

        private static double[] GenerateConstantArray(double value, int length)
        {
            return Enumerable.Range(0, length).Select(_ => value).ToArray();
        }

        private static double[] GenerateNormalDistribution(int length)
        {
            var normal = new Normal();
            return Enumerable.Range(0, length).Select(_ => normal.Sample()).ToArray();
        }

        private static double[] GenerateCauchyDistribution(int length)
        {
            var cauchy = new Cauchy();
            return Enumerable.Range(0, length).Select(_ => cauchy.Sample()).ToArray();
        }

        private static void UpdateChromosome(double[] chromosome, bool[] mutationArray, double[] mutation)
        {
            for (int i = 0; i < chromosome.Length; i++)
            {
                if (mutationArray[i])
                {
                    chromosome[i] += mutation[i];
                }
            }
        }
    }
}
