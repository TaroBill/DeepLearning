using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public class Softmax : IActivation
    {
        private double _totalExponentialValue = 0;

        //不實作
        public double ActivationFunction(double net)
        {
            return net;
        }

        //softmax
        public List<double> ActivationFunction(List<double> net)
        {
            List<double> exponential = new List<double>();
            double totalExponential = 0;
            foreach (double value in net)
            {
                double expResult = Math.Exp(value);
                exponential.Add(expResult);
                totalExponential += expResult;
            }
            _totalExponentialValue = totalExponential;
            List<double> result = new List<double>();
            foreach (double value in exponential)
            {
                result.Add(value / totalExponential);
            }
            return result;
        }

        //softmax對輸出篇微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return Math.Exp(net) / ((_totalExponentialValue - net) + Math.Exp(net) * Math.Exp(net));
        }

        //複製
        public IActivation Copy()
        {
            Softmax softmax = new Softmax();
            softmax._totalExponentialValue = _totalExponentialValue;
            return softmax;
        }

        public string GetName()
        {
            return "Softmax";
        }
    }
}
