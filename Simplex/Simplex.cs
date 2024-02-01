
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
                        Console.WriteLine("Problema sem Solução");
                        return;
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
            return MyMath.MatrixProduct(
                [simplexProblem.baseObjective], simplexProblem.inverseBaseMatrix
            )[0];
        }

        private static List<float> CalculateAllCanonics(SimplexProblem simplexProblem)
        {
            List<float> lambda = CalculateLambda(simplexProblem);
            List<float> allCanonics = new (simplexProblem.nonBaseIndex.Count);

            for(var i=0; i< allCanonics.Count; i++)
            {
                List<List<float>> unitaryMatrix = MyMath.MatrixProduct(
                    [lambda], [simplexProblem.nonBaseMatrix[i]]
                );
                allCanonics[i] = simplexProblem.nonBaseObjective[i] - unitaryMatrix[0][0];
            }

            return allCanonics;
        }

        private static int GetBestCanonicIndex(List<float> targetCanonics)
        {
            List<float> list = new(targetCanonics);
            return list.IndexOf(list.Max());
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
            List<float> finalSolution = new(simplexProblem.rawProblem.dimension);
            float finalValue = 0;
            for (var i = 0; i < finalSolution.Count; i++)
            {
                finalSolution[i] = 0;
            }
            foreach(var value in basicSolution)
            {
                int index = basicSolution.IndexOf(value);
                finalSolution[simplexProblem.baseIndex[index]] = value;
                finalValue += value*simplexProblem.rawProblem.objetive[index];
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
            for(var i=0; i < list.Count; i++)
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

        private static void UpdateSimplexProblem(SimplexProblem simplexProblem, int toAddCollumIndex, int toRemoveCollumIndex)
        {
            (simplexProblem.nonBaseIndex[toAddCollumIndex], simplexProblem.baseIndex[toRemoveCollumIndex]) = (simplexProblem.baseIndex[toRemoveCollumIndex], simplexProblem.nonBaseIndex[toAddCollumIndex]);
            simplexProblem.CalculateMatrixesFromRaw();
            simplexProblem.CalculateObjetivesFromRaw();
        }
    }
}