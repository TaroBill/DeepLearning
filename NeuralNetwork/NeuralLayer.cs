using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralLayer
    {
        private readonly List<NeuralNode> _nodes = new List<NeuralNode>();
        private readonly double _bias;
        private readonly List<double> _biasWeight = new List<double>();
        private readonly double _learningRate;
        private Random _random = new Random();

        public NeuralLayer(int nodesAmount, double learningRate, double bias)
        {
            _learningRate = learningRate;
            _bias = bias;
            InitializeNodes(nodesAmount);
        }

        //初始化節點數
        public void InitializeNodes(int amount)
        {
            _nodes.Clear();
            for (int index = 0; index < amount; index++)
            {
                _nodes.Add(new NeuralNode());
                _random = new Random(Guid.NewGuid().GetHashCode());
                double randomValue = _random.NextDouble();
                _biasWeight.Add((randomValue * 2 - 1));
            }

        }

        //設定輸出的weights數量
        public void InitWeights(int amount)
        {
            foreach (NeuralNode nodes in _nodes)
            {
                nodes.SetWeightsAmount(amount);
            }
        }

        //加入一個節點到Mode
        public void AddNode()
        {
            _nodes.Add(new NeuralNode());
        }

        //計算整層的FeedForward
        public void CalculateFeedForward(NeuralLayer inputLayer)
        {
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
                _nodes[nodeIndex].LogisticFunction(inputLayer._nodes, nodeIndex, _bias, _biasWeight[nodeIndex]);
        }

        //計算輸入整層的FeedForward
        public void CalculateFeedForward(List<double> inputs)
        {
            if (inputs.Count() != _nodes.Count())
                throw new Exception("輸入層與輸入的值個數不同");
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
                _nodes[nodeIndex].Output = inputs[nodeIndex];
        }

        //取得該層所有節點的輸出
        public List<double> GetOutput()
        {
            List<double> output = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
                output.Add(_nodes[nodeIndex].Output);
            return output;
        }

        //使用反向傳播法計算激勵函數為Logistic funtion，輸出層到內層
        public List<double> LogisticBackpropagation(List<double> realValue)
        {
            if (realValue.Count() < _nodes.Count())
                throw new Exception("實際輸出結果數量要跟輸出節點數量相同");
            List<double> outputDelta = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                double outputAtIndex = _nodes[nodeIndex].Output;
                outputDelta.Add(0 - ((realValue[nodeIndex] - outputAtIndex) * outputAtIndex * (1 - outputAtIndex)));
            }
            SetBiasWeight(outputDelta);
            return outputDelta;
        }

        //設定此層的bias和bias weight
        private void SetBiasWeight(List<double> thisDeltas)
        {
            if (thisDeltas.Count() != _biasWeight.Count())
                throw new Exception("計算出的delta量應該要和此層node數量相同");
            for (int deltaIndex = 0; deltaIndex < thisDeltas.Count(); deltaIndex++)
            {
                double result = (thisDeltas[deltaIndex] * _bias);
                _biasWeight[deltaIndex] -= _learningRate * result;
            }
        }

        //使用反向傳播法計算激勵函數為Logistic funtion，隱藏層到輸入層或隱藏層
        public List<double> LogisticBackpropagation()
        {
            List<double> outputDelta = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                double outputAtIndex = _nodes[nodeIndex].Output;
                outputDelta.Add(_nodes[nodeIndex].TotalDeltaWeight * outputAtIndex * (1 - outputAtIndex));
            }
            SetBiasWeight(outputDelta);
            return outputDelta;
        }

        //使用delta來設定Weight(與上面的函式位於不同層)
        public void LogisticBackpropagationSetWeight(List<double> deltaValue)
        {
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                _nodes[nodeIndex].ResetTotalDeltaWeight();
                for (int deltaIndex = 0; deltaIndex < deltaValue.Count(); deltaIndex++)
                {
                    _nodes[nodeIndex].CalculateTotalDeltaWeight(deltaValue[deltaIndex], deltaIndex);
                    _nodes[nodeIndex].SetWeight(deltaValue[deltaIndex], deltaIndex, _learningRate);
                }
            }
        }

        //取得該層的所有節點的所有weight
        public List<List<double>> GetWeights()
        {
            List<List<double>> output = new List<List<double>>();
            foreach (NeuralNode node in _nodes)
            {
                output.Add(node.OutputWeight.ToList());
            }
            return output;
        }

        public int NodeAmount
        {
            get
            {
                return _nodes.Count();
            }
        }
    }
}
