
namespace FunctionExample
{
    static class MyConsole
    {
        public static void PrintSollution(List<float> finalSolution, float finalValue)
        {
            for(int i = 0; i < finalSolution.Count; i++)
            {
                Console.WriteLine("X" + i + " = " + finalSolution[i]);
            }
            Console.WriteLine("Valor Ótimo da Função Objetivo: " + finalValue);
        }

        public static string GetListText<T>(List<T> list)
        {
            return String.Join(", ", list);
        }
    }
}