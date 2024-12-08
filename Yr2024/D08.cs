using System.Drawing;

namespace AdventOfCode.Yr2024
{
    public static class D08
    {
        public static int PartOne(string[] input)
        {
            Dictionary<char, List<Point>> antenna = new();
            HashSet<Point> antinodes = new();

            int yBound = input.Length;
            int xBound = input[0].Length;

            for (int y = 0; y < yBound; y++)
            {
                string line = input[y];
                for (int x = 0; x < xBound; x++)
                {
                    char c = line[x];
                    if (c != '.')
                    {
                        _ = antenna.TryAdd(c, new List<Point>());
                        antenna[c].Add(new Point(x, y));
                    }
                }
            }

            foreach ((_, List<Point> points) in antenna.Where(kv => kv.Value.Count >= 2))
            {
                foreach (Point p1 in points)
                {
                    foreach (Point p2 in points)
                    {
                        if (p1 == p2)
                        {
                            continue;
                        }

                        Point newAntinode = new(p2.X + (p2.X - p1.X), p2.Y + (p2.Y - p1.Y));
                        if (newAntinode.X >= 0 && newAntinode.Y >= 0
                            && newAntinode.X < xBound && newAntinode.Y < yBound)
                        {
                            _ = antinodes.Add(newAntinode);
                        }
                    }
                }
            }

            return antinodes.Count;
        }

        public static int PartTwo(string[] input)
        {
            Dictionary<char, List<Point>> antenna = new();
            HashSet<Point> antinodes = new();

            int yBound = input.Length;
            int xBound = input[0].Length;

            for (int y = 0; y < yBound; y++)
            {
                string line = input[y];
                for (int x = 0; x < xBound; x++)
                {
                    char c = line[x];
                    if (c != '.')
                    {
                        _ = antenna.TryAdd(c, new List<Point>());
                        antenna[c].Add(new Point(x, y));
                    }
                }
            }

            foreach ((_, List<Point> points) in antenna.Where(kv => kv.Value.Count >= 2))
            {
                foreach (Point p1 in points)
                {
                    foreach (Point p2 in points)
                    {
                        if (p1 == p2)
                        {
                            continue;
                        }

                        for (int i = 0;; i++)
                        {
                            Point newAntinode = new(p2.X + ((p2.X - p1.X) * i), p2.Y + ((p2.Y - p1.Y) * i));
                            if (newAntinode.X >= 0 && newAntinode.Y >= 0
                                && newAntinode.X < xBound && newAntinode.Y < yBound)
                            {
                                _ = antinodes.Add(newAntinode);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return antinodes.Count;
        }
    }
}
