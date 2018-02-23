using System;

namespace MatrixOperations
{
    /// <summary>
    /// The Matrix class.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// The matrix.
        /// </summary>
        public readonly double[,] matrix;

        /// <summary>
        /// Count of rows in the matrix.
        /// </summary>
        public readonly int rows;

        /// <summary>
        /// Count of columns in the matrix.
        /// </summary>
        public readonly int cols;

        /// <summary>
        /// Indexer property for Matrix type objects.
        /// </summary>
        /// <param name="i">Index of row.</param>
        /// <param name="j">Index of column.</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                if (i < matrix.GetLength(0) && j < matrix.GetLength(1))
                    return matrix[i, j];
                else
                    throw new IndexOutOfRangeException("Index is out of range.");
            }
            set
            {
                if (i < matrix.GetLength(0) && j < matrix.GetLength(1))
                    matrix[i, j] = value;
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
        }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Matrix() { }

        /// <summary>
        /// Constructor to create Matrix with givan array.
        /// </summary>
        /// <param name="mat"></param>
        public Matrix(double[,] mat)
        {
            this.rows = mat.GetLength(0);
            this.cols = mat.GetLength(1);
            matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = mat[i, j];
                }
        }

        /// <summary>
        /// Constructor to create matrix with givan rows and columns.
        /// </summary>
        /// <param name="r">Count of rows.</param>
        /// <param name="c">Count of columns.</param>
        public Matrix(int r, int c)
        {
            if (r >= 0 && c >= 0)
            {
                matrix = new double[r, c];
                this.rows = r;
                this.cols = c;
            }
            else
                throw new ArgumentOutOfRangeException("The sizes of columns and rows can't be negative.");
        }

        /// <summary>
        /// Copy constructor for Matrix.
        /// </summary>
        /// <param name="original">Original Matrix.</param>
        public Matrix(Matrix original)
        {
            if (original != null)
            {
                matrix = new double[original.rows, original.cols];
                this.rows = original.rows;
                this.cols = original.cols;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = original[i, j];
                    }
                }
            }
        }

        /// <summary>
        /// Overloading + operator to sum two matrices.
        /// </summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns></returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows != matrix2.rows || matrix1.cols != matrix2.cols)
            {
                throw new Exception("The sizes of matrices rows or columns are different.");
            }
            Matrix sum = new Matrix(matrix1.rows, matrix1.cols);
            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.cols; j++)
                {
                    sum[i, j] = matrix1.matrix[i, j] + matrix2.matrix[i, j];
                }
            }
            return sum;
        }

        /// <summary>
        /// Overloading * operator to multiply two matrices.
        /// </summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.cols != matrix2.rows)
            {
                Console.WriteLine("We can't multiply  these matrices because the first's count of columns differ from the second's count of rows.");
                return null;
            }
            double[,] mul = new double[matrix1.rows, matrix2.cols];
            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix2.cols; j++)
                {
                    for (int k = 0; k < matrix1.cols; k++)
                    {
                        mul[i, j] += matrix1.matrix[i, k] * matrix2.matrix[k, j];
                    }
                }
            }
            return new Matrix(mul);
        }

        /// <summary>
        /// Overloading * operator for scalar multiplication.
        /// </summary>
        /// <param name="number">Number with which will be multiplied matrix.</param>
        /// <param name="matrix1">Matrix</param>
        /// <returns></returns>
        public static Matrix operator *(double number, Matrix matrix1)
        {
            Matrix scalar = new Matrix(matrix1.rows, matrix1.cols);
            if (matrix1 is null)
            {
                return null;
            }
            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.cols; j++)
                {
                    scalar[i, j] = matrix1[i, j] * number;
                }
            }
            return scalar;
        }

        /// <summary>
        /// Method to calculate the inverse matrix of the given matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix Inverse()
        {
            if (this.cols != this.rows)
            {
                throw new Exception("This Matrix cann't have Invers.");
            }
            else
            {
                double elem;
                Matrix inverse = new Matrix(rows, cols);
                for (int i = 0; i < rows; i++)
                {
                    inverse[i, i] = 1.0;
                }
                Matrix copy = new Matrix(matrix);
                for (int k = 0; k < rows; k++)
                {
                    elem = copy[k, k];
                    for (int j = 0; j < cols; j++)
                    {
                        copy[k, j] /= elem;
                        inverse[k, j] /= elem;
                    }
                    for (int i = 0; i < rows; i++)
                    {
                        elem = copy[i, k];
                        for (int j = 0; j < cols; j++)
                        {
                            if (i == k)
                            {
                                continue;
                            }
                            else
                            {
                                copy[i, j] -= elem * copy[k, j];
                                inverse[i, j] -= elem * inverse[k, j];
                            }
                        }
                    }
                }
                return inverse;
            }
        }

        /// <summary>
        /// Method to find the transpose matrix of the given matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix trans = new Matrix(cols, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    trans[j, i] = matrix[i, j];
                }
            }
            return trans;
        }

        /// <summary>
        /// Method to test  if matrix is ortogonal or not.
        /// </summary>
        /// <returns></returns>
        public bool Ortogonal()
        {
            Matrix trans = new Matrix(this.Transpose());
            Matrix mul = this * trans;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == j)
                    {
                        if (mul[i, j] != 1)
                            return false;
                    }
                    else
                        if (mul[i, j] != 0)
                        return false;


                }
            }
            return true;
        }

        /// <summary>
        /// Method to transform matrix by translating.
        /// </summary>
        /// <param name="distance">Translation distance.</param>
        /// <returns></returns>
        public Matrix Translation(double[] distance)
        {
            Matrix transform;
            if (distance.Length != this.rows)
            {
                throw new Exception("Count of numbers and rows are different.");
            }
            else
            {
                transform = new Matrix(matrix);
                for (int i = 0; i < distance.Length; i++)
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        transform[i, j] += distance[i];
                    }
                }
            }
            return transform;
        }

        /// <summary>
        /// Method to transformate matrix by rotation in 3D.
        /// </summary>
        /// <param name="axis">The axis the shape will be rotated around.</param>
        /// <param name="angleDegree">The number of degrees the shape will be rotated through.</param>
        /// <returns></returns>
        public Matrix Rotate3D(string axis, double angleDegree)
        {

            Matrix rotate;
            double angle = Math.PI * angleDegree / 180.0;
            if (rows != 3)
            {
                throw new Exception("The matrix isn't 3D .");
            }
            else
            {
                if (axis == "X" || axis == "x")
                {
                    double[,] transMatrix = { { 1, 0, 0 }, { 0, Math.Cos(angle), Math.Sin(angle) }, { 0, Math.Sin(-angle), Math.Cos(angle) }, };
                    rotate = new Matrix(transMatrix);
                }
                else if (axis == "Y" || axis == "y")
                {
                    double[,] transMatrix = { { Math.Cos(angle), 0, Math.Sin(-angle) }, { 0, 1, 0 }, { Math.Sin(angle), 0, Math.Cos(angle) }, };
                    rotate = new Matrix(transMatrix);
                }
                else if (axis == "Z" || axis == "z")
                {
                    double[,] transMatrix = { { Math.Cos(angle), Math.Sin(angle), 0 }, { Math.Sin(-angle), Math.Cos(angle), 0 }, { 0, 0, 1 }, };
                    rotate = new Matrix(transMatrix);
                }
                else
                {
                    throw new Exception("The axis must be X,Y or Z");
                }

            }
            return rotate *= this;
        }

        /// <summary>
        /// Method to transformate matrix by scaling.
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <returns></returns>
        public Matrix Scale(double[] factor)
        {
            Matrix scale = new Matrix(rows, rows);
            Matrix column = new Matrix(rows, 1);
            Matrix scaleMatrix = new Matrix(this);
            Matrix mul = new Matrix(rows, 1);
            if (factor.Length > cols)
            {
                throw new Exception("There is more elements than column's length.");
            }
            else
            {
                for (int j = 0; j < factor.Length; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        column[i,0]=this[i,j];
                        scale[i, i] = factor[j];
                    }
                    mul=scale * column;
                    for(int k = 0; k < rows; k++)
                    {
                        scaleMatrix[k, j] = mul[k, 0];
                    }
                }
            }
            return scaleMatrix;


        }

        /// <summary>
        /// Method to find the largest element in matrix.
        /// </summary>
        /// <returns></returns>
        public  double LargestElement()
        {
                double max;
            if (this != null)
            {
                max = matrix[0, 0];
                for(int i = 0; i < rows; i++)
                {
                    for(int j = 0; j < cols; j++)
                    {
                        if (max < matrix[i, j])
                            max = matrix[i, j];
                    }
                }
                return max;
            }
            else
            {
                throw new Exception("Matrix is empty.");
            }
        }

        /// <summary>
        /// Method to find the smallest element in matrix.
        /// </summary>
        /// <returns></returns>
        public double SmallestElement()
        {
            if (this != null)
            {
                double min = matrix[0, 0];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (min > matrix[i, j])
                            min = matrix[i, j];
                    }
                }
                return min;
            }
            else
            {
                throw new Exception("Matrix is empty.");
            }
        }
        
        /// <summary>
        /// Method to print elements of matrix.
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(Math.Round(matrix[i, j], 2) + "      ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
