using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class Sigmoid : IActivation
    {
        //logistc Function
        public double ActivationFunction(double net)
        {
            return 1.0 / (1 + Math.Exp(0 - net));
        }

        //不實作
        public List<double> ActivationFunction(List<double> net)
        {
            return net;
        }

        //logistc Function對net偏微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return output * (1 - output);
        }

        //複製
        public IActivation Copy()
        {
            return new Sigmoid();
        }

        public string GetName()
        {
            return "Sigmoid";
        }
    }
}
