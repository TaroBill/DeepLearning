using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public class MeanSquareError : ILossFunction
    {
        private double _totalOutput;

        //均方誤差(Mean square error，MSE)
        public double LossFunction(List<double> target, List<double> output)
        {
            double result = 0;
            _totalOutput = target.Count();
            for (int index = 0; index < target.Count(); index++)
            {
                double targetValue = target[index];
                if (target[index] > -10.0001 && target[index] < -9.99999)
                    targetValue = output[index];
                result += (1.0 / target.Count()) * (targetValue - output[index]) * (targetValue - output[index]);
            }
                
            return result;
        }

        //均方誤差對輸出偏微分
        public double PartialDerivativeLossFunction(double target, double output)
        {
            if (target > -10.0001 && target < -9.99999)
                target = output;
            return 0 - (2.0 / _totalOutput) * (target - output);
        }

        //複製
        public ILossFunction Copy()
        {
            MeanSquareError mean = new MeanSquareError();
            mean._totalOutput = _totalOutput;
            return mean;
        }
    }
}
