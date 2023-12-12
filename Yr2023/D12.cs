namespace AdventOfCode.Yr2023
{
    public static class D12
    {
        private static readonly Dictionary<(string, int, int), long> cache = new();
        private static long GetPossibleCombinations(string springRow, int groupSize, int[] brokenPattern)
        {
            if (cache.TryGetValue((springRow, groupSize, brokenPattern.Length), out long value))
            {
                return value;
            }
            if (springRow.Length == 0)
            {
                if ((brokenPattern.Length == 0 && groupSize == 0)
                    || (brokenPattern.Length == 1 && groupSize == brokenPattern[0]))
                {
                    return 1;
                }
                return 0;
            }

            long arrangements = 0;

            switch (springRow[0])
            {
                case '?':
                {
                    arrangements = GetPossibleCombinations(springRow[1..], groupSize + 1, brokenPattern);
                    if (groupSize > 0 && brokenPattern.Length > 0 && brokenPattern[0] == groupSize)
                    {
                        arrangements += GetPossibleCombinations(springRow[1..], 0, brokenPattern[1..]);
                    }
                    else if (groupSize == 0)
                    {
                        arrangements += GetPossibleCombinations(springRow[1..], 0, brokenPattern);
                    }
                    break;
                }
                case '#':
                    arrangements = GetPossibleCombinations(springRow[1..], groupSize + 1, brokenPattern);
                    break;
                case '.':
                    if (groupSize > 0 && brokenPattern.Length > 0 && brokenPattern[0] == groupSize)
                    {
                        arrangements += GetPossibleCombinations(springRow[1..], 0, brokenPattern[1..]);
                    }
                    else if (groupSize == 0)
                    {
                        arrangements += GetPossibleCombinations(springRow[1..], 0, brokenPattern);
                    }
                    break;
            }
            cache[(springRow, groupSize, brokenPattern.Length)] = arrangements;
            return arrangements;
        }

        public static long PartOne(string[] input)
        {
            List<string> springRecords = new();
            List<int[]> springsBroken = new();

            foreach (string line in input)
            {
                string[] components = line.Split(' ');
                springRecords.Add(components[0]);
                springsBroken.Add(components[1].Split(',').Select(int.Parse).ToArray());
            }

            long sum = 0;
            for (int i = 0; i < springRecords.Count; i++)
            {
                string springRow = springRecords[i];
                int[] brokenPattern = springsBroken[i];
                sum += GetPossibleCombinations(springRow, 0, brokenPattern);
                cache.Clear();
            }
            return sum;
        }

        public static long PartTwo(string[] input)
        {
            List<string> springRecords = new();
            List<int[]> springsBroken = new();

            foreach (string line in input)
            {
                string[] components = line.Split(' ');
                springRecords.Add(string.Join('?', Enumerable.Repeat(components[0], 5)));
                springsBroken.Add(Enumerable.Repeat(components[1].Split(',').Select(int.Parse), 5).SelectMany(i => i).ToArray());
            }

            long sum = 0;
            for (int i = 0; i < springRecords.Count; i++)
            {
                string springRow = springRecords[i];
                int[] brokenPattern = springsBroken[i];
                sum += GetPossibleCombinations(springRow, 0, brokenPattern);
                cache.Clear();
            }
            return sum;
        }
    }
}
