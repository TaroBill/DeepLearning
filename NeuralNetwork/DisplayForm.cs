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
            _displayFormPM.LabelChangedEnable += UpdateLabel;
        }

        //更新label
        public void UpdateLabel()
        {
            _resultLabel.Text = _displayFormPM.OutputLabel;
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
    }
}
