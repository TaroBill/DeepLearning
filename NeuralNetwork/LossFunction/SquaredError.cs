using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public class SquaredError : ILossFunction
    {
        //輸出Total Error
        public double LossFunction(List<double> target, List<double> output)
        {
            double result = 0;
            for (int index = 0; index < target.Count(); index++)
                result += 0.5 * (target[index] - output[index]) * (target[index] - output[index]);
            return result;
        }

        //LossFunction對輸出做偏微分
        public double PartialDerivativeLossFunction(double target, double output)
        {
            return 0 - (target - output);
        }

        //複製
        public ILossFunction Copy()
        {
            return new SquaredError();
        }
    }
}
