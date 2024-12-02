namespace AdventOfCode.Yr2024
{
    public static class D02
    {
        public static int PartOne(string[] input)
        {
            int safe = input.Length;

            foreach (string report in input)
            {
                int[] levels = report.Split(' ').Select(int.Parse).ToArray();
                int last = levels[0];
                bool descend = levels[1] < last;
                foreach (int level in levels.Skip(1))
                {
                    int diff = level - last;
                    if (Math.Abs(diff) is < 1 or > 3 || (diff > 0 && descend) || (diff < 0 && !descend))
                    {
                        safe--;
                        break;
                    }
                    last = level;
                }
            }

            return safe;
        }

        public static int PartTwo(string[] input)
        {
            int safe = 0;

            foreach (string report in input)
            {
                int[] levels = report.Split(' ').Select(int.Parse).ToArray();
                bool anyValid = false;
                for (int skip = -1; skip < levels.Length && !anyValid; skip++)
                {
                    List<int> filteredLevels = new(levels);
                    if (skip != -1)
                    {
                        filteredLevels.RemoveAt(skip);
                    }

                    int last = filteredLevels[0];
                    bool descend = filteredLevels[1] < last;
                    bool valid = true;
                    foreach (int level in filteredLevels.Skip(1))
                    {
                        int diff = level - last;
                        if (Math.Abs(diff) is < 1 or > 3 || (diff > 0 && descend) || (diff < 0 && !descend))
                        {
                            valid = false;
                            break;
                        }
                        last = level;
                    }
                    if (valid)
                    {
                        anyValid = true;
                    }
                }
                if (anyValid)
                {
                    safe++;
                }
            }

            return safe;
        }
    }
}
