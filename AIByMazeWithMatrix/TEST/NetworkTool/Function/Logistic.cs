using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool.Function
{
    public class Logistic : IFunction
    {
        public double Activation(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public double PartDerivativeActivation(double value)
        {
            return value * (1 - value);
        }

        public string GetName()
        {
            return "Logistic";
        }

    }
}
