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

        //計算整層的FeedForward
        public void CalculateFeedForward(List<int> inputs)
        {
            if (inputs.Count() != _nodes.Count())
                throw new Exception("輸入層與輸入的值個數不同");
            for (int nodeIndex = 0; nodeIndex < _nodes.Count(); nodeIndex++)
                _nodes[nodeIndex].Output = inputs[nodeIndex];
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
