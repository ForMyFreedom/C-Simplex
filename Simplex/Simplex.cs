
namespace FunctionExample
{
    class Simplex
    {
        public static void Solve(SimplexProblem simplexProblem)
        {
            int interaction = 0;
            while (true)
            {
                List<float> basicSoluction = CalculateBasicSoluction(simplexProblem);
                List<float> targetCanonics = CalculateAllCanonics(simplexProblem);
                if (IsOptimalSolution(targetCanonics))
                {
                    FinishWithOptiomal(simplexProblem, basicSoluction);
                    return;
                }
                else
                {
                    int bestCanonicIndex = GetBestCanonicIndex(targetCanonics);
                    List<float> inverseInCanonicCollum = CalculateInverseInCanonicCollum(simplexProblem, bestCanonicIndex);
                    if (ToContinuingSolving(inverseInCanonicCollum))
                    {
                        int toRemoveCollumIndex = CalculateToRemoveCollumIndex(basicSoluction, inverseInCanonicCollum);
                        UpdateSimplexProblem(simplexProblem, bestCanonicIndex, toRemoveCollumIndex);
                    }
                    else
                    {
                        throw new Exception("Problema sem Solução");
                    }
                }
                interaction++;
            }
        }

        private static List<float> CalculateBasicSoluction(SimplexProblem simplexProblem)
        {
            return MyMath.MatrixProduct(
                simplexProblem.inverseBaseMatrix, [simplexProblem.restrictionEquals]
            )[0];
        }

        private static List<float> CalculateLambda(SimplexProblem simplexProblem)
        {
            return MyMath.LinearizateCollumMatrix(
                MyMath.MatrixProduct(
                    MyMath.MatrixTranspose([simplexProblem.baseObjective]), simplexProblem.inverseBaseMatrix
                )
            );
        }

        private static List<float> CalculateAllCanonics(SimplexProblem simplexProblem)
        {
            List<List<float>> lambda = MyMath.MatrixTranspose([CalculateLambda(simplexProblem)]);
            List<float> allCanonics = new (simplexProblem.nonBaseIndex.Count);

            for(var i=0; i< allCanonics.Capacity; i++)
            {
                List<List<float>> unitaryMatrix = MyMath.MatrixProduct(
                    lambda, [simplexProblem.nonBaseMatrix[i]]
                );
                allCanonics.Add(simplexProblem.nonBaseObjective[i] - unitaryMatrix[0][0]);
            }

            return allCanonics;
        }

        private static int GetBestCanonicIndex(List<float> targetCanonics)
        {
            return targetCanonics.IndexOf(targetCanonics.Min());
        }

        private static bool IsOptimalSolution(List<float> targetCanonics)
        {
            for(var i=0; i<targetCanonics.Count; i++)
            {
                if (targetCanonics[i] < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static void FinishWithOptiomal(SimplexProblem simplexProblem, List<float> basicSolution)
        {
            if (IsImpossible(basicSolution))
            {
                throw new Exception("IMPOSSIBLE SOLUTION!");
            }

            List<float> finalSolution = new(simplexProblem.rawProblem.dimension);
            float finalValue = 0;
            for (var i = 0; i < finalSolution.Capacity; i++)
            {
                finalSolution.Add(0);
            }
            for(var i=0; i < basicSolution.Count; i++)
            {
                float value = basicSolution[i];
                finalSolution[simplexProblem.baseIndex[i]] = value;
                finalValue += value*simplexProblem.rawProblem.objetive[simplexProblem.baseIndex[i]];
            }
            MyConsole.PrintSollution(finalSolution, finalValue);
        }

        private static bool ToContinuingSolving(List<float> inverseInCanonicCollum)
        {
            return ! IsLowerThanZero(inverseInCanonicCollum);
        }

        private static List<float> CalculateInverseInCanonicCollum(SimplexProblem simplexProblem, int bestCanonicIndex)
        {
            return MyMath.MatrixProduct(
                simplexProblem.inverseBaseMatrix, [simplexProblem.nonBaseMatrix[bestCanonicIndex]]
            )[0];
        }

        private static int CalculateToRemoveCollumIndex(List<float> basicSoluction, List<float> inverseInCanonicCollum)
        {
            List<float> list = new(inverseInCanonicCollum.Count);
            for(var i=0; i < list.Capacity; i++)
            {
                float value;
                if (inverseInCanonicCollum[i] > 0)
                {
                    value = basicSoluction[i]/inverseInCanonicCollum[i];
                } else
                {
                    value = float.PositiveInfinity;
                }
                list.Add(value);
            }
            return list.IndexOf(list.Min());
        }

        private static bool IsLowerThanZero(List<float> list)
        {
            foreach(var item in list)
            {
                if(item > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsImpossible(List<float> list)
        {
            foreach (var item in list)
            {
                if (item < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static void UpdateSimplexProblem(SimplexProblem simplexProblem, int toAddCollumIndex, int toRemoveCollumIndex)
        {
            (simplexProblem.nonBaseIndex[toAddCollumIndex], simplexProblem.baseIndex[toRemoveCollumIndex]) = (simplexProblem.baseIndex[toRemoveCollumIndex], simplexProblem.nonBaseIndex[toAddCollumIndex]);
            simplexProblem.CalculateMatrixesFromRaw();
            simplexProblem.CalculateObjetivesFromRaw();
        }
    }
}