using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class Matrix<T>
    {
        private int _rowNumber;
        private int _columnNumber;
        private T[][] _data;

        public Matrix(int numberOfRow, int numberOfColumn)
        {
            _rowNumber = numberOfRow;
            _columnNumber = numberOfColumn;
            _data = new T[_rowNumber][];

            for (int i = 0; i < _rowNumber; i++)
            {
                _data[i] = new T[_columnNumber];
                for (int j = 0; j < _columnNumber; j++)
                {
                    _data[i][j] = default(T);
                }
            }

        }
        public Matrix(Matrix<T> matrix)
        {
            _rowNumber = matrix._rowNumber;
            _columnNumber = matrix._columnNumber;
            _data = new T[_rowNumber][];

            for (int i = 0; i < _rowNumber; i++)
            {
                _data[i] = new T[_columnNumber];
                for (int j = 0; j < _columnNumber; j++)
                {
                    _data[i][j] = matrix._data[i][j];
                }
            }

        }
        public T this[int rowIndex, int columnIndex]
        {
            get
            {
                return _data[rowIndex][columnIndex];
            }
            set
            {
                _data[rowIndex][columnIndex] = value;
            }
        }

        public int RowCount 
        {
            get => _rowNumber;
        }
        public int ColumnCount
        {
            get => _columnNumber;
        }
        public Matrix<T> GetRow(int index)
        {
            Matrix<T> result = new Matrix<T>(1, this._columnNumber);

            for (int i = 0; i < result._columnNumber; i++)
            {
                result._data[0][i] = this._data[index][i];
            }

            return result;
        }
        public Matrix<T> GetColumn(int index)
        {
            Matrix<T> result = new Matrix<T>(this._rowNumber, 1);

            for (int i = 0; i < result._rowNumber; i++)
            {
                result._data[i][0] = this._data[i][index];
            }

            return result;
        }
        public Matrix<T> Transposition()
        {
            Matrix<T> result = new Matrix<T>(this._columnNumber, this._rowNumber);

            for (int i = 0; i < _rowNumber; i++)
            {
                for (int j = 0; j < _columnNumber; j++)
                {
                    result._data[j][i] = this._data[i][j];
                }
            }

            return result;
        }

        public static Matrix<T> operator +(Matrix<T> matrix) => new Matrix<T>(matrix);
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            Matrix<T> result = new Matrix<T>(matrix._rowNumber, matrix._columnNumber);

            for (int i = 0; i < result._rowNumber; i++)
            {
                for (int j = 0; j < result._columnNumber; j++)
                {
                    dynamic value = matrix._data[i][j];
                    result._data[i][j] = -value;
                }
            }

            return result;
        }

        public static Matrix<T> operator +(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1._rowNumber != matrix2._rowNumber)
                throw new ArithmeticException("The two matrices have different sizes.");
            if (matrix1._columnNumber != matrix2._columnNumber)
                throw new ArithmeticException("The two matrices have different sizes.");

            Matrix<T> result = new Matrix<T>(matrix1._rowNumber, matrix1._columnNumber);

            for (int i = 0; i < result._rowNumber; i++)
            {
                for (int j = 0; j < result._columnNumber; j++)
                {
                    dynamic value1 = matrix1._data[i][j];
                    dynamic value2 = matrix2._data[i][j];
                    result._data[i][j] = value1 + value2;
                }
            }

            return result;
        }
        public static Matrix<T> operator -(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1._rowNumber != matrix2._rowNumber)
                throw new ArithmeticException("The two matrices have different sizes.");
            if (matrix1._columnNumber != matrix2._columnNumber)
                throw new ArithmeticException("The two matrices have different sizes.");

            Matrix<T> result = matrix1 + (-matrix2);

            return result;
        }
        public static Matrix<T> operator *(T constant, Matrix<T> matrix)
        {
            Matrix<T> result = new Matrix<T>(matrix.RowCount, matrix.ColumnCount);

            for (int i = 0; i < result._rowNumber; i++)
            {
                for (int j = 0; j < result._columnNumber; j++)
                {
                    dynamic value = matrix._data[i][j];
                    result._data[i][j] = constant * value;
                }
            }

            return result;
        }
        public static Matrix<T> operator *(Matrix<T> matrix, T constant)
        {
            Matrix<T> result = new Matrix<T>(matrix.RowCount, matrix.ColumnCount);

            for (int i = 0; i < result._rowNumber; i++)
            {
                for (int j = 0; j < result._columnNumber; j++)
                {
                    dynamic value = matrix._data[i][j];
                    result._data[i][j] = constant * value;
                }
            }

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1._columnNumber != matrix2._rowNumber)
                throw new ArithmeticException("The sizes of the two matrices don't match.");

            Matrix<T> result = new Matrix<T>(matrix1.RowCount, matrix2.ColumnCount);
            int length = matrix1._columnNumber;
            T sum;

            for (int i = 0; i < result._rowNumber; i++)
            {
                for (int j = 0; j < result._columnNumber; j++)
                {
                    sum = default(T);
                    for (int k = 0; k < length; k++)
                    {
                        dynamic value1 = matrix1._data[i][k];
                        dynamic value2 = matrix2._data[k][j];
                        sum += value1 * value2;
                    }
                    result._data[i][j] = sum;
                }
            }

            return result;
        }
        public static T InnerProduct(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1._rowNumber != matrix2._rowNumber)
                throw new ArithmeticException("The two matrices have different sizes.");
            if (matrix1._columnNumber != matrix2._columnNumber)
                throw new ArithmeticException("The two matrices have different sizes.");

            T result = default;

            for (int i = 0; i < matrix1._rowNumber; i++)
            {
                for (int j = 0; j < matrix1._columnNumber; j++)
                {
                    dynamic value1 = matrix1._data[i][j];
                    dynamic value2 = matrix2._data[i][j];
                    result += value1 * value2;
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string temp;

            for (int i = 0; i < _rowNumber; i++)
            {
                temp = "[";
                for (int j = 0; j < _columnNumber; j++)
                {
                    temp += _data[i][j].ToString();
                    temp += ",";
                }
                temp += "]";
                result.AppendLine(temp);
            }

            return result.ToString();
        }

        public string ToString(Func<T, string> format)
        {
            StringBuilder result = new StringBuilder();
            string temp;

            for (int i = 0; i < _rowNumber; i++)
            {
                temp = "[";
                for (int j = 0; j < _columnNumber; j++)
                {
                    temp += format(_data[i][j]);
                    temp += ",";
                }
                temp += "]";
                result.AppendLine(temp);
            }

            return result.ToString();
        }

        public Matrix<TResult> ConvertTo<TResult>(Func<T, TResult> conversion)
        {
            Matrix<TResult> result = new Matrix<TResult>(this._rowNumber, this._columnNumber);

            for (int i = 0; i < _rowNumber; i++)
            {
                for (int j = 0; j < _columnNumber; j++)
                {
                    result._data[i][j] = conversion(this._data[i][j]);
                }
            }

            return result;
        }

        public Matrix<TResult> Joint<TJoint, TResult>(Matrix<TJoint> joinedMatrix, Func<T, TJoint, TResult> joinedFunction)
        {
            if (this._rowNumber != joinedMatrix._rowNumber)
                throw new ArithmeticException("The two matrices have different sizes.");
            if (this._columnNumber != joinedMatrix._columnNumber)
                throw new ArithmeticException("The two matrices have different sizes.");

            Matrix<TResult> result = new Matrix<TResult>(this._rowNumber, this._columnNumber);

            for (int i = 0; i < _rowNumber; i++)
            {
                for (int j = 0; j < _columnNumber; j++)
                {
                    result._data[i][j] = joinedFunction(this._data[i][j], joinedMatrix._data[i][j]);
                }
            }

            return result;
        }
    }
}
