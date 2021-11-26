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
                result += (1.0 / target.Count()) * (target[index] - output[index]) * (target[index] - output[index]);
            return result;
        }

        //均方誤差對輸出偏微分
        public double PartialDerivativeLossFunction(double target, double output)
        {
            return 0 - (2 / _totalOutput) * (target - output);
        }
    }
}
