﻿using KnapsackGenetic.Algorithm;
using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers;
using KnapsackGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KnapsackGenetic.UI
{
    public partial class NQueens : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        private IFitnessFunction fitnessFunction;
        private ISelectionOperator selectionOperator;
        private ISelectionOperator elitistSelection;
        private ICrossoverOperator crossoverOperator;
        private IMutationOperator mutationOperator;
        private IInitialPopulationProvider initialPopulationProvider;
        private Settings settings;

        private int numberOfGenes;
        private int weightLimit = 30;
        private int numberOfElites = 1;
        private int initialPopulationSize = 50;
        private double crossoverRate = 0.5;
        private double mutationRate = 0.05;

        private List<double> generationsPlotData;
        private List<double> averageScorePlotData;
        private List<double> bestScorePlotData;

        public NQueens()
        {
            InitializeComponent();

            fitnessFunction = new FitnessFunction();
            selectionOperator = new RouletteWheelSelection();
            elitistSelection = new ElitistSelection();
            crossoverOperator = new OrderOneCrossoverOperator();
            mutationOperator = new MutationOperator();
            initialPopulationProvider = new InitialPopulationProvider();

            var items = GetItems();
            numberOfGenes = items.Count;

            settings = new Settings
            {
                Items = GetItems(),
                NumberOfGenes = numberOfGenes,
                WeightLimit = weightLimit,
                NumberOfElites = numberOfElites,
                InitialPopulationSize = initialPopulationSize,
                CrossoverRate = crossoverRate,
                MutationRate = mutationRate
            };

            InitializeGeneticAlgorithm();

            chartAverageScore.plt.XLabel("Generation #");
            chartAverageScore.plt.YLabel("Average Fitness Score");
            chartBestScore.plt.XLabel("Generation #");
            chartBestScore.plt.YLabel("Best Fitness Score");
            Plot();
        }

        private void InitializeGeneticAlgorithm()
        {
            geneticAlgorithm = new GeneticAlgorithm(settings, initialPopulationProvider, fitnessFunction, selectionOperator, elitistSelection, crossoverOperator, mutationOperator);

            generationsPlotData = new List<double>();
            averageScorePlotData = new List<double>();
            bestScorePlotData = new List<double>();
            UpdatePlotData();
        }

        private List<Item> GetItems()
        {
            return new List<Item>
            {
                new Item{ Weight = 7, Value = 5},
                new Item{ Weight = 2, Value = 4},
                new Item{ Weight = 1, Value = 7},
                new Item{ Weight = 9, Value = 2},
                new Item{ Weight = 20, Value = 5},
                new Item{ Weight = 11, Value = 6},
                new Item{ Weight = 2, Value = 6},
                new Item{ Weight = 15, Value = 10},
                new Item{ Weight = 3, Value = 1},
                new Item{ Weight = 4, Value = 2},
                new Item{ Weight = 8, Value = 6},
            };
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
            labelGenerationInfo.Text = $"Generation #{generationNumberString}\nAverage: {averageScore2DecimalPlaces}\nBest Solution: {geneticAlgorithm.CurrentBestSolution}";
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
    }
}
