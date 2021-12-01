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

        //激勵函式(需要用到所有輸出)例如softmax
        List<double> ActivationFunction(List<double> net);

        //對激勵函式微分
        double PartialDerivativeActivationFunction(double net, double output);

        //複製
        IActivation Copy();
    }
}
