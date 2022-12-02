namespace AdventOfCode.Yr2022
{
    public static class D02
    {
        private static readonly string[] opponentPlays = new string[3] { "A", "B", "C" };
        private static readonly string[] ourPlays = new string[3] { "X", "Y", "Z" };

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
                string[] splitLine = line.Split(' ');
                int opponentIndex = Array.IndexOf(opponentPlays, splitLine[0]);
                int ourIndex = Array.IndexOf(ourPlays, splitLine[1]);
                totalScore += (Mod(ourIndex - (opponentIndex - 1), 3) * 3) + ourIndex + 1;
            }
            return totalScore;
        }

        public static int PartTwo(string[] input)
        {
            int totalScore = 0;
            foreach (string line in input)
            {
                string[] splitLine = line.Split(' ');
                int opponentIndex = Array.IndexOf(opponentPlays, splitLine[0]);
                int ourIndex = Array.IndexOf(ourPlays, splitLine[1]);
                totalScore += Mod(ourIndex + (opponentIndex - 1), 3) + (ourIndex * 3) + 1;
            }
            return totalScore;
        }
    }
}
