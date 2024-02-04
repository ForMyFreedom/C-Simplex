
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
                baseIndex.Add(i);
            }

            nonBaseIndex = new List<int>(rawProblem.dimension - rawProblem.restrictionAmount);
            for (var i = 0; i < nonBaseIndex.Capacity; i += 1)
            {
                nonBaseIndex.Add(i + rawProblem.restrictionAmount);
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
            baseMatrix = [];
            for (var i = 0; i < baseIndex.Count; i++)
            {
                int selectedIndex = baseIndex[i];
                baseMatrix.Add(new(rawProblem.restrictionAmount));
                for (var j = 0; j < rawProblem.restrictionAmount; j++)
                {
                    baseMatrix[i].Add(rawProblem.restrictionLeft[j][selectedIndex]);
                }
            }

            SetBaseMatrix(baseMatrix); // Here to calculate the 'inverseBaseMatrix'

            nonBaseMatrix = [];
            for(var i=0; i<nonBaseIndex.Count; i++)
            {
                int selectedIndex = nonBaseIndex[i];
                nonBaseMatrix.Add(new(rawProblem.restrictionAmount));
                for (var j = 0; j < rawProblem.restrictionAmount; j++)
                {
                    nonBaseMatrix[i].Add(rawProblem.restrictionLeft[j][selectedIndex]);
                }
            }
        }

        public void CalculateObjetivesFromRaw()
        {
            baseObjective = [];
            foreach (int i in baseIndex)
            {
                baseObjective.Add(rawProblem.objetive[i]);
            }
            nonBaseObjective = [];
            foreach (int i in nonBaseIndex)
            {
                nonBaseObjective.Add(rawProblem.objetive[i]);
            }
        }
    }
}