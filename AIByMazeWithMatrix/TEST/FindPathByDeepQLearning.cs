using System;
using System.Collections.Generic;
using System.IO;
using NetworkTool;
using StatisticalTool;
using Matrix;

namespace TEST
{
    //new_Q(s,a)= oldQ(s,a) + alpha * (R(s,a,s')+ (gamma * MaxQ(s')) - oldQ(s,a))
    public class FindPathByDeepQLearning:AISample
    {
        QNetwork _qNetwork;
        LineChart _chart;
        int _previousAct;
        private const double DISCOUNT_FACTOR = 0.1;
        private List<double> _preXs;
        private List<double> _preYs;
        int counter;
        const string weightFilePath = "./record.log";

        public FindPathByDeepQLearning(int width,int height) : base(width, height)
        {
            _qNetwork = new QNetwork(6, 4, 2, 7, 8);
            counter = 0;
            _preXs = new List<double>();
            _preYs = new List<double>();
            for (int i = 0; i < 4; i++)
            {
                _preXs.Add(-1);
                _preYs.Add(-1);
            }
            File.WriteAllText(weightFilePath, "");
        }

        public FindPathByDeepQLearning(LineChart chart, int width, int height) : this(width, height)
        {
            _chart = chart;
        }

        private void RecordErrorToChart(double error)
        {
            _chart.AddData(error);
        }

        public override void Learn()
        {
            int act = LearnAndGetActSelection();
            counter++;
            if (act >= 0)
            {
                _previousAct = act;
                _preXs.RemoveAt(0);
                _preXs.Add(_x);
                _preYs.RemoveAt(0);
                _preYs.Add(_y);
            }

            switch (act)
            {
                case 0:
                    UpdatePosition(_x, _y, 1, 0);
                    break;
                case 1:
                    UpdatePosition(_x, _y, 0, 1);
                    break;
                case 2:
                    UpdatePosition(_x, _y, -1, 0);
                    break;
                case 3:
                    UpdatePosition(_x, _y, 0, -1);
                    break;
                default:
                    break;
            }
            //throw new NotImplementedException();
        }

        private int LearnAndGetActSelection()
        {
            int nextX = _x;
            int nextY = _y;
            double[] currentState = GetData(_x, _y, _previousAct);
            int actIndex = _qNetwork.ChooseActIndex(currentState);
            int actualAct = actIndex;
            //取得下一個狀態
            switch (actIndex)
            {
                case 0:
                    nextX += 1;
                    break;
                case 1:
                    nextY += 1;
                    break;
                case 2:
                    nextX -= 1;
                    break;
                case 3:
                    nextY -= 1;
                    break;
                default:
                    break;
            }
            double reward = GetReward(nextX, nextY);
            if (IsWall(nextX, nextY))
            {
                nextX = _x;
                nextY = _y;
                actualAct = -1;
            }
            double[] nextState = GetData(nextX, nextY, actualAct);
            _qNetwork.Feedback(currentState, actIndex, reward, nextState);
            return actualAct;
        }

        private double GetReward(int x, int y)
        {
            if (IsWall(x, y))
                return -100;
            else if (_mazeData[x, y] == GetIndexOfType(GridType.End))
            {
                SaveModel("./temp.model");
                RecordErrorToChart(_qNetwork.GetAverageError());
                return 100;
            }
            else
            {
                int index = -1;
                for (int i = 3; i >= 0; i--)
                {
                    if(x==_preXs[i]&&y==_preYs[i])
                    {
                        index = i;
                        break;
                    }
                }
                if (index < 0)
                    return 10;
                else if (counter > 100)
                    return -1 * (index + 1);
                else
                    return 0;
            }

        }

        private bool IsWall(int x,int y)
        {
            if (!IsInsideMap(x, y))
                return true;
            else if (_mazeData[x, y] == GetIndexOfType(GridType.Wall))
                return true;
            else
                return false;
        }

