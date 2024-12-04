namespace AdventOfCode.Yr2024
{
    public static class D04
    {
        public static int PartOne(string[] input)
        {
            const string findWord = "XMAS";

            int total = 0;

            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if (dy == 0 && dx == 0)
                            {
                                continue;
                            }

                            bool match = true;

                            for (int i = 0; i < findWord.Length; i++)
                            {
                                int newY = y + (dy * i);
                                int newX = x + (dx * i);
                                if (newY < 0 || newY >= input.Length
                                    || newX < 0 || newX >= input[0].Length
                                    || input[newY][newX] != findWord[i])
                                {
                                    match = false;
                                    break;
                                }
                            }

                            if (match)
                            {
                                total++;
                            }
                        }
                    }
                }
            }

            return total;
        }

        public static int PartTwo(string[] input)
        {
            int total = 0;

            for (int y = 1; y < input.Length - 1; y++)
            {
                string line = input[y];
                for (int x = 1; x < line.Length - 1; x++)
                {
                    if (input[y][x] == 'A'
                        && input[y - 1][x - 1] is 'M' or 'S'
                        && input[y + 1][x - 1] is 'M' or 'S'
                        && input[y - 1][x + 1] is 'M' or 'S'
                        && input[y + 1][x + 1] is 'M' or 'S'
                        && input[y - 1][x + 1] != input[y + 1][x - 1]
                        && input[y - 1][x - 1] != input[y + 1][x + 1])
                    {
                        total += 1;
                    }
                }
            }

            return total;
        }
    }
}
