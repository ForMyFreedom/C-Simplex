
namespace FunctionExample
{
    class SimplexProblem
    {
        public List<int> baseIndex;
        public List<int> nonBaseIndex;
        public List<List<float>> baseMatrix { get; protected set; }
        public List<List<float>> inverseBaseMatrix { get; protected set; }
        public List<List<float>> nonBaseMatrix;
        public List<float> baseObjective;
        public List<float> nonBaseObjective;
        public List<float> restrictionEquals;
        public RawProblem rawProblem;

        public void SetBaseMatrix(List<List<float>> baseMatrix)
        {
            this.baseMatrix = baseMatrix;
            inverseBaseMatrix = MyMath.CalculateInverse(baseMatrix);
        }

        public SimplexProblem(RawProblem rawProblem)
        {
            this.rawProblem = rawProblem;

            baseIndex = new List<int>(rawProblem.restrictionAmount);
            for (var i = 0; i < rawProblem.restrictionAmount; i += 1)
            {
                baseIndex[i] = i;
            }

            nonBaseIndex = new List<int>(rawProblem.dimension - rawProblem.restrictionAmount);
            for (var i = 0; i < nonBaseIndex.Count; i += 1)
            {
                baseIndex[i] = i + rawProblem.restrictionAmount;
            }

            baseMatrix = new(baseIndex.Count);
            inverseBaseMatrix = new(baseIndex.Count);
            nonBaseMatrix = new(nonBaseIndex.Count);
            CalculateMatrixesFromRaw();

            baseObjective = new(baseIndex.Count);
            nonBaseObjective = new(nonBaseIndex.Count);
            CalculateObjetivesFromRaw();

            this.restrictionEquals = new(rawProblem.restrictionRight);
        }


        public void CalculateMatrixesFromRaw()
        {
            foreach (int i in baseIndex)
            {
                baseMatrix[i] = new(rawProblem.dimension);
                for (var j = 0; j < rawProblem.dimension; j++)
                {
                    baseMatrix[i][j] = rawProblem.restrictionLeft[j][i];
                }
            }

            SetBaseMatrix(baseMatrix); // Here to calculate the 'inverseBaseMatrix'

            foreach (int i in nonBaseIndex)
            {
                nonBaseMatrix[i] = new(rawProblem.dimension);
                for (var j = 0; j < rawProblem.dimension; j++)
                {
                    nonBaseMatrix[i][j] = rawProblem.restrictionLeft[j][i];
                }
            }
        }

        public void CalculateObjetivesFromRaw()
        {
            foreach (int i in baseIndex)
            {
                baseObjective[i] = rawProblem.objetive[i];
            }

            foreach (int i in nonBaseIndex)
            {
                nonBaseObjective[i] = rawProblem.objetive[i];
            }
        }
    }
}