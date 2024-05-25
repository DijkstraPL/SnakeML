using SnakeMLDesktop.GeneticAlgorithm;
using System.Collections.Generic;

namespace SnakeMLDesktop.UI
{
    //public partial class MainWindow : Form
    //{
    //    private Settings settings;
    //    private double _SBX_eta;
    //    private double[] _mutation_bins;
    //    private double[] _crossover_bins;
    //    private string _SPBX_type;
    //    private double _mutation_rate;
    //    private int _next_gen_size;
    //    private int boardSize;
    //    private (int, int) SQUARE_SIZE = (35, 35);

    //    private int top = 150;
    //    private int left = 150;
    //    private int _snake_widget_width;
    //    private int _snake_widget_height;
    //    private int currentGeneration;
    //    private int _current_individual;
    //    private Population population;
    //    private Snake snake;
    //    private int bestScore;
    //    private double bestFitness;

    //    private Timer timer;
    //    private SnakeWidget snakeWidgetWindow;
    //    private NeuralNetworkViz nnVizWindow;
    //    private GeneticAlgoWidget gaWindow;

    //    public MainWindow(Settings settings, bool show = true, int fps = 200)
    //    {
    //        InitializeComponent();
    //        this.settings = settings;
    //        _SBX_eta = settings.SBX_eta;
    //        _mutation_bins = new double[] { settings.ProbabilityGaussian, settings.ProbabilityRandomUniform };
    //        _crossover_bins = new double[] { settings.ProbabilitySBX, settings.ProbabilitySPBX };
    //        _SPBX_type = settings.SPBX_type.ToLower();
    //        _mutation_rate = settings.MutationRate;

    //        _next_gen_size = settings.SelectionType.ToLower() == "plus" ?
    //            settings.NumParents + settings.NumOffspring : settings.NumOffspring;

    //        boardSize = settings.BoardSize;
    //        int borderLeft = 0, borderTop = 10, borderRight = 0, borderBottom = 10;

    //        int snakeWidgetWidth = SQUARE_SIZE.Item1 * boardSize.Item1;
    //        int snakeWidgetHeight = SQUARE_SIZE.Item2 * boardSize.Item2;

    //        _snake_widget_width = Math.Max(snakeWidgetWidth, 620);
    //        _snake_widget_height = Math.Max(snakeWidgetHeight, 600);

    //        int width = _snake_widget_width + 700 + borderLeft + borderRight;
    //        int height = _snake_widget_height + borderTop + borderBottom + 200;

    //        individuals = new List<Individual>();

    //        for (int i = 0; i < settings.NumParents; i++)
    //        {
    //            Individual individual = new Snake(boardSize, settings.HiddenNetworkArchitecture,
    //                settings.HiddenLayerActivation, settings.OutputLayerActivation,
    //                settings.Lifespan, settings.AppleAndSelfVision);

    //            individuals.Add(individual);
    //        }

    //        bestScore = 0;
    //        bestFitness = 0;

    //        _current_individual = 0;
    //        population = new Population(individuals);

    //        snake = population.Individuals[_current_individual];
    //        currentGeneration = 0;

    //        InitWindow();

    //        timer = new Timer();
    //        timer.Interval = 1000 / fps;
    //        timer.Tick += Update;

    //        if (show)
    //        {
    //            Show();
    //        }
    //    }

    //    private void InitWindow()
    //    {
    //        centralWidget = new Panel();
    //        Controls.Add(centralWidget);
    //        Text = "Snake AI";
    //        SetBounds(top, left, width, height);

    //        // Create the Neural Network window
    //        nnVizWindow = new NeuralNetworkViz(centralWidget, snake);
    //        nnVizWindow.SetBounds(0, 0, 600, _snake_widget_height + borderTop + borderBottom + 200);
    //        nnVizWindow.Name = "nnVizWindow";

    //        // Create SnakeWidget window
    //        snakeWidgetWindow = new SnakeWidget(centralWidget, boardSize, snake);
    //        snakeWidgetWindow.SetBounds(600 + borderLeft, borderTop, _snake_widget_width, _snake_widget_height);
    //        snakeWidgetWindow.Name = "snakeWidgetWindow";

