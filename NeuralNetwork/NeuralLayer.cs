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
    public class NeuralLayer
    {
        private readonly List<NeuralNode> _nodes = new List<NeuralNode>();
        private readonly double _bias;
        private List<double> _biasWeight = new List<double>();
        private readonly double _learningRate;
        private IActivation _activation;
        private IOptimizer _optimizer;

        public NeuralLayer(int nodesAmount, double learningRate, double bias, IActivation activation = null)
        {
             _activation = activation ?? new Sigmoid();
            _learningRate = learningRate;
            _bias = bias;
            _optimizer = new SGD();
            InitializeNodes(nodesAmount);
        }

        public NeuralLayer(string data)
        {
            int countOfbracket = 0;
            int indexOfReadLine = 1;
            int indexOfStart;
            int indexOfEnd;
            string NameOfReadingData = "";
            string[] dataLine = data.Split('\n');
            string currentLine;
            string tempData = "";
            _nodes = new List<NeuralNode>();

            if (dataLine[0].Contains('{'))
                countOfbracket++;
            while (countOfbracket > 0)
            {
                currentLine = dataLine[indexOfReadLine];
                indexOfReadLine++;
                if (currentLine.Contains('{'))
                {
                    countOfbracket++;
                    if (NameOfReadingData == "Bias")
                        continue;
                }
                else if (currentLine.Contains('}'))
                {
                    countOfbracket--;
                    if (NameOfReadingData == "Bias")
                    {
                        NameOfReadingData = "";
                        continue;
                    }
                }
                else if (countOfbracket == 1)
                {
                    if (currentLine.Contains("Optimizer"))
                    {
                        indexOfStart = currentLine.IndexOf(":") + 2;
                        indexOfEnd = currentLine.LastIndexOf(",") - 2;
                        _optimizer = ObjectGeneratorByName.GetOptimizer(currentLine.Substring(indexOfStart, indexOfEnd - indexOfStart + 1));
                    }
                    else if (currentLine.Contains("Activation"))
                    {
                        indexOfStart = currentLine.IndexOf(":") + 2;
                        indexOfEnd = currentLine.LastIndexOf(",") - 2;
                        _activation = ObjectGeneratorByName.GetActivation(currentLine.Substring(indexOfStart, indexOfEnd - indexOfStart + 1));
                    }
                    else if (currentLine.Contains("LearningRate"))
                    {
                        indexOfStart = currentLine.IndexOf(":") + 1;
                        indexOfEnd = currentLine.LastIndexOf(",") - 1;
                        _learningRate = Convert.ToDouble(currentLine.Substring(indexOfStart, indexOfEnd - indexOfStart + 1));
                    }
                    else if (currentLine.Contains("Bias"))
                        NameOfReadingData = "Bias";
                    else if (currentLine.Contains("Nodes")) ;

                    continue;
                }
                else if (NameOfReadingData == "Bias")
                {
                    if (currentLine.Contains("Value"))
                    {
                        indexOfStart = currentLine.IndexOf(":") + 1;
                        indexOfEnd = currentLine.LastIndexOf(",") - 1;
                        _bias = Convert.ToDouble(currentLine.Substring(indexOfStart, indexOfEnd - indexOfStart + 1));
                    }
                    else if (currentLine.Contains("Weight")) ;
                    else
                    {
                        string trimString = currentLine.Trim('[', ']', '\r');
                        if (trimString == "")
                            continue;
                        _biasWeight = trimString.Split(',').Select(x => Convert.ToDouble(x)).ToList();
                    }
                    continue;
                }
                tempData += currentLine + "\n";
                if (countOfbracket == 1)
                {
                    _nodes.Add(new NeuralNode(tempData));
                    tempData = "";
                }
            }
        }

        /// <summary>
        /// 初始化節點數
        /// </summary>
        /// <param name="amount"></param>
        public void InitializeNodes(int amount)
        {
            _nodes.Clear();
            for (int index = 0; index < amount; index++)
            {
                _nodes.Add(new NeuralNode(_activation, _optimizer));
                _biasWeight.Add(MyRandom.NextXavier(1, 0.5, amount));
            }
        }

        /// <summary>
        /// 設定輸出的weights數量
        /// </summary>
        /// <param name="amount"></param>
        public void InitWeights(int amount)
        {
            foreach (NeuralNode nodes in _nodes)
            {
                nodes.SetWeightsAmount(amount);
            }
        }

        /// <summary>
        /// 加入一個節點到Layer
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(NeuralNode node)
        {
            _nodes.Add(node);
            _biasWeight.Add(MyRandom.NextXavier(1, 0.5, NodeAmount + 1));
        }

        /// <summary>
        /// 計算整層的FeedForward
        /// </summary>
        /// <param name="inputLayer"></param>
        public void CalculateFeedForward(NeuralLayer inputLayer)
        {
            List<double> allOutput = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
            {
                double outputValue = _nodes[nodeIndex].ActivationFunction(inputLayer._nodes, nodeIndex, _bias, _biasWeight[nodeIndex]);
                allOutput.Add(outputValue);
            }
            List<double> result = _activation.ActivationFunction(allOutput);
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
                _nodes[nodeIndex].Output = result[nodeIndex];
        }

        /// <summary>
        /// 設置優化器
        /// </summary>
        /// <param name="optimizer"></param>
        public void SetOptimizer(IOptimizer optimizer)
        {
            _optimizer = optimizer.Copy();
            foreach (NeuralNode node in _nodes)
            {
                node.SetOptimizer(optimizer);
            }
        }

        /// <summary>
        /// 計算輸入整層的FeedForward
        /// </summary>
        /// <param name="inputs"></param>
        public void CalculateFeedForward(List<double> inputs)
        {
            if (inputs.Count != _nodes.Count)
                throw new Exception("輸入層與輸入的值個數不同");
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
                _nodes[nodeIndex].Output = inputs[nodeIndex];
        }

        /// <summary>
        /// 取得該層所有節點的輸出
        /// </summary>
        /// <returns></returns>
        public List<double> GetOutput()
        {
            List<double> output = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
                output.Add(_nodes[nodeIndex].Output);
            return output;
        }

        /// <summary>
        /// 使用反向傳播法計算激勵函數，輸出層到內層
        /// </summary>
        /// <param name="realValue"></param>
        /// <param name="lossFunction"></param>
        /// <returns></returns>
        public List<double> Backpropagation(List<double> realValue, ILossFunction lossFunction)
        {
            if (realValue.Count < _nodes.Count)
                throw new Exception("實際輸出結果數量要跟輸出節點數量相同");
            List<double> outputDelta = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
            {
                double lossPartialDerivative = lossFunction.PartialDerivativeLossFunction(realValue[nodeIndex], _nodes[nodeIndex].Output);
                double activationPartialDerivative = _activation.PartialDerivativeActivationFunction(_nodes[nodeIndex].Net, _nodes[nodeIndex].Output);
                outputDelta.Add(lossPartialDerivative * activationPartialDerivative);
            }
            SetBiasWeight(outputDelta);
            return outputDelta;
        }

        /// <summary>
        /// 設定此層的bias和bias weight
        /// </summary>
        /// <param name="thisDeltas"></param>
        private void SetBiasWeight(List<double> thisDeltas)
        {
            if (thisDeltas.Count != _biasWeight.Count)
                throw new Exception("計算出的delta量應該要和此層node數量相同");
            for (int deltaIndex = 0; deltaIndex < thisDeltas.Count; deltaIndex++)
            {
                double gradient = (thisDeltas[deltaIndex] * _bias);
                _biasWeight[deltaIndex] -= _optimizer.GetResult(gradient, _learningRate);
            }
        }

        /// <summary>
        /// 使用反向傳播法計算激勵函數，隱藏層到輸入層或隱藏層
        /// </summary>
        /// <returns></returns>
        public List<double> Backpropagation()
        {
            List<double> outputDelta = new List<double>();
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
            {
                outputDelta.Add(_nodes[nodeIndex].TotalDeltaWeight * _activation.PartialDerivativeActivationFunction(_nodes[nodeIndex].Net, _nodes[nodeIndex].Output));
            }
            SetBiasWeight(outputDelta);
            return outputDelta;
        }

        /// <summary>
        /// 使用delta來設定Weight(與上面的函式位於不同層)
        /// </summary>
        /// <param name="deltaValue"></param>
        public void BackpropagationSetWeight(List<double> deltaValue)
        {
            for (int nodeIndex = 0; nodeIndex < _nodes.Count; nodeIndex++)
            {
                _nodes[nodeIndex].ResetTotalDeltaWeight();
                for (int deltaIndex = 0; deltaIndex < deltaValue.Count; deltaIndex++)
                {
                    _nodes[nodeIndex].AddTotalDeltaWeight(deltaValue[deltaIndex], deltaIndex);
                    _nodes[nodeIndex].SetWeight(deltaValue[deltaIndex], deltaIndex, _learningRate);
                }
            }
        }

        /// <summary>
        /// 取得該層的所有節點的所有weight
        /// </summary>
        /// <returns></returns>
        public List<List<double>> GetWeights()
        {
            List<List<double>> output = new List<List<double>>();
            foreach (NeuralNode node in _nodes)
            {
                output.Add(node.OutputWeight.ToList());
            }
            return output;
        }

        /// <summary>
        /// 設置bias的weights
        /// </summary>
        /// <param name="biasWeight"></param>
        public void InitBiasWeight(List<double> biasWeight)
        {
            if (biasWeight.Count != this.NodeAmount)
                return;
            _biasWeight.Clear();
            foreach (double weight in biasWeight)
                _biasWeight.Add(weight);
        }

        public int NodeAmount
        {
            get
            {
                return _nodes.Count;
            }
        }

        /// <summary>
        /// 複製此layer
        /// </summary>
        /// <returns></returns>
        public NeuralLayer Copy()
        {
            NeuralLayer outputLayer = new NeuralLayer(NodeAmount, _learningRate, _bias, _activation);
            outputLayer._activation = _activation.Copy();
            outputLayer._optimizer = _optimizer.Copy();
            outputLayer._nodes.Clear();
            foreach (NeuralNode node in _nodes)
            {
                outputLayer.AddNode(node.Copy());
            }
            outputLayer.InitBiasWeight(_biasWeight);
            return outputLayer;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("{");
            result.AppendLine($"\"Optimizer\":\"{_optimizer.GetName()}\",");
            result.AppendLine($"\"Activation\":\"{_activation.GetName()}\",");
            result.AppendLine($"\"LearningRate\":{_learningRate},");

            result.AppendLine($"\"Bias\":");
            result.AppendLine("{");
            result.AppendLine($"\"Value\":{_bias},");
            result.AppendLine($"\"Weight\":");
            result.Append("[");

            int numberOfBiasWeight = _biasWeight.Count;
            if (numberOfBiasWeight > 0)
            {
                result.Append(_biasWeight[0].ToString());
                for (int i = 1; i < numberOfBiasWeight; i++)
                {
                    result.Append(",");
                    result.Append(_biasWeight[i].ToString());
                }
            }
            result.AppendLine("]");
            result.AppendLine("},");

            result.AppendLine($"\"Nodes\":");
            result.AppendLine("[");
            int numberOfNodes = _nodes.Count;
            if (numberOfNodes > 0)
            {
                result.Append(_nodes[0].ToString());
                for (int i = 1; i < numberOfNodes; i++)
                {
                    result.AppendLine(",");
                    result.Append(_nodes[i].ToString());
                }
                result.AppendLine();
            }
            result.AppendLine("]");
            result.Append("}");
            return result.ToString();
        }
    }
}
