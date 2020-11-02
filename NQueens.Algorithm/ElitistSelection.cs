using NQueens.Domain;
using NQueens.Algorithm.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace NQueens.Algorithm
{
    public class ElitistSelection : IElitistSelection
    {
        public List<Solution> SelectMany(int n, List<Solution> solutions)
        {
            return solutions.OrderBy(s => s.FitnessScore).Take(n).ToList();
        }

        Solution IElitistSelection.SelectOne(List<Solution> solutions)
        {
            return solutions.OrderBy(s => s.FitnessScore).First();
        }
    }
}
