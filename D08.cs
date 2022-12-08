namespace AdventOfCode.Yr2022
{
    public static class D08
    {
        public static int PartOne(string[] input)
        {
            int[][] trees = new int[input.Length][];
            HashSet<(int, int)> visibleTrees = new();

            for (int y = 0; y < input.Length; y++)
            {
                trees[y] = new int[input[0].Length];
                for (int x = 0; x < input[0].Length; x++)
                {
                    trees[y][x] = input[y][x] - 48;
                }
            }

            for (int y = 0; y < input.Length; y++)
            {
                int maxInRow = -1;
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (trees[y][x] > maxInRow)
                    {
                        _ = visibleTrees.Add((x, y));
                        maxInRow = trees[y][x];
                    }
                }
                maxInRow = -1;
                for (int x = input[0].Length - 1; x >= 0; x--)
                {
                    if (trees[y][x] > maxInRow)
                    {
                        _ = visibleTrees.Add((x, y));
                        maxInRow = trees[y][x];
                    }
                }
            }
            for (int x = 0; x < input[0].Length; x++)
            {
                int maxInCol = -1;
                for (int y = 0; y < input.Length; y++)
                {
                    if (trees[y][x] > maxInCol)
                    {
                        _ = visibleTrees.Add((x, y));
                        maxInCol = trees[y][x];
                    }
                }
                maxInCol = -1;
                for (int y = input.Length - 1; y >= 0; y--)
                {
                    if (trees[y][x] > maxInCol)
                    {
                        _ = visibleTrees.Add((x, y));
                        maxInCol = trees[y][x];
                    }
                }
            }
            return visibleTrees.Count;
        }

        public static int PartTwo(string[] input)
        {
            int[][] trees = new int[input.Length][];
            List<int> scenicScores = new();

            for (int y = 0; y < input.Length; y++)
            {
                trees[y] = new int[input[0].Length];
                for (int x = 0; x < input[0].Length; x++)
                {
                    trees[y][x] = input[y][x] - 48;
                }
            }

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    int scenic = 1;
                    int i = 0;
                    for (int tx = x + 1; tx < input[0].Length; tx++)
                    {
                        i++;
                        if (trees[y][tx] >= trees[y][x])
                        {
                            break;
                        }
                    }
                    scenic *= i;
                    i = 0;
                    for (int tx = x - 1; tx >= 0; tx--)
                    {
                        i++;
                        if (trees[y][tx] >= trees[y][x])
                        {
                            break;
                        }
                    }
                    scenic *= i;
                    i = 0;
                    for (int ty = y + 1; ty < input.Length; ty++)
                    {
                        i++;
                        if (trees[ty][x] >= trees[y][x])
                        {
                            break;
                        }
                    }
                    scenic *= i;
                    i = 0;
                    for (int ty = y - 1; ty >= 0; ty--)
                    {
                        i++;
                        if (trees[ty][x] >= trees[y][x])
                        {
                            break;
                        }
                    }
                    scenic *= i;
                    scenicScores.Add(scenic);
                }
            }
            return scenicScores.Max();
        }
    }
}
