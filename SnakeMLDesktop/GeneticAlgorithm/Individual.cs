namespace SnakeMLDesktop.GeneticAlgorithm
{
    public abstract class Individual
    {
        public abstract void CalculateFitness();

        public abstract double Fitness { get; set; }

        public abstract void EncodeChromosome();

        public abstract void DecodeChromosome();

        public abstract string Chromosome { get; set; }
    }
}
