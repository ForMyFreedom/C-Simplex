
namespace FunctionExample
{
    static class MyMath
    {
        public static List<List<float>> MatrixProduct(List<List<float>> matrix1, List<List<float>> matrix2)
        {
            int dimensionColumn = matrix1[0].Count;
            int dimensionLine = matrix2.Count;
            int dimensionIntersect = matrix2[0].Count;
            List<List<float>> matrixProduct = [];

            if (matrix1.Count != dimensionIntersect)
            {
                throw new Exception("Multiplicação Inválida");
            }

            for (int i = 0; i < dimensionLine; i++)
            {
                matrixProduct.Add([]);
            }

            for (int i = 0; i < dimensionLine; i++)
            {
                for (int j = 0; j < dimensionColumn; j++)
                {
                    matrixProduct[i].Add(0);
                }
            }

            for (int i = 0; i < dimensionLine; i++)
            {
                for (int j = 0; j < dimensionColumn; j++)
                {
                    for (int k = 0; k < dimensionIntersect; k++)
                    {
                        matrixProduct[i][j] = matrixProduct[i][j] + matrix2[i][k] * matrix1[k][j];
                    }
                }
            }

            return matrixProduct;
        }

        public static List<List<float>> CalculateInverse(List<List<float>> matrix)
        {
            int dimension = matrix[0].Count;
            float determinant = CalculateDeterminant(matrix);

            if (determinant == 0)
            {
                throw new Exception("Não é possível inverter a matriz");
            }
            else
            {
                List<List<float>> inverseMatrix = CalculateAdjunt(matrix);

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        inverseMatrix[i][j] = inverseMatrix[i][j] / determinant;
                    }
                }

                return inverseMatrix;
            }
        }

        public static List<List<float>> MatrixTranspose(List<List<float>> matrix)
        {
            int n = matrix[0].Count;
            int m = matrix.Count;

            List<List<float>> transpostMatrix = [];

            MatrixInicializate(transpostMatrix, m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transpostMatrix[j][i] = matrix[i][j];
                }
            }

            return transpostMatrix;
        }

        public static List<List<float>> CalculateAdjunt(List<List<float>> matrix)
        {
            int dimension = matrix[0].Count;
            List<List<float>> adjuntMatrix = [];
            MatrixInicializate(adjuntMatrix, dimension);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    List<List<float>> auxiliarMatrix = matrix.Select(x => x.ToList()).ToList();

                    auxiliarMatrix.RemoveAt(i);
                    for (int k = 0; k < dimension - 1; k++)
                    {
                        auxiliarMatrix[k].RemoveAt(j);
                    }

                    adjuntMatrix[i][j] = (float) Math.Pow(-1, i + j) * CalculateDeterminant(auxiliarMatrix);
                }
            }

            return MatrixTranspose(adjuntMatrix);
        }

        public static float CalculateDeterminant(List<List<float>> matrix)
        {
            float determinant = 0;
            int dimension = matrix[0].Count;

            if (dimension < 1)
            {
                throw new Exception("Matriz Vazia");
            }
            else if (dimension < 2)
            {
                determinant = matrix[0][0];
            }
            else if (dimension < 3)
            {
                determinant = (matrix[0][0] * matrix[1][1]) - (matrix[0][1] * matrix[1][0]);
            }
            else
            {
                for (int i = 0; i < dimension; i++)
                {

                    List<List<float>> auxiliarMatrix = matrix.Select(x => x.ToList()).ToList();

                    auxiliarMatrix.RemoveAt(0);
                    for (int k = 0; k < dimension - 1; k++)
                    {
                        auxiliarMatrix[k].RemoveAt(i);
                    }

                    determinant += matrix[0][i] * (float)Math.Pow(-1, i) * CalculateDeterminant(auxiliarMatrix);
                }
            }

            return determinant;
        }

        public static void MatrixInicializate(List<List<float>> matrix, int dimension)
        {
            MatrixInicializate(matrix, dimension, dimension);
        }

        public static void MatrixInicializate(List<List<float>> matrix, int n, int m)
        {

            for (int i = 0; i < m; i++)
            {
                matrix.Add([]);
                for (int j = 0; j < n; j++)
                {
                    matrix[i].Add(0);
                }
            }
        }

        public static List<float> LinearizateCollumMatrix(List<List<float>> collumMatrix)
        {
            if (collumMatrix[0].Count > 1)
            {
                throw new Exception("Não é uma matrix coluna");
            }

            List<float> response = [];
            for(int i = 0; i < collumMatrix.Count; i++)
            {
                response.Add(collumMatrix[i][0]);
            }
            return response;
        }
    }
}