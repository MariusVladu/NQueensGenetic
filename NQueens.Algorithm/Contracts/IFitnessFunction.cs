using KnapsackGenetic.Domain;

namespace KnapsackGenetic.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        int GetFitnessScore(Individual individual);
    }
}
