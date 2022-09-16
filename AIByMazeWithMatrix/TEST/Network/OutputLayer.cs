using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class OutputLayer : Layer
    {
        public OutputLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes, numberOfWeightsPerNode) { }

        public OutputLayer(string data) : base(data) { }

        public override double[] InputData(double[] data)
        {
            return base.InputData(data);
        }

        public override double[] InputError(double[] data, double[] error)
        {
            return base.InputError(data, error);
        }

        public double[] InputTarget(double[] data, double[] target)
        {
            int count = _layer.Count;
            double[] error = new double[count];
            for (int i = 0; i < count; i++)
            {
                error[i] = _layer[i].GetValue() - target[i];
            }
            return InputError(data, error);
        }

        public void AddError(params double[] error)
        {
            int i = 0;
            foreach (Node node in _layer)
            {
                node.AddError(error[i++]);
            }
        }

        public void AddErrorAt(int index ,double error)
        {
            _layer[index].AddError(error);
        }

        public void AddErrorByTarget(params double[] target)
        {
            int count = _layer.Count;
            for (int i = 0; i < count; i++)
            {
                _layer[i].AddError(_layer[i].GetValue() - target[i]);
            }
        }

        public void AddErrorByTargetAt(int index, double target)
        {
            _layer[index].AddError(_layer[index].GetValue() - target);
        }

        public double[] Update(params double[] data)
        {
            int i = 0;
            int count = data.Length;
            double[] output = new double[count];
            for (i = 0; i < count; i++)
            {
                output[i] = 0;
            }
            foreach (Node node in _layer)
            {
                i = 0;
                foreach (double value in node.UpdateWeightsAndGetErrors(LEARNING_RATE, BackwardFuntion, data))
                {
                    output[i++] += value;
                }
            }
            return output;
        }

        public double GetResidualSumofSquares()
        {
            double error = 0;
            foreach (Node node in _layer)
            {
                error += Math.Pow(node.GetError(), 2);
            }
            error /= 2;
            return error;
        }

        public double GetMaxValue()
        {
            double max = _layer[0].GetValue();
            foreach (Node node in _layer)
            {
                if (max < node.GetValue())
                {
                    max = node.GetValue();
                }
            }
            return max;
        }

        public int GetMaxIndex()
        {
            double max = _layer[0].GetValue();
            int index = 0;
            int i = 0;
            foreach (Node node in _layer)
            {
                if (max < node.GetValue())
                {
                    max = node.GetValue();
                    index = i;
                }
                i++;
            }
            return index;
        }

        public double GetValue(int index)
        {
            return _layer[index].GetValue();
        }

        public override string ToString()
        {
            return "Output" + base.ToString();
        }

    }
}
