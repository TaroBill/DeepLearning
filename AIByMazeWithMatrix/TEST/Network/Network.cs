using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class Network
    {
        List<Layer> _network;
        InputLayer _input;
        OutputLayer _output;

        public Network(int numberOfInput, int numberOfOutput, int numberOfHiddenLayer, params int[] numberOfHideNode)
        {
            _network = new List<Layer>();
            _input = new InputLayer(numberOfInput);
            _output = new OutputLayer(numberOfOutput, numberOfHideNode.Last() + 1);
            _output.SetFunction(Functions.LogisticActivation, Functions.LogisticActivation);
            _network.Add(_input);
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                _network.Add(new HiddenLayer(numberOfHideNode[i], _network[i].GetNumberOfNode()));
                _network[i+1].SetFunction(Functions.LogisticActivation, Functions.LogisticActivation);
            }
            _network.Add(_output);
        }

        public Network(string data)
        {
            StringBuilder tempLayerData = new StringBuilder();
            string[] values = data.Replace("\r\n", "\n").Split('\n');
            string type = "";
            _network = new List<Layer>();
            foreach (string layerData in values)
            {
                if (layerData.First() != '@')
                {
                    switch (type)
                    {
                        case "Layer":
                            _network.Add(new Layer(tempLayerData.ToString()));
                            break;
                        case "InputLayer":
                            _network.Add(new InputLayer(tempLayerData.ToString()));
                            break;
                        case "OutputLayer":
                            _network.Add(new OutputLayer(tempLayerData.ToString()));
                            _network.Last().SetFunction(Functions.LogisticActivation, Functions.LogisticActivation);
                            break;
                        case "HiddenLayer":
                            _network.Add(new HiddenLayer(tempLayerData.ToString()));
                            _network.Last().SetFunction(Functions.LogisticActivation, Functions.LogisticActivation);
                            break;
                        default:
                            break;
                    }
                    tempLayerData.Clear();
                    type = layerData;
                }
                tempLayerData.AppendLine(layerData);
            }
            _input = (InputLayer)_network.First();
            _output = (OutputLayer)_network.Last();
        }

        public void RandomlyInitializeWeights()
        {
            foreach (Layer layer in _network)
            {
                layer.RandomlyInitializeWeights();
            }
        }

        public double GetNodeValue(int layerIndex,int nodeIndex)
        {
            return _network[layerIndex].GetNodeValue(nodeIndex);
        }

        public double GetNodeWeight(int layerIndex, int nodeIndex, int weightIndex)
        {
            return _network[layerIndex].GetNodeWeight(nodeIndex,weightIndex);
        }

        public double[][] GetAllNodeValue()
        {
            int numberOfLayer = _network.Count;
            double[][] values = new double[numberOfLayer][];
            for (int i = 0; i < numberOfLayer; i++)
            {
                values[i] = _network[i].GetAllNodeValue();
            }
            return values;
        }

        public double[][][] GetAllNodeWeight()
        {
            int numberOfLayer = _network.Count;
            double[][][] weights = new double[numberOfLayer][][];
            for (int i = 0; i < numberOfLayer; i++)
            {
                weights[i] = _network[i].GetAllNodeWeight();
            }
            return weights;
        }

        public void InputDataToNetwork(params double[] data)
        {
            ForwardCalculateNetwork(data);
        }

        private void ForwardCalculateNetwork(double[] data)
        {
            //輸入Input
            double[] tempData = data;
            //計算各節點的值
            foreach (Layer layer in _network)
            {
                tempData = layer.InputData(tempData);
            }
        }

        public void InputTargetToNetwork(params double[] target)
        {
            //輸入Error
            int numberOfOutput = _output.GetNumberOfNode();
            for (int i = 0; i < numberOfOutput; i++)
            {
                _output.AddErrorByTargetAt(i, target[i]);
            }
        }

        public void InputTargetToNetworkAt(int index, double target)
        {
            //輸入Error
            _output.AddErrorByTargetAt(index, target);
        }

        public double GetResidualSumofSquares()
        {
            return _output.GetResidualSumofSquares();
        }

        public void Update()
        {
            BackwardUpdateNetwork();
        }

        private void BackwardUpdateNetwork()
        {
            int indexOfLastHiddenLayer = _network.Count - 2;
            double[] tempError = _output.Update(_network[indexOfLastHiddenLayer].GetData());
            _output.ResetError();
            for (int i = indexOfLastHiddenLayer; i > 0; i--)
            {
                tempError = _network[i].InputError(_network[i - 1].GetData(), tempError);
                _network[i].ResetError();
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (Layer layer in _network)
            {
                result.AppendLine($"Layer {index++}");
                result.Append($"{layer}");
            }
            result.Append("End of file");
            return result.ToString();
        }

        public void Save(string path)
        {
            File.WriteAllText(path, this.ToString());
        }

    }
}
