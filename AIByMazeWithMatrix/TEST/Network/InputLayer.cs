using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class InputLayer : Layer
    {
        public InputLayer(int numberOfNodes) : base(numberOfNodes + 1, 0)
        {
            _layer[0].SetValue(1);
        }

        public InputLayer(string data) : base(data)
        {
            _layer[0].SetValue(1);
        }

        public override double[] InputData(double[] data)
        {
            int count = _layer.Count;
            double[] output = new double[count];
            output[0] = _layer[0].GetValue();
            for (int i = 1; i < count; i++)
            {
                _layer[i].SetValue(data[i - 1]);
                output[i] = data[i - 1];
            }
            return output;
        }

        public override double[] InputError(double[] data, double[] error)
        {
            return new double[0];
        }

        public override string ToString()
        {
            return "Input"+base.ToString();
        }

    }
}
