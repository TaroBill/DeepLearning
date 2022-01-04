using System;
using System.Collections.Generic;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
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

        //private NeuralNetwork _neuralNetwork;
        //private List<List<double>> _inputs;
        //private List<List<double>> _realResults;

        private NDArray _input = new double[,]
        {
            {0, 0 },
            {0, 1 },
            {1, 0 },
            {1, 1 }
        };

        private NDArray _realResult = new double[,]
        {
            {1, 0 },
            {0, 1 },
            {0, 1 },
            {1, 0 }
        };

        private (Operation, Tensor, Tensor, Tensor) MakeGraph(Tensor features, Tensor labels, int num_hidden = 8)
        {

            var hidenLayer = keras.layers.dense(features, 8, activation: keras.activations.Relu);
            var hidenLayer2 = keras.layers.dense(hidenLayer, 8, activation: keras.activations.Relu);
            var outputLayer = keras.layers.dense(hidenLayer2, 2);

            var predictions = tf.tanh(tf.squeeze(outputLayer));
            var loss = tf.reduce_mean(tf.square(predictions - labels), name: "loss");

            var gs = tf.Variable(0, trainable: false, name: "global_step");
            var optimizer = tf.train.AdamOptimizer(0.02f);
            var train_op = optimizer.minimize(loss, global_step: gs);
            return (train_op, loss, gs, predictions);
        }

        public DisplayFormPM()
        {
            InitXorNeuralNetwork();
            StartTrainButtonEnable = true;
        }

        //輸出給comboBox可選清單
        public List<string> GetInputs()
        {
            /*List<string> output = new List<string>();
            foreach (List<double> input in _inputs)
            {
                output.Add(input[0].ToString() + " " + input[1].ToString());
            }
            return output;*/
            throw new NotImplementedException();
        }

        //增加訓練資料
        public void AddTrainData(List<double> input, List<double> result)
        {
            //_inputs.Add(input);
            //_realResults.Add(result);
            //_neuralNetwork.SetTrainData(_inputs, _realResults);
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
                for (int index = 0; index < 2; index++)
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
                for (int index = 0; index < 2; index++)
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
            /*
            List<List<double>> xorInputs = new List<List<double>>();
            xorInputs.Add(new List<double>() { 0, 0 });
            xorInputs.Add(new List<double>() { 0, 1 });
            xorInputs.Add(new List<double>() { 1, 0 });
            xorInputs.Add(new List<double>() { 1, 1 });
            _inputs = xorInputs;

            List<List<double>> xorRealResult = new List<List<double>>();
            xorRealResult.Add(new List<double>() { 0 });
            xorRealResult.Add(new List<double>() { -1 });
            xorRealResult.Add(new List<double>() { -1 });
            xorRealResult.Add(new List<double>() { 0 });
            _realResults = xorRealResult;*/

            /*_neuralNetwork = new NeuralNetwork(_inputs, realResults);
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.46));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(5, 0.1, 0.35));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.18));*/
        }

        //開始訓練此模型
        public void StartTrain(int trainTimes)
        {
            StartTrainButtonEnable = false;
           /* _neuralNetwork.StartTrain(trainTimes);
            Console.WriteLine(_neuralNetwork.Loss());*/
            StartTrainButtonEnable = true;
        }

        //輸出權重
        public void PrintWeight(int layerIndex)
        {
            //_neuralNetwork.PrintWeights(layerIndex);
        }

        //取得結果
        public void GetResult(int input)
        {
            if (input < 0)
                return;
          /*  List<double> output =_neuralNetwork.GetResult(_inputs[input]);
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
            }*/
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
