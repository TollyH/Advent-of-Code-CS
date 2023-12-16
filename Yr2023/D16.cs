using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D16
    {
        private class Beam
        {
            public Point Position;
            public Point Direction;
            public bool Dead;

            public Beam(Point position, Point direction)
            {
                Position = position;
                Direction = direction;
            }
        }

        private static int GetEnergizedCount(char[,] contraption, Beam startBeam)
        {
            int width = contraption.GetLength(0);
            int height = contraption.GetLength(1);

            List<Beam> beams = new() { startBeam };
            HashSet<(Point Position, Point Direction)> seenBeforeBeams = new() { (startBeam.Position, startBeam.Direction) };

            while (beams.Any(b => !b.Dead))
            {
                List<Beam> newBeams = new();
                foreach (Beam beam in beams.Where(beam => !beam.Dead))
                {
                    switch (contraption[beam.Position.X, beam.Position.Y])
                    {
                        case '/':
                            beam.Direction = new Point(-beam.Direction.Y, -beam.Direction.X);
                            goto default;
                        case '\\':
                            beam.Direction = new Point(beam.Direction.Y, beam.Direction.X);
                            goto default;
                        default:
                            beam.Position = new Point(beam.Position.X + beam.Direction.X, beam.Position.Y + beam.Direction.Y);
                            if (beam.Position.X < 0 || beam.Position.Y < 0 || beam.Position.X >= width || beam.Position.Y >= height)
                            {
                                beam.Dead = true;
                            }
                            else
                            {
                                if (!seenBeforeBeams.Add((beam.Position, beam.Direction)))
                                {
                                    beam.Dead = true;
                                }
                            }
                            break;
                        case '-' when beam.Direction.Y != 0:
                            beam.Dead = true;
                            if (beam.Position.X > 0)
                            {
                                Beam newBeam = new(beam.Position with { X = beam.Position.X - 1 }, new Point(-1, 0));
                                if (seenBeforeBeams.Add((newBeam.Position, newBeam.Direction)))
                                {
                                    newBeams.Add(newBeam);
                                }
                            }
                            if (beam.Position.X < width - 1)
                            {
                                Beam newBeam = new(beam.Position with { X = beam.Position.X + 1 }, new Point(1, 0));
                                if (seenBeforeBeams.Add((newBeam.Position, newBeam.Direction)))
                                {
                                    newBeams.Add(newBeam);
                                }
                            }
                            break;
                        case '|' when beam.Direction.X != 0:
                            beam.Dead = true;
                            if (beam.Position.Y > 0)
                            {
                                Beam newBeam = new(beam.Position with { Y = beam.Position.Y - 1 }, new Point(0, -1));
                                if (seenBeforeBeams.Add((newBeam.Position, newBeam.Direction)))
                                {
                                    newBeams.Add(newBeam);
                                }
                            }
                            if (beam.Position.Y < height - 1)
                            {
                                Beam newBeam = new(beam.Position with { Y = beam.Position.Y + 1 }, new Point(0, 1));
                                if (seenBeforeBeams.Add((newBeam.Position, newBeam.Direction)))
                                {
                                    newBeams.Add(newBeam);
                                }
                            }
                            break;
                    }
                }
                beams.AddRange(newBeams);
            }

            return seenBeforeBeams.Select(b => b.Position).Distinct().Count();
        }

        public static int PartOne(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;

            char[,] contraption = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    contraption[x, y] = line[x];
                }
            }

            return GetEnergizedCount(contraption, new Beam(new Point(0, 0), new Point(1, 0)));
        }

        public static int PartTwo(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;

            char[,] contraption = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = input[y];
                for (int x = 0; x < width; x++)
                {
                    contraption[x, y] = line[x];
                }
            }

            int max = 0;
            for (int x = 0; x < width; x++)
            {
                int topCount = GetEnergizedCount(contraption, new Beam(new Point(x, 0), new Point(0, 1)));
                if (topCount > max)
                {
                    max = topCount;
                }

                int bottomCount = GetEnergizedCount(contraption, new Beam(new Point(x, height - 1), new Point(0, -1)));
                if (bottomCount > max)
                {
                    max = bottomCount;
                }
            }

            for (int y = 0; y < height; y++)
            {
                int leftCount = GetEnergizedCount(contraption, new Beam(new Point(0, y), new Point(1, 0)));
                if (leftCount > max)
                {
                    max = leftCount;
                }

                int rightCount = GetEnergizedCount(contraption, new Beam(new Point(width - 1, y), new Point(-1, 0)));
                if (rightCount > max)
                {
                    max = rightCount;
                }
            }
            return max;
        }
    }
}
