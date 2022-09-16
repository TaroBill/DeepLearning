using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.LossFunction
{
    public interface ILossFunction
    {
        /// <summary>
        /// 損失函式
        /// </summary>
        /// <param name="target"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        double LossFunction(List<double> target, List<double> output);

        /// <summary>
        /// 損失函式對輸出偏微分
        /// </summary>
        /// <param name="target"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        double PartialDerivativeLossFunction(double target, double output);

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        ILossFunction Copy();
    }
}
