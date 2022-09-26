using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public interface IOptimizer
    {
        /// <summary>
        /// 輸入當前梯度取得優化梯度
        /// </summary>
        /// <param name="gradient"></param>
        /// <param name="learningRate"></param>
        /// <returns></returns>
        double GetResult(double gradient, double learningRate);

        /// <summary>
        /// 複製
        /// </summary>
        /// <returns></returns>
        IOptimizer Copy();

        string GetName();
    }
}
