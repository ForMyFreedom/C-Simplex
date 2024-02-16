namespace FunctionExample
{
    class MainApp
    {
        static void Main()
        {
            RawProblem rawProblem = RawDataInput.EnterProblemData();
            SimplexProblem simplexProblem = new(rawProblem);
            AllPossibilities.RunSimplexInAllBases(simplexProblem);
        }
    }
}