namespace AdventOfCode.Yr2024
{
    public static class D05
    {
        public static int PartOne(string[] input)
        {
            int total = 0;
            Dictionary<int, HashSet<int>> rules = new();

            int i = 0;
            for (; input[i] != ""; i++)
            {
                string[] split = input[i].Split('|');
                int rule = int.Parse(split[0]);
                _ = rules.TryAdd(rule, new HashSet<int>());
                _ = rules[rule].Add(int.Parse(split[1]));
            }

            for (i++; i < input.Length; i++)
            {
                int[] pageNumbers = input[i].Split(',').Select(int.Parse).ToArray();
                int middleNumber = pageNumbers[pageNumbers.Length / 2];

                HashSet<int> seen = new();
                bool valid = true;
                foreach (int number in pageNumbers)
                {
                    if (rules.TryGetValue(number, out HashSet<int>? checks) && seen.Overlaps(checks))
                    {
                        valid = false;
                        break;
                    }
                    _ = seen.Add(number);
                }

                if (valid)
                {
                    total += middleNumber;
                }
            }

            return total;
        }

        public static int PartTwo(string[] input)
        {
            int total = 0;
            Dictionary<int, HashSet<int>> rules = new();

            int i = 0;
            for (; input[i] != ""; i++)
            {
                string[] split = input[i].Split('|');
                int rule = int.Parse(split[0]);
                _ = rules.TryAdd(rule, new HashSet<int>());
                _ = rules[rule].Add(int.Parse(split[1]));
            }

            for (i++; i < input.Length; i++)
            {
                List<int> pageNumbers = input[i].Split(',').Select(int.Parse).ToList();

                HashSet<int> seen = new();
                bool valid = true;
                for (int j = 0; j < pageNumbers.Count; j++)
                {
                    int number = pageNumbers[j];
                    if (rules.TryGetValue(number, out HashSet<int>? checks) && seen.Overlaps(checks))
                    {
                        valid = false;
                        pageNumbers.RemoveAt(j);
                        int toInsertIndex = 0;
                        for (int k = 0; k < pageNumbers.Count; k++)
                        {
                            if (checks.Contains(pageNumbers[k]))
                            {
                                toInsertIndex = k;
                                break;
                            }
                        }
                        pageNumbers.Insert(toInsertIndex, number);
                    }
                    _ = seen.Add(number);
                }

                if (!valid)
                {
                    total += pageNumbers[pageNumbers.Count / 2];
                }
            }

            return total;
        }
    }
}
