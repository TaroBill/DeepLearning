using NeuralNetwork.ActivationFunction;
using NeuralNetwork.LossFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class DisplayFormPM
    {
        public delegate void LabelTextChangedEventHandler();
        public LabelTextChangedEventHandler LabelChangedEnable
        {
            get; set;
        }
        public delegate void InputsDataChangedEventHandler();
        public InputsDataChangedEventHandler InputDataChangedEnable
        {
            get; set;
        }
        //private bool _isPressedStart = false;

        //發出通知Labelchanged
        public void NotifyLabelChanged()
        {
            LabelChangedEnable?.Invoke();
        }

        //發出通知inputsDataChanged
        public void NotifyInputsDataChanged()
        {
            InputDataChangedEnable?.Invoke();
        }

        private NeuralNetwork _neuralNetwork;
        private List<List<double>> _inputs;
        private List<List<double>> _realResults;

        public DisplayFormPM()
        {
            InitXorNeuralNetwork();
            StartTrainButtonEnable = true;
        }

        //輸出給comboBox可選清單
        public List<string> GetInputs()
        {
            List<string> output = new List<string>();
            foreach (List<double> input in _inputs)
            {
                output.Add(input[0].ToString() + " " + input[1].ToString());
            }
            return output;
        }

        //增加訓練資料
        public void AddTrainData(List<double> input, List<double> result)
        {
            _inputs.Add(input);
            _realResults.Add(result);
            _neuralNetwork.SetTrainData(_inputs, _realResults);
            NotifyInputsDataChanged();
        }

        //標準化
        public List<List<double>> Normalize(List<List<double>> input)
        {
            List<List<double>> output = new List<List<double>>();
            List<double> max = new List<double>() { double.MinValue, double.MinValue, double.MinValue };
            List<double> min = new List<double>() { double.MaxValue, double.MaxValue, double.MaxValue };
            foreach (List<double> data in input)
            {
                for (int index = 0; index < 3; index++)
                {
                    if (data[index] > max[index])
                        max[index] = data[index];
                    if (data[index] < min[index])
                        min[index] = data[index];
                }
            }
            foreach (List<double> data in input)
            {
                List<double> outputData = new List<double>();
                for (int index = 0; index < 3; index++)
                {
                    outputData.Add((data[index] - min[index]) / (max[index] - min[index]));
                }
                output.Add(outputData);
            }
            return output;
        }

        //初始化NeuralNetwork
        private void InitXorNeuralNetwork()
        {
            
            /*List<List<double>> xorInputs = new List<List<double>>();
            xorInputs.Add(new List<double>() { 0, 0 });
            xorInputs.Add(new List<double>() { 0, 1 });
            xorInputs.Add(new List<double>() { 1, 0 });
            xorInputs.Add(new List<double>() { 1, 1 });
            _inputs = xorInputs;

            List<List<double>> xorRealResult = new List<List<double>>();
            xorRealResult.Add(new List<double>() { 1 });
            xorRealResult.Add(new List<double>() { -1 });
            xorRealResult.Add(new List<double>() { -1 });
            xorRealResult.Add(new List<double>() { 0 });
            _realResults = xorRealResult;*/
            
            
            List<List<double>> inputs = new List<List<double>>();
            List<List<double>> realResults = new List<List<double>>();
            for (int index = 0; index < 50; index++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                List<double> input = new List<double>()
                {
                    random.Next(0, 10),
                    random.Next(0, 10),
                    random.Next(0, 4),
                };
                int value = random.Next(-1, 1);
                List<double> realResult = new List<double>()
                {
                    value == 0 ? MyRandom.NextGuass(0,0.05) : value
                };
                if (inputs.Contains(input))
                    continue;
                inputs.Add(input);
                realResults.Add(realResult);
            }
            _inputs = Normalize(inputs);
            _realResults = realResults;

            /*xorRealResult.Add(new List<double>() { 1, 0 });
            xorRealResult.Add(new List<double>() { 0, 1 });
            xorRealResult.Add(new List<double>() { 0, 1 });
            xorRealResult.Add(new List<double>() { 1, 0 });*/
            //_realResults = xorRealResult;

            _neuralNetwork = new NeuralNetwork(_inputs, _realResults);


            /*_neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Logistic()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Logistic()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Softmax()));
            _neuralNetwork.SetLossFunction(new MeanSquareError());*/

            _neuralNetwork.AddNeuralLayer(new NeuralLayer(3, 0.0025, 0.1, new Relu()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(20, 0.0025, 0.1, new Relu()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(20, 0.0025, 0.1, new Relu()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(1, 0.0025, 0.1, new Tanh()));
            _neuralNetwork.SetLossFunction(new MeanSquareError());

            /*_neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.46));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(5, 0.1, 0.35));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.18));*/
        }

        //開始訓練此模型
        public void StartTrain(int trainTimes)
        {
            StartTrainButtonEnable = false;
            _neuralNetwork.StartTrain(trainTimes);
            Console.WriteLine(_neuralNetwork.Loss());
            StartTrainButtonEnable = true;
        }

        //輸出權重
        public void PrintWeight(int layerIndex)
        {
            _neuralNetwork.PrintWeights(layerIndex);
        }

        //取得結果
        public void GetResult(int input)
        {
            if (input < 0)
                return;
            List<double> output =_neuralNetwork.GetResult(_inputs[input]);
            _outputLabel = "";
            for (int outputIndex = 0; outputIndex < output.Count(); outputIndex++)
            {
                _outputLabel += output[outputIndex].ToString();
                _outputLabel += "\n";
            }
            _expectOutputLabel = "";
            foreach (double realResult in _realResults[input])
            {
                _expectOutputLabel += realResult.ToString();
                _expectOutputLabel += "\n";
            }
            NotifyLabelChanged();
        }

        //textBox只允許輸入數字
        public bool InputTextBoxNumberOnly(int key)
        {
            if ((key < 48 | key > 57) & key != 8 & key != 46)
            {
                return true;
            }
            return false;
        }


        private bool _startTrainButtonEnable;
        public bool StartTrainButtonEnable
        {
            get
            {
                return _startTrainButtonEnable;
            }
            set
            {
                _startTrainButtonEnable = value;
                NotifyLabelChanged();
            }
        }

        private string _outputLabel;
        public string OutputLabel
        {
            get
            {
                return _outputLabel;
            }
        }

        private string _expectOutputLabel;
        public string ExpectOutputLabel
        {
            get
            {
                return _expectOutputLabel;
            }
        }
    }
}
