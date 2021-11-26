using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class Relu : IActivation
    {
        //Relu
        public double ActivationFunction(double net)
        {
            return net > 0 ? net : 0;
        }

        //不實作
        public List<double> ActivationFunction(List<double> net)
        {
            return net;
        }

        //Relu對輸出做偏微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return net > 0 ? 1 : 0;
        }
    }
}
