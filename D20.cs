namespace AdventOfCode.Yr2022
{
    public static class D20
    {
        private static int Mod(long a, long b)
        {
            // Needed because C#'s % operator finds the remainder, not modulo
            return (int)(((a % b) + b) % b);
        }

        public static int PartOne(string[] input)
        {
            List<int> values = input.Select(x => int.Parse(x)).ToList();
            List<int> indexes = new();

            for (int i = 0; i < values.Count; i++)
            {
                indexes.Add(i);
            }

            for (int i = 0; i < values.Count; i++)
            {
                int index = indexes.IndexOf(i);
                indexes.RemoveAt(index);
                indexes.Insert(Mod(index + values[i], indexes.Count), i);
            }

            int zeroLocation = indexes.IndexOf(values.IndexOf(0));
            return values[indexes[Mod(zeroLocation + 1000, values.Count)]]
                + values[indexes[Mod(zeroLocation + 2000, values.Count)]]
                + values[indexes[Mod(zeroLocation + 3000, values.Count)]];
        }

        public static long PartTwo(string[] input)
        {
            List<long> values = input.Select(x => long.Parse(x) * 811589153).ToList();
            List<int> indexes = new();

            for (int i = 0; i < values.Count; i++)
            {
                indexes.Add(i);
            }

            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    int index = indexes.IndexOf(i);
                    indexes.RemoveAt(index);
                    indexes.Insert(Mod(index + values[i], indexes.Count), i);
                }
            }

            int zeroLocation = indexes.IndexOf(values.IndexOf(0));
            return values[indexes[Mod(zeroLocation + 1000, values.Count)]]
                + values[indexes[Mod(zeroLocation + 2000, values.Count)]]
                + values[indexes[Mod(zeroLocation + 3000, values.Count)]];
        }
    }
}
