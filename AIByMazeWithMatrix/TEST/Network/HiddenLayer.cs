using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes, numberOfWeightsPerNode)
        {
            _layer.Insert(0, new Node(0));
            _layer[0].SetValue(1);
        }

        public HiddenLayer(string data) : base(data)
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
                output[i] = _layer[i].CalculateValue(ForwardFuntion, data);
            }
            return output;
        }

        public override double[] InputError(double[] data, double[] error)
        {
            return base.InputError(data, error);
        }

        public override string ToString()
        {
            return "Hidden" + base.ToString();
        }

    }
}
