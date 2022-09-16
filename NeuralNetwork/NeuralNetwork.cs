using NeuralNetwork.ActivationFunction;
using NeuralNetwork.LossFunction;
using NeuralNetwork.Optimizer;
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
        private readonly Random _random = new Random();
        private ILossFunction _lossFunction;
        private IOptimizer _optimizer;
        private double _loss;
        private int _batch = 1;

        public NeuralNetwork(List<List<double>> inputs, List<List<double>> realResults)
        {
            TensorFlow.TGr
            _lossFunction =  new SquaredError();
            _neuralLayers = new List<NeuralLayer>();
            _inputs = inputs;
            _realResult = realResults;
            _optimizer = new SGD();
        }

        public NeuralNetwork(ILossFunction lossFunction = null)
        {
            _lossFunction = new SquaredError() ?? lossFunction;
            _neuralLayers = new List<NeuralLayer>();
        }

        /// <summary>
        /// 設置訓練資料
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="realResults"></param>
        public void SetTrainData(List<List<double>> inputs, List<List<double>> realResults)
        {
            _inputs = inputs;
            _realResult = realResults;
        }

        /// <summary>
        /// 加入一層神經網路
        /// </summary>
        /// <param name="layer"></param>
        public void AddNeuralLayer(NeuralLayer layer)
        {
            if (_neuralLayers.Count > 0)
                _neuralLayers[_neuralLayers.Count - 1].InitWeights(layer.NodeAmount);
            _neuralLayers.Add(layer);
        }

        /// <summary>
        /// 開始訓練n次
        /// </summary>
        /// <param name="trainTimes"></param>
        public void StartTrain(int trainTimes)
        {
            if (_inputs.Count != _realResult.Count)
                throw new Exception("輸入數量必須和結果數量相同");
            for (int times = 0; times < trainTimes; times++)
            {
                int randomChoose = _random.Next(0, _inputs.Count);
                CalculateResult(_inputs[randomChoose]);
                _loss = _lossFunction.LossFunction(_realResult[randomChoose], _neuralLayers[_neuralLayers.Count - 1].GetOutput());
                Backpropagation(_realResult[randomChoose]);
            }
        }

        /// <summary>
        /// 取得輸入的預測結果
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public List<double> GetResult(List<double> inputs)
        {
            CalculateResult(inputs);
            return _neuralLayers[_neuralLayers.Count - 1].GetOutput();
        }

        /// <summary>
        /// 計算此次輸出
        /// </summary>
        /// <param name="inputs"></param>
        public void CalculateResult(List<double> inputs)
        {
            if (_neuralLayers == null)
                throw new Exception("沒有輸入任何Layer");
            else if (_neuralLayers.Count < 3)
                throw new Exception("至少要有三層，輸入，中間，輸出");
            _neuralLayers[0].CalculateFeedForward(inputs);
            NeuralLayer lastLayer = _neuralLayers[0];
            for (int layerIndex = 1; layerIndex < _neuralLayers.Count; layerIndex++)
            {
                _neuralLayers[layerIndex].CalculateFeedForward(lastLayer);
                lastLayer = _neuralLayers[layerIndex];
            }
        }

        /// <summary>
        /// 使用反向傳播法計算激勵函數為Logistic funtion
        /// </summary>
        /// <param name="realResults"></param>
        public void Backpropagation(List<double> realResults)
        {
            List<double> outputNodesDelta = _neuralLayers[_neuralLayers.Count - 1].Backpropagation(realResults, _lossFunction);
            _neuralLayers[_neuralLayers.Count - 2].BackpropagationSetWeight(outputNodesDelta);
            for (int layerIndex = _neuralLayers.Count - 2; layerIndex > 0; layerIndex--)
            {
                outputNodesDelta = _neuralLayers[layerIndex].Backpropagation();
                _neuralLayers[layerIndex - 1].BackpropagationSetWeight(outputNodesDelta);
            }
        }

        /// <summary>
        /// 設置lossFunction
        /// </summary>
        /// <param name="lossFunction"></param>
        public void SetLossFunction(ILossFunction lossFunction)
        {
            _lossFunction = lossFunction;
        }

        /// <summary>
        /// 設置lossFunction
        /// </summary>
        /// <param name="optimizer"></param>
        public void SetOptimizer(IOptimizer optimizer)
        {
            _optimizer = optimizer.Copy();
            foreach (NeuralLayer layer in _neuralLayers)
            {
                layer.SetOptimizer(optimizer);
            }
        }

        /// <summary>
        /// 輸出誤差值
        /// </summary>
        /// <returns></returns>
        public double Loss()
        {
            double totalLoss = 0;
            if (_inputs.Count != _realResult.Count)
                throw new Exception("實際值總數與輸入總數不相同");
            for (int index = 0; index < _inputs.Count; index++)
            {
                CalculateResult(_inputs[index]);
                totalLoss += _lossFunction.LossFunction(_realResult[index], _neuralLayers[_neuralLayers.Count - 1].GetOutput());
            }
            totalLoss /= _inputs.Count;
            return totalLoss;
        }

        /// <summary>
        /// 印出該層的所有weight
        /// </summary>
        /// <param name="layerIndex"></param>
        public void PrintWeights(int layerIndex)
        {
            List<List<double>> result = _neuralLayers[layerIndex].GetWeights();
            for (int index = 0; index < result.Count; index++)
            {
                for (int weightIndex = 0; weightIndex < result[index].Count; weightIndex++)
                {
                    Console.WriteLine(result[index][weightIndex]);
                }
                Console.WriteLine("==============");
            }
            Console.WriteLine("////////////////////");
        }

        /// <summary>
        /// 印出該層的所有weight
        /// </summary>
        /// <returns></returns>
        public NeuralNetwork Copy()
        {
            NeuralNetwork outputNetwork = new NeuralNetwork(_inputs, _realResult);
            outputNetwork._lossFunction = _lossFunction.Copy();
            outputNetwork._optimizer = _optimizer.Copy();
            foreach (NeuralLayer layer in _neuralLayers)
            {
                outputNetwork.AddNeuralLayer(layer.Copy());
            }
            outputNetwork.SetLossFunction(_lossFunction);
            return outputNetwork;
        }
    }
}
