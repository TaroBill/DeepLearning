using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public interface IOptimizer
    {
        void Init(params double[] parameters);

        double GetDelta(double error);

        void Reset();
    }
}
