using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool.Function
{
    public interface IFunction
    {
        double Activation(double value);

        double PartDerivativeActivation(double value);

        string GetName();
    }
}
