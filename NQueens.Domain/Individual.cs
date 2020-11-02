using System.Linq;

namespace NQueens.Domain
{
    public class Individual
    {
        public byte[] Genes { get; set; }

        public Individual Clone()
        {
            return new Individual
            {
                Genes = (byte[])this.Genes.Clone()
            };
        }

        public override string ToString()
        {
            return $"{string.Join(", ", Genes)}";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Individual individual = (Individual)obj;

                return Enumerable.SequenceEqual(this.Genes, individual.Genes);
            }
        }

        public override int GetHashCode()
        {
            int hash = 13;

            foreach (var gene in Genes)
                hash = hash * 7 + gene;

            return hash;
        }
    }
}
