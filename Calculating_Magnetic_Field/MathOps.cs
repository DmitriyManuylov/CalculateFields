using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers.Preconditioners;

namespace Calculating_Magnetic_Field
{
    public static class MathOps
    {
        

        public static double[,] MultMatrices(double[,] leftMatrix, double[,] rightMatrix)
        {
            int n = leftMatrix.GetLength(0);
            double[,] result = new double[n, n];

            if (leftMatrix.GetLength(1) != rightMatrix.GetLength(0))
            {
                throw new ArgumentOutOfRangeException("Несовпадение размерностей");
            }
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    double sum = 0;
                    
                    for (int k = 0; k < n; k++)
                    {
                        sum += leftMatrix[i, k] * rightMatrix[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] Matrix)
        {
            int n = Matrix.GetLength(0);
            double[,] result = new double[n, n];


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = Matrix[j, i];
                }
            }
            return result;
        }

        public static void MultMatrixOnNumber(double[,] Matrix, double mult)
        {
            int n = Matrix.GetLength(0);
            double[,] result = new double[n, n];


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matrix[j, i] *= mult;
                }
            }
        }

        public static double[] MultMatrixOnVector(double[,] matrix, double[] vector)
        {
            int n = matrix.GetLength(0);
            double[] result = new double[n];

            if (matrix.GetLength(1) != vector.GetLength(0))
            {
                throw new ArgumentOutOfRangeException("Несовпадение размерностей");
            }

            for (int i = 0; i < n; i++)
            {
                    double sum = 0;

                    for (int k = 0; k < n; k++)
                    {
                        sum += matrix[i, k] * vector[k];
                    }
                    result[i] = sum;
            }
            return result;
        }

        public static double[,] SumMatrices(double[,] matr1, double[,] matr2)
        {
            int n = matr1.GetLength(0);
            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = matr1[i, j] + matr2[i, j];
                }
            }

            return result;
        }


        /// <summary>
        /// Решение СЛАУ
        /// </summary>
        /// <param name="matrix">Матрица коэффициентов</param>
        /// <param name="freeF">Столбец  свободных членов</param>
        /// <param name="n">Размерность</param>
        /// <returns></returns>
        public static void SolveEq(double[,] matrix, double[] freeF, out double[] result, int n)
        {
            double[,] matr = (double[,])matrix.Clone();
            result = new double[freeF.Length];
            int count, i, j, i_2, j_2;
            double elem;
            double[] vector = new double[n];
            double changingEl_F;

            for (count = n - 1; count >= 0; count--)
            {
                // перестановка строк в случае,
                // если элемент matrix[count,count]=0
                if (matr[count, count] == 0)
                {
                    i_2 = count;
                    while (matr[i_2, count] == 0)
                    {
                        i_2 -= 1;
                    }
                    changingEl_F = freeF[i_2];
                    freeF[i_2] = freeF[count];
                    freeF[count] = changingEl_F;
                    for (j_2 = 0; j_2 <= count; j_2++)
                    {
                        vector[j_2] = matr[count, j_2];
                        matr[count, j_2] = matr[i_2, j_2];
                        matr[i_2, j_2] = vector[j_2];
                    }
                }

                //фиксирование элемента matrix[count,count]
                elem = matr[count, count];
                //деление строки count на зафиксированный элемент
                freeF[count] /= elem;
                for (i = 0; i <= n - 1; i++)
                {
                    matr[count, i] = matr[count, i] / elem;
                }

                for (i = 0; i <= count - 1; i++)
                {
                    //Фиксирование элемента i-й строки(выше строки count) столбца count
                    elem = matr[i, count];
                    freeF[i] = freeF[i] - freeF[count] * elem;
                    //Вычитание строки count из верхних строк
                    for (j = 0; j <= n - 1; j++)
                    {
                        matr[i, j] = matr[i, j] - matr[count, j] * elem;
                    }
                }
            }
            for (count = 0; count <= n - 1; count++)
            {
                for (i = count + 1; i <= n - 1; i++)
                {
                    freeF[i] -= freeF[count] * matr[i, count];
                    matr[i, count] = 0;
                }
            }
            result = freeF;
        }



        public static Vector<double> SolveWithTichonovRegularization(double alpha, double[,] matrix, double[] freeMem)
        {
            int n = matrix.GetLength(0);
            double[,] transposedMatrix = TransposeMatrix(matrix);
            double[,] moddedMatr = MultMatrices(transposedMatrix, matrix);
            for (int i = 0; i < n; i++)
            {
                moddedMatr[i, i] += alpha;
            }
            double[] rightPart = MultMatrixOnVector(transposedMatrix, freeMem);
            Matrix<double> matrix1 = Matrix.Create(moddedMatr);
            Vector<double> vector = Vector.Create(rightPart);
            BiConjugateGradientSolver<double> BiCgStabSolve = new BiConjugateGradientSolver<double>(matrix1);
            BiCgStabSolve.Preconditioner = new IdentityPreconditioner<double>(matrix1);
            return BiCgStabSolve.Solve(vector);
        }
    }
}
