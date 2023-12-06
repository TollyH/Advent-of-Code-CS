namespace AdventOfCode.Yr2023
{
    public static class D06
    {
        public static int PartOne(string[] input)
        {
            int[] times = input[0].Split(':')[1].Split(' ').Where(t => t != "").Select(int.Parse).ToArray();
            int[] distances = input[1].Split(':')[1].Split(' ').Where(t => t != "").Select(int.Parse).ToArray();

            int product = 1;
            for (int i = 0; i < times.Length; i++)
            {
                int time = times[i];
                int recordDistance = distances[i];
                int numberWinning = 0;
                for (int hold = 0; hold <= time; hold++)
                {
                    int raceDistance = (time - hold) * hold;
                    if (raceDistance > recordDistance)
                    {
                        numberWinning++;
                    }
                }
                product *= numberWinning;
            }
            return product;
        }

        public static long PartTwo(string[] input)
        {
            long time = long.Parse(input[0].Split(':')[1].Replace(" ", ""));
            long recordDistance = long.Parse(input[1].Split(':')[1].Replace(" ", ""));

            long numberWinning = 0;
            for (long hold = 0; hold <= time; hold++)
            {
                long raceDistance = (time - hold) * hold;
                if (raceDistance > recordDistance)
                {
                    numberWinning++;
                }
            }
            return numberWinning;
        }
    }
}
