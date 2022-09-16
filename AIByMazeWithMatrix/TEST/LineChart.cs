using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalTool
{
    public class LineChart
    {
        public delegate void DataChangedEventHandler();
        public event DataChangedEventHandler DataChanged;

        List<double> _data;

        public LineChart()
        {
            _data = new List<double>();
        }

        private void ChangedData()
        {
            if (DataChanged != null)
                DataChanged();
        }

        public int Count()
        {
            return _data.Count();
        }

        public void AddData(double value)
        {
            _data.Add(value);
            ChangedData();
        }

        public void InsertData(int index, double value)
        {
            _data.Insert(index, value);
        }

        public double GetDataAt(int index)
        {
            return _data[index];
        }

        public void RemoveDataAt(int index)
        {
            _data.RemoveAt(index);
        }

        public void Clear()
        {
            _data = new List<double>();
        }

        public List<double> GetAllData()
        {
            return _data.ToList();
        }

        /// <summary>
        /// 輸出第一筆是平均值
        /// 輸出第二筆是標準差
        /// 其餘為轉換後資料
        /// </summary>
        /// <returns></returns>
        public List<double> GetAllDataByZScore()
        {
            int numberOfData = _data.Count;
            double sumOfX = _data.Sum();
            double sumOfSquareX = _data.Sum(x => x * x);
            double mean = sumOfX / numberOfData;
            double standard_deviation = sumOfSquareX - numberOfData * mean * mean;
            List<double> result = new List<double>();
            result.Add(mean);
            result.Add(standard_deviation);
            foreach (double data in _data)
            {
                result.Add((data - mean) / standard_deviation);
            }
            return result;
        }

        /// <summary>
        /// 輸出第一筆是最小值
        /// 輸出第二筆是最大值
        /// 其餘為轉換後資料
        /// </summary>
        /// <returns></returns>
        public List<double> GetAllDataByMinMax()
        {
            double min = _data.Min();
            double max = _data.Max();
            double range = max - min;
            List<double> result = new List<double>();
            result.Add(min);
            result.Add(max);
            foreach (double data in _data)
            {
                result.Add((data - min) / range);
            }
            return result;
        }

    }
}
