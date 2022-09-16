using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    public interface IActivation
    {
        /// <summary>
        /// 激勵函式
        /// </summary>
        /// <param name="net"></param>
        /// <returns></returns>
        double ActivationFunction(double net);

        /// <summary>
        /// 激勵函式(需要用到所有輸出)例如softmax
        /// </summary>
        /// <param name="net"></param>
        /// <returns></returns>
        List<double> ActivationFunction(List<double> net);

        /// <summary>
        /// 對激勵函式微分
        /// </summary>
        /// <param name="net"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        double PartialDerivativeActivationFunction(double net, double output);

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        IActivation Copy();
    }
}
