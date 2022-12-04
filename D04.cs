namespace AdventOfCode.Yr2022
{
    public static class D04
    {
        public static int PartOne(string[] input)
        {
            int totalOverlaps = 0;
            foreach (string line in input)
            {
                int[][] ranges = line.Split(",").Select(x => x.Split("-").Select(x => int.Parse(x)).ToArray()).ToArray();
                if ((ranges[0][0] <= ranges[1][0] && ranges[0][1] >= ranges[1][1])
                    || (ranges[0][0] >= ranges[1][0] && ranges[0][1] <= ranges[1][1]))
                {
                    totalOverlaps++;
                }
            }
            return totalOverlaps;
        }

        public static int PartTwo(string[] input)
        {
            int totalOverlaps = 0;
            foreach (string line in input)
            {
                int[][] ranges = line.Split(",").Select(x => x.Split("-").Select(x => int.Parse(x)).ToArray()).ToArray();
                if (ranges[0][0] <= ranges[1][1] && ranges[1][0] <= ranges[0][1])
                {
                    totalOverlaps++;
                }
            }
            return totalOverlaps;
        }
    }
}
