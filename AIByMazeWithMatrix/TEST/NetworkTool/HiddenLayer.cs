using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTool.Function;

namespace NetworkTool
{
    public class HiddenLayer : Layer
    {
        private readonly IFunction DEFAULT_FUNCTION = new Function.ReLu();

        //public HiddenLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes + 1, numberOfWeightsPerNode)
        public HiddenLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes, numberOfWeightsPerNode)
        {
            _funtion = DEFAULT_FUNCTION;
        }
        
        public HiddenLayer(string data) : base(data)
        {
            _funtion = DEFAULT_FUNCTION;
        }

        public override void RandomlyInitializeWeights(int seed)
        {
            base.RandomlyInitializeWeights(seed);
        }

        public override Matrix<double> InputData(Matrix<double> data)
        {
            base.InputData(data);

            return new Matrix<double>(_outputs);
        }

        public override Matrix<double> InputError(Matrix<double> errors)
        {
            return base.InputError(errors);
        }

        public override string ToString()
        {
            return "Hidden" + base.ToString();
        }
        //*/
    }
}
