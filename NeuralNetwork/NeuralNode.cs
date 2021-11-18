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
        private List<double> _outputWeight;
        private Random _random;
        private double _totalAkaWeight;

        public NeuralNode()
        {
            _output = 0;
            _totalAkaWeight = 0;
            _outputWeight = new List<double>();
            _random = new Random();
        }

        //設定輸出有多少Node
        public void SetWeightsAmount(int amount)
        {
            _outputWeight.Clear();
            for (int index = 0; index < amount; index++)
            {
                _random = new Random(Guid.NewGuid().GetHashCode());
                double randomValue = _random.NextDouble();
                _outputWeight.Add((randomValue * 2 - 1));
            }
        }

        //增加一個輸出節點
        public void AddNewNodeWeight()
        {
            _random = new Random();
            double randomValue = _random.NextDouble();
            _outputWeight.Add((randomValue * 2 - 1));
        }

        //對該節點進行Logistic運算(中間層)
        public double LogisticFunction(List<NeuralNode> inputNodes, int thisNodeIndex, double bias, double biasWeight)
        {
            double net = 0;
            foreach (NeuralNode Neuron in inputNodes)
                net += (Neuron.Output * Neuron._outputWeight[thisNodeIndex]);
            net += bias * biasWeight;
            _output = 1.0 / (1 + Math.Exp(0 - net));
            return _output;
        }

        //利用aka設定該節點到輸出的權重
        public void SetWeight(double aka, int outputNodeIndex, double learningRate = 0.01)
        {
            double result = (aka * _output);
            _outputWeight[outputNodeIndex] -= learningRate * result;
        }

        //把該節點和所有輸出節點間的weight乘與Aka再相加存起來
        public void CalculateTotalAkaWeight(double aka, int outputNodeIndex)
        {
            _totalAkaWeight += aka * _outputWeight[outputNodeIndex];
        }

        //將該節點的totalAkaWeight重置
        public void ResetTotalAkaWeight()
        {
            _totalAkaWeight = 0;
        }

        public double TotalAkaWeight
        {
            get
            {
                return _totalAkaWeight;
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
