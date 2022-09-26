using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class Linear : IActivation
    {
        //激勵函式
        public double ActivationFunction(double net)
        {
            return net;
        }

        //不實作
        public List<double> ActivationFunction(List<double> net)
        {
            return net;
        }

        //激勵函式偏微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return 1;
        }

        //複製
        public IActivation Copy()
        {
            return new Linear();
        }

        public string GetName()
        {
            return "Linear";
        }
    }
}
