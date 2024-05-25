using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeML.NeuralNet
{
    internal class NeuralNet
    {
        public int InputNodesCount { get; }
        public int HiddenNodesCount { get; }
        public int OutputNodesCount { get; }

        public Matrix InputHiddenWeights { get; private set; }
        public Matrix HiddenSecondHiddenWeights { get; private set; }
        public Matrix HiddenOutputWeights { get; private set; }

        public NeuralNet(int inputNodesCount, int hiddenNodesCount, int outputNodesCount)
        {
            InputNodesCount = inputNodesCount;
            HiddenNodesCount = hiddenNodesCount;
            OutputNodesCount = outputNodesCount;

            InputHiddenWeights = new Matrix(HiddenNodesCount, InputNodesCount + 1);
            HiddenSecondHiddenWeights = new Matrix(HiddenNodesCount, HiddenNodesCount + 1);
            HiddenOutputWeights = new Matrix(OutputNodesCount, HiddenNodesCount + 1);

            InputHiddenWeights.Randomize();
            HiddenSecondHiddenWeights.Randomize();
            HiddenOutputWeights.Randomize();
        }

        public void Mutate(double mutation)
        {
            InputHiddenWeights.Mutate(mutation);
            HiddenSecondHiddenWeights.Mutate(mutation);
            HiddenOutputWeights.Mutate(mutation);
        }

        public double[] Output(double[] inputs)
        {
            Matrix inputsMatrix = Matrix.SingleColumnMatrixFromArray(inputs);
            Matrix inputsBias = inputsMatrix.AddBias();

            Matrix hiddenInputs = InputHiddenWeights.Dot(inputsBias);
            Matrix hiddenOutputs = hiddenInputs.Activate();
            Matrix hiddenOutputsBias = hiddenOutputs.AddBias();

            Matrix hiddenInputs2 = HiddenSecondHiddenWeights.Dot(hiddenOutputsBias);
            Matrix hiddenOutputs2 = hiddenInputs2.Activate();
            Matrix hiddenOutputsBias2 = hiddenOutputs2.AddBias();

            Matrix outputInputs = HiddenOutputWeights.Dot(hiddenOutputsBias2);
            Matrix outputs = outputInputs.Activate();
            return outputs.ToArray();
        }

        public NeuralNet Crossover(NeuralNet partner)
        {
            NeuralNet child = new NeuralNet(InputNodesCount, HiddenNodesCount, OutputNodesCount);
            child.InputHiddenWeights = InputHiddenWeights.Crossover(partner.InputHiddenWeights);
            child.HiddenSecondHiddenWeights = HiddenSecondHiddenWeights.Crossover(partner.HiddenSecondHiddenWeights);
            child.HiddenOutputWeights = HiddenOutputWeights.Crossover(partner.HiddenOutputWeights);
            return child;
        }

        public NeuralNet Clone()
        {
            NeuralNet clone = new NeuralNet(InputNodesCount, HiddenNodesCount, OutputNodesCount);
            clone.InputHiddenWeights = InputHiddenWeights.Clone();
            clone.HiddenSecondHiddenWeights = HiddenSecondHiddenWeights.Clone();
            clone.HiddenOutputWeights = HiddenOutputWeights.Clone();
            return clone;
        }
       public  string NetToTable()
        {
            var sb = new StringBuilder();

            double[] whiArr = InputHiddenWeights.ToArray();
            double[] whhArr = HiddenSecondHiddenWeights.ToArray();
            double[] wohArr = HiddenOutputWeights.ToArray();

            for (int i = 0; i < whiArr.Length; i++)
            {
                sb.Append(whiArr[i].ToString(CultureInfo.InvariantCulture));
                sb.Append(";");
            }
            sb.AppendLine("#");

            for (int i = 0; i < whhArr.Length; i++)
            {
                sb.Append(whhArr[i].ToString(CultureInfo.InvariantCulture));
                sb.Append(";");
            }
            sb.AppendLine("#");

            for (int i = 0; i < wohArr.Length; i++)
            {
                sb.Append(wohArr[i].ToString(CultureInfo.InvariantCulture));
                sb.Append(";");
            }
            sb.AppendLine("#");

            var result = sb.ToString();
            return result;
        }


        public void TableToNet(string table)
        {
            double[] whiArr = new double[InputHiddenWeights.Rows * InputHiddenWeights.Columns];
            double[] whhArr = new double[HiddenSecondHiddenWeights.Rows * HiddenSecondHiddenWeights.Columns];
            double[] wohArr = new double[HiddenOutputWeights.Rows * HiddenOutputWeights.Columns];

            var arrays= table.Split('#');
            var whiVal =  arrays[0].Split(';', StringSplitOptions.RemoveEmptyEntries);
            var whhVal =  arrays[1].Split(';', StringSplitOptions.RemoveEmptyEntries);
            var whoVal = arrays[2].Split(';', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < whiArr.Length; i++)
            {
                double.TryParse(whiVal[i],
                    NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    CultureInfo.InvariantCulture,
                    out double val);
                whiArr[i] = val ;
            }
            for (int i = 0; i < whhArr.Length; i++)
            {
                double.TryParse(whhVal[i],
                    NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    CultureInfo.InvariantCulture,
                    out double val);
                whhArr[i] = val;
            }
            for (int i = 0; i < wohArr.Length; i++)
            {
                double.TryParse(whoVal[i],
                    NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    CultureInfo.InvariantCulture,
                    out double val);
                wohArr[i] = val;
            }

            InputHiddenWeights.FromArray(whiArr);
            HiddenSecondHiddenWeights.FromArray(whhArr);
            HiddenOutputWeights.FromArray(wohArr);
        }
    }
}