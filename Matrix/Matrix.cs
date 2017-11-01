using System;

namespace Matrix {
    public class Matrix {
        /// <summary>
        /// Stores the matrix elements.
        /// </summary>
        double[,] matrix;

        /// <summary>
        /// Creates a new matrix with the specified number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows in the matrix.</param>
        /// <param name="cols">The number of columns in matrix.</param>
        public Matrix(int rows, int columns) {
            matrix = new double[rows, columns];
        }
        /// <summary>
        /// Creates a matrix based a 2D double array.
        /// </summary>
        /// <param name="sourceMatrix">A 2D double array</param>
        public Matrix(double[,] sourceMatrix) {
            matrix = new double[sourceMatrix.GetUpperBound(0) + 1, sourceMatrix.GetUpperBound(1) + 1];
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    this[r, c] = sourceMatrix[r, c];
                }
            }
        }
        /// <summary>
        /// Creates a matrix from a 2D boolean array. True values are converted to 1, false are converted to -1
        /// </summary>
        /// <param name="sourceMatrix">A 2D boolean array.</param>
        public Matrix(bool[,] sourceMatrix) {
            matrix = new double[sourceMatrix.GetUpperBound(0) + 1, sourceMatrix.GetUpperBound(1) + 1];
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    if (sourceMatrix[r, c]) {
                        this[r, c] = 1;
                    }
                    else {
                        this[r, c] = -1;
                    }
                }
            }
        }
        /// <summary>
        /// Creates a single row matrix.
        /// </summary>
        /// <param name="input">An array of double values.</param>
        /// <returns>A single row matrix.</returns>
        public static Matrix CreateRowMatrix(double[] input) {
            double[,] temp = new double[1, input.Length];
            for (int col = 0; col < input.Length; col++) {
                temp[0, col] = input[col];
            }
            return new Matrix(temp);
        }
        /// <summary>
        /// Creates a single column matrix.
        /// </summary>
        /// <param name="input">An array of double values.</param>
        /// <returns>A single column matrix.</returns>
        public static Matrix CreateColumnMatrix(double[] input) {
            double[,] temp = new double[input.Length, 1];
            for (int row = 0; row < input.Length; row++) {
                temp[row, 0] = input[row];
            }
            return new Matrix(temp);
        }
        /// <summary>
        /// Clear the matrix. 
        /// </summary>
        public void Clear() {
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    this[r, c] = 0;
                }
            }
        }
        /// <summary>
        /// Take the values of the matrix from a packed array.
        /// </summary>
        /// <param name="array">The array to read from.</param>
        /// <param name="index">The index to begin reading at.</param>
        /// <returns>The new index ater this matrix has been read.</returns>
        public int FromPackedArray(double[] array, int index) {
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    matrix[r, c] = array[index++];
                }
            }
            return index;
        }
        /// <summary>
        /// Converts the matrix to a packed array.
        /// </summary>
        /// <returns>A packed array.</returns>
        public double[] ToPackedArray() {
            double[] result = new double[Rows * Cols];
            int index = 0;
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    result[index++] = matrix[r, c];
                }
            }
            return result;
        }
        /// <summary>
        /// Get one row of the matrix to a row matrix.
        /// </summary>
        /// <param name="row">The index of desired row.</param>
        /// <returns>The row matrix.</returns>
        public Matrix GetRow(int row) {
            if (row > Rows) {
                throw new MatrixException(string.Format("Can't get row '{0}' because it doesn't exist.", row));
            }
            double[,] newMatrix = new double[1, Cols];
            for (int col = 0; col < Cols; col++) {
                newMatrix[0, col] = matrix[row, col];
            }
            return new Matrix(newMatrix);
        }
        /// <summary>
        /// Get one column of the matrix to a column matrix.
        /// </summary>
        /// <param name="col">The index of desired column.</param>
        /// <returns>The column matrix.</returns>
        public Matrix GetCol(int col) {
            if (col > Cols) {
                throw new MatrixException(string.Format("Can't get column '{0}' because it doesn't exist.", col));
            }
            double[,] newMatrix = new double[Rows, 1];
            for (int row = 0; row < Rows; row++) {
                newMatrix[row, 0] = matrix[row, col];
            }
            return new Matrix(newMatrix);
        }
        /// <summary>
        /// Verifies if the matrix is a vector. A vector matrix has only a single row or column.
        /// </summary>
        /// <returns>True if the matrix is a vector, false otherwise.</returns>
        public bool IsVector() {
            if (Rows == 1) {
                return true;
            }
            else {
                return Cols == 1;
            }
        }
        /// <summary>
        /// Determines if all elements in the matrix are zero.
        /// </summary>
        /// <returns>True if all elements in the matrix are zero, false otherwise.</returns>
        public bool IsZero() {
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    if (this[r, c] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Fill the matrix with random numbers in the specified range.
        /// </summary>
        /// <param name="min">The minimum value for the random numbers.</param>
        /// <param name="max">The maximum values for the random numbers.</param>
        public void Randomize(double min, double max) {
            Random rand = new Random();
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    this[r, c] = (rand.NextDouble() * (max - min)) + min;
                }
            }
        }
        /// <summary>
        /// Clones the matrix.
        /// </summary>
        /// <returns>A cloned copy of the matrix.</returns>
        public Matrix Clone() {
            return new Matrix(matrix);
        }
        public bool Equals(Matrix matrix) {
            return equals(matrix, 10);
        }
        /// <summary>
        /// Compare the matrix to another with the specified level of precision.
        /// </summary>
        /// <param name="matrix">The other matrix to compare.</param>
        /// <param name="precision">The number of decimal places of precision to use.</param>
        /// <returns>Tre if the matrices are equal, false otherwise.</returns>
        private bool equals(Matrix matrix, int precision) {
            if (precision < 0) {
                throw new MatrixException("Precision can't be a negative number.");
            }
            double test = Math.Pow(10.0, precision);
            if (double.IsInfinity(test) || (double.IsNaN(test))) {
                throw new MatrixException(string.Format("Precision of {0} decimal places is not supported.", precision));
            }
            precision = (int)Math.Pow(10, precision);
            for (int r = 0; r < Rows; r++) {
                for (int c = 0; c < Cols; c++) {
                    if ((long)(this[r, c] * precision) != (long)(matrix[r, c] * precision)) {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Determines if the matrix is inversible.
        /// </summary>
        /// <returns></returns>
        public bool IsInversible() {
            ///Matrix need to be square to be inversible.
            if (Rows != Cols) {
                return false;
            }
            
            //Implementar resto do código...
            return true;
        }
        /// <summary>
        /// Determines if the matrix has the same number of rows and columns.
        /// </summary>
        /// <returns>True if the number of rows an columns in the matrix are the same. False oherwise.</returns>
        public bool IsSquare() {
            return Rows == Cols;
        }

        /// <summary>
        /// Validate that the specified row and column are inside of the range of the matrix.
        /// </summary>
        /// <param name="row">The index of row to validate.</param>
        /// <param name="col">The index of column to validate.</param>
        private void Validate(int row, int col) {
            if ((row > Rows) || (row < 0)) {
                throw new MatrixException(string.Format("The row {0} is out of range: {1}", row, Rows));
            }
            if ((col > Cols) || (col < 0)) {
                throw new MatrixException(string.Format("The column {0} is out of range: {1}", col, Cols));
            }
        }
        /// <summary>
        /// Allows index access to the elements of the matrix.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <param name="col">The column to access.</param>
        /// <returns>The element at the specified position of the matrix.</returns>
        public double this[int row, int col] {
            get {
                Validate(row, col);
                return matrix[row, col];
            }
            set {
                Validate(row, col);
                if (double.IsInfinity(value) || double.IsNaN(value)) {
                    throw new MatrixException(string.Format("Trying to assign invalid number to matrix: {0}.", value));
                }
                matrix[row, col] = value;
            }
        }
        /// <summary>
        /// The number of rows in the matrix.
        /// </summary>
        public int Rows {
            get { return matrix.GetUpperBound(0) + 1; }
        }
        /// <summary>
        /// The number of columns in the matrix.
        /// </summary>
        public int Cols {
            get { return matrix.GetUpperBound(1) + 1; }
        }
        /// <summary>
        /// The number of elements in the matrix.
        /// </summary>
        public int Size {
            get { return Rows * Cols; }
        }
    }

    /// <summary>
    /// Exception thrown when a matrix error occurs
    /// </summary>
    public class MatrixException : System.Exception {
        /// <summary>
        /// Constructor for a simple message exception.
        /// </summary>
        /// <param name="str">The error message.</param>
        public MatrixException(string str) : base(str) {
        }
    }
}
