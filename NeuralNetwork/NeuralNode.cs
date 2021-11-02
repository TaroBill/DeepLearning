﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNode
    {
        private double _output;
        private List<double> _outputWeight;
        private readonly Random _random;

        public NeuralNode()
        {
            _output = 0;
            _outputWeight = new List<double>();
            _random = new Random();
        }

        //設定輸出有多少Node
        public void CreateNewWeight(int amount)
        {
            _outputWeight.Clear();
            for (int index = 0; index < amount; index++)
                _outputWeight.Add(_random.NextDouble());
        }

        //增加一個輸出節點
        public void AddNewNodeWeight()
        {
            _outputWeight.Add(_random.NextDouble());
        }

        //對該節點進行Logistic運算(中間層)
        public double LogisticFunction(List<NeuralNode> inputNodes, int thisNodeIndex, double bias, double biasWeight)
        {
            double net = 0;
            foreach (NeuralNode Neuron in inputNodes)
                net += (Neuron.Output * Neuron._outputWeight[thisNodeIndex]);
            net += bias * biasWeight;
            _output = 1.0 / (1 + Math.Exp(0 - net));
            return Output;
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
