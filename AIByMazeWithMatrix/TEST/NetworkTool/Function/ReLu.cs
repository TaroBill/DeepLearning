using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool.Function
{
    public class ReLu : IFunction
    {
        public double Activation(double value)
        {
            return value < 0 ? 0 : value;
        }

        public double PartDerivativeActivation(double value)
        {
            return 1;
        }

        public string GetName()
        {
            return "ReLu";
        }

    }
}
