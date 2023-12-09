namespace AdventOfCode.Yr2023
{
    public static class D09
    {
        public static int PartOne(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                List<int[]> sequences = new() { line.Split(' ').Select(int.Parse).ToArray() };
                bool done = false;
                while (!done)
                {
                    int[] currentSequence = sequences[^1];
                    int[] newSequence = new int[currentSequence.Length - 1];
                    sequences.Add(newSequence);
                    for (int i = 0; i < currentSequence.Length - 1; i++)
                    {
                        newSequence[i] = currentSequence[i + 1] - currentSequence[i];
                    }
                    if (newSequence.All(n => n == 0))
                    {
                        done = true;
                    }
                }
                int lastNumber = 0;
                for (int i = sequences.Count - 2; i >= 0; i--)
                {
                    lastNumber += sequences[i][^1];
                }
                sum += lastNumber;
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                List<int[]> sequences = new() { line.Split(' ').Select(int.Parse).ToArray() };
                bool done = false;
                while (!done)
                {
                    int[] currentSequence = sequences[^1];
                    int[] newSequence = new int[currentSequence.Length - 1];
                    sequences.Add(newSequence);
                    for (int i = 0; i < currentSequence.Length - 1; i++)
                    {
                        newSequence[i] = currentSequence[i + 1] - currentSequence[i];
                    }
                    if (newSequence.All(n => n == 0))
                    {
                        done = true;
                    }
                }
                int lastNumber = 0;
                for (int i = sequences.Count - 2; i >= 0; i--)
                {
                    lastNumber = sequences[i][0] - lastNumber;
                }
                sum += lastNumber;
            }
            return sum;
        }
    }
}
