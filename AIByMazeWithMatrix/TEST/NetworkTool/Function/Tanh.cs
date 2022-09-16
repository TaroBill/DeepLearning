using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool.Function
{
    public class Tanh : IFunction
    {
        public double Activation(double value)
        {
            return Math.Tanh(value);
        }

        public double PartDerivativeActivation(double value)
        {
            return 1 - value * value;
        }

        public string GetName()
        {
            return "Tanh";
        }

    }
}
