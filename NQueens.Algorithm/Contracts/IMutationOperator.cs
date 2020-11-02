using NQueens.Domain;

namespace NQueens.Algorithm.Contracts
{
    public interface IMutationOperator
    {
        void ApplyMutation(Individual individual, double mutationRate);
    }
}
