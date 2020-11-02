using NQueens.Domain;

namespace NQueens.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        int GetFitnessScore(Individual individual);
    }
}
