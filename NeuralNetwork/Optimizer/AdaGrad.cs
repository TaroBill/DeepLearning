using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public class AdaGrad : IOptimizer
    {
        private double _lastGradients;
        private int nowIndex;
        private double _epsilon;

        public AdaGrad(double epsilon = -1)
        {
            _epsilon = epsilon < 0 ? 0.000001 : epsilon;
            _lastGradients = 0;
            nowIndex = -1;
        }

        //複製
        public IOptimizer Copy()
        {
            AdaGrad adaGrad = new AdaGrad(_epsilon)
            {
                _lastGradients = _lastGradients
            };
            return adaGrad;
        }

        //輸出
        public double GetResult(double gradient, double learningRate)
        {
            nowIndex++;
            _lastGradients += gradient * gradient;
            return (learningRate * (Math.Sqrt((1.0 / (nowIndex + _epsilon)) * _lastGradients) + _epsilon)) * gradient;
        }

        public string GetName()
        {
            return $"AdaGrad({_epsilon})";
        }
    }
}
