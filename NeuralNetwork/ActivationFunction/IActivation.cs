using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public interface IActivation
    {
        //激勵函式
        double ActivationFunction(double net);

        //對激勵函式微分
        double PartialDerivativeActivationFunction(double net, double output);
    }
}
