using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    public class TrainData
    {
        private double[] _currentState;
        private int _actIndex;
        private double _reward;
        private double[] _nextState;

        public TrainData(double[] currentState, int actIndex, double reward, double[] nextState)
        {
            _currentState = currentState;
            _actIndex = actIndex;
            _reward = reward;
            _nextState = nextState;
        }

        public double[] GetCurrentState()
        {
            return _currentState;
        }

        public int GetActIndex()
        {
            return _actIndex;
        }

        public double GetReward()
        {
            return _reward;
        }

        public double[] GetNextState()
        {
            return _nextState;
        }

    }
}
    