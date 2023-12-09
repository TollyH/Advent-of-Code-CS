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
                    sum += currentSequence[^1];
                    if (newSequence.All(n => n == 0))
                    {
                        done = true;
                    }
                }
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                List<int[]> sequences = new() { line.Split(' ').Select(int.Parse).Reverse().ToArray() };
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
                    sum += currentSequence[^1];
                    if (newSequence.All(n => n == 0))
                    {
                        done = true;
                    }
                }
            }
            return sum;
        }
    }
}
