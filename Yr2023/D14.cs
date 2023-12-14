using System.Text;

namespace AdventOfCode.Yr2023
{
    public static class D14
    {
        private static void TiltNorth(char[,] platform)
        {
            for (int sy = 1; sy < platform.GetLength(1); sy++)
            {
                for (int y = sy; y > 0; y--)
                {
                    for (int x = 0; x < platform.GetLength(0); x++)
                    {
                        if (platform[x, y] == 'O' && platform[x, y - 1] == '.')
                        {
                            platform[x, y - 1] = 'O';
                            platform[x, y] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltSouth(char[,] platform)
        {
            for (int sy = platform.GetLength(1) - 1; sy >= 0; sy--)
            {
                for (int y = sy; y < platform.GetLength(1) - 1; y++)
                {
                    for (int x = 0; x < platform.GetLength(0); x++)
                    {
                        if (platform[x, y] == 'O' && platform[x, y + 1] == '.')
                        {
                            platform[x, y + 1] = 'O';
                            platform[x, y] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltWest(char[,] platform)
        {
            for (int sx = 1; sx < platform.GetLength(0); sx++)
            {
                for (int x = sx; x > 0; x--)
                {
                    for (int y = 0; y < platform.GetLength(1); y++)
                    {
                        if (platform[x, y] == 'O' && platform[x - 1, y] == '.')
                        {
                            platform[x - 1, y] = 'O';
                            platform[x, y] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltEast(char[,] platform)
        {
            for (int sx = platform.GetLength(0) - 1; sx >= 0; sx--)
            {
                for (int x = sx; x < platform.GetLength(0) - 1; x++)
                {
                    for (int y = 0; y < platform.GetLength(1); y++)
                    {
                        if (platform[x, y] == 'O' && platform[x + 1, y] == '.')
                        {
                            platform[x + 1, y] = 'O';
                            platform[x, y] = '.';
                        }
                    }
                }
            }
        }

        public static int PartOne(string[] input)
        {
            int height = input.Length;
            int width = input[0].Length;

            char[,] platform = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    platform[x, y] = line[x];
                }
            }

            TiltNorth(platform);

            int sum = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (platform[x, y] == 'O')
                    {
                        sum += height - y;
                    }
                }
            }
            return sum;
        }

        private static string GetPlatformIndexer(char[,] platform)
        {
            StringBuilder sb = new();
            for (int x = 0; x < platform.GetLength(0); x++)
            {
                for (int y = 0; y < platform.GetLength(1); y++)
                {
                    if (platform[x, y] == 'O')
                    {
                        _ = sb.Append(x).Append(',').Append(y).Append('.');
                    }
                }
            }
            return sb.ToString();
        }

        private static char[,] Clone2D(this char[,] array)
        {
            char[,] copy = new char[array.GetLength(0), array.GetLength(1)];
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    copy[x, y] = array[x, y];
                }
            }
            return copy;
        }

        private static readonly int targetCycles = 1000000000;
        public static int PartTwo(string[] input)
        {
            int height = input.Length;
            int width = input[0].Length;

            char[,] platform = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    platform[x, y] = line[x];
                }
            }

            Dictionary<string, int> seenBefore = new();
            Dictionary<int, char[,]> seenBeforePlatforms = new();
            int loopStart = 0;
            int loopEnd = 0;
            int loopLength = 0;
            for (int i = 0; loopLength == 0; i++)
            {
                string index = GetPlatformIndexer(platform);
                _ = seenBefore.TryAdd(index, i);
                _ = seenBeforePlatforms.TryAdd(i, platform.Clone2D());
                TiltNorth(platform);
                TiltWest(platform);
                TiltSouth(platform);
                TiltEast(platform);
                if (seenBefore.TryGetValue(GetPlatformIndexer(platform), out int j))
                {
                    loopStart = j;
                    loopEnd = i + 1;
                    loopLength = i - j + 1;
                }
            }

            int indexToCheck = ((targetCycles - loopEnd) % loopLength) + loopStart;
            platform = seenBeforePlatforms[indexToCheck];

            int sum = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (platform[x, y] == 'O')
                    {
                        sum += height - y;
                    }
                }
            }
            return sum;
        }
    }
}
