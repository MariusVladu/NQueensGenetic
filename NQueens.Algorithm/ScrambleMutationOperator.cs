using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using NQueens.Algorithm.Helpers;
using System;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class ScrambleMutationOperator : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int left = random.Next(individual.Genes.Length);
            int right = random.Next(individual.Genes.Length);

            CommonFunctions.SwapIfInvalidInterval(ref left, ref right);

            var genesToScramble = GetGenesToScramble(left, right, individual);

            for (int i = left; i < right; i++)
            {
                var randomIndex = random.Next(genesToScramble.Count);

                individual.Genes[i] = genesToScramble[randomIndex];

                genesToScramble.RemoveAt(randomIndex);
            }
        }

        private List<byte> GetGenesToScramble(int left, int right, Individual individual)
        {
            var genesToScramble = new List<byte>(individual.Genes.Length);
            for (int i = left; i < right; i++)
                genesToScramble.Add(individual.Genes[i]);

            return genesToScramble;
        }
    }
}
