using System;
using System.Drawing;
using System.Text;

namespace AdventOfCode.Yr2023
{
    public static class D21
    {
        private static readonly Point[] cardinals = new Point[4]
        {
            new(1, 0),
            new(-1, 0),
            new(0, -1),
            new(0, 1),
        };
        private static readonly int targetStepsPartOne = 64;

        private static readonly HashSet<(Point, int)> processedValues = new();
        private static void TakeSteps(bool[,] map, Point position, HashSet<Point> destinations, int currentDepth, int width, int height)
        {
            if (!processedValues.Add((position, currentDepth)))
            {
                return;
            }
            if (currentDepth == targetStepsPartOne)
            {
                _ = destinations.Add(position);
                return;
            }
            foreach (Point direction in cardinals)
            {
                Point newPosition = new(position.X + direction.X, position.Y + direction.Y);
                if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X >= width || newPosition.Y >= height)
                {
                    continue;
                }
                if (map[newPosition.X, newPosition.Y])
                {
                    continue;
                }
                TakeSteps(map, newPosition, destinations, currentDepth + 1, width, height);
            }
        }

        public static int PartOne(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            Point startPos = new();

            bool[,] map = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = line[x] == '#';
                    if (line[x] == 'S')
                    {
                        startPos = new Point(x, y);
                    }
                }
            }

            HashSet<Point> destinations = new();
            TakeSteps(map, startPos, destinations, 0, width, height);
            return destinations.Count;
        }

        private static int Mod(int a, int b)
        {
            // Needed because C#'s % operator finds the remainder, not modulo
            return ((a % b) + b) % b;
        }

        private static readonly int targetStepsPartTwo = 26501365;

        public static long PartTwo(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            Point startPos = new();

            bool[,] map = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = line[x] == '#';
                    if (line[x] == 'S')
                    {
                        startPos = new Point(x, y);
                    }
                }
            }

            HashSet<Point> destinationsFirst = new();
            HashSet<Point> destinationsSecond = new();
            HashSet<Point> destinationsThird = new();

            HashSet<(Point, int)> queueProcessedValues = new();
            Queue<(Point, int)> positionQueue = new();
            positionQueue.Enqueue((startPos, 0));
            int lastDepth = 0;

            while (positionQueue.TryDequeue(out (Point, int) pos))
            {
                Point position = pos.Item1;
                int currentDepth = pos.Item2;
                if (!queueProcessedValues.Add((position, currentDepth)))
                {
                    continue;
                }
                if (currentDepth > lastDepth)
                {
                    lastDepth = currentDepth;
                    queueProcessedValues.Clear();
                }
                switch (currentDepth)
                {
                    case 65:
                        _ = destinationsFirst.Add(position);
                        break;
                    case 65 + 131:
                        _ = destinationsSecond.Add(position);
                        break;
                    case 65 + 131 + 131:
                        _ = destinationsThird.Add(position);
                        continue;
                }
                foreach (Point direction in cardinals)
                {
                    Point newPosition = new(position.X + direction.X, position.Y + direction.Y);
                    if (map[Mod(newPosition.X, width), Mod(newPosition.Y, height)])
                    {
                        continue;
                    }
                    positionQueue.Enqueue((newPosition, currentDepth + 1));
                }
            }

            long firstSecondDiff = destinationsSecond.Count - destinationsFirst.Count;
            long secondThirdDiff = destinationsThird.Count - destinationsSecond.Count;
            
            long diffOfDiff = secondThirdDiff - firstSecondDiff;
            
            long a = diffOfDiff / 2;
            long b = firstSecondDiff - (a * 3);
            long c = -a - b + destinationsFirst.Count;

            long value = (targetStepsPartTwo - 65) / 131 + 1;

            return (long)(a * Math.Pow(value, 2)) + (b * value) + c;
        }
    }
}
