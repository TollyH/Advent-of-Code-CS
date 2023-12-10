using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D10
    {
        private static readonly Point north = new(0, -1);
        private static readonly Point south = new(0, 1);
        private static readonly Point east = new(1, 0);
        private static readonly Point west = new(-1, 0);
        private static readonly Point[] cardinals = new [] { north, south, east, west };

        private static readonly Dictionary<char, Point[]> pipeDirections = new()
        {
            { '|', new [] { north, south } },
            { '-', new [] { east, west } },
            { 'L', new [] { north, east } },
            { 'J', new [] { north, west } },
            { '7', new [] { south, west } },
            { 'F', new [] { south, east } },
            { 'S', new [] { north, south, east, west } },
            { '.', Array.Empty<Point>() },
        };

        private class SearchPosition
        {
            public readonly Point ThisPosition;
            public readonly SearchPosition? LastPosition;
            public readonly int Length;

            public SearchPosition(Point thisPosition, SearchPosition? lastPosition, int length)
            {
                ThisPosition = thisPosition;
                LastPosition = lastPosition;
                Length = length;
            }
        }

        private static SearchPosition GetLoopingPath(char[,] maze, Point startPos, int xLength, int yLength)
        {
            SearchPosition? loopingPath = null;
            Queue<SearchPosition> searchQueue = new();
            searchQueue.Enqueue(new SearchPosition(startPos, null, 0));
            while (loopingPath is null)
            {
                SearchPosition pos = searchQueue.Dequeue();
                foreach (Point diff in cardinals)
                {
                    Point newPoint = new(pos.ThisPosition.X + diff.X, pos.ThisPosition.Y + diff.Y);
                    if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X >= xLength || newPoint.Y >= yLength
                        || newPoint == pos.LastPosition?.ThisPosition)
                    {
                        continue;
                    }

                    char thisChar = maze[pos.ThisPosition.X, pos.ThisPosition.Y];
                    if (!pipeDirections[thisChar].Contains(diff))
                    {
                        continue;
                    }

                    char newChar = maze[newPoint.X, newPoint.Y];
                    if (newChar == 'S')
                    {
                        loopingPath = new SearchPosition(newPoint, pos, pos.Length + 1);
                        break;
                    }

                    if (!pipeDirections[newChar].Contains(new Point(-diff.X, -diff.Y)))
                    {
                        continue;
                    }
                    searchQueue.Enqueue(new SearchPosition(newPoint, pos, pos.Length + 1));
                }
            }

            return loopingPath;
        }

        public static int PartOne(string[] input)
        {
            int xLength = input[0].Length;
            int yLength = input.Length;

            char[,] maze = new char[xLength, yLength];

            Point startPos = new(0, 0);
            for (int y = 0; y < yLength; y++)
            {
                string line = input[y];
                for (int x = 0; x < xLength; x++)
                {
                    char c = line[x];
                    maze[x, y] = c;
                    if (c == 'S')
                    {
                        startPos = new Point(x, y);
                    }
                }
            }

            return GetLoopingPath(maze, startPos, xLength, yLength).Length / 2;
        }

        public static int PartTwo(string[] input)
        {
            int xLength = input[0].Length;
            int yLength = input.Length;

            char[,] maze = new char[xLength, yLength];

            Point startPos = new(0, 0);
            for (int y = 0; y < yLength; y++)
            {
                string line = input[y];
                for (int x = 0; x < xLength; x++)
                {
                    char c = line[x];
                    maze[x, y] = c;
                    if (c == 'S')
                    {
                        startPos = new Point(x, y);
                    }
                }
            }

            SearchPosition? loopingPath = GetLoopingPath(maze, startPos, xLength, yLength);

            List<Point> fullPath = new();
            while (loopingPath is not null)
            {
                fullPath.Add(loopingPath.ThisPosition);
                loopingPath = loopingPath.LastPosition;
            }

            List<Point> vertices = fullPath.Where(p => maze[p.X, p.Y] is not '-' and not '|' and not 'S').ToList();
            if (maze[fullPath[1].X, fullPath[1].Y] != maze[fullPath[^2].X, fullPath[^2].Y])
            {
                vertices.Insert(0, startPos);
            }

            int area = 0;
            for (int i = 0; i < vertices.Count; i++)
            {
                int next = (i + 1) % vertices.Count;
                area += (vertices[i].X * vertices[next].Y) - (vertices[next].X * vertices[i].Y);
            }
            area = Math.Abs(area) / 2;
            return area - ((fullPath.Count - 1) / 2) + 1;
        }
    }
}
