using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using StatisticalTool;

namespace TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            _model = new MazeModel();
            _chart = new LineChart();
            _presentationModel = new PresentationModel(_model, _chart);
            InitializeComponent();
        }

        LineChart _chart;
        MazeModel _model;
        PresentationModel _presentationModel;
        Label[,] labs;
        int _delayTime;
        int round;

        private void Form1_Load(object sender, EventArgs e)
        {
            labs = new Label[_model.Width, _model.Height];
            for (int x = 0; x < _model.Width; x++)
            {
                for (int y = 0; y < _model.Height; y++)
                {

                    labs[x, y] = new Label
                    {
                        Font = new Font("Agency FB", 8.5F, FontStyle.Regular),
                        Size = new Size(60, 50),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    labs[x, y].Location = new Point(_presentationModel.GetXOfPosition(x), _presentationModel.GetYOfPosition(y));
                    labs[x, y].Click += SelectGrid;
                    labs[x, y].MouseEnter += DrawGrid;
                    this.Controls.Add(labs[x, y]);
                }
            }
            _model.MovedPosition += MovedPosition;
            _presentationModel.ChangedMazeState += ChangeMapState;
            _presentationModel.ChangedGrid += ChangeGrid;
            _presentationModel.ArrivedGoal += ArrivedGoal;
            _presentationModel.ClearMap();
            _presentationModel.LoadMazeSample1();
            _delayTime = 100;
        }

        //啟動尋路
        private void Find_Path(object sender, EventArgs e)
        {
            int counter = 0;
            if (_model.IsMazeComplete())
            {
                _model.ResetPosition();
                _presentationModel.ResetPreviousPosition();
                _presentationModel.Resume();
                richTextBox1.Text = "";
                round = 1;

                while (!_presentationModel.IsPause())
                {
                    _presentationModel.Move();
                    this.Text = $"目前已經走了\t{ _presentationModel.CurrentStep }步";
                    Delay(_delayTime);
                }
            }
        }

        private void ChangeSpeed(object sender, EventArgs e)
        {
            _delayTime = 100 / _delayTime;
        }

        public void Delay(int milliseconds)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).Milliseconds < milliseconds)
            {
                Application.DoEvents();
            }
        }

        public void ArrivedGoal(int totalStep)
        {
            richTextBox1.Text += $"{round++}:\t{totalStep}\r\n";
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        #region Set the Map

        private void StartToSetStartPoint(object sender, EventArgs e)
        {
            _presentationModel.StartToSelectStartPoint();
        }

        private void StartToSetEndPoint(object sender, EventArgs e)
        {
            _presentationModel.StartToSelectEndPoint();
        }

        private void SelectGrid(object sender, EventArgs e)
        {
            Point position = ((Label)sender).Location;
            int x = _presentationModel.GetXOfIndex(position.X);
            int y = _presentationModel.GetYOfIndex(position.Y);
            _presentationModel.SelectGridWithJudge(x, y);
        }

        private void DrawGrid(object sender, EventArgs e)
        {
            Point position = ((Label)sender).Location;
            int x = _presentationModel.GetXOfIndex(position.X);
            int y = _presentationModel.GetYOfIndex(position.Y);
            _presentationModel.DrawGrid(x, y);
        }

        private void GenerateWallsRandomly(object sender, EventArgs e)
        {
            _presentationModel.SetMazeRandomly();
        }

        private void ClearMap(object sender, EventArgs e)
        {
            _presentationModel.ClearMap();
        }

        #endregion

        //顯示Label的Text

        private void ChangeMapState(string outputText)
        {
            this.Text = outputText;
        }

        private void ChangeGrid(int x, int y, Color color)
        {
            labs[x, y].BackColor = color;
        }

        private void MovedPosition(int x, int y)
        {
            labs[_presentationModel.PreviousX, _presentationModel.PreviousY].Text = "";
            labs[x, y].Text = "( 0w0 )";
            _presentationModel.PreviousX = x;
            _presentationModel.PreviousY = y;
        }

        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            _presentationModel.Pause();
        }

        private void Pause(object sender, EventArgs e)
        {
            _presentationModel.Pause();
        }

        private void SaveModel(object sender, EventArgs e)
        {
            _presentationModel.SaveModel("./trainModel.model");
            MessageBox.Show("已完成存檔");
        }

        private void LoadModel(object sender, EventArgs e)
        {
            _presentationModel.LoadModel("./trainModel.model");
            MessageBox.Show("已完成讀檔");
        }

        private void OpenErrorChart(object sender, EventArgs e)
        {
            _buttonSeeError.Enabled = false;
            ChartForm form = new ChartForm(_chart);
            form.FormClosed += CloseErrorChart;
            form.Show();
        }

        private void CloseErrorChart(object sender, EventArgs e)
        {
            _buttonSeeError.Enabled = true;
        }

    }

    public class PresentationModel
    {
        public event Action<string> ChangedMazeState;
        public event Action<int> ArrivedGoal;
        public event Action<int, int, Color> ChangedGrid;

        private MazeModel _model;
        private AISample _ai;
        private readonly Color[] ColorSet = { Color.Wheat, Color.Gray, Color.Red, Color.Green, Color.LightPink, Color.LemonChiffon };
        private bool _isSelectingStart;
        private bool _isSelectingEnd;
        private bool _isDarwing;
        private bool _isPause;
        private int _previousX;
        private int _previousY;
        private Random _random = new Random();
        private double _rand => _random.NextDouble();
        public int CurrentStep;

        public PresentationModel(MazeModel model, LineChart chart)
        {
            this._model = model;
            this._isSelectingStart = false;
            this._isSelectingEnd = false;
            this._isDarwing = false;
            this._isPause = false;
            _model.ChangedGrid += OnGridChanged;
            _previousX = 0;
            _previousY = 0;
            _ai = new FindPathByDeepQLearning(chart, _model.Width, _model.Height);
            _ai.PointMoved += LoadPosition;
            CurrentStep = 0;
        }

        public void LoadMazeSample1()
        {
            for (int y = 0; y < _model.Height; y++)
            {
                if (y % 2 == 1)
                {
                    for (int x = 0; x < _model.Width; x++)
                    {
                        switch ((y/2)%2)
                        {
                            case 0:
                                if (x == 9)
                                    continue;
                                break;
                            case 1:
                                if (x == 0)
                                    continue;
                                break;
                            default:
                                break;
                        }
                        _model.SetWall(x, y);
                    }
                }
            }
            _model.SetStartPoint(0, 0);
            _model.SetEndPoint(_model.Width - 1, _model.Height - 1);
        }

        private void OnMazeStateChanged(string outputText)
        {
            if (ChangedMazeState != null)
                ChangedMazeState(outputText);
        }

        private void OnGridChanged(int x, int y, int typeIndex)
        {
            _ai.SetGrid(x, y, typeIndex);
            if (ChangedGrid != null)
                ChangedGrid(x, y, ColorSet[typeIndex]);
        }

        private void OnArriveGoal()
        {
            if (ChangedGrid != null)
                ArrivedGoal(CurrentStep);
        }

        #region AIOperation

        public void Move()
        {
            _ai.Learn();
            CurrentStep++;
            if (_model.IsGoal())
            {
                _ai.SetPoint(_model.GetPosition().X, _model.GetPosition().Y);
                OnArriveGoal();
                CurrentStep = 0;
            }
        }

        private void LoadPosition(int dx, int dy)
        {
            _model.Offset(dx, dy);
        }

        public void SaveModel(string path)
        {
            _ai.SaveModel(path);
        }

        public void LoadModel(string path)
        {
            _ai.LoadModel(path);
        }

        #endregion

        public int GetXOfIndex(int position)
        {
            return (position - 200) / 61;
        }

        public int GetXOfPosition(int index)
        {
            return 61 * index + 200;
        }

        public int GetYOfIndex(int position)
        {
            return position / 51;
        }

        public int GetYOfPosition(int index)
        {
            return 51 * index;
        }

        public int PreviousX
        {
            get
            {
                return _previousX;
            }
            set
            {
                _previousX = value;
            }
        }

        public int PreviousY
        {
            get
            {
                return _previousY;
            }
            set
            {
                _previousY = value;
            }
        }

        public void ResetPreviousPosition()
        {
            PreviousX = _model.GetStartPoint().X;
            PreviousY = _model.GetStartPoint().Y;
        }

        public void Pause()
        {
            this._isPause = true;
            OnMazeStateChanged("暫停");
        }

        public void Resume()
        {
            this._isPause = false;
            OnMazeStateChanged("");
        }

        public bool IsPause()
        {
            return _isPause;
        }

        public void StartToSelectStartPoint()
        {
            _isDarwing = false;
            _isSelectingStart = true;
            _isSelectingEnd = false;
            OnMazeStateChanged("選擇起點中");
        }

        public void StartToSelectEndPoint()
        {
            _isDarwing = false;
            _isSelectingStart = false;
            _isSelectingEnd = true;
            OnMazeStateChanged("選擇終點中");
        }

        public void SetIsDrawing(bool value)
        {
            _isDarwing = value;
            if (_isDarwing)
            {
                _isSelectingStart = false;
                _isSelectingEnd = false;
                OnMazeStateChanged("繪製牆壁中");
            }
            else
                OnMazeStateChanged("");
        }

        public void ChangeIsDrawing()
        {
            if (_isSelectingStart || _isSelectingEnd) return;
            SetIsDrawing(!_isDarwing);
        }

        public void SelectGrid(int x, int y)
        {
            if (_isSelectingStart)
            {
                _model.SetStartPoint(x, y);
                _isSelectingStart = false;
                OnMazeStateChanged("");
            }
            else if (_isSelectingEnd)
            {
                _model.SetEndPoint(x, y);
                _isSelectingEnd = false;
                OnMazeStateChanged("");
            }
            else
            {
                if (_model.GetGrid(x, y) == (int)MazeModel.GridType.Road)
                    _model.SetWall(x, y);
                else
                    _model.SetRoad(x, y);
            }
        }

        public void SelectGridWithJudge(int x, int y)
        {
            ChangeIsDrawing();
            if (_isSelectingStart || _isSelectingEnd || _isDarwing)
                SelectGrid(x, y);
        }

        public void DrawGrid(int x, int y)
        {
            if (!_isDarwing) return;
            SelectGrid(x, y);
        }

        public void SetMazeRandomly()
        {
            UpdateAllGrid(SetGridRandomly);
        }

        private void SetGridRandomly(int x, int y)
        {
            if (_rand * 2 < 1)
                _model.SetWall(x, y);
            else
                _model.SetRoad(x, y);
        }

        public void ClearMap()
        {
            UpdateAllGrid(_model.SetRoad);
        }

        private void UpdateAllGrid(Action<int, int> action)
        {
            for (int y = 0; y < _model.Height; y++)
            {
                for (int x = 0; x < _model.Width; x++)
                {
                    action(x, y);
                }
            }
        }
    }

    public class MazeModel
    {
        public event Action<int, int, int> ChangedGrid;
        public event Action<int, int> MovedPosition;

        public enum GridType
        {
            Road = 0,
            Wall = 1,
            Start = 2,
            End = 3
        }
        private int[,] _maze;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Point _position;
        private int PositionX
        {
            get => _position.X;
            set => _position.X = value;
        }
        private int PositionY
        {
            get => _position.Y;
            set => _position.Y = value;
        }
        private Point _startPoint;
        private Point _endPoint;

        public MazeModel()
        {
            Width = 10;
            Height = 10;
            _maze = new int[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.SetRoad(x, y);
                }
            }
            _startPoint = new Point(-1, -1);
            _endPoint = new Point(-1, -1);
            ResetPosition();
        }

        private void OnGridChanged(int x, int y)
        {
            if (ChangedGrid != null)
                ChangedGrid(x, y, _maze[x, y]);
        }

        private void OnPositionMoved()
        {
            if (MovedPosition != null)
                MovedPosition(PositionX, PositionY);
        }

        private int ToInt(GridType dataType)
        {
            return (int)dataType;
        }

        public void SetRoad(int x, int y)
        {
            if (!IsInsideMap(x, y))
                return;
            if (_maze[x, y] == ToInt(GridType.Start))
                _startPoint = new Point(-1, -1);
            else if (_maze[x, y] == ToInt(GridType.End))
                _endPoint = new Point(-1, -1);
            _maze[x, y] = ToInt(GridType.Road);
            OnGridChanged(x, y);
        }

        public void SetWall(int x, int y)
        {
            if (!IsInsideMap(x, y))
                return;
            if (_maze[x, y] == ToInt(GridType.Start))
                _startPoint = new Point(-1, -1);
            else if (_maze[x, y] == ToInt(GridType.End))
                _endPoint = new Point(-1, -1);
            _maze[x, y] = ToInt(GridType.Wall);
            OnGridChanged(x, y);
        }
        
        public int GetGrid(int x, int y)
        {
            if (IsInsideMap(x, y))
                return _maze[x, y];
            else
                return -1;
        }

        public void SetStartPoint(int x, int y)
        {
            if (IsInsideMap(_startPoint.X, _startPoint.Y))
            {
                if (_maze[_startPoint.X, _startPoint.Y] == ToInt(GridType.Start))
                {
                    _maze[_startPoint.X, _startPoint.Y] = ToInt(GridType.Road);
                    OnGridChanged(_startPoint.X, _startPoint.Y);
                }
            }
            if (IsInsideMap(x, y))
            {
                _startPoint = new Point(x, y);
                _maze[_startPoint.X, _startPoint.Y] = ToInt(GridType.Start);
                OnGridChanged(x, y);
            }
        }

        public Point GetStartPoint()
        {
            return new Point(_startPoint.X, _startPoint.Y);
        }

        public void SetEndPoint(int x, int y)
        {
            if (IsInsideMap(_endPoint.X, _endPoint.Y))
            {
                if (_maze[_endPoint.X, _endPoint.Y] == ToInt(GridType.End))
                {
                    _maze[_endPoint.X, _endPoint.Y] = ToInt(GridType.Road);
                    OnGridChanged(_endPoint.X, _endPoint.Y);
                }
            }
            if (IsInsideMap(x, y))
            {
                _endPoint = new Point(x, y);
                _maze[_endPoint.X, _endPoint.Y] = ToInt(GridType.End);
                OnGridChanged(x, y);
            }
        }

        public Point GetEndPoint()
        {
            return new Point(_endPoint.X, _endPoint.Y);
        }

        public void ResetPosition()
        {
            PositionX = _startPoint.X;
            PositionY = _startPoint.Y;
            OnPositionMoved();
        }

        public Point GetPosition()
        {
            return new Point(PositionX, PositionY);
        }

        private bool IsValueBetween(int limit1, int limit2, int value)
        {
            return (limit1 - value) * (value - limit2) >= 0;
        }

        private bool IsInsideMap(int x, int y)
        {
            if (!IsValueBetween(0, Width - 1, x))
                return false;
            if (!IsValueBetween(0, Height - 1, y))
                return false;
            return true;
        }

        public bool Offset(int dx, int dy)
        {
            if (!IsInsideMap(PositionX + dx, PositionY + dy))
                return false;
            if (_maze[PositionX + dx, PositionY + dy] == ToInt(GridType.Wall))
                return false;
            PositionX += dx;
            PositionY += dy;
            OnPositionMoved();
            return true;
        }

        public bool IsGoal()
        {
            if (PositionX == _endPoint.X && PositionY == _endPoint.Y)
            {
                ResetPosition();
                return true;
            }
            return false;
        }

        public bool IsMazeComplete()
        {
            if (!IsInsideMap(_startPoint.X, _startPoint.Y))
                return false;
            if (!IsInsideMap(_endPoint.X, _endPoint.Y))
                return false;
            ResetPosition();
            return true;
        }

    }

    public abstract class AISample
    {
        public event Action<int, int> PointMoved;

        public enum GridType
        {
            Road = 0,
            Wall = 1,
            Start = 2,
            End = 3
        }

        protected int _x;
        protected int _y;
        protected int _width;
        protected int _height;
        protected int[,] _mazeData;
        protected Random _random = new Random();
        protected double _rand => _random.NextDouble();

        protected AISample(int width, int height)
        {
            _width = width;
            _height = height;
            _mazeData = new int[width, height];
        }

        public void LoadMaze(int[,] maze)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _mazeData[x, y] = maze[x, y];
                }
            }
        }

        public void SetGrid(int x, int y, int data)
        {
            _mazeData[x, y] = data;
        }

        public int GetIndexOfType(GridType type)
        {
            return (int)type;
        }

        private bool IsValueBetween(int limit1, int limit2, int value)
        {
            return (limit1 - value) * (value - limit2) >= 0;
        }

        protected bool IsInsideMap(int x, int y)
        {
            if (!IsValueBetween(0, _width - 1, x))
                return false;
            if (!IsValueBetween(0, _height - 1, y))
                return false;
            return true;
        }

        public void DoMovePoint(int dx, int dy)
        {
            _x += dx;
            _y += dy;
            if (PointMoved != null)
                PointMoved(dx, dy);
        }

        public void SetPoint(int x, int y)
        {
            DoMovePoint(x - _x, y - _y);
        }

        public abstract void Learn();

        public abstract void SaveModel(string path);

        public abstract void LoadModel(string path);

    }

}