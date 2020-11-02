namespace KnapsackGenetic.Domain
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
    }
}
