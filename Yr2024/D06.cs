using System.Drawing;

namespace AdventOfCode.Yr2024
{
    public static class D06
    {
        public static int PartOne(string[] input)
        {
            Point guardPos = new();
            Point guardVector = new(0, -1);

            bool[,] map = new bool[input[0].Length, input.Length];
            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    map[x, y] = c == '#';
                    if (c == '^')
                    {
                        guardPos = new Point(x, y);
                    }
                }
            }

            HashSet<Point> visited = new();

            while (guardPos.X >= 0 && guardPos.X < map.GetLength(0)
                && guardPos.Y >= 0 && guardPos.Y < map.GetLength(1))
            {
                if (map[guardPos.X, guardPos.Y])
                {
                    // Undo last move
                    guardPos = new Point(guardPos.X - guardVector.X, guardPos.Y - guardVector.Y);
                    // Rotate 90deg clockwise
                    guardVector = new Point(-guardVector.Y, guardVector.X);
                }
                else
                {
                    _ = visited.Add(guardPos);
                }

                guardPos = new Point(guardPos.X + guardVector.X, guardPos.Y + guardVector.Y);
            }

            return visited.Count;
        }

        public static int PartTwo(string[] input)
        {
            Point originalGuardPos = new();

            bool[,] map = new bool[input[0].Length, input.Length];
            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    map[x, y] = c == '#';
                    if (c == '^')
                    {
                        originalGuardPos = new Point(x, y);
                    }
                }
            }

            int validBlocks = 0;

            for (int blockX = 0; blockX < map.GetLength(0); blockX++)
            {
                for (int blockY = 0; blockY < map.GetLength(1); blockY++)
                {
                    if (blockX == originalGuardPos.X && blockY == originalGuardPos.Y)
                    {
                        continue;
                    }

                    Dictionary<Point, HashSet<Point>> visited = new();

                    Point guardPos = originalGuardPos;
                    Point guardVector = new(0, -1);

                    while (guardPos.X >= 0 && guardPos.X < map.GetLength(0)
                        && guardPos.Y >= 0 && guardPos.Y < map.GetLength(1))
                    {
                        if (map[guardPos.X, guardPos.Y] || (blockX == guardPos.X && blockY == guardPos.Y))
                        {
                            // Undo last move
                            guardPos = new Point(guardPos.X - guardVector.X, guardPos.Y - guardVector.Y);
                            // Rotate 90deg clockwise
                            guardVector = new Point(-guardVector.Y, guardVector.X);
                        }
                        else
                        {
                            _ = visited.TryAdd(guardPos, new HashSet<Point>());
                            _ = visited[guardPos].Add(guardVector);
                        }

                        guardPos = new Point(guardPos.X + guardVector.X, guardPos.Y + guardVector.Y);

                        if (visited.TryGetValue(guardPos, out HashSet<Point>? points) && points.Contains(guardVector))
                        {
                            validBlocks++;
                            break;
                        }
                    }
                }
            }

            return validBlocks;
        }
    }
}
