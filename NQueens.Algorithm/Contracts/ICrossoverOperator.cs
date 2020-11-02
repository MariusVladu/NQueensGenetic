using NQueens.Domain;
using System;

namespace NQueens.Algorithm.Contracts
{
    public interface ICrossoverOperator
    {
        Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate);
    }
}
