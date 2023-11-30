using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D23
    {
        public static int PartOne(string[] input)
        {
            List<Point> elves = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        elves.Add(new Point(x, y));
                    }
                }
            }

            List<Point[]> movements = new()
            {
                new Point[3] { new Point( 0, -1), new Point(-1, -1), new Point( 1, -1) },
                new Point[3] { new Point( 0,  1), new Point(-1,  1), new Point( 1,  1) },
                new Point[3] { new Point(-1,  0), new Point(-1, -1), new Point(-1,  1) },
                new Point[3] { new Point( 1,  0), new Point( 1, -1), new Point( 1,  1) }
            };

            for (int i = 0; i < 10; i++)
            {
                List<Point> proposals = new();
                for (int e = 0; e < elves.Count; e++)
                {
                    Point elf = elves[e];
                    List<Point> adjacent = new();
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if (dy != 0 || dx != 0)
                            {
                                Point pt = new(elf.X + dx, elf.Y + dy);
                                if (elves.Contains(pt))
                                {
                                    adjacent.Add(pt);
                                }
                            }
                        }
                    }

                    if (adjacent.Count > 0)
                    {
                        bool added = false;
                        foreach (Point[] move in movements)
                        {
                            if (!adjacent.Contains(new Point(elf.X + move[0].X, elf.Y + move[0].Y))
                                && !adjacent.Contains(new Point(elf.X + move[1].X, elf.Y + move[1].Y))
                                && !adjacent.Contains(new Point(elf.X + move[2].X, elf.Y + move[2].Y)))
                            {
                                proposals.Add(new Point(elf.X + move[0].X, elf.Y + move[0].Y));
                                added = true;
                                break;
                            }
                        }
                        if (!added)
                        {
                            proposals.Add(elf);
                        }
                    }
                    else
                    {
                        proposals.Add(elf);
                    }
                }

                HashSet<Point> duplicates = proposals.Where(p => proposals.Count(pp => pp == p) > 1).ToHashSet();
                for (int e = 0; e < elves.Count; e++)
                {
                    if (duplicates.Contains(proposals[e]))
                    {
                        proposals[e] = elves[e];
                    }
                }
                elves = proposals;

                Point[] toEnd = movements[0];
                movements.RemoveAt(0);
                movements.Add(toEnd);
            }

            int empty = 0;
            for (int y = elves.MinBy(e => e.Y).Y; y <= elves.MaxBy(e => e.Y).Y; y++)
            {
                for (int x = elves.MinBy(e => e.X).X; x <= elves.MaxBy(e => e.X).X; x++)
                {
                    if (!elves.Contains(new Point(x, y)))
                    {
                        empty++;
                    }
                }
            }

            return empty;
        }

        public static int PartTwo(string[] input)
        {
            List<Point> elves = new();
            HashSet<Point> elvesSet = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        elves.Add(new Point(x, y));
                        _ = elvesSet.Add(new Point(x, y));
                    }
                }
            }

            List<Point[]> movements = new()
            {
                new Point[3] { new Point( 0, -1), new Point(-1, -1), new Point( 1, -1) },
                new Point[3] { new Point( 0,  1), new Point(-1,  1), new Point( 1,  1) },
                new Point[3] { new Point(-1,  0), new Point(-1, -1), new Point(-1,  1) },
                new Point[3] { new Point( 1,  0), new Point( 1, -1), new Point( 1,  1) }
            };

            int round = 1;
            while (true)
            {
                List<Point> proposals = new();
                int moves = 0;
                for (int e = 0; e < elves.Count; e++)
                {
                    Point elf = elves[e];
                    List<Point> adjacent = new();
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if (dy != 0 || dx != 0)
                            {
                                Point pt = new(elf.X + dx, elf.Y + dy);
                                if (elvesSet.Contains(pt))
                                {
                                    adjacent.Add(pt);
                                }
                            }
                        }
                    }

                    if (adjacent.Count > 0)
                    {
                        bool added = false;
                        foreach (Point[] move in movements)
                        {
                            if (!adjacent.Contains(new Point(elf.X + move[0].X, elf.Y + move[0].Y))
                                && !adjacent.Contains(new Point(elf.X + move[1].X, elf.Y + move[1].Y))
                                && !adjacent.Contains(new Point(elf.X + move[2].X, elf.Y + move[2].Y)))
                            {
                                proposals.Add(new Point(elf.X + move[0].X, elf.Y + move[0].Y));
                                added = true;
                                moves++;
                                break;
                            }
                        }
                        if (!added)
                        {
                            proposals.Add(elf);
                        }
                    }
                    else
                    {
                        proposals.Add(elf);
                    }
                }

                HashSet<Point> duplicates = proposals.Where(p => proposals.Count(pp => pp == p) > 1).ToHashSet();
                for (int e = 0; e < elves.Count; e++)
                {
                    if (duplicates.Contains(proposals[e]))
                    {
                        proposals[e] = elves[e];
                        moves--;
                    }
                }

                if (moves == 0)
                {
                    return round;
                }
                
                elves = proposals;
                elvesSet = proposals.ToHashSet();
                Point[] toEnd = movements[0];
                movements.RemoveAt(0);
                movements.Add(toEnd);
                round++;
            }
        }
    }
}
