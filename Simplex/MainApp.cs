
namespace FunctionExample
{
    class MainApp
    {
        static void Main()
        {
            RawProblem rawProblem = RawDataInput.EnterProblemData();
            SimplexProblem simplexProblem = new (rawProblem);
            Simplex.Solve(simplexProblem);
        }
    }
}