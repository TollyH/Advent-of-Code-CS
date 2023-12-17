using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D17
    {
        private class PathPoint
        {
            public readonly Point Position;
            public readonly int StraightLineLength;
            public readonly PathPoint? Previous;

            public PathPoint(Point position, int straightLineLength, PathPoint? previous)
            {
                Position = position;
                StraightLineLength = straightLineLength;
                Previous = previous;
            }
        }

        private readonly struct VisitEntry
        {
            public readonly Point Position;
            public readonly Point Direction;
            public readonly int StraightLineLength;

            public VisitEntry(Point position, Point direction, int straightLineLength)
            {
                Position = position;
                Direction = direction;
                StraightLineLength = straightLineLength;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Position.GetHashCode(), Direction.GetHashCode(), StraightLineLength);
            }
        }

        public static int PartOne(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            Point target = new(width - 1, height - 1);

            Dictionary<VisitEntry, int> visited = new();
            int[,] city = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    city[x, y] = line[x] - '0';
                }
            }

            PriorityQueue<PathPoint, int> searchQueue = new();
            searchQueue.Enqueue(new PathPoint(new Point(0, 0), 0, null), 0);

            int finalHeatLoss = 0;
            while (searchQueue.TryDequeue(out PathPoint? pt, out int heatLoss))
            {
                if (pt.Position == target)
                {
                    finalHeatLoss = heatLoss;
                    break;
                }

                PathPoint? lastPoint = pt.Previous;
                Point direction = new(pt.Position.X - lastPoint?.Position.X ?? 0, pt.Position.Y - lastPoint?.Position.Y ?? 0);

                Point rightTurn = new(-direction.Y, direction.X);
                Point leftTurn = new(direction.Y, -direction.X);
                Point newPoint;

                if (pt.StraightLineLength < 2 && lastPoint is not null)
                {
                    newPoint = new Point(pt.Position.X + direction.X, pt.Position.Y + direction.Y);
                    if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height)
                    {
                        int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                        int newStraight = pt.StraightLineLength + 1;
                        VisitEntry visit = new(newPoint, direction, newStraight);
                        if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                        {
                            PathPoint newPathPoint = new(newPoint, newStraight, pt);
                            searchQueue.Enqueue(newPathPoint, newHeatLoss);
                            visited[visit] = newHeatLoss;
                        }
                    }
                }

                newPoint = lastPoint is not null ? new Point(pt.Position.X + rightTurn.X, pt.Position.Y + rightTurn.Y)
                        : new Point(1, 0);
                if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height)
                {
                    int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                    VisitEntry visit = new(newPoint, rightTurn, 0);
                    if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                    {
                        PathPoint newPathPoint = new(newPoint, 0, pt);
                        searchQueue.Enqueue(newPathPoint, newHeatLoss);
                        visited[visit] = newHeatLoss;
                    }
                }

                newPoint = lastPoint is not null ? new Point(pt.Position.X + leftTurn.X, pt.Position.Y + leftTurn.Y)
                    : new Point(0, 1);
                if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height)
                {
                    int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                    VisitEntry visit = new(newPoint, leftTurn, 0);
                    if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                    {
                        PathPoint newPathPoint = new(newPoint, 0, pt);
                        searchQueue.Enqueue(newPathPoint, newHeatLoss);
                        visited[visit] = newHeatLoss;
                    }
                }
            }

            return finalHeatLoss;
        }

        public static int PartTwo(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            Point target = new(width - 1, height - 1);

            Dictionary<VisitEntry, int> visited = new();
            int[,] city = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    city[x, y] = line[x] - '0';
                }
            }

            PriorityQueue<PathPoint, int> searchQueue = new();
            searchQueue.Enqueue(new PathPoint(new Point(0, 0), 0, null), 0);

            int finalHeatLoss = 0;
            while (searchQueue.TryDequeue(out PathPoint? pt, out int heatLoss))
            {
                if (pt.Position == target)
                {
                    finalHeatLoss = heatLoss;
                    break;
                }

                PathPoint? lastPoint = pt.Previous;
                Point direction = new(pt.Position.X - lastPoint?.Position.X ?? 0, pt.Position.Y - lastPoint?.Position.Y ?? 0);

                Point rightTurn = new(-direction.Y, direction.X);
                Point leftTurn = new(direction.Y, -direction.X);
                Point newPoint;

                if (pt.StraightLineLength < 9 && lastPoint is not null)
                {
                    newPoint = new Point(pt.Position.X + direction.X, pt.Position.Y + direction.Y);
                    if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height && (newPoint != target || pt.StraightLineLength >= 2))
                    {
                        int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                        int newStraight = pt.StraightLineLength + 1;
                        VisitEntry visit = new(newPoint, direction, newStraight);
                        if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                        {
                            PathPoint newPathPoint = new(newPoint, newStraight, pt);
                            searchQueue.Enqueue(newPathPoint, newHeatLoss);
                            visited[visit] = newHeatLoss;
                        }
                    }
                }
                if (pt.StraightLineLength >= 3 || lastPoint is null)
                {
                    newPoint = lastPoint is not null
                        ? new Point(pt.Position.X + rightTurn.X, pt.Position.Y + rightTurn.Y)
                        : new Point(1, 0);
                    if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height && newPoint != target)
                    {
                        int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                        VisitEntry visit = new(newPoint, rightTurn, 0);
                        if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                        {
                            PathPoint newPathPoint = new(newPoint, 0, pt);
                            searchQueue.Enqueue(newPathPoint, newHeatLoss);
                            visited[visit] = newHeatLoss;
                        }
                    }

                    newPoint = lastPoint is not null
                        ? new Point(pt.Position.X + leftTurn.X, pt.Position.Y + leftTurn.Y)
                        : new Point(0, 1);
                    if (newPoint is { X: >= 0, Y: >= 0 } && newPoint.X < width && newPoint.Y < height && newPoint != target)
                    {
                        int newHeatLoss = heatLoss + city[newPoint.X, newPoint.Y];
                        VisitEntry visit = new(newPoint, leftTurn, 0);
                        if (!visited.TryGetValue(visit, out int minHeatLost) || newHeatLoss < minHeatLost)
                        {
                            PathPoint newPathPoint = new(newPoint, 0, pt);
                            searchQueue.Enqueue(newPathPoint, newHeatLoss);
                            visited[visit] = newHeatLoss;
                        }
                    }
                }
            }

            return finalHeatLoss;
        }
    }
}
