using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class Tanh : IActivation
    {
        //tanh
        public double ActivationFunction(double net)
        {
            return (Math.Exp(net) - Math.Exp(-net)) / (Math.Exp(net) + Math.Exp(-net));
        }

        //不實作
        public List<double> ActivationFunction(List<double> net)
        {
            return net;
        }

        //tanh對輸出篇微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return 4.0 / ((Math.Exp(net) + Math.Exp(-net)) * (Math.Exp(net) + Math.Exp(-net)));
        }

        //複製
        public IActivation Copy()
        {
            return new Tanh();
        }

        public string GetName()
        {
            return "Tanh";
        }
    }
}