    //        // Genetic Algorithm Stats window
    //        gaWindow = new GeneticAlgoWidget(centralWidget, settings);
    //        gaWindow.SetBounds(600, borderTop + borderBottom + _snake_widget_height,
    //            _snake_widget_width + borderLeft + borderRight + 100, 200 - 10);
    //        gaWindow.Name = "gaWindow";
    //    }

    //    private void Update(object sender, EventArgs e)
    //    {
    //        snakeWidgetWindow.Update();
    //        nnVizWindow.Update();

    //        if (snake.IsAlive)
    //        {
    //            snake.Move();
    //            if (snake.Score > bestScore)
    //            {
    //                bestScore = snake.Score;
    //                gaWindow.BestScoreLabel.Text = bestScore.ToString();
    //            }
    //        }
    //        else
    //        {
    //            snake.CalculateFitness();
    //            double fitness = snake.Fitness;
    //            Console.WriteLine(_current_individual + " " + fitness);

    //            if (fitness > bestFitness)
    //            {
    //                bestFitness = fitness;
    //                gaWindow.BestFitnessLabel.Text = bestFitness.ToString("0.00E+0");
    //            }

    //            _current_individual++;

    //            if (currentGeneration > 0 && _current_individual == _next_gen_size ||
    //                currentGeneration == 0 && _current_individual == settings.NumParents)
    //            {
    //                Console.WriteLine(settings);
    //                Console.WriteLine("======================= Generation " + currentGeneration + " =======================");
    //                Console.WriteLine("----Max fitness: " + population.FittestIndividual.Fitness);
    //                Console.WriteLine("----Best Score: " + population.FittestIndividual.Score);
    //                Console.WriteLine("----Average fitness: " + population.AverageFitness);
    //                NextGeneration();
    //            }
    //            else
    //            {
    //                int currentPop = currentGeneration == 0 ? settings.NumParents : _next_gen_size;
    //                gaWindow.CurrentIndividualLabel.Text = _current_individual + 1 + "/" + currentPop;
    //            }

    //            snake = population.Individuals[_current_individual];
    //            snakeWidgetWindow.Snake = snake;
    //            nnVizWindow.Snake = snake;
    //        }
    //    }

    //    private void NextGeneration()
    //    {
    //        IncrementGeneration();
    //        _current_individual = 0;

    //        foreach (Individual individual in population.Individuals)
    //        {
    //            individual.CalculateFitness();
    //        }

    //        population.Individuals = ElitismSelection(population, settings.NumParents);

    //        RandomizeList(population.Individuals);
    //        List<Individual> nextPop = new List<Individual>();

    //        if (settings.SelectionType.ToLower() == "plus")
    //        {
    //            foreach (Individual individual in population.Individuals)
    //            {
    //                individual.Lifespan--;
    //                if (individual.Lifespan > 0)
    //                {
    //                    Snake s = new Snake(boardSize, individual.Network.Params,
    //                        individual.HiddenLayerArchitecture, individual.HiddenActivation,
    //                        individual.OutputActivation, individual.Lifespan,
    //                        settings.AppleAndSelfVision);

    //                    nextPop.Add(s);
    //                }
    //            }
    //        }

    //        while (nextPop.Count < _next_gen_size)
    //        {
    //            Individual p1, p2;
    //            RouletteWheelSelection(population, out p1, out p2, 2);

    //            int L = p1.Network.LayerNodes.Length;
    //            Dictionary<string, double[,]> c1Params = new Dictionary<string, double[,]>();
    //            Dictionary<string, double[,]> c2Params = new Dictionary<string, double[,]>();

    //            for (int l = 1; l < L; l++)
    //            {
    //                double[,] p1_W_l = p1.Network.Params["W" + l];
    //                double[,] p2_W_l = p2.Network.Params["W" + l];
    //                double[,] p1_b_l = p1.Network.Params["b" + l];
    //                double[,] p2_b_l = p2.Network.Params["b" + l];

    //                double[,] c1_W_l, c2_W_l, c1_b_l, c2_b_l;

    //                Crossover(p1_W_l, p2_W_l, p1_b_l, p2_b_l, out c1_W_l, out c2_W_l, out c1_b_l, out c2_b_l);
    //                Mutation(c1_W_l, c2_W_l, c1_b_l, c2_b_l);

