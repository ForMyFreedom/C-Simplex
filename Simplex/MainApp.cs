
namespace FunctionExample
{
    class MainApp
    {
        /*
         * To Do:
           There is some obnoxius 'protect set' in SimplexProblem
        */

        static void Main()
        {
            RawProblem rawProblem = RawDataInput.EnterProblemData();
            SimplexProblem simplexProblem = new (rawProblem);
            Simplex.Solve(simplexProblem);
        }
    }
}