using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMLDesktop.GeneticAlgorithm
{
    public class Population
    {
        private List<Individual> individuals;

        public Population(List<Individual> individuals)
        {
            this.individuals = individuals;
        }

        public List<Individual> Individuals => individuals;

        public int NumIndividuals
        {
            get { return individuals.Count; }
            set { throw new Exception("Cannot set the number of individuals. You must change Population.Individuals instead."); }
        }

        public int NumGenes
        {
            get { return individuals[0].Chromosome.Length; }
            set { throw new Exception("Cannot set the number of genes. You must change Population.Individuals instead."); }
        }

        public float AverageFitness
        {
            get { return (float)(individuals.Sum(individual => individual.Fitness) / NumIndividuals); }
            set { throw new Exception("Cannot set average fitness. This is a read-only property."); }
        }

        public Individual FittestIndividual
        {
            get { return individuals.OrderByDescending(individual => individual.Fitness).First(); }
            set { throw new Exception("Cannot set fittest individual. This is a read-only property."); }
        }

        public void CalculateFitness()
        {
            foreach (var individual in individuals)
            {
                individual.CalculateFitness();
            }
        }

        public float GetFitnessStd()
        {
            var fitnessArray = individuals.Select(individual => individual.Fitness).ToArray();
            return (float)Math.Sqrt(fitnessArray.Select(fitness => Math.Pow(fitness - AverageFitness, 2)).Sum() / NumIndividuals);
        }
    }
}
