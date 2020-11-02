using NQueens.Algorithm;
using NQueens.Algorithm.Contracts;
using NQueens.Domain;
using NQueens.Providers;
using NQueens.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NQueens.UI
{
    public partial class NQueens : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        private IFitnessFunction fitnessFunction;
        private ISelectionOperator selectionOperator;
        private IElitistSelection elitistSelection;
        private ICrossoverOperator crossoverOperator;
        private IMutationOperator mutationOperator;
        private IInitialPopulationProvider initialPopulationProvider;
        private Settings settings;

        private List<double> generationsPlotData;
        private List<double> averageScorePlotData;
        private List<double> bestScorePlotData;

        public NQueens()
        {
            InitializeComponent();

            fitnessFunction = new FitnessFunction();
            selectionOperator = new TournamentSelection(8);
            elitistSelection = new ElitistSelection();
            crossoverOperator = new OrderOneCrossoverOperator();
            mutationOperator = new ScrambleMutationOperator();
            initialPopulationProvider = new InitialPopulationProvider();

            InitializeGeneticAlgorithm();

            chartAverageScore.plt.XLabel("Generation #");
            chartAverageScore.plt.YLabel("Average Fitness Score");
            chartBestScore.plt.XLabel("Generation #");
            chartBestScore.plt.YLabel("Best Fitness Score");
            Plot();
        }

        private void InitializeGeneticAlgorithm()
        {
            settings = new Settings
            {
                BoardSize = Convert.ToInt32(inputBoardSize.Value),
                NumberOfGenes = Convert.ToInt32(inputBoardSize.Value),
                NumberOfElites = Convert.ToInt32(inputElites.Value),
                InitialPopulationSize = Convert.ToInt32(inputMaxPopulation.Value),
                MaxPopulationSize = Convert.ToInt32(inputMaxPopulation.Value),
                CrossoverRate = Convert.ToDouble(inputCrossoverRate.Value),
                MutationRate = Convert.ToDouble(inputMutationRate.Value)
            };

            geneticAlgorithm = new GeneticAlgorithm(settings, initialPopulationProvider, fitnessFunction, selectionOperator, elitistSelection, crossoverOperator, mutationOperator);

            generationsPlotData = new List<double>();
            averageScorePlotData = new List<double>();
            bestScorePlotData = new List<double>();
            UpdatePlotData();
        }

        private void buttonNextGeneration_Click(object sender, EventArgs e)
        {
            geneticAlgorithm.ComputeNextGeneration();

            UpdatePlotData();
            Plot();
        }

        private void UpdatePlotData()
        {
            generationsPlotData.Add(geneticAlgorithm.CurrentGenerationNumber);
            averageScorePlotData.Add(geneticAlgorithm.AverageScore);
            bestScorePlotData.Add(geneticAlgorithm.CurrentBestSolution.FitnessScore);
        }

        private void Plot()
        {
            var generationsPlotArray = generationsPlotData.ToArray();

            chartAverageScore.plt.Clear();
            chartAverageScore.plt.PlotScatter(generationsPlotArray, averageScorePlotData.ToArray(), Color.Blue);
            chartAverageScore.plt.AxisAuto();
            chartAverageScore.Render();

            chartBestScore.plt.Clear();
            chartBestScore.plt.PlotScatter(generationsPlotArray, bestScorePlotData.ToArray(), Color.Green);
            chartBestScore.plt.AxisAuto();
            chartBestScore.Render();

            ShowBestSolution();
        }

        private void ShowBestSolution()
        {
            var generationNumberString = geneticAlgorithm.CurrentGenerationNumber.ToString().PadLeft(4, '0'); ;
            var averageScore2DecimalPlaces = string.Format("{0:00.00}", geneticAlgorithm.AverageScore);
            labelGenerationInfo.Text = $"Generation #{generationNumberString}\nAverage: {averageScore2DecimalPlaces}\nBest Solution: {geneticAlgorithm.CurrentBestSolution}\nBest Score: {geneticAlgorithm.CurrentBestSolution.FitnessScore}";
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();

            for (int i = 0; i < inputGenerationsNumber.Value; i++)
            {
                geneticAlgorithm.ComputeNextGeneration();

                UpdatePlotData();

                if (i % 50 == 0) Plot();
            }

            Plot();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();
            Plot();
        }
    }
}
