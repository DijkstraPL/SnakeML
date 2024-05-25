using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMLDesktop.NeuralNet
{

    public delegate double ActivationFunction(double X);

    public static class ActivationFunctions
    {
        public static ActivationFunction Sigmoid => X => 1.0 / (1.0 + Math.Exp(-X));
        public static ActivationFunction Tanh => Math.Tanh;
        public static ActivationFunction ReLU => X => Math.Max(0, X);
        public static ActivationFunction LeakyReLU => X => X > 0 ? X : X * 0.01;
        public static ActivationFunction Linear => X => X;

        public static ActivationFunction GetActivationByName(string name)
        {
            var activations = new List<(string, ActivationFunction)>
        {
            ("relu", ReLU),
            ("sigmoid", Sigmoid),
            ("linear", Linear),
            ("leaky_relu", LeakyReLU),
            ("tanh", Tanh),
        };

            var func = activations.FirstOrDefault(activation => activation.Item1.ToLower() == name.ToLower());
            if (func.Item2 != null)
            {
                return func.Item2;
            }

            throw new ArgumentException($"Activation function '{name}' not found.");
        }
    }
}
