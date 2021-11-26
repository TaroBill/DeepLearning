using NeuralNetwork.ActivationFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNode
    {
        private double _output;
        private double _net;
        private List<double> _outputWeight;
        private Random _random;
        private double _totalDeltaWeight;
        private IActivation _activationFunction;

        public NeuralNode(IActivation activationFunction)
        {
            _output = 0;
            _totalDeltaWeight = 0;
            _outputWeight = new List<double>();
            _random = new Random();
            _activationFunction = activationFunction;
        }

        //設定輸出有多少Node
        public void SetWeightsAmount(int amount)
        {
            _outputWeight.Clear();
            for (int index = 0; index < amount; index++)
            {
                _random = new Random(Guid.NewGuid().GetHashCode());
                double randomValue = _random.NextDouble();
                //_outputWeight.Add((randomValue * 2 - 1));
                _random = new Random(Guid.NewGuid().GetHashCode());
                double randomValue2 = _random.NextDouble();
                double mean = 0;
                double standard = 0.3;
                double guassRandom = Math.Sqrt(-2 * Math.Log(randomValue)) * Math.Cos(2 * Math.PI * randomValue2) * standard + mean;
                _outputWeight.Add(guassRandom * Math.Sqrt(2.0 / amount));
            }
        }

        //從資料設定輸出weights
        public void LoadWeights(List<double> weights)
        {
            _outputWeight.Clear();
            for (int index = 0; index < weights.Count(); index++)
            {
                _outputWeight.Add(weights[index]);
            }
        }

        //增加一個輸出節點
        public void AddNewNodeWeight()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            double randomValue = _random.NextDouble();
            //_outputWeight.Add((randomValue * 2 - 1));
            _random = new Random(Guid.NewGuid().GetHashCode());
            double randomValue2 = _random.NextDouble();
            double mean = 0;
            double standard = 0.3;
            double guassRandom = Math.Sqrt(-2 * Math.Log(randomValue)) * Math.Cos(2 * Math.PI * randomValue2) * standard + mean;
            _outputWeight.Add(guassRandom * Math.Sqrt(2.0 / (_outputWeight.Count()+1)));
        }

        //對該節點進行Logistic運算(中間層)
        public double ActivationFunction(List<NeuralNode> inputNodes, int thisNodeIndex, double bias, double biasWeight)
        {
            double net = 0;
            foreach (NeuralNode Neuron in inputNodes)
                net += (Neuron.Output * Neuron._outputWeight[thisNodeIndex]);
            net += bias * biasWeight;
            _net = net;
            _output = _activationFunction.ActivationFunction(net);
            return _output;
        }

        //利用delta設定該節點到輸出的權重
        public void SetWeight(double delta, int outputNodeIndex, double learningRate = 0.01)
        {
            double result = (delta * _output);
            _outputWeight[outputNodeIndex] -= learningRate * result;
        }

        //把該節點和所有輸出節點間的weight乘與Delta再相加存起來
        public void AddTotalDeltaWeight(double delta, int outputNodeIndex)
        {
            _totalDeltaWeight += delta * _outputWeight[outputNodeIndex];
        }

        //將該節點的totalDeltaWeight重置
        public void ResetTotalDeltaWeight()
        {
            _totalDeltaWeight = 0;
        }

        public double TotalDeltaWeight
        {
            get
            {
                return _totalDeltaWeight;
            }
        }

        public double Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
            }
        }

        public double Net
        {
            get
            {
                return _net;
            }
        }

        public List<double> OutputWeight
        {
            get
            {
                return _outputWeight;
            }
            set
            {
                _outputWeight = value;
            }
        }
    }
}
