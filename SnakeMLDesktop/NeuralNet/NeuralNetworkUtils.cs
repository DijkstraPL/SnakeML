namespace SnakeMLDesktop.NeuralNet
{
    public static class NeuralNetworkUtils
    {
        public static ActivationFunction GetActivationByName(string name)
        {
            return ActivationFunctions.GetActivationByName(name);
        }
    }
}
