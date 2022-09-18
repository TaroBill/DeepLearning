using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class HiddenLayer : Layer
    {
        private double bias;

        //public HiddenLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes + 1, numberOfWeightsPerNode)
        public HiddenLayer(int numberOfNodes, int numberOfWeightsPerNode) : base(numberOfNodes, numberOfWeightsPerNode)
        {
            _funtion = new Function.ReLu();
        }
        
        public HiddenLayer(string data) : base(data)
        {
            _funtion = new Function.ReLu();
        }

        public override void RandomlyInitializeWeights(int seed)
        {
            Random random = new Random(seed);
            int rowCount = _weights.RowCount;
            int columnCount = _weights.ColumnCount;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    _weights[i, j] = random.NextDouble();
                }
            }
        }

        /*private void SetBias()
        {
            int numberOfRow = _outputs.RowCount;
            for (int i = 0; i < numberOfRow; i++)
            {
                _outputs[i, 0] = bias;
            }
        }//*/

        public override Matrix<double> InputData(Matrix<double> data)
        {
            base.InputData(data);
            //SetBias();

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
