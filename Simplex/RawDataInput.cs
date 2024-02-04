
namespace FunctionExample
{
    static class RawDataInput
    {
        public static RawProblem EnterProblemData()
        {
            int dimension = EnterProblemDimension();
            float[] objetive = EnterObjetiveData(dimension);
            int restrictionAmount = EnterRestrictionAmount();
            float[][] restrictionLeft = EnterLeftRestricionsData(dimension, restrictionAmount);
            float[] restrictionRight = EnterRightRestricionsData(restrictionAmount);
            return new RawProblem(dimension, objetive, restrictionAmount, restrictionLeft, restrictionRight);
        }

        private static int EnterProblemDimension()
        {
            Console.Write("Insira a Dimensão do Problema: ");
            int dimension = int.Parse(Console.ReadLine() ?? "");
            return dimension;
        }

        private static float[] EnterObjetiveData(int dimension)
        {
            float[] objetive = new float[dimension];
            for (int i = 0; i < dimension; i++)
            {
                Console.Write("Insira o valor de x" + i + " na Função Objetivo: ");
                float value = float.Parse(Console.ReadLine() ?? "");
                objetive[i] = value;
            }
            return objetive;
        }

        private static int EnterRestrictionAmount()
        {
            Console.Write("Insira a Quantidade de Restrições: ");
            int amount = int.Parse(Console.ReadLine() ?? "");
            return amount;
        }

        private static float[][] EnterLeftRestricionsData(int dimension, int restrictionAmount)
        {
            float[][] restrictions = new float[restrictionAmount][];
            for (int i = 0; i < restrictionAmount; i++)
            {
                restrictions[i] = new float[dimension];
                Console.WriteLine("####### Linha " + i + " #######");
                for (int j = 0; j < dimension; j++)
                {
                    Console.Write("Insira o valor de x" + j + " na Linha " + i + " das Restrições: ");
                    float value = float.Parse(Console.ReadLine() ?? "");
                    restrictions[i][j] = value;
                }
            }
            return restrictions;
        }

        private static float[] EnterRightRestricionsData(int restrictionAmount)
        {
            float[] restrictionRight = new float[restrictionAmount];
            Console.WriteLine("######################");
            for (int i = 0; i < restrictionAmount; i++)
            {
                Console.Write("Insira o valor a direita do '=' na Restrição " + i + ": ");
                float value = float.Parse(Console.ReadLine() ?? "");
                restrictionRight[i] = value;
            }
            return restrictionRight;
        }
    }
}