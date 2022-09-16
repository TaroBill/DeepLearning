using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    class Adam : IOptimizer
    {

        private double _memoryMomentum = 0;
        private double _memoryVelocity = 0;
        private double _momentumCorrection = 1;
        private double _velocityCorrection = 1;
        private double _learningRate = 0.1;
        private double _momentumMemoryDiscountRate = 0.9;
        private double _velocityMemoryDiscountRate = 0.999;
        private double _minimumValue = 1.0E-8;

        /// <summary>
        /// 第一個參數是學習率
        /// 第二個參數是誤差損失率
        /// 第三個參數是誤差平方損失率
        /// 第四個參數是最小值
        /// </summary>
        /// <param name="parameters"></param>
        public void Init(params double[] parameters)
        {
            _learningRate = parameters[0];
            _momentumMemoryDiscountRate = parameters[1];
            _velocityMemoryDiscountRate = parameters[2];
            _minimumValue = parameters[3];
        }

        public double GetDelta(double error)
        {
            _memoryMomentum = _memoryMomentum * _momentumMemoryDiscountRate + error * (1 - _momentumMemoryDiscountRate);
            _memoryVelocity = _memoryVelocity * _velocityMemoryDiscountRate + error * error * (1 - _velocityMemoryDiscountRate);
            _momentumCorrection *= _momentumMemoryDiscountRate;
            _velocityCorrection *= _velocityMemoryDiscountRate;
            double momentum = _memoryMomentum / (1 - _momentumCorrection);
            double velocity = Math.Sqrt(_memoryVelocity / (1 - _velocityCorrection)) + _minimumValue;
            double delta = _learningRate * momentum / velocity;
            return delta;
        }

        public void Reset()
        {
            _memoryMomentum = 0;
            _memoryVelocity = 0;
            _momentumCorrection = 1;
            _velocityCorrection = 1;
        }

    }
}
