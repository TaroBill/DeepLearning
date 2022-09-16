using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    class RandomGenerater
    {
        Random _random;
        const int _maxNumber = 10000;
        int _counter;
        bool _isDebugMode;

        public RandomGenerater()
        {
            _random = new Random();
        }

        public void StartDebugMode()
        {
            _isDebugMode = true;
        }

        public void EndDebugMode()
        {
            _isDebugMode = false;
        }

        public int Next()
        {
            if (_isDebugMode)
            {
                _counter %= _maxNumber;
                return _counter++;
            }
            else
            {
                return _random.Next();
            }
        }

        public double NextDouble()
        {
            if (_isDebugMode)
            {
                _counter %= _maxNumber;
                return (_counter++) / _maxNumber;
            }
            else
            {
                return _random.NextDouble();
            }
        }

    }
}
