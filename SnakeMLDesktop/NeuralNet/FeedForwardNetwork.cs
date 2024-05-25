using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMLDesktop.NeuralNet
{
    public class FeedForwardNetwork
    {
        private Dictionary<string, double[,]> paramsDict;
        private List<int> layerNodes;
        private ActivationFunction hiddenActivation;
        private ActivationFunction outputActivation;
        private double[] inputs;
        private double[] output;

        private Random rand;

        public FeedForwardNetwork(List<int> layerNodes, ActivationFunction hiddenActivation, ActivationFunction outputActivation, string initMethod = "uniform", int? seed = null)
        {
            paramsDict = new Dictionary<string, double[,]>();
            this.layerNodes = layerNodes;
            this.hiddenActivation = hiddenActivation;
            this.outputActivation = outputActivation;
            inputs = null;
            output = null;

            rand = seed.HasValue ? new Random(seed.Value) : new Random();

            for (int l = 1; l < layerNodes.Count; l++)
            {
                if (initMethod == "uniform")
                {
                    paramsDict.Add($"W{l}", InitializeWeights(layerNodes[l], layerNodes[l - 1]));
                    paramsDict.Add($"b{l}", InitializeBiases(layerNodes[l]));
                }
                else
                {
                    throw new Exception("Implement more options, bro");
                }

                paramsDict.Add($"A{l}", null);
            }
        }

        private double[,] InitializeWeights(int rows, int cols)
        {
            double[,] weights = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    weights[i, j] = rand.NextDouble() * 2 - 1; // Uniform initialization between -1 and 1
                }
            }
            return weights;
        }

        private double[,] InitializeBiases(int size)
        {
            double[,] biases = new double[size, 1];
            for (int i = 0; i < size; i++)
            {
                biases[i, 0] = rand.NextDouble() * 2 - 1; // Uniform initialization between -1 and 1
            }
            return biases;
        }

        public double[] FeedForward(double[] X)
        {
            double[] APrev = X;
            int L = layerNodes.Count - 1;

            for (int l = 1; l < L; l++)
            {
                double[,] W = paramsDict[$"W{l}"];
                double[,] b = paramsDict[$"b{l}"];
                double[,] Z = MatrixAddition(MatrixMultiplication(W, APrev), b);
                APrev = ApplyActivationFunction(Z, hiddenActivation);
                paramsDict[$"A{l}"] = APrev;
            }

            double[,] lastLayerW = paramsDict[$"W{L}"];
            double[,] lastLayerB = paramsDict[$"b{L}"];
            double[,] lastLayerZ = MatrixAddition(MatrixMultiplication(lastLayerW, APrev), lastLayerB);
            output = ApplyActivationFunction(lastLayerZ, outputActivation);
            paramsDict[$"A{L}"] = output;

            return output;
        }

        private double[,] MatrixMultiplication(double[,] matrixA, double[] vectorB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);

            double[,] result = new double[rowsA, 1];

            for (int i = 0; i < rowsA; i++)
            {
                double sum = 0;
                for (int j = 0; j < colsA; j++)
                {
                    sum += matrixA[i, j] * vectorB[j];
                }
                result[i, 0] = sum;
            }

            return result;
        }

        private double[,] MatrixAddition(double[,] matrixA, double[,] matrixB)
        {
            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);

            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return result;
        }

        private double[,] ApplyActivationFunction(double[,] matrix, ActivationFunction activationFunction)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = activationFunction(matrix[i, j]);
                }
            }

            return result;
        }

        public double[] Softmax(double[] X)
        {
            double[] expX = X.Select(Math.Exp).ToArray();
            double sumExpX = expX.Sum();
            return expX.Select(x => x / sumExpX).ToArray();
        }
    }
}
