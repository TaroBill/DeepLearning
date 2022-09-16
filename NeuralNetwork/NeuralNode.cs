﻿using NeuralNetwork.ActivationFunction;
using NeuralNetwork.Optimizer;
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
        private double _net;
        private List<double> _outputWeight;
        private double _totalDeltaWeight;
        private readonly IActivation _activationFunction;
        private IOptimizer _optimizer;

        public NeuralNode(IActivation activationFunction, IOptimizer optimizer)
        {
            _output = 0;
            _totalDeltaWeight = 0;
            _outputWeight = new List<double>();
            _activationFunction = activationFunction;
            _optimizer = optimizer.Copy();
        }

        //設定輸出有多少Node
        public void SetWeightsAmount(int amount)
        {
            _outputWeight.Clear();
            for (int index = 0; index < amount; index++)
            {
                _outputWeight.Add(MyRandom.NextXavier(0, 0.3, amount));
            }
        }

        //從資料設定輸出weights
        public void LoadWeights(List<double> weights)
        {
            _outputWeight.Clear();
            for (int index = 0; index < weights.Count; index++)
            {
                _outputWeight.Add(weights[index]);
            }
        }

        //增加一個輸出節點
        public void AddNewNodeWeight()
        {
            _outputWeight.Add(MyRandom.NextXavier(0, 0.3, _outputWeight.Count+1));
        }

        //對該節點進行Logistic運算(中間層)
        public double ActivationFunction(List<NeuralNode> inputNodes, int thisNodeIndex, double bias, double biasWeight)
        {
            double net = 0;
            foreach (NeuralNode Neuron in inputNodes)
                net += (Neuron.Output * Neuron._outputWeight[thisNodeIndex]);
            net += bias * biasWeight;
            _net = net;
            _output = _activationFunction.ActivationFunction(net);
            return _output;
        }

        //利用delta設定該節點到輸出的權重
        public void SetWeight(double delta, int outputNodeIndex, double learningRate = 0.01)
        {
            double gradient = (delta * _output);
            _outputWeight[outputNodeIndex] -= _optimizer.GetResult(gradient, learningRate);
        }

        //把該節點和所有輸出節點間的weight乘與Delta再相加存起來
        public void AddTotalDeltaWeight(double delta, int outputNodeIndex)
        {
            _totalDeltaWeight += delta * _outputWeight[outputNodeIndex];
        }

        //將該節點的totalDeltaWeight重置
        public void ResetTotalDeltaWeight()
        {
            _totalDeltaWeight = 0;
        }

        //設置Optimizer
        public void SetOptimizer(IOptimizer optimizer)
        {
            _optimizer = optimizer.Copy();
        }

        //複製此node
        public NeuralNode Copy()
        {
            NeuralNode neuralNode = new NeuralNode(_activationFunction.Copy(), _optimizer.Copy());
            neuralNode.Output = _output;
            neuralNode._net = _net;
            neuralNode.OutputWeight = _outputWeight.ToList();
            neuralNode._totalDeltaWeight = _totalDeltaWeight;
            return neuralNode;
        }

        public double TotalDeltaWeight
        {
            get
            {
                return _totalDeltaWeight;
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

        public double Net
        {
            get
            {
                return _net;
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
