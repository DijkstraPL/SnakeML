using SnakeMLDesktop.NeuralNet.GameEngine;
using System.Windows.Controls;

namespace SnakeMLDesktop.UI
{
    //public class NeuralNetworkViz : UserControl
    //{
    //    private int _frames;
    //    private int _frames_since_last_apple;
    //    private Snake _snake;
    //    private int _horizontal_distance_between_layers;
    //    private int _vertical_distance_between_nodes;
    //    private int _num_neurons_in_largest_layer;
    //    private Dictionary<Tuple<int, int>, Tuple<int, int>> _neuronLocations;

    //    public NeuralNetworkViz(Control parent, Snake snake)
    //    {
    //        Parent = parent;
    //        _snake = snake;
    //        _horizontal_distance_between_layers = 50;
    //        _vertical_distance_between_nodes = 10;
    //        _num_neurons_in_largest_layer = _snake.network.layer_nodes.Max();
    //        _neuronLocations = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
    //        Show();
    //    }

    //    protected override void OnPaint(PaintEventArgs e)
    //    {
    //        base.OnPaint(e);
    //        ShowNetwork(e.Graphics);
    //    }

    //    public void ShowNetwork(Graphics g)
    //    {
    //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    //        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

    //        var verticalSpace = 8;
    //        var radius = 8;
    //        var height = Height;
    //        var width = Width;
    //        var layerNodes = _snake.network.layer_nodes;

    //        var defaultOffset = 30;
    //        var hOffset = defaultOffset;
    //        var inputs = _snake.vision_as_array;
    //        var output = _snake.network.FeedForward(inputs);
    //        var maxOutput = Array.IndexOf(output, output.Max());

    //        // Draw nodes
    //        for (var layer = 0; layer < layerNodes.Length; layer++)
    //        {
    //            var vOffset = (height - (2 * radius + verticalSpace) * layerNodes[layer]) / 2;
    //            double[] activations = null;

    //            if (layer > 0)
    //            {
    //                activations = _snake.network.params["A" + layer];
    //            }

    //            for (var node = 0; node < layerNodes[layer]; node++)
    //            {
    //                var xLoc = hOffset;
    //                var yLoc = node * (radius * 2 + verticalSpace) + vOffset;
    //                var key = Tuple.Create(layer, node);

    //                if (!_neuronLocations.ContainsKey(key))
    //                {
    //                    _neuronLocations[key] = Tuple.Create(xLoc, (int)(yLoc + radius));
    //                }

    //                if (layer == 0)
    //                {
    //                    g.FillEllipse(inputs[node, 0] > 0 ? Brushes.Green : Brushes.White, xLoc, yLoc, radius * 2, radius * 2);
    //                }
    //                else if (layer > 0 && layer < layerNodes.Length - 1)
    //                {
    //                    var saturation = Math.Min(Math.Max(activations[node], 0.0), 1.0);
    //                    var color = Color.FromArgb(255, 125 * 239, (int)(saturation * 240), 120 * 240);
    //                    g.FillEllipse(new SolidBrush(color), xLoc, yLoc, radius * 2, radius * 2);
    //                }
    //                else if (layer == layerNodes.Length - 1)
    //                {
    //                    var text = new[] { 'U', 'D', 'L', 'R' }[node].ToString();
    //                    g.DrawString(text, DefaultFont, Brushes.Black, xLoc + 30, yLoc + 1.5f * radius);

    //                    g.FillEllipse(node == maxOutput ? Brushes.Green : Brushes.White, xLoc, yLoc, radius * 2, radius * 2);
    //                }
    //            }

    //            hOffset += 150;
    //        }

    //        // Reset horizontal offset for the weights
    //        hOffset = defaultOffset;

    //        // Draw weights
    //        for (var l = 1; l < layerNodes.Length; l++)
    //        {
    //            var weights = _snake.network.params["W" + l];
    //            var prevNodes = weights.GetLength(1);
    //            var currNodes = weights.GetLength(0);

    //            for (var prevNode = 0; prevNode < prevNodes; prevNode++)
    //            {
    //                for (var currNode = 0; currNode < currNodes; currNode++)
    //                {
    //                    var penColor = weights[currNode, prevNode] > 0 ? Pens.Blue : Pens.Red;

    //                    var start = _neuronLocations[Tuple.Create(l - 1, prevNode)];
    //                    var end = _neuronLocations[Tuple.Create(l, currNode)];

    //                    g.DrawLine(penColor, start.Item1 + radius * 2, start.Item2, end.Item1, end.Item2);
    //                }
    //            }
    //        }
    //    }
   // }
}
