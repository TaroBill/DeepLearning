﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        private readonly List<NeuralLayer> _neuralLayers;
        private List<List<double>> _inputs;
        private List<List<double>> _realResult;
        private Random _random = new Random();

        public NeuralNetwork(List<List<double>> inputs, List<List<double>> realResults)
        {
            _neuralLayers = new List<NeuralLayer>();
            _inputs = inputs;
            _realResult = realResults;
        }

        //加入一層神經網路
        public void AddNeuralLayer(NeuralLayer layer)
        {
            if (_neuralLayers.Count() > 0)
                _neuralLayers[_neuralLayers.Count() - 1].InitWeights(layer.NodeAmount);
            _neuralLayers.Add(layer);
        }

        //開始訓練n次
        public void StartTrain(int trainTimes)
        {
            if (_inputs.Count() != _realResult.Count())
                throw new Exception("輸入數量必須和結果數量相同");
            for (int times = 0; times < trainTimes; times++)
            {
                int randomChoose = _random.Next(0, _inputs.Count());
                CalculateResult(_inputs[randomChoose]);
                LogisticBackpropagation(_realResult[randomChoose]);
            }
        }

        //取得輸入的預測結果
        public List<double> GetResult(List<double> inputs)
        {
            CalculateResult(inputs);
            return _neuralLayers[_neuralLayers.Count() - 1].GetOutput();
        }

        //計算此次輸出
        private void CalculateResult(List<double> inputs)
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
                lastLayer = _neuralLayers[layerIndex];
            }
        }

        //使用反向傳播法計算激勵函數為Logistic funtion
        private void LogisticBackpropagation(List<double> realResults)
        {
            List<double> outputNodesAka = _neuralLayers[_neuralLayers.Count() - 1].LogisticBackpropagation(realResults);
            _neuralLayers[_neuralLayers.Count() - 2].LogisticBackpropagationSetWeight(outputNodesAka);
            for (int layerIndex = _neuralLayers.Count() - 2; layerIndex > 0; layerIndex--)
            {
                outputNodesAka = _neuralLayers[layerIndex].LogisticBackpropagation();
                _neuralLayers[layerIndex - 1].LogisticBackpropagationSetWeight(outputNodesAka);
            }
        }

        //印出該層的所有weight
        public void PrintWeights(int layerIndex)
        {
            List<List<double>> result = _neuralLayers[layerIndex].GetWeights();
            for (int index = 0; index < result.Count(); index++)
            {
                for (int weightIndex = 0; weightIndex < result[index].Count(); weightIndex++)
                {
                    Console.WriteLine(result[index][weightIndex]);
                }
                Console.WriteLine("==============");
            }
            Console.WriteLine("////////////////////");
        }
    }
}
