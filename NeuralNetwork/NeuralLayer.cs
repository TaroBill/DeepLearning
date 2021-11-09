using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralLayer
    {
        public enum LayerType
        {
            Input,
            Hiden,
            Output
        };
        private List<NeuralNode> _nodes = new List<NeuralNode>();
        LayerType _type;
        private double _bias;
        private double _biasWeight;
        private double _learningRate;

        public NeuralLayer(int nodesAmount, LayerType type, double learningRate, double bias, double biasWeight)
        {
            _type = type;
            _learningRate = learningRate;
            _bias = bias;
            _biasWeight = biasWeight;
            InitializeNodes(nodesAmount);
        }

        //初始化節點數
        public void InitializeNodes(int amount)
        {
            _nodes.Clear();
            for (int index = 0; index < amount; index++)
                _nodes.Add(new NeuralNode());
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
                _nodes[nodeIndex].LogisticFunction(inputLayer.Nodes, nodeIndex, _bias, _biasWeight);
        }

        //計算輸入整層的FeedForward
        public void CalculateFeedForward(List<double> inputs)
        {
            if (inputs.Count() != _nodes.Count())
                throw new Exception("輸入層與輸入的值個數不同");
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
                _nodes[nodeIndex].Output = inputs[nodeIndex];
        }

        //使用反向傳播法計算激勵函數為Logistic funtion，輸出層到內層
        public List<double> LogisticBackpropagation(List<double> realValue)
        {
            if (realValue.Count() < _nodes.Count())
                throw new Exception("實際輸出結果數量要跟輸出節點數量相同");
            List<double> outputAka = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                double outputAtIndex = _nodes[nodeIndex].Output;
                outputAka.Add(0 - (realValue[nodeIndex] - outputAtIndex) * outputAtIndex * (1 - outputAtIndex));
            }
            return outputAka;
        }

        //使用反向傳播法計算激勵函數為Logistic funtion，隱藏層到輸入層或隱藏層
        public List<double> LogisticBackpropagation()
        {
            List<double> outputAka = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                double outputAtIndex = _nodes[nodeIndex].Output;
                outputAka.Add(_nodes[nodeIndex].TotalAkaWeight * outputAtIndex * (outputAtIndex - 1));
            }
            return outputAka;
        }

        //使用aka來設定Weight(與上面的函示位於不同層)
        public void LogisticBackpropagationSetWeight(List<double> akaValue)
        {
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
            {
                _nodes[nodeIndex].ResetTotalAkaWeight();
                for (int akaIndex = 0; akaIndex < akaValue.Count(); akaIndex++)
                {
                    _nodes[nodeIndex].CalculateTotalAkaWeight(akaValue[akaIndex], akaIndex);
                    _nodes[nodeIndex].SetWeight(akaValue[akaIndex], akaIndex, _learningRate);
                }
            }
        }

        public List<NeuralNode> Nodes
        {
            get
            {
                return _nodes;
            }
        }
    }
}
