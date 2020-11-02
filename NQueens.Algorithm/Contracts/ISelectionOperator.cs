using NQueens.Domain;
using System.Collections.Generic;

namespace NQueens.Algorithm.Contracts
{
    public interface ISelectionOperator
    {
        Individual SelectOne(List<Solution> solutions);
    }
}
