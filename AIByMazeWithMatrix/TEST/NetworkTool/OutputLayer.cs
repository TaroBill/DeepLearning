using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTool.Function;

namespace NetworkTool
{
    public class OutputLayer : Layer
    {
        private readonly IFunction DEFAULT_FUNCTION = new Function.Logistic();

        public OutputLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes, numberOfWeightsPerNode)
        {
            _funtion = DEFAULT_FUNCTION;
        }

        public OutputLayer(string data) : base(data)
        {
            _funtion = DEFAULT_FUNCTION;
        }

        public override void RandomlyInitializeWeights(int seed)
        {
            base.RandomlyInitializeWeights(seed);
        }

        public override Matrix<double> InputData(Matrix<double> data)
        {
            return base.InputData(data);
        }

        public override Matrix<double> InputError(Matrix<double> errors)
        {
            return base.InputError(errors);
        }
        
        /*
        public double[] InputTarget(double[] data, double[] target)
        {
            int count = _layerWeights.Count;
            double[] error = new double[count];
            for (int i = 0; i < count; i++)
            {
                error[i] = _layerWeights[i].GetValue() - target[i];
            }
            return InputError(data, error);
        }

        public void AddError(params double[] error)
        {
            int i = 0;
            foreach (Node node in _layerWeights)
            {
                node.AddError(error[i++]);
            }
        }

        public void AddErrorAt(int index ,double error)
        {
            _layerWeights[index].AddError(error);
        }

        public void AddErrorByTarget(params double[] target)
        {
            int count = _layerWeights.Count;
            for (int i = 0; i < count; i++)
            {
                _layerWeights[i].AddError(_layerWeights[i].GetValue() - target[i]);
            }
        }

        public void AddErrorByTargetAt(int index, double target)
        {
            _layerWeights[index].AddError(_layerWeights[index].GetValue() - target);
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
            foreach (Node node in _layerWeights)
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
            foreach (Node node in _layerWeights)
            {
                error += Math.Pow(node.GetError(), 2);
            }
            error /= 2;
            return error;
        }

        public double GetMaxValue()
        {
            double max = _layerWeights[0].GetValue();
            foreach (Node node in _layerWeights)
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
            double max = _layerWeights[0].GetValue();
            int index = 0;
            int i = 0;
            foreach (Node node in _layerWeights)
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

        public double GetError(int index)
        {
            return _layerWeights[index].GetError();
        }

        public Matrix<double> GetValue()
        {
            return _outputs;
        }
        //*/

        public override string ToString()
        {
            return "Output" + base.ToString();
        }
    }
}
