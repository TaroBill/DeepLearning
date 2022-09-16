using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatisticalTool;

namespace TEST
{
    public partial class ChartForm : Form
    {
        public ChartForm(LineChart chart)
        {
            InitializeComponent();
            _chart = chart;
            _chart.DataChanged += UpdateChart;
            _bitmap = new Bitmap(1920, 1080);
            _graphics = Graphics.FromImage(_bitmap);
            UpdateChart();
        }

        LineChart _chart;
        Bitmap _bitmap;
        Graphics _graphics;

        private void UpdateChart()
        {
            _graphics.Clear(Color.Wheat);
            if (_chart.Count() < 2)
            {
                pictureBox.Image = _bitmap;
                return;
            }
            while (_chart.Count() > 192)
                _chart.RemoveDataAt(0);
            List<double> data = _chart.GetAllDataByMinMax();
            int numberOfData = data.Count();
            double min = data[0];
            double range = data[1] - min;
            double x1;
            double y1;
            double x2 = 0;
            double y2 = data[2] * range + min;

            for (int i = 3; i < numberOfData; i++)
            {
                x1 = x2;
                y1 = y2;
                x2 += 10;
                y2 = data[i] * range + min;
                _graphics.DrawLine(new Pen(Color.Indigo, 2), (float)x1, 540 - 200f * (float)y1, (float)x2, 540 - 50f * (float)y2);
                Application.DoEvents();
            }

            pictureBox.Image = _bitmap;
        }

    }
}
