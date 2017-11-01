namespace Matrix {
    public class MatrixMath {
        /// <summary>
        /// Add two matrices.
        /// </summary>
        /// <param name="a">The first matrix to add.</param>
        /// <param name="b">The second Matrix to add.</param>
        /// <returns>A matrix with the added values.</returns>
        public static Matrix Add(Matrix a, Matrix b) {
            if (a.Rows != b.Rows) {
                throw new MatrixException(string.Format("The matrices must have the same number of rows and columns.\nMatrix A has {0} rows and matrix B has {1} rows.", a.Rows, b.Rows));
            }
            if (a.Cols != b.Cols) {
                throw new MatrixException(string.Format("The matrices must have the same number of rows and columns.\nMatrix A has {0} columns and matrix B has {1} columns.", a.Cols, b.Cols));
            }
            double[,] result = new double[a.Rows, a.Cols];
            for (int r = 0; r < a.Rows; r++) {
                for (int c = 0; c < a.Cols; c++) {
                    result[r, c] = a[r, c] + b[r, c];
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The matrix to subtract from.</param>
        /// <param name="b">The matrix that will be subtracted from the first matrix.</param>
        /// <returns>A matrix with the subtraction result.</returns>
        public static Matrix Subtract(Matrix a, Matrix b) {
            if (a.Rows != b.Rows) {
                throw new MatrixException(string.Format("The matrices must have the same number of rows and columns.\nMatrix A has {0} rows and matrix B has {1} rows.", a.Rows, b.Rows));
            }
            if (a.Cols != b.Cols) {
                throw new MatrixException(string.Format("The matrices must have the same number of rows and columns.\nMatrix A has {0} columns and matrix B has {1} columns.", a.Cols, b.Cols));
            }
            double[,] result = new double[a.Rows, a.Cols];
            for (int r = 0; r < a.Rows; r++) {
                for (int c = 0; c < a.Cols; c++) {
                    result[r, c] = a[r, c] - b[r, c];
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// Multiply all elements of a matrix by the specified value.
        /// </summary>
        /// <param name="matrix">The matrix to multiply.</param>
        /// <param name="value">Tyhe value to multiply matrix elements for.</param>
        /// <returns>A matrix with the multiplication result.</returns>
        public static Matrix Multiply(Matrix matrix, double value) {
            double[,] result = new double[matrix.Rows, matrix.Cols];
            for (int r = 0; r < matrix.Rows; r++) {
                for (int c = 0; c < matrix.Cols; c++) {
                    result[r, c] = matrix[r, c] * value;
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// Multiply two matrices.
        /// </summary>
        /// <param name="a">The first matrix. The number of columns must match the number of rows in de second matrix.</param>
        /// <param name="b">The second matrix. The number of rows must match the number of columns in the fist matrix.</param>
        /// <returns>A matrix with the multiplication result.</returns>
        public static Matrix Multiply(Matrix a, Matrix b) {
            if (a.Cols != b.Rows) {
                throw new MatrixException("To multiply two matrices, the number of columns in the first matrix must match the number of rows in the second.");
            }
            double[,] result = new double[a.Rows, b.Cols];
            for (int r = 0; r < a.Rows; r++) {
                for (int c = 0; c < b.Cols; c++) {
                    double value = 0;
                    for (int i = 0; i < a.Cols; i++) {
                        value += a[r, i] * b[i, c];
                    }
                    result[r, c] = value;
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// Divide each element of a matrix by a specified value.
        /// </summary>
        /// <param name="matrix">The matrix to be divided.</param>
        /// <param name="value">The value to divide matrix.</param>
        /// <returns>A matrix with the division result.</returns>
        public static Matrix Divide(Matrix matrix, double value) {
            double[,] result = new double[matrix.Rows, matrix.Cols];
            for (int r = 0; r < matrix.Rows; r++) {
                for (int c = 0; c < matrix.Cols; c++) {
                    result[r, c] = matrix[r, c] / value;
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// Create an identity matrix with the specified size.
        /// </summary>
        /// <param name="size">The size of the identity matrix.</param>
        /// <returns>An identity matrix with the specified size.</returns>
        public static Matrix Identity(int size) {
            if (size < 1) {
                throw new MatrixException("Size of identity matrix must be at least 1.");
            }
            Matrix result = new Matrix(size, size);
            for (int i = 0; i < size; i++) {
                result[i, i] = 1;
            }
            return result;
        }
        /// <summary>
        /// Calculates the scalar (dot product) of two matrices.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix</param>
        /// <returns>The scalar product of the matrices.</returns>
        public static double Scalar(Matrix a, Matrix b) {
            if (!a.IsVector() || !b.IsVector()) {
                throw new MatrixException("To take scalar product, both matrices must be vectors.");
            }
            double[] arrayA = a.ToPackedArray();
            double[] arrayB = b.ToPackedArray();
            if (arrayA.Length != arrayB.Length) {
                throw new MatrixException("To take scalar product, both matrices must have the same lenght.");
            }
            double result = 0;
            for (int i = 0; i < arrayA.Length; i++) {
                result += arrayA[i] * arrayB[i];
            }
            return result;
        }

        /*
        /// <summary>
        /// Returns the inverse matrix of the input matrix.
        /// </summary>
        /// <param name="input">The input matrix.</param>
        /// <returns>The inverse matrix of the input matrix.</returns>
        public Matrix Inverse(Matrix input) {
            if (!input.IsInversible()) {
                throw new MatrixException("Input matrix is not inversible.");
            }
            double[,] output = new double[input.Rows, input.Cols];
            //IMPLEMETAR CÁLCULOS

            return new Matrix(output);
        } */
        
    }
}
