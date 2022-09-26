using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public class CrossEntropy : ILossFunction
    {
        //損失函式
        public double LossFunction(List<double> target, List<double> output)
        {
            double result = 0;
            for (int index = 0; index < target.Count; index++)
                result += (target[index] * Math.Log(output[index]));
            return 0 - result;
        }

        //損失函式對該輸出偏微分
        public double PartialDerivativeLossFunction(double target, double output)
        {
            double result = (0 - target) / (output * Math.Log(2, Math.E));
            return result;
        }

        //複製
        public ILossFunction Copy()
        {
            return new CrossEntropy();
        }

        public string GetName()
        {
            return "CrossEntropy";
        }
    }
}
