namespace NQueens.Domain
{
    public class Settings
    {
        public int BoardSize { get; set; }
        public int NumberOfGenes { get; set; }
        public int NumberOfElites { get; set; }
        public int InitialPopulationSize { get; set; }
        public int MaxPopulationSize { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
    }
}
