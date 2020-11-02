using NQueens.Domain;
using System.Collections.Generic;

namespace NQueens.Algorithm.Contracts
{
    public interface IElitistSelection
    {
        Solution SelectOne(List<Solution> solutions);
        List<Solution> SelectMany(int n, List<Solution> solutions);
    }
}
