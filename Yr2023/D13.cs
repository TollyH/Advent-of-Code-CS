namespace AdventOfCode.Yr2023
{
    public static class D13
    {
        private static bool[,] To2DArray(this List<List<bool>> list)
        {
            int width = list[0].Count;
            int height = list.Count;
            bool[,] newArray = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                List<bool> row = list[y];
                for (int x = 0; x < width; x++)
                {
                    newArray[x, y] = row[x];
                }
            }

            return newArray;
        }

        private static int GetVerticalSymmetryLine(bool[,] pattern)
        {
            int width = pattern.GetLength(0);
            int height = pattern.GetLength(1);
            for (int x = 0; x < width - 1; x++)
            {
                bool symmetryPossible = true;
                for (int dx = 0; symmetryPossible; dx++)
                {
                    int lower = x - dx;
                    int upper = x + dx + 1;
                    if (lower < 0 || upper >= width)
                    {
                        return x;
                    }
                    for (int y = 0; y < height; y++)
                    {
                        if (pattern[lower, y] != pattern[upper, y])
                        {
                            symmetryPossible = false;
                            break;
                        }
                    }
                }
            }
            return -1;
        }

        private static int GetHorizontalSymmetryLine(bool[,] pattern)
        {
            int width = pattern.GetLength(0);
            int height = pattern.GetLength(1);
            for (int y = 0; y < height - 1; y++)
            {
                bool symmetryPossible = true;
                for (int dy = 0; symmetryPossible; dy++)
                {
                    int lower = y - dy;
                    int upper = y + dy + 1;
                    if (lower < 0 || upper >= height)
                    {
                        return y;
                    }
                    for (int x = 0; x < width; x++)
                    {
                        if (pattern[x, lower] != pattern[x, upper])
                        {
                            symmetryPossible = false;
                            break;
                        }
                    }
                }
            }
            return -1;
        }

        public static int PartOne(string[] input)
        {
            List<bool[,]> patterns = new();
            List<List<bool>> currentPattern = new() { new List<bool>() };
            foreach (string line in input)
            {
                foreach (char character in line)
                {
                    currentPattern[^1].Add(character == '#');
                }

                if (currentPattern[^1].Count == 0)
                {
                    currentPattern.RemoveAt(currentPattern.Count - 1);
                    patterns.Add(currentPattern.To2DArray());
                    currentPattern = new List<List<bool>> { new() };
                }
                else
                {
                    currentPattern.Add(new List<bool>());
                }
            }
            if (currentPattern[^1].Count == 0)
            {
                currentPattern.RemoveAt(currentPattern.Count - 1);
                patterns.Add(currentPattern.To2DArray());
            }

            int sum = 0;
            foreach (bool[,] pattern in patterns)
            {
                int verticalSymmetry = GetVerticalSymmetryLine(pattern);
                if (verticalSymmetry != -1)
                {
                    sum += verticalSymmetry + 1;
                }
                else
                {
                    sum += (GetHorizontalSymmetryLine(pattern) + 1) * 100;
                }
            }

            return sum;
        }

        private static int GetSmudgedVerticalSymmetryLine(bool[,] pattern)
        {
            int width = pattern.GetLength(0);
            int height = pattern.GetLength(1);
            for (int x = 0; x < width - 1; x++)
            {
                int numDifferent = 0;
                bool symmetryPossible = true;
                for (int dx = 0; symmetryPossible; dx++)
                {
                    int lower = x - dx;
                    int upper = x + dx + 1;
                    if (lower < 0 || upper >= width)
                    {
                        if (numDifferent == 1)
                        {
                            return x;
                        }
                        break;
                    }
                    for (int y = 0; y < height; y++)
                    {
                        if (pattern[lower, y] != pattern[upper, y])
                        {
                            numDifferent++;
                        }
                    }
                    if (numDifferent > 1)
                    {
                        symmetryPossible = false;
                    }
                }
            }
            return -1;
        }

        private static int GetSmudgedHorizontalSymmetryLine(bool[,] pattern)
        {
            int width = pattern.GetLength(0);
            int height = pattern.GetLength(1);
            for (int y = 0; y < height - 1; y++)
            {
                int numDifferent = 0;
                bool symmetryPossible = true;
                for (int dy = 0; symmetryPossible; dy++)
                {
                    int lower = y - dy;
                    int upper = y + dy + 1;
                    if (lower < 0 || upper >= height)
                    {
                        if (numDifferent == 1)
                        {
                            return y;
                        }
                        break;
                    }
                    for (int x = 0; x < width; x++)
                    {
                        if (pattern[x, lower] != pattern[x, upper])
                        {
                            numDifferent++;
                        }
                    }
                    if (numDifferent > 1)
                    {
                        symmetryPossible = false;
                    }
                }
            }
            return -1;
        }

        public static int PartTwo(string[] input)
        {
            List<bool[,]> patterns = new();
            List<List<bool>> currentPattern = new() { new List<bool>() };
            foreach (string line in input)
            {
                foreach (char character in line)
                {
                    currentPattern[^1].Add(character == '#');
                }

                if (currentPattern[^1].Count == 0)
                {
                    currentPattern.RemoveAt(currentPattern.Count - 1);
                    patterns.Add(currentPattern.To2DArray());
                    currentPattern = new List<List<bool>> { new() };
                }
                else
                {
                    currentPattern.Add(new List<bool>());
                }
            }
            if (currentPattern[^1].Count == 0)
            {
                currentPattern.RemoveAt(currentPattern.Count - 1);
                patterns.Add(currentPattern.To2DArray());
            }

            int sum = 0;
            foreach (bool[,] pattern in patterns)
            {
                int verticalSymmetry = GetSmudgedVerticalSymmetryLine(pattern);
                if (verticalSymmetry != -1)
                {
                    sum += verticalSymmetry + 1;
                }
                else
                {
                    sum += (GetSmudgedHorizontalSymmetryLine(pattern) + 1) * 100;
                }
            }

            return sum;
        }
    }
}
