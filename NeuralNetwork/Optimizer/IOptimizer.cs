using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public interface IOptimizer
    {
        //輸入當前梯度取得優化梯度
        double GetResult(double gradient, double learningRate);

        //複製
        IOptimizer Copy();
    }
}
