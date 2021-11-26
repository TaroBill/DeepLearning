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
        //private bool _isPressedStart = false;

        //發出通知Labelchanged
        public void NotifyLabelChanged()
        {
            LabelChangedEnable?.Invoke();
        }

        private NeuralNetwork _neuralNetwork;
        private List<List<double>> _inputs;
        //private List<List<double>> _realResults;

        public DisplayFormPM()
        {
            InitXorNeuralNetwork();
            StartTrainButtonEnable = true;
        }


        //初始化NeuralNetwork
        private void InitXorNeuralNetwork()
        {
            List<List<double>> xorInputs = new List<List<double>>();
            xorInputs.Add(new List<double>() { 0, 0 });
            xorInputs.Add(new List<double>() { 0, 1 });
            xorInputs.Add(new List<double>() { 1, 0 });
            xorInputs.Add(new List<double>() { 1, 1 });
            _inputs = xorInputs;

            List<List<double>> xorRealResult = new List<List<double>>();
           /* xorRealResult.Add(new List<double>() { 0 });
            xorRealResult.Add(new List<double>() { 1 });
            xorRealResult.Add(new List<double>() { 1 });
            xorRealResult.Add(new List<double>() { 0 });*/
            xorRealResult.Add(new List<double>() { 1, 0 });
            xorRealResult.Add(new List<double>() { 0, 1 });
            xorRealResult.Add(new List<double>() { 0, 1 });
            xorRealResult.Add(new List<double>() { 1, 0 });
            //_realResults = xorRealResult;

            _neuralNetwork = new NeuralNetwork(xorInputs, xorRealResult);
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Logistic()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Logistic()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 1, new Softmax()));
            _neuralNetwork.SetLossFunction(new MeanSquareError());

           /* _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.01, 1, new Relu()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(5, 0.01, 1, new Relu()));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.01, 1, new Relu()));*/

            /*_neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.46));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(5, 0.1, 0.35));
            _neuralNetwork.AddNeuralLayer(new NeuralLayer(2, 0.1, 0.18));*/
        }

        //開始訓練此模型
        public void StartTrain(int trainTimes)
        {
            StartTrainButtonEnable = false;
            _neuralNetwork.StartTrain(trainTimes);
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
            NotifyLabelChanged();
        }

        //textBox只允許輸入數字
        public bool InputTextBoxNumberOnly(int key)
        {
            if ((key < 48 | key > 57) & key != 8)
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
    }
}