        /*private double[] GetData(int x, int y, int previousAct)
        {
            double[] data = new double[10];
            for (int i = 0; i < 9; i++)
            {
                if (GetGridDataRelatively(x, y, i) == GetIndexOfType(GridType.Wall))
                    data[i] = -1;
                else
                    data[i] = 1;
            }
            data[9] = previousAct;
            return data;
        }//*/

        /*private double[] GetData(int x, int y, int previousAct)
        {
            double[] data = new double[8];
            for (int i = 0; i < 9; i++)
            {
                data[i / 3] *= 2;
                data[(i % 3) + 3] *= 2;
                if (GetGridDataRelatively(x, y, i) != GetIndexOfType(GridType.Wall))
                {

                    data[i / 3] += 1;
                    data[(i % 3) + 3] += 1;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                data[i] = data[i] / 7 - 0.5;
            }
            data[6] = Math.Tanh(GetReward(x, y));
            data[7] = previousAct;
            return data;
        }//*/

        private double[] GetData(int x, int y, int previousAct)
        {
            double[] data = new double[6];
            for (int i = 0; i < 4; i++)
            {
                if (GetGridDataRelatively(x, y, i * 2 + 1) != GetIndexOfType(GridType.Wall))
                    data[i] = 1;
                else
                    data[i] = -1;
            }
            data[4] = Math.Tanh(GetReward(x, y));
            data[5] = previousAct;
            return data;
        }//*/

        private int GetGridDataRelatively(int x, int y, int index)
        {
            if (index < 0 || index > 8)
                throw new IndexOutOfRangeException();
            int dx = (index % 3) - 1;
            int dy = (index / 3) - 1;
            if (IsInsideMap(x + dx, y + dy))
                return _mazeData[x + dx, y + dy];
            else
                return GetIndexOfType(GridType.Wall);
        }
        
        private void UpdatePosition(int x, int y, int dx, int dy)
        {
            base.DoMovePoint(dx, dy);
        }

        public override void LoadModel(string path)
        {
            _qNetwork.Load(path);
        }

        public override void SaveModel(string path)
        {
            //throw new NotImplementedException();
            _qNetwork.Save(path);
        }

    }

    public class QNetwork
    {
        public delegate void UpdateEventHandler(double error);
        public event UpdateEventHandler NetworkUpdated;

        public Random random = new Random();
        public const double DISCOUNT_VALUE = 0.3;
        public double _curiosity = 0.05;

        private Network _evaluatingNetwork;
        private Network _learningNetwork;
        private int _numberOfInput;
        private int _numberOfOutput;

        private List<TrainData> _memorySet;             //記憶集
        private const int MEMORY_MAXIMUM_SIZE = 2000;   //記憶集最大儲存量
        private const int MEMORY_MINIMUM_SIZE = 200;    //記憶集最小運作量
        private const int TRAIN_BATCH_SIZE = 32;        //一次訓練量
        private const int LEARNING_CYCLE = 20;          //更新學習網路週期
        private const int COPY_CYCLE = 200;             //更新現實網路週期
        private int _learningCounter;                   //學習網路計數器
        private int _copyCounter;                       //現實網路計數器

        //private double[] _actionTotalError;
        //private int[] _counterForAddedError;
        private List<double> _errorRecord;
        private const int ERROR_RECORD_SIZE = 100;

        public QNetwork(int numberOfStateData, int numberOfAction, int numberOfHiddenLayer, params int[] numberOfHideNode)
        {
            _numberOfInput = numberOfStateData;
            _numberOfOutput = numberOfAction;
            _learningNetwork = new Network(_numberOfInput, _numberOfOutput, numberOfHiddenLayer, numberOfHideNode);
            _learningNetwork.RandomlyInitializeWeights(random.Next());
            _evaluatingNetwork = new Network(_learningNetwork.ToString());
            //_actionTotalError = new double[numberOfAction];
            //_counterForAddedError = new int[numberOfAction];
            _memorySet = new List<TrainData>();
            _errorRecord = new List<double>();
            _learningCounter = 0;
            _copyCounter = 0;
        }

        private void Updated(double error)
        {
            if (NetworkUpdated != null)
                NetworkUpdated(error);
        }

        #region Value Getter

