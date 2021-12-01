using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork
{
    public partial class DisplayForm : Form
    {
        private DisplayFormPM _displayFormPM;
        public DisplayForm(DisplayFormPM displayFormPM)
        {
            InitializeComponent();
            _displayFormPM = displayFormPM;
            _trainTimesTextBox.KeyPress += PressKeyInNumberOnlyTextBox;
            _layerIndexTextBox.KeyPress += PressKeyInNumberOnlyTextBox;
            _inputTextBox1.KeyPress += PressKeyInNumberOnlyTextBox;
            _inputTextBox2.KeyPress += PressKeyInNumberOnlyTextBox;
            _ResultTextBox.KeyPress += PressKeyInNumberOnlyTextBox;
            _displayFormPM.LabelChangedEnable += UpdateLabel;
            _printWeightButton.Click += ClickPrintWeightButton;
            _displayFormPM.InputDataChangedEnable += UpdateComboBox;
        }
        
        //更新可以選擇的comboBox
        private void UpdateComboBox()
        {
            _inputComboBox.DataSource = _displayFormPM.GetInputs();
        }

        //更新label
        public void UpdateLabel()
        {
            _resultLabel.Text = _displayFormPM.OutputLabel;
            _expectOutputLabel.Text = _displayFormPM.ExpectOutputLabel;
            _trainButton.Enabled = _displayFormPM.StartTrainButtonEnable;
        }

        //按下訓練按鈕
        private void ClickTrainButton(object sender, EventArgs e)
        {
            _inputComboBox.Enabled = false;
            _inputComboBox.SelectedIndex = -1;
            _displayFormPM.StartTrain(Convert.ToInt32(_trainTimesTextBox.Text));
            _inputComboBox.Enabled = true;
        }

        //按下訓練按鈕
        private void ClickPrintWeightButton(object sender, EventArgs e)
        {
            _displayFormPM.PrintWeight(Int32.Parse(_layerIndexTextBox.Text));
        }

        //textBox按鈕輸入事件
        private void PressKeyInNumberOnlyTextBox(object sender, KeyPressEventArgs e)
        {
            e.Handled = _displayFormPM.InputTextBoxNumberOnly((int)e.KeyChar);
        }

        //當選擇輸入選項
        private void SelectedIndexChangedInputComboBox(object sender, EventArgs e)
        {
            _displayFormPM.GetResult(_inputComboBox.SelectedIndex);
        }

        //當按下增加訓練及內容
        private void ClickAddTrainDataButton(object sender, EventArgs e)
        {
            Double.TryParse(_inputTextBox1.Text, out double inputDouble1);
            Double.TryParse(_inputTextBox2.Text, out double inputDouble2);
            Double.TryParse(_ResultTextBox.Text, out double resultDouble);
            _displayFormPM.AddTrainData(new List<double>() { inputDouble1, inputDouble2 }, new List<double>() { resultDouble });
        }
    }
}
