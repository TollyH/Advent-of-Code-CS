using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D23
    {
        private readonly struct PathPoint
        {
            public readonly Point Position;
            public readonly HashSet<Point> PreviousPoints;

            public PathPoint(Point position, HashSet<Point> previousPoints)
            {
                Position = position;
                PreviousPoints = new HashSet<Point>(previousPoints);
                _ = PreviousPoints.Add(Position);
            }
        }

        private static readonly Point[] cardinals = new Point[4]
        {
            new(1, 0),
            new(-1, 0),
            new(0, -1),
            new(0, 1),
        };

        private static readonly Dictionary<char, Point> slopeDirections = new()
        {
            { '>', new Point(1, 0) },
            { '<', new Point(-1, 0) },
            { 'v', new Point(0, 1) },
            { '^', new Point(0, -1) },
        };

        public static int PartOne(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            char[,] forest = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    forest[x, y] = line[x];
                }
            }

            Point start = new(1, 0);
            Point end = new(width - 2, height - 1);

            PriorityQueue<PathPoint, int> pathQueue = new();
            pathQueue.Enqueue(new PathPoint(start, new HashSet<Point>()), 0);

            int longest = 0;
            while (pathQueue.TryDequeue(out PathPoint pt, out int length))
            {
                if (pt.Position == end)
                {
                    if (-length > longest)
                    {
                        longest = -length;
                    }
                    continue;
                }

                foreach (Point direction in cardinals)
                {
                    Point newPoint = new(pt.Position.X + direction.X, pt.Position.Y + direction.Y);
                    if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X >= width || newPoint.Y >= height)
                    {
                        continue;
                    }
                    if (forest[newPoint.X, newPoint.Y] == '#')
                    {
                        continue;
                    }
                    if (pt.PreviousPoints.Contains(newPoint))
                    {
                        continue;
                    }
                    if (slopeDirections.TryGetValue(forest[pt.Position.X, pt.Position.Y], out Point slope) && slope != direction)
                    {
                        continue;
                    }
                    PathPoint newPathPoint = new(newPoint, pt.PreviousPoints);
                    pathQueue.Enqueue(newPathPoint, length - 1);
                }
            }

            return longest;
        }

        private static int GetLongestPath(char[,] forest, Point currentPoint, int currentLength, bool[,] visited, int width, int height, Point target)
        {
            if (currentPoint == target)
            {
                return currentLength;
            }
            int longest = int.MinValue;
            foreach (Point direction in cardinals)
            {
                Point newPoint = new(currentPoint.X + direction.X, currentPoint.Y + direction.Y);
                if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X >= width || newPoint.Y >= height)
                {
                    continue;
                }
                if (forest[newPoint.X, newPoint.Y] == '#')
                {
                    continue;
                }
                if (visited[newPoint.X, newPoint.Y])
                {
                    continue;
                }
                visited[newPoint.X, newPoint.Y] = true;
                int length = GetLongestPath(forest, newPoint, currentLength + 1, visited, width, height, target);
                visited[newPoint.X, newPoint.Y] = false;
                if (length > longest)
                {
                    longest = length;
                }
            }
            return longest;
        }

        public static int PartTwo(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            char[,] forest = new char[width, height];

            bool[,] visited = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    forest[x, y] = line[x];
                    visited[x, y] = false;
                }
            }

            Point start = new(1, 0);
            Point end = new(width - 2, height - 1);

            int longest = 0;
            // This sucks
            Thread thread = new(() => longest = GetLongestPath(forest, start, 0, visited, width, height, end), int.MaxValue);
            thread.Start();
            thread.Join();
            return longest;
        }
    }
}
