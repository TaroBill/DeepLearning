using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTool
{
    public static class Functions
    {

        public static double noActivation(double value)
        {
            return value;
        }

        public static double NoPartDerivativeActivation(double value)
        {
            return 1;
        }

        public static double ReLuActivation(double value)
        {
            return value < 0 ? 0 : value;
        }

        public static double ReLuPartDerivativeActivation(double value)
        {
            return 1;
        }

        public static double LogisticActivation(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public static double LogisticPartDerivativeActivation(double value)
        {
            return value * (1 - value);
        }

    }
}
