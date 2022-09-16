using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public class InputLayer : Layer
    {
        private double bias = 0.1;

        public InputLayer(int numberOfNodes) : base(numberOfNodes + 1, 0) { }

        public InputLayer(string data) : base(data)
        {

        }

        public override void RandomlyInitializeWeights(int seed) { }

        private Matrix<double> GenerateCopyMatrix(int numberOfNode)
        {
            Matrix<double> result = new Matrix<double>(numberOfNode, numberOfNode + 1);
            for (int i = 0; i < numberOfNode; i++)
            {
                result[i, i + 1] = 1;
            }
            return result;
        }

        private void SetBias()
        {
            int numberOfRow = _outputs.RowCount;
            for (int i = 0; i < numberOfRow; i++)
            {
                _outputs[i, 0] = bias;
            }
        }

        public override Matrix<double> InputData(Matrix<double> data)
        {
            int numberOfData = data.RowCount;

            _outputs = data * GenerateCopyMatrix(data.ColumnCount);
            SetBias();

            return new Matrix<double>(_outputs);
        }

        public override Matrix<double> InputError(Matrix<double> error)
        {
            return new Matrix<double>(0, 0);
        }

        public override string ToString()
        {
            return "Input"+base.ToString();
        }
        //*/
    }
}
