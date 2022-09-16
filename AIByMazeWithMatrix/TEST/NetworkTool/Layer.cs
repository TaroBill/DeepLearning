using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using NetworkTool.Function;

namespace NetworkTool
{
    public class Layer
    {
        protected const double LEARNING_RATE = 0.01;
        protected Matrix<double> _inputs;
        protected Matrix<double> _weights;
        protected Matrix<double> _outputs;
        protected IFunction _funtion;

        public Layer()
        {
            _funtion = new Function.Nope();
            _inputs = new Matrix<double>(0, 0);
            _weights = new Matrix<double>(0, 0);
            _outputs = new Matrix<double>(0, 0);
        }

        public Layer(int numberOfNodes, int numberOfWeightsPerNode) : this()
        {
            _inputs = new Matrix<double>(1, numberOfWeightsPerNode);
            _weights = new Matrix<double>(numberOfNodes, numberOfWeightsPerNode);
            _outputs = new Matrix<double>(1, numberOfNodes);
        }

        public Layer(string data) : this()
        {
            string[] values = data.Split('\n');
            string[] rowValues;
            int input = Convert.ToInt32(values[1].Split(':')[1]);
            int output = Convert.ToInt32(values[2].Split(':')[1]);
            _inputs = new Matrix<double>(1, input);
            _weights = new Matrix<double>(output, input);
            _outputs = new Matrix<double>(1, output);
            for (int i = 0; i < output; i++)
            {
                rowValues = values[i + 3].Substring(1, values[i + 3].Length - 2).Split(',');
                for (int j = 0; j < input; j++)
                {
                    _weights[i, j] = Convert.ToDouble(rowValues[j]);
                }
            }
        }

        public void SetFunction(IFunction ActivationFuntion)
        {
            _funtion = ActivationFuntion;
        }

        public string GetFunctionName()
        {
            return _funtion.GetName();
        }

        public void SetWeightAt(int nodeIndex, int weightIndex, double value)
        {
            _weights[nodeIndex, weightIndex] = value;
        }

        public virtual void RandomlyInitializeWeights(int seed)
        {
            Random random = new Random(seed);
            int rowCount = _weights.RowCount;
            int columnCount = _weights.ColumnCount;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    _weights[i, j] = random.NextDouble();
                }
            }
        }

        #region Getter

        public int GetNumberOfNode()
        {
            return _outputs.ColumnCount;
        }

        public Matrix<double> GetWeightMatrix()
        {
            return new Matrix<double>(_weights);
        }

        public Matrix<double> GetOutputMatrix()
        {
            return new Matrix<double>(_outputs);
        }

        #endregion

        public virtual Matrix<double> InputData(Matrix<double> data)
        {
            _inputs = new Matrix<double>(data);
            Matrix<double> sum = _inputs * _weights.Transposition();

            _outputs = sum.ConvertTo<double>(_funtion.Activation);

            return new Matrix<double>(_outputs);
        }

        public virtual Matrix<double> InputError(Matrix<double> errors)
        {
            Func<double, double, double> calculateDelta = (output, error) =>
              {
                  return _funtion.PartDerivativeActivation(output) * error;
              };
            Matrix<double> delta = _outputs.Joint(errors, calculateDelta);
            
            Matrix<double> outErrors = delta * _weights;
            _weights -= LEARNING_RATE * (delta.Transposition() * _inputs);

            return outErrors;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            Func<double, string> stringFormat = (value) =>
             {
                 int digit = 6;
                 int convertedValue = (int)(value * Math.Pow(10, digit) + 0.5);
                 string temp = convertedValue.ToString().PadRight(digit+1, '0');
                 return $"\t{temp.Remove(temp.Count() - digit)}.{temp.Remove(0, temp.Count() - digit)}";
             };
            result.AppendLine($"Layer");
            result.AppendLine($"Input:{_inputs.ColumnCount}");
            result.AppendLine($"Output:{_outputs.ColumnCount}");
            result.AppendLine(_weights.ToString(stringFormat));
            return result.ToString();
        }

    }

}
