using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMLDesktop.GeneticAlgorithm
{
    public static class SelectionFunctions
    {
        public static List<Individual> ElitismSelection(Population population, int numIndividuals)
        {
            var individuals = population.Individuals.OrderByDescending(individual => individual.Fitness).ToList();
            return individuals.Take(numIndividuals).ToList();
        }

        public static List<Individual> RouletteWheelSelection(Population population, int numIndividuals)
        {
            var selection = new List<Individual>();
            var wheel = population.Individuals.Sum(individual => individual.Fitness);

            for (int i = 0; i < numIndividuals; i++)
            {
                var pick = new Random().NextDouble() * wheel;
                var current = 0.0;

                foreach (var individual in population.Individuals)
                {
                    current += individual.Fitness;

                    if (current > pick)
                    {
                        selection.Add(individual);
                        break;
                    }
                }
            }

            return selection;
        }

        public static List<Individual> TournamentSelection(Population population, int numIndividuals, int tournamentSize)
        {
            var selection = new List<Individual>();

            for (int i = 0; i < numIndividuals; i++)
            {
                var tournament = population.Individuals.OrderBy(_ => new Random().Next()).Take(tournamentSize).ToList();
                var bestFromTournament = tournament.OrderByDescending(individual => individual.Fitness).First();
                selection.Add(bestFromTournament);
            }

            return selection;
        }
    }
}
