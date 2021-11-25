using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public interface ILossFunction
    {
        //損失函式
        double LossFunction(List<double> target, List<double> output);

        //損失函式對輸出偏微分
        double PartialDerivativeLossFunction(double target, double output);
    }
}
