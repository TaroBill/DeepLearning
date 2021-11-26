using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public static class MyRandom
    {
        //高斯分布隨機
        public static double NextGuass(double mean, double standard)
        {
            Random random;
            random = new Random(Guid.NewGuid().GetHashCode());
            double randomValue = random.NextDouble();
            random = new Random(Guid.NewGuid().GetHashCode());
            double randomValue2 = random.NextDouble();
            //_biasWeight.Add((randomValue * 2 - 1));
            double guassRandom = Math.Sqrt(-2 * Math.Log(randomValue)) * Math.Cos(2 * Math.PI * randomValue2) * standard + mean;
            return guassRandom;
        }

        //使用Xavier隨機分布
        public static double NextXavier(double mean, double standard, int amount)
        {
            double randomValue = NextGuass(mean, standard);
            return randomValue * Math.Sqrt(2.0 / amount);
        }
    }
}
