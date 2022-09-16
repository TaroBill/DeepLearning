using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using NetworkTool.Function;

namespace NetworkTool
{
    public class NormalizationGate
    {
        private Matrix<double> _inputs;
        private Matrix<double> _convertedInput;
        private Matrix<double> _outputs;
        private Matrix<double> _scale;
        private Matrix<double> _bias;
        private const double EPSILON = 0.0001;
        private Matrix<double> _tempAverage;
        private Matrix<double> _tempVariance;

        public NormalizationGate()
        {
            _inputs = new Matrix<double>(0, 0);
            _outputs = new Matrix<double>(0, 0);
            _scale = new Matrix<double>(1, 0);
            _bias = new Matrix<double>(1, 0);
        }

        public NormalizationGate(int numberOfNodes) : this()
        {
            _inputs = new Matrix<double>(1, numberOfNodes);
            _outputs = new Matrix<double>(1, numberOfNodes);
            _scale = new Matrix<double>(1, numberOfNodes);
            _bias = new Matrix<double>(1, numberOfNodes);
            _tempAverage = new Matrix<double>(1, numberOfNodes);
            _tempVariance = new Matrix<double>(1, numberOfNodes);

            for (int i = 0; i < numberOfNodes; i++)
            {
                _scale[0, i] = 1;
                _bias[0, i] = 0;
            }
        }

        public NormalizationGate(string data) : this()
        {
            //TODO NormalizationGate Load
            throw new NotImplementedException();
        }

        public virtual void RandomlyInitializeWeights(int seed)
        {
            Random random = new Random(seed);
            int numberOfNodes = _inputs.ColumnCount;
            for (int i = 0; i < numberOfNodes; i++)
            {
                _scale[0, i] = random.NextDouble() + 0.5;
                _bias[0, i] = random.NextDouble() * 2 - 1;
            }
        }

        #region Getter

        public int GetNumberOfNode()
        {
            return _outputs.ColumnCount;
        }
        
        #endregion

        public virtual Matrix<double> InputData(Matrix<double> data)
        {
            _inputs = new Matrix<double>(data);

            CalculateAverage();
            CalculateVariance();
            NormalizeInput();
            LinearTransform();

            return new Matrix<double>(_outputs);
        }

        private Matrix<double> OneMatrix(int numberOfRow, int numberOfColumn)
        {
            Matrix<double> result = new Matrix<double>(numberOfRow, numberOfColumn);
            for (int i = 0; i < numberOfRow; i++)
            {
                for (int j = 0; j < numberOfColumn; j++)
                {
                    result[i, j] = 1;
                }
            }
            return result;
        }

        private void CalculateAverage()
        {
            int numberOfData = _inputs.RowCount;
            int numberOfNode = _inputs.ColumnCount;

            _tempAverage = OneMatrix(1, numberOfData) * _inputs * (1.0 / numberOfData);
        }

        private void CalculateVariance()
        {
            int numberOfData = _inputs.RowCount;
            int numberOfNode = _inputs.ColumnCount;

            Matrix<double> calculatedResult = _inputs - (OneMatrix(numberOfData, 1) * _tempAverage);
            _tempVariance = OneMatrix(1, numberOfData) * calculatedResult.ConvertTo(x => x * x) * (1.0 / numberOfData);
        }

        private void NormalizeInput()
        {
            int numberOfData = _inputs.RowCount;
            int numberOfNode = _inputs.ColumnCount;

            Matrix<double> calculatedResult = _inputs - (OneMatrix(numberOfData, 1) * _tempAverage);
            _convertedInput = calculatedResult.Joint(OneMatrix(numberOfData, 1) * _tempVariance, (x, y) => (x / Math.Sqrt(y + EPSILON)));

        }

        private void LinearTransform()
        {
            int numberOfData = _inputs.RowCount;
            int numberOfNode = _inputs.ColumnCount;

            _outputs = _convertedInput.Joint(OneMatrix(numberOfData, 1) * _scale, (x, y) => x * y) + (OneMatrix(numberOfData, 1) * _bias);
        }

        public virtual Matrix<double> InputError(Matrix<double> errors)
        {
            throw new NotImplementedException();
            /*Func<double, double, double> calculateDelta = (output, error) =>
              {
                  return _funtion.PartDerivativeActivation(output) * error;
              };
            Matrix<double> delta = _outputs.Joint(errors, calculateDelta);
            
            Matrix<double> outErrors = delta * _weights;
            _weights -= LEARNING_RATE * (delta.Transposition() * _inputs);

            return outErrors;//*/
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
            result.AppendLine($"Normalization Gate");
            result.AppendLine($"Input:{_inputs.ColumnCount}");
            result.AppendLine($"Output:{_outputs.ColumnCount}");
            result.AppendLine($"Scale:{_scale}");
            result.AppendLine($"Bias:{_bias}");
            return result.ToString();
        }

    }

}
