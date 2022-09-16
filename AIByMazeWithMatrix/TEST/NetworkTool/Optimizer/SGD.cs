using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class SGD : IOptimizer
    {

        private double _learningRate = 0.001;

        /// <summary>
        /// 第一個參數是學習率
        /// </summary>
        /// <param name="parameters"></param>
        public void Init(params double[] parameters)
        {
            _learningRate = parameters[0];
        }

        public double GetDelta(double error)
        {
            double delta = _learningRate * error;
            return delta;
        }

        public void Reset() { }

    }
}
