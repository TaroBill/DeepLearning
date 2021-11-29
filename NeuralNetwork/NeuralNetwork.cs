using NeuralNetwork.ActivationFunction;
using NeuralNetwork.LossFunction;
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
        private List<List<double>> _inputs;
        private List<List<double>> _realResult;
        private Random _random = new Random();
        private ILossFunction _lossFunction;

        public NeuralNetwork(List<List<double>> inputs, List<List<double>> realResults)
        {
            _lossFunction =  new SquaredError();
            _neuralLayers = new List<NeuralLayer>();
            _inputs = inputs;
            _realResult = realResults;
        }

        public NeuralNetwork(ILossFunction lossFunction = null)
        {
            _lossFunction = new SquaredError() ?? lossFunction;
            _neuralLayers = new List<NeuralLayer>();
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
                _lossFunction.LossFunction(_realResult[randomChoose], _neuralLayers[_neuralLayers.Count() - 1].GetOutput());
                Backpropagation(_realResult[randomChoose]);
            }
        }

        //取得輸入的預測結果
        public List<double> GetResult(List<double> inputs)
        {
            CalculateResult(inputs);
            return _neuralLayers[_neuralLayers.Count() - 1].GetOutput();
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
                lastLayer = _neuralLayers[layerIndex];
            }
        }

        //使用反向傳播法計算激勵函數為Logistic funtion
        public void Backpropagation(List<double> realResults)
        {
            List<double> outputNodesDelta = _neuralLayers[_neuralLayers.Count() - 1].Backpropagation(realResults, _lossFunction);
            _neuralLayers[_neuralLayers.Count() - 2].BackpropagationSetWeight(outputNodesDelta);
            for (int layerIndex = _neuralLayers.Count() - 2; layerIndex > 0; layerIndex--)
            {
                outputNodesDelta = _neuralLayers[layerIndex].Backpropagation();
                _neuralLayers[layerIndex - 1].BackpropagationSetWeight(outputNodesDelta);
            }
        }

        //設置lossFunction
        public void SetLossFunction(ILossFunction lossFunction)
        {
            _lossFunction = lossFunction;
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
