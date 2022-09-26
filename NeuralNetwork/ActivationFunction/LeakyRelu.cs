using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class LeakyRelu : IActivation
    {
        double _linearConstant;
        public LeakyRelu(double linearConstant = 0.1)
        {
            _linearConstant = linearConstant;
        }
        //Relu
        public double ActivationFunction(double net)
        {
            return net > 0 ? net : _linearConstant * net;
        }

        //不實作
        public List<double> ActivationFunction(List<double> net)
        {
            return net;
        }

        //Relu對輸出做偏微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return net > 0 ? 1 : _linearConstant;
        }

        //複製
        public IActivation Copy()
        {
            return new LeakyRelu(_linearConstant);
        }

        public string GetName()
        {
            return $"LeakyRelu({_linearConstant})";
        }
    }
}
