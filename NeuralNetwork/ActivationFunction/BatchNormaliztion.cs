using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ActivationFunction
{
    class BatchNormaliztion : IActivation
    {
        private readonly double _beta;
        private readonly double _gamma;
        private double _sigma2;//σ^2
        private double _mean;//µ
        private int _totalAmount;//N
        private double _totalXHead;//輸出結果總和

        public BatchNormaliztion(double beta, double gamma)
        {
            _beta = beta;
            _gamma = gamma;
        }

        //不實作
        public double ActivationFunction(double net)
        {
            return net;
        }

        //softmax
        public List<double> ActivationFunction(List<double> net)
        {
            List<double> output = new List<double>();
            int totalAmount = net.Count();
            double sigma2 = 0;
            double mean = 0;
            foreach (double xi in net)
                mean += xi;
            mean /= totalAmount;
            foreach (double xi in net)
                sigma2 += (xi - mean) * (xi - mean);
            sigma2 /= totalAmount;
            _totalXHead = 0;
            foreach (double xi in net)
            {
                double xHead = (xi - mean) / Math.Sqrt(sigma2 + Math.Pow(10, -8));
                _totalXHead += xHead;
                output.Add(_gamma * xHead + _beta);
            }
            _sigma2 = sigma2;
            _mean = mean;
            _totalAmount = totalAmount;
            return output;
        }

        //softmax對輸出篇微分
        public double PartialDerivativeActivationFunction(double net, double output)
        {
            return net;
        }

        //複製
        public IActivation Copy()
        {
            BatchNormaliztion batch = new BatchNormaliztion(_beta, _gamma);
            return batch;
        }

    }
}
