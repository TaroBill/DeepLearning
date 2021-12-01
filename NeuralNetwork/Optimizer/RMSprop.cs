using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public class RMSprop : IOptimizer
    {
        private double _lastSigma = 0;
        private double _epsilon;
        private double _alpha;

        public RMSprop(double alpha = 0.9, double epsilon = -1)
        {
            _epsilon = epsilon < 0 ? 0.0000001 : epsilon;
            _alpha = alpha;
        }

        //複製
        public IOptimizer Copy()
        {
            RMSprop rmsprop = new RMSprop(_alpha, _epsilon)
            {
                _lastSigma = _lastSigma
            };
            return rmsprop;
        }

        //輸出
        public double GetResult(double gradient, double learningRate) // RMSprop推薦learningRate為0.001
        {
            double sigma = _lastSigma == 0 ? gradient : Math.Sqrt((_alpha * _lastSigma * _lastSigma) + ((1 - _alpha) * gradient * gradient) + _epsilon);
            _lastSigma = sigma;
            return (learningRate / (sigma + _epsilon)) * gradient;
        }
    }
}
