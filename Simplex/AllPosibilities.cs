namespace FunctionExample
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class AllPossibilities
    {
        public static void RunSimplexInAllBases(SimplexProblem simplexProblem)
        {
            List<List<int>> allBases = GetAllBases(
                simplexProblem.rawProblem.restrictionAmount, simplexProblem.rawProblem.dimension
            );
            bool sucess;
            foreach (var currentBase in allBases)
            {
                sucess = true;
                List<int> currentNonBase = GetNonBaseFromBase(currentBase, simplexProblem.rawProblem.dimension);
                simplexProblem.baseIndex = currentBase;
                simplexProblem.nonBaseIndex = currentNonBase;
                String initialBase = MyConsole.GetListText(currentBase);
                try
                {
                    simplexProblem.CalculateMatrixesFromRaw();
                    simplexProblem.CalculateObjetivesFromRaw();
                    Simplex.Solve(simplexProblem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Falha na Base: " + initialBase);
                    sucess = false;
                }
                if (sucess) {
                    String finalBase = MyConsole.GetListText(currentBase);
                    Console.WriteLine("Sucesso na Base: " + initialBase);
                    Console.WriteLine("Base Final: " + finalBase);
                    return;
                }
            }
        }

        static List<List<int>> GetAllBases(int length, int dimension)
        {
            int x = dimension-1;
            var list = new List<int>();
            for (int i = 0; i <= x; i++)
            {
                list.Add(i);
            }
            return GetCombinations(list, length).Select(e => e.ToList()).ToList();
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(List<T> list, int length) where T : IComparable
        {
            if (length == 0) return new[] { new T[0] };
            if (list.Count == length) return new[] { list.ToArray() };
            return list.SelectMany((value, index) =>
                GetCombinations(list.Skip(index + 1).ToList(), length - 1)
                    .Select(result => new[] { value }.Concat(result)));
        }

        static List<int> GetNonBaseFromBase(List<int> list, int dimension)
        {
            List<int> nonBase = [];
            for (var i = 0; i < dimension; i++)
            {
                if (!list.Contains(i))
                {
                    nonBase.Add(i);
                }
            }
            return nonBase;
        }
    }
}