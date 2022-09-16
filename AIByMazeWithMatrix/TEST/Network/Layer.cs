using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class Layer
    {
        protected const double LEARNING_RATE = 0.01;
        protected List<Node> _layer;
        protected Func<double, double> ForwardFuntion;
        protected Func<double, double> BackwardFuntion;
        protected double ReLuActivation(double value) => value < 0 ? 0 : value;
        protected double ReLuPartDerivateOfActivation(double value) => 1;

        public Layer()
        {
            ForwardFuntion = ReLuActivation;
            BackwardFuntion = ReLuPartDerivateOfActivation;
            _layer = new List<Node>();
        }

        public Layer(int numberOfNodes, int numberOfWeightsPerNode) : this()
        {
            for (int i = 0; i < numberOfNodes; i++)
            {
                _layer.Add(new Node(numberOfWeightsPerNode));
                for (int j = 0; j < numberOfWeightsPerNode; j++)
                {
                    _layer[i].SetWeightAt(j, 0);
                }
            }
        }

        public Layer(string data):this()
        {
            string[] values = data.Split('\n');
            foreach (string nodeData in values)
            {
                if (nodeData == "")
                    continue;
                if (nodeData.First() != '@')
                    continue;
                _layer.Add(new Node(nodeData.Remove(0, 1)));
            }
        }

        public void SetFunction(Func<double, double> Activation, Func<double, double> PartDerivativeOfActivation)
        {
            ForwardFuntion = Activation;
            BackwardFuntion = PartDerivativeOfActivation;
        }

        public void RandomlyInitializeWeights()
        {
            foreach (Node node in _layer)
            {
                node.RandomlyInitializeWeights();
            }
        }

        #region Getter

        public double GetNodeValue(int nodeIndex)
        {
            return _layer[nodeIndex].GetValue();
        }

        public double GetNodeWeight(int nodeIndex, int weightIndex)
        {
            return _layer[nodeIndex].GetWeightAt(weightIndex);
        }

        public double[] GetAllNodeValue()
        {
            int numberOfNode = _layer.Count;
            double[] values = new double[numberOfNode];
            for (int i = 0; i < numberOfNode; i++)
            {
                values[i] = _layer[i].GetValue();
            }
            return values;
        }

        public double[][] GetAllNodeWeight()
        {
            int numberOfNode = _layer.Count;
            double[][] weights = new double[numberOfNode][];
            for (int i = 0; i < numberOfNode; i++)
            {
                weights[i] = _layer[i].GetAllWeight();
            }
            return weights;
        }

        public int GetNumberOfNode()
        {
            return _layer.Count;
        }

        public double[] GetData()
        {
            int count = _layer.Count;
            double[] data = new double[count];
            for (int i = 0; i < count; i++)
            {
                data[i] = _layer[i].GetValue();
            }
            return data;
        }

        #endregion

        public virtual double[] InputData(double[] data)
        {
            double[] output = new double[_layer.Count];
            int i = 0;
            foreach (Node node in _layer)
            {
                output[i++] = node.CalculateValue(ForwardFuntion, data);
            }
            return output;
        }

        public virtual double[] InputError(double[] data, double[] error)
        {
            int i = 0;
            int count = data.Length;
            double[] output = new double[count];
            i = 0;
            foreach (Node node in _layer)
            {
                node.SetError(error[i++]);
            }
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

        public void ResetError()
        {
            foreach (Node node in _layer)
            {
                node.SetError(0);
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Layer");
            foreach (Node node in _layer)
            {
                result.AppendLine($"@{node}");
            }
            return result.ToString();
        }

    }

}
