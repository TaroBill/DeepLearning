using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public class MeanError : ILossFunction
    {

        private double _totalOutput;
        //lossfunction
        public double LossFunction(List<double> target, List<double> output)
        {
            double result = 0;
            _totalOutput = target.Count();
            for (int index = 0; index < target.Count(); index++)
                result += Math.Abs(target[index] - output[index]);
            return (1.0 / target.Count()) * result;
        }

        //lossfunction偏微分
        public double PartialDerivativeLossFunction(double target, double output)
        {
            double outputValue = (-1.0 / _totalOutput) * (Math.Abs(target - output) / (target - output));
            return outputValue;
        }

        //複製
        public ILossFunction Copy()
        {
            MeanError mean = new MeanError();
            mean._totalOutput = _totalOutput;
            return mean;
        }
    }
}