        private Matrix<double> GetMaxActionValue(Network network, Matrix<double> data)
        {
            network.InputDataToNetwork(data);
            Matrix<double> output = network.GetOutput();
            int numberOfStates = output.ColumnCount;
            int numberOfData = output.RowCount;
            for (int i = 1; i < numberOfStates; i++)
            {
                for (int j = 0; j < numberOfData; j++)
                {
                    if (output[j, 0] < output[j, i])
                        output[j, 0] = output[j, i];
                }
            }

            Matrix<double> maxValues = new Matrix<double>(numberOfData, 1);
            for (int i = 0; i < numberOfData; i++)
            {
                    maxValues[i, 0] = output[i, 0];
            }
            return maxValues;
        }

        private Matrix<int> GetMaxActionIndex(Network network, Matrix<double> data)
        {
            network.InputDataToNetwork(data);
            Matrix<double> output = network.GetOutput();
            int numberOfStates = output.ColumnCount;
            int numberOfData = output.RowCount;
            Matrix<int> maxIndex = new Matrix<int>(numberOfData, 1);

            for (int i = 1; i < numberOfStates; i++)
            {
                for (int j = 0; j < numberOfData; j++)
                {
                    if (output[j, 0] < output[j, i])
                    {
                        output[j, 0] = output[j, i];
                        maxIndex[j, 0] = i;
                    }
                }
            }

            return maxIndex;
        }

        private Matrix<double> GetActionValue(Network network, Matrix<int> index, Matrix<double> data)
        {
            int numberOfData = data.RowCount;
            network.InputDataToNetwork(data);
            Matrix<double> output = network.GetOutput();
            Matrix<double> result = new Matrix<double>(numberOfData,1);
            for (int i = 0; i < numberOfData; i++)
            {
                result[i, 0] = output[i, index[i, 0]];
            }
            return result;
        }

        public double GetAverageError()
        {
            double result = 0;
            if (_errorRecord.Count != 0) 
            {
                foreach (double error in _errorRecord)
                {
                    result += error * error;
                }
                result /= _errorRecord.Count;
            }
            return result;
        }

        #endregion

        public int ChooseActIndex(params double[] state)
        {
            int length = state.Length;
            Matrix<double> convertedState = new Matrix<double>(1, length);
            for (int i = 0; i < length; i++)
            {
                convertedState[0, i] = state[i];
            }
            return ChooseActIndex(convertedState)[0, 0];
        }

        public Matrix<int> ChooseActIndex(Matrix<double> state)
        {//d*d 1*d
            int numberOfData = state.RowCount;
            Matrix<int> mask = new Matrix<int>(numberOfData, numberOfData);
            Matrix<int> result = new Matrix<int>(numberOfData, 1);

            for (int i = 0; i < numberOfData; i++)
            {
                if (random.NextDouble() < _curiosity)
                    result[i, 0] = (int)(random.NextDouble() * 4);
                else
                    mask[i, i] = 1;
            }

            result += mask * GetMaxActionIndex(_evaluatingNetwork, state);
            return result;
        }

        public void Feedback(double[] currentState, int actIndex, double reward, double[] nextState)
        {
            StoreMemory(currentState, actIndex, reward, nextState);
            if (_memorySet.Count > MEMORY_MINIMUM_SIZE)
            {
                Update();
            }
        }

        private void StoreMemory(double[] currentState, int actIndex, double reward, double[] nextState)
        {
            TrainData data = new TrainData(currentState, actIndex, reward, nextState);
            _memorySet.Add(data);
            while (_memorySet.Count > MEMORY_MAXIMUM_SIZE)
                _memorySet.RemoveAt(0);
        }

        public void Update()
        {
            if (_copyCounter == COPY_CYCLE)
            {
                UpdateLearningNetwork();
                _copyCounter = 0;
            }
            if (_learningCounter == LEARNING_CYCLE)
            {
                UpdateEvalutingNetwork();
                _learningCounter = 0;
                _copyCounter++;
            }
            _learningCounter++;
        }

