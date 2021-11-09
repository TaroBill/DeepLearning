using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        private readonly List<NeuralLayer> _neuralLayers;


        //加入一層神經網路
        public void AddNeuralLayer(NeuralLayer layer)
        {
            _neuralLayers.Add(layer);
        }

        //計算此次輸出
        public void CalculateResult(List<double> inputs)
        {
            if (_neuralLayers == null)
                throw new Exception("沒有輸入任何Layer");
            else if (_neuralLayers.Count() < 3)
                throw new Exception("至少要有三層，輸入，中間，輸出");
            _neuralLayers[0].CalculateFeedForward(inputs);
            NeuralLayer lastLayer = _neuralLayers[0];
            for (int layerIndex = 1; layerIndex < _neuralLayers.Count(); layerIndex++)
            {
                _neuralLayers[layerIndex].CalculateFeedForward(lastLayer);
            }
        }

        //使用反向傳播法計算激勵函數為Logistic funtion
        public void LogisticBackpropagation(List<double> realResults)
        {
            List<double> outputNodesAka = _neuralLayers[_neuralLayers.Count() - 1].LogisticBackpropagation(realResults);
            _neuralLayers[_neuralLayers.Count() - 2].LogisticBackpropagationSetWeight(outputNodesAka);
            for (int layerIndex = _neuralLayers.Count() - 2; layerIndex > 0; layerIndex--)
            {
                outputNodesAka = _neuralLayers[layerIndex].LogisticBackpropagation();
                _neuralLayers[layerIndex - 1].LogisticBackpropagationSetWeight(outputNodesAka);
            }
        }
    }
}
