using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Optimizer
{
    public class SGD : IOptimizer
    {
        //複製
        public IOptimizer Copy()
        {
            return new SGD();
        }

        //取得SGD輸出結果
        public double GetResult(double gradient, double learningRate)
        {
            return gradient * learningRate;
        }
    }
}
