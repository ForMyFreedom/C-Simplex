namespace FunctionExample
{
    class RawProblem(int dimension, float[] objetive, int restrictionAmount, float[][] restrictionLeft, float[] restrictionRight)
    {
        public int dimension = dimension;
        public float[] objetive = objetive;
        public int restrictionAmount = restrictionAmount;
        public float[][] restrictionLeft = restrictionLeft;
        public float[] restrictionRight = restrictionRight;
    }
}