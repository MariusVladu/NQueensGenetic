using NQueens.Algorithm.Contracts;
using NQueens.Domain;
using System;

namespace NQueens.Algorithm
{
    public class FitnessFunction : IFitnessFunction
    {
        public int GetFitnessScore(Individual individual)
        {
            var numberOfPossibleAttacks = 0;

            for (byte i = 0; i < individual.Genes.Length; i++)
            {
                for (byte j = 0; j < individual.Genes.Length; j++)
                {
                    if (i == j) continue;

                    if (QueenCanAttack(
                            queen1Row: individual.Genes[i],
                            queen1Col: i,
                            queen2Row: individual.Genes[j],
                            queen2Col: j))
                    {
                        numberOfPossibleAttacks++;
                    }
                }
            }

            return numberOfPossibleAttacks;
        }

        private bool QueenCanAttack(byte queen1Row, byte queen1Col, byte queen2Row, byte queen2Col)
        {
            return queen1Col == queen2Col
                || queen1Row == queen2Row
                || Math.Abs(queen1Col - queen2Col) == Math.Abs(queen1Row - queen2Row);
        }
    }
}
