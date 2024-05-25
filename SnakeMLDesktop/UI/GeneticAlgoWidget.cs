using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SnakeMLDesktop.UI
{
    //public class GeneticAlgoWidget : UserControl
    //{
    //    private Label currentGenerationLabel;
    //    private Label currentIndividualLabel;
    //    private Label bestScoreLabel;
    //    private Label bestFitnessLabel;

    //    public GeneticAlgoWidget(Control parent, Dictionary<string, object> settings)
    //    {
    //        Font font = new Font("Times", 11, FontStyle.Normal);
    //        Font fontBold = new Font("Times", 11, FontStyle.Bold);

    //        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
    //        tableLayoutPanel.Dock = DockStyle.Fill;
    //        tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

    //        int row = 0;
    //        int labelCol = 0;
    //        int statsCol = 1;

    //        AddLabelWidgetToGrid("Generation: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        currentGenerationLabel = CreateLabelWidget("1", font);
    //        tableLayoutPanel.Controls.Add(currentGenerationLabel, row, statsCol, TOP_LEFT);
    //        row++;

    //        AddLabelWidgetToGrid("Individual: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        currentIndividualLabel = CreateLabelWidget("1/" + settings["num_parents"], font);
    //        tableLayoutPanel.Controls.Add(currentIndividualLabel, row, statsCol, TOP_LEFT);
    //        row++;

    //        AddLabelWidgetToGrid("Best Score: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        bestScoreLabel = CreateLabelWidget("0", font);
    //        tableLayoutPanel.Controls.Add(bestScoreLabel, row, statsCol, TOP_LEFT);
    //        row++;

    //        AddLabelWidgetToGrid("Best Fitness: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        bestFitnessLabel = CreateLabelWidget("0.1", font);
    //        tableLayoutPanel.Controls.Add(bestFitnessLabel, row, statsCol, TOP_LEFT);

    //        row = 0;
    //        labelCol += 2;
    //        statsCol += 2;

    //        AddLabelWidgetToGrid("GA Settings", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        row++;

    //        string selectionType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(settings["selection_type"].ToString().ToLower());
    //        AddLabelWidgetToGrid("Selection Type: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(selectionType, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        double probSBX = Convert.ToDouble(settings["probability_SBX"]) * 100;
    //        double probSPBX = Convert.ToDouble(settings["probability_SPBX"]) * 100;
    //        string crossoverType = $"{probSBX}% SBX\n{probSPBX}% SPBX";
    //        AddLabelWidgetToGrid("Crossover Type: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(crossoverType, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        double probGaussian = Convert.ToDouble(settings["probability_gaussian"]) * 100;
    //        double probUniform = Convert.ToDouble(settings["probability_random_uniform"]) * 100;
    //        string mutationType = $"{probGaussian}% Gaussian\n{probUniform}% Uniform";
    //        AddLabelWidgetToGrid("Mutation Type: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(mutationType, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        AddLabelWidgetToGrid("Mutation Rate:", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        double mutationRate = Convert.ToDouble(settings["mutation_rate"]) * 100;
    //        string mutationRateType = settings["mutation_rate_type"].ToString().ToLower().Capitalize();
    //        string mutationRateString = $"{mutationRate}% + {mutationRateType}";
    //        AddLabelWidgetToGrid(mutationRateString, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        AddLabelWidgetToGrid("Lifespan: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        string lifespan = settings["lifespan"].ToString();
    //        if (lifespan == "Infinity")
    //            lifespan = "Infinite";
    //        AddLabelWidgetToGrid(lifespan, font, tableLayoutPanel, row, statsCol, TOP_LEFT);

    //        row = 0;
    //        labelCol += 2;
    //        statsCol += 2;

    //        AddLabelWidgetToGrid("NN Settings", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        row++;

    //        string hiddenLayerActivation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(settings["hidden_layer_activation"].ToString().ToLower());
    //        AddLabelWidgetToGrid("Hidden Activation: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(hiddenLayerActivation, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        string outputLayerActivation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(settings["output_layer_activation"].ToString().ToLower());
    //        AddLabelWidgetToGrid("Output Activation: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(outputLayerActivation, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        string networkArchitecture = $"[{settings["vision_type"] * 3 + 4 + 4}, {string.Join(", ", (int[])settings["hidden_network_architecture"])}, 4]";
    //        AddLabelWidgetToGrid("NN Architecture: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(networkArchitecture, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        string snakeVision = settings["vision_type"] + " directions";
    //        AddLabelWidgetToGrid("Snake Vision: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(snakeVision, font, tableLayoutPanel, row, statsCol, TOP_LEFT);
    //        row++;

    //        string appleSelfVisionType = settings["apple_and_self_vision"].ToString().ToLower();
    //        AddLabelWidgetToGrid("Apple/Self Vision: ", fontBold, tableLayoutPanel, row, labelCol, TOP_LEFT);
    //        AddLabelWidgetToGrid(appleSelfVisionType, font, tableLayoutPanel, row, statsCol, TOP_LEFT);

    //        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
    //        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
    //        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

    //        Controls.Add(tableLayoutPanel);
    //        Show();
    //    }

    //    private Label CreateLabelWidget(string stringLabel, Font font)
    //    {
    //        Label label = new Label();
    //        label.Text = stringLabel;
    //        label.Font = font;
    //        label.Margin = Padding.Empty;
    //        return label;
    //    }

    //    private void AddLabelWidgetToGrid(string stringLabel, Font font, TableLayoutPanel grid, int row, int col, Padding alignment)
    //    {
    //        Label label = new Label();
    //        label.Text = stringLabel;
    //        label.Font = font;
    //        label.Margin = Padding.Empty;
    //        grid.Controls.Add(label, col, row);
    //    }
    //}
}
