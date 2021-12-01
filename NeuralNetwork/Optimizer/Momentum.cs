using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public class Momentum : IOptimizer
    {
        private double _lastVector;
        private double _resistance;

        public Momentum(double resistance)
        {
            _resistance = resistance;
            _lastVector = 0;
        }

        //複製
        public IOptimizer Copy()
        {
            Momentum momentum = new Momentum(_resistance)
            {
                _lastVector = _lastVector
            };
            return momentum;
        }

        //輸出
        public double GetResult(double gradient, double learningRate)
        {
            double vectorNow = _lastVector * _resistance - gradient * learningRate;
            _lastVector = vectorNow;
            return 0 - vectorNow;
        }
    }
}
