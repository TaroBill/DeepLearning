using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{

    public class Node
    {
        double _value;
        double _error;
        List<double> _weights;
        IOptimizer _optimizer;

        public Node()
        {
            _value = 0;
            _error = 0;
            _weights = new List<double>();
            _optimizer = new SGD();
        }

        public Node(int numberOfWeight, IOptimizer optimizer) : this()
        {
            for (int i = 0; i < numberOfWeight; i++)
            {
                _weights.Add(0);
            }
            _optimizer = optimizer;
        }

        public Node(string data) : this()
        {
            string temp = "";
            foreach (char c in data)
            {
                switch (c)
                {
                    case ',':
                        _weights.Add(double.Parse(temp));
                        temp = "";
                        break;
                    default:
                        temp += c;
                        break;
                }
            }
        }

        public void RandomlyInitializeWeights(int seed)
        {
            Random random = new Random(seed);
            int number = _weights.Count();
            for (int i = 0; i < number; i++)
            {
                _weights[i] = random.NextDouble();
            }
        }

        #region Data

        public void AddWeight(double value)
        {
            _weights.Add(0);
        }

        public void RemoveWeightAt(int index)
        {
            _weights.RemoveAt(index);
        }

        public void SetWeightAt(int index, double value)
        {
            _weights[index] = value;
        }

        public double GetWeightAt(int index)
        {
            return _weights[index];
        }

        public double[] GetAllWeight()
        {
            int numberOfWeight = _weights.Count();
            double[] weights = new double[numberOfWeight];
            for (int i = 0; i < numberOfWeight; i++)
            {
                weights[i] = _weights[i];
            }
            return weights;
        }

        public int GetNumberOfWeights()
        {
            return _weights.Count();
        }

        public void SetValue(double value)
        {
            _value = value;
        }

        public double GetValue()
        {
            return _value;
        }

        public double CalculateValue(params double[] values)
        {
            _value = GetSum(values);
            return _value;
        }

        public double CalculateValue(Func<double, double> conversionFunction, params double[] values)
        {
            _value = GetSum(conversionFunction, values);
            return _value;
        }

        private double GetSum(params double[] values)
        {
            int numberOfWeight = _weights.Count;
            //int numberOfValue = values.Length;

            if (numberOfWeight == 0)
                return _value;

            double sum = 0;
            for (int i = 0; i < numberOfWeight; i++)
            {
                sum += values[i] * _weights[i];
            }
            return sum;
        }

        private double GetSum(Func<double, double> conversionFunction, params double[] values)
        {
            return conversionFunction(GetSum(values));
        }

        public void SetError(double value)
        {
            _error = value;
        }

        public void AddError(double value)
        {
            _error += value;
        }

        public double[] UpdateWeightsAndGetErrors(double learningRate, Func<double, double> conversionFunction, params double[] values)
        {
            int numberOfWeights = this.GetNumberOfWeights();
            double[] errors = new double[numberOfWeights];
            double delta = _error * conversionFunction(_value);
            for (int i = 0; i < numberOfWeights; i++)
            {
                errors[i] += delta * _weights[i];
                _weights[i] -= _optimizer.GetDelta(delta * values[i]);
            }
            return errors;
        }

        public double GetError()
        {
            return _error;
        }

        #endregion

        public override string ToString()
        {
            string result = "";
            foreach (double value in _weights)
            {
                result += $"{FormatNumber(value, 6)},";
            }
            return result;
        }

        private string FormatNumber(double value, int digit)
        {
            int minUnit = (int)Math.Pow(10, digit);
            int converted = (int)(value * minUnit + 0.5);
            string result = "";
            if (converted < 0)
            {
                result += "-";
                converted *= -1;
            }
            else
            {
                result += " ";
            }
            result += $"{converted / minUnit}.";
            result += $"{converted % minUnit}".PadRight(digit, '0');
            return result;
        }

    }

}