        public void UpdateEvalutingNetwork()
        {
            int trainDataIndex;
            int memorySize = _memorySet.Count;
            Matrix<double> currentState = new Matrix<double>(TRAIN_BATCH_SIZE, _numberOfInput);
            Matrix<int> actIndex = new Matrix<int>(TRAIN_BATCH_SIZE, 1);
            Matrix<double> reward = new Matrix<double>(TRAIN_BATCH_SIZE, 1);
            Matrix<double> nextState = new Matrix<double>(TRAIN_BATCH_SIZE, _numberOfInput);
            int index;
            for (int i = 0; i < TRAIN_BATCH_SIZE; i++)
            {
                trainDataIndex = (int)(random.NextDouble() * memorySize);
                TrainData trainData = _memorySet[trainDataIndex];
                index = 0;
                foreach (double value in trainData.GetCurrentState())
                {
                    currentState[i, index++] = value;
                }
                actIndex[i, 0] = trainData.GetActIndex();
                reward[i, 0] = trainData.GetReward();
                index = 0;
                foreach (double value in trainData.GetNextState())
                {
                    nextState[i, index++] = value;
                }
            }
            
            Matrix<double> error = Train(currentState, actIndex, reward, nextState);

            _evaluatingNetwork.UpdateByError(currentState,error);
        }

        private Matrix<double> ConvertActIndexToMask(Matrix<int> actIndex)
        {
            int numberOfData = actIndex.RowCount;
            Matrix<double> mask = new Matrix<double>(numberOfData, _numberOfOutput);
            for (int i = 0; i < numberOfData; i++)
            {
                mask[i, actIndex[i, 0]] = 1;
            }
            return mask;
        }

        /// <summary>
        /// Return a matrix of errors
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="actIndex"></param>
        /// <param name="reward"></param>
        /// <param name="nextState"></param>
        private Matrix<double> Train(Matrix<double> currentState, Matrix<int> actIndex, Matrix<double> reward, Matrix<double> nextState)
        {
            //Matrix<double> newQValue = reward + GetMaxActionValue(_learningNetwork, nextState);//Nature DQN
            Matrix<double> newQValue = reward + GetActionValue(_learningNetwork, GetMaxActionIndex(_evaluatingNetwork, nextState), nextState);//Double DQN
            Matrix<double> oldQValue = GetActionValue(_evaluatingNetwork, actIndex, currentState);
            Matrix<double> error = oldQValue - newQValue;

            Func<double, double, double> productFunction = (value1, value2) =>
              {
                  return value1 * value2;
              };
            Matrix<double> errorMatrix = new Matrix<double>(1, _numberOfOutput);
            for (int i = 0; i < _numberOfOutput; i++)
            {
                errorMatrix[0, i] = 1;
            }
            errorMatrix = error * errorMatrix;
            errorMatrix = errorMatrix.Joint<double, double>(ConvertActIndexToMask(actIndex), productFunction);

            double avgError = (error.Transposition() * error)[0, 0];
            avgError /= error.RowCount;
            _errorRecord.Add(avgError);
            while (_errorRecord.Count > ERROR_RECORD_SIZE)
                _errorRecord.RemoveAt(0);
            
            Updated(avgError);
            return errorMatrix;
        }

        private void UpdateLearningNetwork()
        {
            _learningNetwork = new Network(_evaluatingNetwork.ToString());
        }

        public void Load(string path)
        {
            string data = File.ReadAllText(path);
            _learningNetwork = new Network(data);
            _evaluatingNetwork = new Network(data);
        }

        public void Save(string path)
        {
            _learningNetwork.Save(path);
            File.AppendAllText(path, $"Error = {GetAverageError()}\n");
        }

        #region ForTest

        public void RecordErrors(string path)
        {
            foreach (double value in _errorRecord)
            {
                File.AppendAllText(path, $"{value},");
                if (double.IsNaN(value))
                    throw new InvalidDataException();
            }
            File.AppendAllText(path, "\r\n");
        }

        public void RecordValue(string path, double value)
        {
            File.AppendAllText(path, $"{value}\r\n");
            if (double.IsNaN(value))
                throw new InvalidDataException();
        }

        #endregion

    }

}
