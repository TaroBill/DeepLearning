using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class Momentum : IOptimizer
    {

        private double _learningRate = 0.001;
        private double _velocityDiscountRate = 0.9;
        private double _lastDelta = 0;

        /// <summary>
        /// 第一個參數是學習率
        /// 第二個參數是速度損失率
        /// </summary>
        /// <param name="parameters"></param>
        public void Init(params double[] parameters)
        {
            _learningRate = parameters[0];
            _velocityDiscountRate = parameters[1];
        }

        public double GetDelta(double error)
        {
            double delta = _learningRate * error;
             delta += _velocityDiscountRate * _lastDelta;
            _lastDelta = delta;
            return delta;
        }

        public void Reset()
        {
            _lastDelta = 0;
        }

    }
}
