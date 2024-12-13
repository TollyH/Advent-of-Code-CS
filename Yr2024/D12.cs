using System.Drawing;

namespace AdventOfCode.Yr2024
{
    public static class D12
    {
        private static readonly Point[] allowedMoves = new Point[4]
        {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1)
        };

        private readonly struct Region(int perimeter, int area)
        {
            public readonly int Perimeter = perimeter;
            public readonly int Area = area;
        }

        public static int PartOne(string[] input)
        {
            HashSet<Point> searchedPoints = new();
            List<Region> regions = new();

            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    Point pnt = new(x, y);

                    int perimeter = 0;
                    int area = 0;

                    Queue<Point> searchQueue = new();
                    searchQueue.Enqueue(pnt);

                    while (searchQueue.TryDequeue(out pnt))
                    {
                        if (!searchedPoints.Add(pnt))
                        {
                            continue;
                        }

                        area++;

                        foreach (Point move in allowedMoves)
                        {
                            Point newPoint = new(pnt.X + move.X, pnt.Y + move.Y);
                            if (newPoint.X >= 0 && newPoint.Y >= 0
                                && newPoint.X < input.Length && newPoint.Y < input[0].Length
                                && input[newPoint.Y][newPoint.X] == input[pnt.Y][pnt.X])
                            {
                                searchQueue.Enqueue(newPoint);
                            }
                            else
                            {
                                perimeter++;
                            }
                        }
                    }

                    regions.Add(new Region(perimeter, area));
                }
            }

            return regions.Sum(r => r.Perimeter * r.Area);
        }

        public static int PartTwo(string[] input)
        {
            return 0;
        }
    }
}
