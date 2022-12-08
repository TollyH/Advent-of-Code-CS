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
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (trees[y][..x].Where(t => t < trees[y][x]).Count() == trees[y][..x].Length)
                    {
                        _ = visibleTrees.Add((x, y));
                    }
                }
                for (int x = input[0].Length - 1; x >= 0; x--)
                {
                    if (trees[y][(x + 1)..].Where(t => t < trees[y][x]).Count() == trees[y][(x + 1)..].Length)
                    {
                        _ = visibleTrees.Add((x, y));
                    }
                }
            }
            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (trees[..y].Select(t => t[x]).Where(t => t < trees[y][x]).Count() == trees[..y].Select(t => t[x]).Count())
                    {
                        _ = visibleTrees.Add((x, y));
                    }
                }
                for (int y = input.Length - 1; y >= 0; y--)
                {
                    if (trees[(y + 1)..].Select(t => t[x]).Where(t => t < trees[y][x]).Count() == trees[(y + 1)..].Select(t => t[x]).Count())
                    {
                        _ = visibleTrees.Add((x, y));
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
