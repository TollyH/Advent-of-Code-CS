using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D24
    {
        private static Dictionary<char, Point> directions = new()
        {
            { '>', new Point( 1,  0) },
            { 'v', new Point( 0,  1) },
            { '<', new Point(-1,  0) },
            { '^', new Point( 0, -1) }
        };

        private struct Blizzard
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Direction { get; set; }
        }

        private struct State
        {
            public Point Position { get; set; }
            public int Round { get; set; }
        }

        private static int Mod(int a, int b)
        {
            // Needed because C#'s % operator finds the remainder, not modulo
            return ((a % b) + b) % b;
        }

        public static int PartOne(string[] input)
        {
            List<Blizzard> startBlizzards = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (directions.ContainsKey(input[y][x]))
                    {
                        startBlizzards.Add(new Blizzard() { X = x - 1, Y = y - 1, Direction = input[y][x] });
                    }
                }
            }
            int width = input[0].Length - 2;
            int height = input.Length - 2;
            Point goal = new(input[0].Length - 3, input.Length - 2);

            List<List<Blizzard>> blizzardStates = new() { startBlizzards };
            List<HashSet<Point>> blizzardPositionStates = new() { startBlizzards.Select(b => new Point(b.X, b.Y)).ToHashSet() };

            HashSet<State> visited = new();
            Queue<State> queue = new();
            queue.Enqueue(new State() { Position = new Point(0, -1), Round = 0 });

            while (queue.TryDequeue(out State currentState))
            {
                if (visited.Contains(currentState))
                {
                    continue;
                }
                if (visited.Any() && currentState.Round > visited.First().Round)
                {
                    visited.Clear();
                }
                _ = visited.Add(currentState);

                if (blizzardStates.Count <= currentState.Round + 1)
                {
                    List<Blizzard> newBlizzards = new(blizzardStates[currentState.Round]);
                    HashSet<Point> newBlizzardPositions = new();
                    for (int i = 0; i < newBlizzards.Count; i++)
                    {
                        Point direction = directions[newBlizzards[i].Direction];
                        newBlizzards[i] = new Blizzard()
                        {
                            X = Mod(newBlizzards[i].X + direction.X, width),
                            Y = Mod(newBlizzards[i].Y + direction.Y, height),
                            Direction = newBlizzards[i].Direction
                        };
                        _ = newBlizzardPositions.Add(new Point(newBlizzards[i].X, newBlizzards[i].Y));
                    }
                    blizzardStates.Add(newBlizzards);
                    blizzardPositionStates.Add(newBlizzardPositions);
                }

                HashSet<Point> blizzardPositions = blizzardPositionStates[currentState.Round + 1];

                Point right = new(currentState.Position.X + 1, currentState.Position.Y);
                Point down = new(currentState.Position.X, currentState.Position.Y + 1);
                Point left = new(currentState.Position.X - 1, currentState.Position.Y);
                Point up = new(currentState.Position.X, currentState.Position.Y - 1);

                if (down == goal)
                {
                    return currentState.Round + 1;
                }

                if (!blizzardPositions.Contains(right) && right.Y >= 0 && right.Y < height && right.X >= 0 && right.X < width)
                {
                    queue.Enqueue(new State() { Position = right, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(down) && down.Y >= 0 && down.Y < height && down.X >= 0 && down.X < width)
                {
                    queue.Enqueue(new State() { Position = down, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(left) && left.Y >= 0 && left.Y < height && left.X >= 0 && left.X < width)
                {
                    queue.Enqueue(new State() { Position = left, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(up) && up.Y >= 0 && up.Y < height && up.X >= 0 && up.X < width)
                {
                    queue.Enqueue(new State() { Position = up, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(currentState.Position))
                {
                    queue.Enqueue(new State() { Position = currentState.Position, Round = currentState.Round + 1 });
                }
            }

            return -1;
        }

        public static int PartTwo(string[] input)
        {
            List<Blizzard> startBlizzards = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (directions.ContainsKey(input[y][x]))
                    {
                        startBlizzards.Add(new Blizzard() { X = x - 1, Y = y - 1, Direction = input[y][x] });
                    }
                }
            }
            int width = input[0].Length - 2;
            int height = input.Length - 2;
            Point goal = new(input[0].Length - 3, input.Length - 2);
            int goalsReached = 0;

            List<List<Blizzard>> blizzardStates = new() { startBlizzards };
            List<HashSet<Point>> blizzardPositionStates = new() { startBlizzards.Select(b => new Point(b.X, b.Y)).ToHashSet() };

            HashSet<State> visited = new();
            Queue<State> queue = new();
            queue.Enqueue(new State() { Position = new Point(0, -1), Round = 0 });

            while (queue.TryDequeue(out State currentState))
            {
                if (visited.Contains(currentState))
                {
                    continue;
                }
                if (visited.Any() && currentState.Round > visited.First().Round)
                {
                    visited.Clear();
                }
                _ = visited.Add(currentState);

                if (blizzardStates.Count <= currentState.Round + 1)
                {
                    List<Blizzard> newBlizzards = new(blizzardStates[currentState.Round]);
                    HashSet<Point> newBlizzardPositions = new();
                    for (int i = 0; i < newBlizzards.Count; i++)
                    {
                        Point direction = directions[newBlizzards[i].Direction];
                        newBlizzards[i] = new Blizzard()
                        {
                            X = Mod(newBlizzards[i].X + direction.X, width),
                            Y = Mod(newBlizzards[i].Y + direction.Y, height),
                            Direction = newBlizzards[i].Direction
                        };
                        _ = newBlizzardPositions.Add(new Point(newBlizzards[i].X, newBlizzards[i].Y));
                    }
                    blizzardStates.Add(newBlizzards);
                    blizzardPositionStates.Add(newBlizzardPositions);
                }

                HashSet<Point> blizzardPositions = blizzardPositionStates[currentState.Round + 1];

                Point right = new(currentState.Position.X + 1, currentState.Position.Y);
                Point down = new(currentState.Position.X, currentState.Position.Y + 1);
                Point left = new(currentState.Position.X - 1, currentState.Position.Y);
                Point up = new(currentState.Position.X, currentState.Position.Y - 1);

                if (up == goal || down == goal)
                {
                    visited.Clear();
                    queue.Clear();
                    queue.Enqueue(new State() { Position = goal, Round = currentState.Round + 1 });
                    goalsReached++;
                    goal = goalsReached == 1 ? new Point(0, -1) : new Point(input[0].Length - 3, input.Length - 2);
                    if (goalsReached == 3)
                    {
                        return currentState.Round + 1;
                    }
                    continue;
                }

                if (!blizzardPositions.Contains(right) && right.Y >= 0 && right.Y < height && right.X >= 0 && right.X < width)
                {
                    queue.Enqueue(new State() { Position = right, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(down) && down.Y >= 0 && down.Y < height && down.X >= 0 && down.X < width)
                {
                    queue.Enqueue(new State() { Position = down, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(left) && left.Y >= 0 && left.Y < height && left.X >= 0 && left.X < width)
                {
                    queue.Enqueue(new State() { Position = left, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(up) && up.Y >= 0 && up.Y < height && up.X >= 0 && up.X < width)
                {
                    queue.Enqueue(new State() { Position = up, Round = currentState.Round + 1 });
                }
                if (!blizzardPositions.Contains(currentState.Position))
                {
                    queue.Enqueue(new State() { Position = currentState.Position, Round = currentState.Round + 1 });
                }
            }

            return -1;
        }
    }
}