    //                c1Params["W" + l] = c1_W_l;
    //                c2Params["W" + l] = c2_W_l;
    //                c1Params["b" + l] = c1_b_l;
    //                c2Params["b" + l] = c2_b_l;

    //                ClipToRange(c1Params["W" + l], -1, 1);
    //                ClipToRange(c2Params["W" + l], -1, 1);
    //                ClipToRange(c1Params["b" + l], -1, 1);
    //                ClipToRange(c2Params["b" + l], -1, 1);
    //            }

    //            Snake c1 = new Snake(boardSize, c1Params, p1.HiddenLayerArchitecture,
    //                p1.HiddenActivation, p1.OutputActivation, settings.Lifespan);

    //            Snake c2 = new Snake(boardSize, c2Params, p2.HiddenLayerArchitecture,
    //                p2.HiddenActivation, p2.OutputActivation, settings.Lifespan);

    //            nextPop.Add(c1);
    //            nextPop.Add(c2);
    //        }

    //        RandomizeList(nextPop);
    //        population.Individuals = nextPop;
    //    }

    //    private void IncrementGeneration()
    //    {
    //        currentGeneration++;
    //        gaWindow.CurrentGenerationLabel.Text = (currentGeneration + 1).ToString();
    //    }

    //    private void Crossover(double[,] parent1Weights, double[,] parent2Weights,
    //                           double[,] parent1Bias, double[,] parent2Bias,
    //                           out double[,] child1Weights, out double[,] child2Weights,
    //                           out double[,] child1Bias, out double[,] child2Bias)
    //    {
    //        double randCrossover = new Random().NextDouble();
    //        int crossoverBucket = Array.FindIndex(_crossover_bins, bin => randCrossover < bin) + 1;

    //        child1Weights = null;
    //        child2Weights = null;
    //        child1Bias = null;
    //        child2Bias = null;

    //        if (crossoverBucket == 1)
    //        {
    //            SBX(parent1Weights, parent2Weights, _SBX_eta, out child1Weights, out child2Weights);
    //            SBX(parent1Bias, parent2Bias, _SBX_eta, out child1Bias, out child2Bias);
    //        }
    //        else if (crossoverBucket == 2)
    //        {
    //            SinglePointBinaryCrossover(parent1Weights, parent2Weights, _SPBX_type, out child1Weights, out child2Weights);
    //            SinglePointBinaryCrossover(parent1Bias, parent2Bias, _SPBX_type, out child1Bias, out child2Bias);
    //        }
    //        else
    //        {
    //            throw new Exception("Unable to determine valid crossover based off probabilities");
    //        }
    //    }

    //    private void Mutation(double[,] child1Weights, double[,] child2Weights,
    //                          double[,] child1Bias, double[,] child2Bias)
    //    {
    //        double scale = 0.2;
    //        double randMutation = new Random().NextDouble();
    //        int mutationBucket = Array.FindIndex(_mutation_bins, bin => randMutation < bin) + 1;

    //        double mutationRate = _mutation_rate;

    //        if (settings.MutationRateType.ToLower() == "decaying")
    //        {
    //            mutationRate = mutationRate / Math.Sqrt(currentGeneration + 1);
    //        }

    //        if (mutationBucket == 1)
    //        {
    //            GaussianMutation(child1Weights, mutationRate, scale);
    //            GaussianMutation(child2Weights, mutationRate, scale);
    //            GaussianMutation(child1Bias, mutationRate, scale);
    //            GaussianMutation(child2Bias, mutationRate, scale);
    //        }
    //        else if (mutationBucket == 2)
    //        {
    //            RandomUniformMutation(child1Weights, mutationRate, -1, 1);
    //            RandomUniformMutation(child2Weights, mutationRate, -1, 1);
    //            RandomUniformMutation(child1Bias, mutationRate, -1, 1);
    //            RandomUniformMutation(child2Bias, mutationRate, -1, 1);
    //        }
    //        else
    //        {
    //            throw new Exception("Unable to determine valid mutation based off probabilities.");
    //        }
    //    }

    //    // Define other methods and classes here
    //}
}
