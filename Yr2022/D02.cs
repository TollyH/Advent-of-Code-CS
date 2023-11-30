namespace AdventOfCode.Yr2022
{
    public static class D02
    {
        private static int Mod(int a, int b)
        {
            // Needed because C#'s % operator finds the remainder, not modulo
            return ((a % b) + b) % b;
        }

        public static int PartOne(string[] input)
        {
            int totalScore = 0;
            foreach(string line in input)
            {
                int opponentIndex = line[0] - 65;
                int ourIndex = line[2] - 88;
                totalScore += (Mod(ourIndex - (opponentIndex - 1), 3) * 3) + ourIndex + 1;
            }
            return totalScore;
        }

        public static int PartTwo(string[] input)
        {
            int totalScore = 0;
            foreach (string line in input)
            {
                int opponentIndex = line[0] - 65;
                int ourIndex = line[2] - 88;
                totalScore += Mod(ourIndex + (opponentIndex - 1), 3) + (ourIndex * 3) + 1;
            }
            return totalScore;
        }
    }
}
