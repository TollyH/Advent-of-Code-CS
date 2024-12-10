using System.Drawing;

namespace AdventOfCode.Yr2024
{
    public static class D10
    {
        private static readonly Point[] allowedMoves = new Point[4]
        {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1)
        };

        public static int PartOne(string[] input)
        {
            int[,] map = new int[input[0].Length, input.Length];
            List<Point> trailheads = new();

            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    map[x, y] = c - '0';
                    if (c == '0')
                    {
                        trailheads.Add(new Point(x, y));
                    }
                }
            }

            int total = 0;
            foreach (Point start in trailheads)
            {
                HashSet<Point> reachable = new();

                Queue<Point> searchQueue = new();
                searchQueue.Enqueue(start);

                while (searchQueue.TryDequeue(out Point pos))
                {
                    int currentHeight = map[pos.X, pos.Y];
                    if (currentHeight == 9)
                    {
                        _ = reachable.Add(pos);
                        continue;
                    }

                    foreach (Point move in allowedMoves)
                    {
                        Point newPos = new(pos.X + move.X, pos.Y + move.Y);
                        if (newPos.X >= 0 && newPos.X < map.GetLength(0)
                            && newPos.Y >= 0 && newPos.Y < map.GetLength(1)
                            && map[newPos.X, newPos.Y] == currentHeight + 1)
                        {
                            searchQueue.Enqueue(newPos);
                        }
                    }
                }

                total += reachable.Count;
            }

            return total;
        }

        public static int PartTwo(string[] input)
        {
            int[,] map = new int[input[0].Length, input.Length];
            List<Point> trailheads = new();

            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    map[x, y] = c - '0';
                    if (c == '0')
                    {
                        trailheads.Add(new Point(x, y));
                    }
                }
            }

            int total = 0;
            foreach (Point start in trailheads)
            {
                Queue<Point> searchQueue = new();
                searchQueue.Enqueue(start);

                while (searchQueue.TryDequeue(out Point pos))
                {
                    int currentHeight = map[pos.X, pos.Y];
                    if (currentHeight == 9)
                    {
                        total++;
                        continue;
                    }

                    foreach (Point move in allowedMoves)
                    {
                        Point newPos = new(pos.X + move.X, pos.Y + move.Y);
                        if (newPos.X >= 0 && newPos.X < map.GetLength(0)
                            && newPos.Y >= 0 && newPos.Y < map.GetLength(1)
                            && map[newPos.X, newPos.Y] == currentHeight + 1)
                        {
                            searchQueue.Enqueue(newPos);
                        }
                    }
                }
            }

            return total;
        }
    }
}
