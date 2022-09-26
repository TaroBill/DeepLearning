using Matrix;
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
        List<ILayer> _network;
        InputLayer _input;
        OutputLayer _output;
        Matrix<double> _errors;

        public Network(int numberOfInput, int numberOfOutput, int numberOfHiddenLayer, params int[] numberOfHiddenNode)
        {
            _network = new List<ILayer>();
            _input = new InputLayer(numberOfInput);
            _output = new OutputLayer(numberOfOutput, numberOfHiddenNode.Last());
            _errors = new Matrix<double>(1, numberOfOutput);

            //_network.Add(new NormalizationGate(numberOfInput));
            _network.Add(_input);
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                //_network.Add(new NormalizationGate(_network[i * 2].GetNumberOfNode()));
                //_network.Add(new NormalizationGate(numberOfHiddenNode[i]));
                _network.Add(new HiddenLayer(numberOfHiddenNode[i], _network[i].GetNumberOfNode()));
            }
            //_network.Add(new NormalizationGate(_network[numberOfHiddenLayer * 2].GetNumberOfNode()));
            _network.Add(_output);//*/
        }

        //TODO Network Load
        public Network(string data)
        {
            StringBuilder tempLayerData = new StringBuilder();
            string[] values = data.Replace("\r\n", "\n").Split('\n');
            string type = "";
            int counter = 0;
            _network = new List<ILayer>();
            foreach (string layerData in values)
            {
                if (layerData == "")
                    continue;
                if (counter != 0)
                {
                    if (type == "")
                        type = layerData;
                    tempLayerData.AppendLine(layerData);
                }
                if (layerData.Contains("{"))
                    counter++;
                if (layerData.Contains("}"))
                    counter--;

                if (counter == 0)
                    if (tempLayerData.Length != 0)
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
                                break;
                            case "HiddenLayer":
                                _network.Add(new HiddenLayer(tempLayerData.ToString()));
                                break;
                            default:
                                break;
                        }
                        type = "";
                        tempLayerData.Clear();
                        if (layerData == "End of file" && counter == 0)
                            break;
                    }
            }
            _input = (InputLayer)_network.First();
            _output = (OutputLayer)_network.Last();//*/
        }

        public void RandomlyInitializeWeights(int seed)
        {
            Random random = new Random(seed);
            foreach (ILayer layer in _network)
            {
                layer.RandomlyInitializeWeights(random.Next());
            }
        }

        public Matrix<double> GetAllOutputError()
        {
            return new Matrix<double>(_errors);
        }

        public List<Matrix<double>> GetAllNodeValue()
        {
            List<Matrix<double>> values = new List<Matrix<double>>();
            foreach (ILayer layer in _network)
            {
                values.Add(layer.GetOutputMatrix());
            }//*/
            return values;
        }

        public List<Matrix<double>> GetAllNodeWeight()
        {
            List<Matrix<double>> weights = new List<Matrix<double>>();
            foreach (ILayer layer in _network)
            {
                weights.Add(layer.GetWeightMatrix());
            }//*/
            return weights;
        }

        public void InputDataToNetwork(Matrix<double> data)
        {
            //輸入Input
            Matrix<double> tempData = data;
            //計算各節點的值
            foreach (ILayer layer in _network)
            {
                tempData = layer.InputData(tempData);
            }
        }

        public void InputTargetToNetwork(Matrix<double> target)
        {
            InputErrorToNetwork(GetOutput() - target);
        }

        public void InputErrorToNetwork(Matrix<double> error)
        {
            //輸入Error
            _errors = error;
            /*Matrix<double> ones = new Matrix<double>(1, error.RowCount);
            for (int i = 0; i < error.RowCount; i++)
            {
                ones[0, i] = 1;
            }//*/
            Matrix<double> tempError = error;
            //傳播各節點的誤差
            int numberOfLayer = _network.Count();
            for (int i = numberOfLayer - 1; i > 0; i--)
            {
                tempError = _network[i].InputError(tempError);
            }
        }

        public double GetResidualSumofSquares()
        {
            int numberOfRow = _errors.RowCount;
            int numberOfColumn = _errors.ColumnCount;
            double result = 0;

            for (int row = 0; row < numberOfRow; row++)
            {
                for (int column = 0; column < numberOfColumn; column++)
                {
                    result += Math.Pow(_errors[row, column], 2);
                }
            }
            return result;
        }

        public void Update(Matrix<double> data, Matrix<double> target)
        {
            InputDataToNetwork(data);
            InputTargetToNetwork(target);
        }

        public void UpdateByError(Matrix<double> data, Matrix<double> error)
        {
            InputDataToNetwork(data);
            InputErrorToNetwork(error);
        }

        public Matrix<double> GetOutput()
        {
            return _output.GetOutputMatrix();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (ILayer layer in _network)
            {
                result.AppendLine($"Layer {index++} {{");
                result.Append($"{layer}");
                result.AppendLine($"}}");
            }
            result.Append("End of file\n");
            return result.ToString();
        }

        public void Save(string path)
        {
            File.WriteAllText(path, this.ToString());
        }

    }
}
