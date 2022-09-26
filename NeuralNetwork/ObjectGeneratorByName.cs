using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.LossFunction;
using NeuralNetwork.Optimizer;
using NeuralNetwork.ActivationFunction;

namespace NeuralNetwork
{
    public static class ObjectGeneratorByName
    {
        public static ILossFunction GetLossFunction(string name)
        {
            switch (name)
            {
                case "MeanError":
                    return new MeanError();
                case "MeanSquareError":
                    return new MeanSquareError();
                case "SquaredError":
                    return new SquaredError();
                case "CrossEntropy":
                    return new CrossEntropy();
                default:
                    throw new Exception("Unknow Name");
            }
        }

        public static IOptimizer GetOptimizer(string name)
        {
            string argString = "";
            List<double> args = new List<double>();
            if (name.Contains("("))
            {
                int index = name.IndexOf("(");//"("的位置
                argString = name.Substring(index).Trim('(', ')');
                args = argString.Split(',').Select(x => Convert.ToDouble(x)).ToList();
                name = name.Remove(index);
            }
            switch (name)
            {
                case "SGD":
                    return new SGD();
                case "Momentum":
                    return new Momentum(args[0]);
                case "AdaGrad":
                    return new AdaGrad(args[0]);
                case "RMSprop":
                    return new RMSprop(args[0], args[1]);
                default:
                    throw new Exception("Unknow Name");
            }
        }

        public static IActivation GetActivation(string name)
        {
            string argString = "";
            List<double> args = new List<double>();
            if (name.Contains("("))
            {
                int index = name.IndexOf("(");//"("的位置
                argString = name.Substring(index).Trim('(', ')');
                args = argString.Split(',').Select(x => Convert.ToDouble(x)).ToList();
                name = name.Remove(index);
            }
            switch (name)
            {
                case "Linear":
                    return new Linear();
                case "Sigmoid":
                    return new Sigmoid();
                case "Relu":
                    return new Relu();
                case "LeakyRelu":
                    return new LeakyRelu(args[0]);
                case "Softmax":
                    return new Softmax();
                case "Tanh":
                    return new Tanh();
                default:
                    throw new Exception("Unknow Name");
            }
        }
    }
}
