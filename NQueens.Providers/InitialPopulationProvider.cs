using NQueens.Domain;
using NQueens.Providers.Contracts;
using System;
using System.Collections.Generic;

namespace NQueens.Providers
{
    public class InitialPopulationProvider : IInitialPopulationProvider
    {
        private readonly static Random random = new Random();

        public List<Individual> GetInitialPopulation(int populationSize, int numberOfGenes)
        {
            var initialPopulation = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
            {
                initialPopulation.Add(new Individual
                {
                    Genes = GetRandomPermutation(numberOfGenes)
                });
            }

            return initialPopulation;
        }

        private byte[] GetRandomPermutation(int numberOfGenes)
        {
            var allGenes = new List<byte>();

            for (byte i = 0; i < numberOfGenes; i++)
                allGenes.Add(i);

            var permutation = new byte[numberOfGenes];

            for (int i = 0; i < numberOfGenes; i++)
            {
                var randomIndex = random.Next(allGenes.Count);

                permutation[i] = allGenes[randomIndex];

                allGenes.RemoveAt(randomIndex);
            }

            return permutation;
        }
    }
}
