using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using NetworkTool;

namespace TEST
{
    public class TrainXor
    {
        Network _network;
        Random _random = new Random();
        public TrainXor()
        {
            _network = new Network(2, 2, 1, 3);
            _network.RandomlyInitializeWeights(_random.Next());
        }

        public void Train()
        {
            Matrix<double> data = new Matrix<double>(4, 2);
            Matrix<double> target = new Matrix<double>(4, 2);

            for (int i = 0; i < 4; i++)
            {
                data[i, 0] = i % 2;
                data[i, 1] = i / 2;
                target[i, 0] = (i % 2) ^ (i / 2);
                target[i, 1] = 1 - target[i, 0];
            }

            _network.Update(data, target);

            Console.WriteLine(data.ToString());
            Console.WriteLine(_network.GetOutput().ToString());
            Console.WriteLine(_network.GetAllOutputError().ToString());
        }
    }
}
