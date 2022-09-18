using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using NetworkTool.Function;

namespace NetworkTool
{
    public interface ILayer
    {

        Matrix<double> InputData(Matrix<double> data);

        Matrix<double> InputError(Matrix<double> errors);

        void RandomlyInitializeWeights(int seed);

        int GetNumberOfNode();


    }

}
