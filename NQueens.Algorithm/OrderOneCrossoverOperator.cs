using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class OrderOneCrossoverOperator : ICrossoverOperator
    {
        private static readonly Random random = new Random();

        public Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate)
        {
            ValidateParameters(parent1, parent2, crossoverRate);

            if(random.NextDouble() < crossoverRate)
            {
                return PerformCrossover(parent1, parent2);
            }

            return CloneParents(parent1, parent2);
        }

        private Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var numberOfGenes = parent1.Genes.Length;
            var left = random.Next(numberOfGenes);
            var right = random.Next(numberOfGenes);

            if (left < right)
            {
                var temp = left;
                left = right;
                right = temp;
            }

            var offspring1 = GetOffspring(left, right, numberOfGenes, parent1, parent2);
            var offspring2 = GetOffspring(left, right, numberOfGenes, parent2, parent1);

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private Individual GetOffspring(int left, int right, int numberOfGenes, Individual parent1, Individual parent2)
        {
            var offspring = new Individual { Genes = new byte[numberOfGenes] };

            for (int i = left; i < right; i++)
                offspring.Genes[i] = parent1.Genes[i];

            var genesToAddInOrder = GetGenesInOrder(left, right, numberOfGenes, offspring, parent2);

            for (int i = right; i < numberOfGenes; i++)
                offspring.Genes[i] = genesToAddInOrder.Dequeue();

            for (int i = 0; i < left; i++)
                offspring.Genes[i] = genesToAddInOrder.Dequeue();

            return offspring;
        }

        private Queue<byte> GetGenesInOrder(int left, int right, int numberOfGenes, Individual offspring, Individual parent)
        {
            var genesInOrder = new Queue<byte>(numberOfGenes);

            for (int i = right; i < numberOfGenes; i++)
                if(!GeneAlreadyExists(parent.Genes[i], left, right, offspring))
                    genesInOrder.Enqueue(parent.Genes[i]);

            return genesInOrder;
        }

        private bool GeneAlreadyExists(byte gene, int left, int right, Individual offspring)
        {
            for (int i = left; i < right; i++)
                if (offspring.Genes[i] == gene) 
                    return true;

            return false;
        }

        private Tuple<Individual, Individual> CloneParents(Individual parent1, Individual parent2)
        {
            return new Tuple<Individual, Individual>(parent1.Clone(), parent2.Clone());
        }

        private void ValidateParameters(Individual parent1, Individual parent2, double crossoverRate)
        {
            if (parent1 == null) throw new ArgumentNullException(nameof(parent1));
            if (parent1.Genes == null) throw new ArgumentNullException(nameof(parent1.Genes));
            if (parent2 == null) throw new ArgumentNullException(nameof(parent2));
            if (parent2.Genes == null) throw new ArgumentNullException(nameof(parent2.Genes));
            if (crossoverRate < 0 || crossoverRate > 1) throw new ArgumentException($"{nameof(crossoverRate)} must be in [0, 1]");
        }
    }
}
