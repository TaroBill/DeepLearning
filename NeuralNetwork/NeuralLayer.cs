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
            for (int index = 0; index < nodesAmount; index++)
                _nodes.Add(new NeuralNode());
        }
    }
}
