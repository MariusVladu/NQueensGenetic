using NQueens.Domain;
using System.Collections.Generic;

namespace NQueens.Providers.Contracts
{
    public interface IInitialPopulationProvider
    {
        List<Individual> GetInitialPopulation(int populationSize, int numberOfGenes);
    }
}
