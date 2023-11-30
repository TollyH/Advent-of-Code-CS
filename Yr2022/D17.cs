using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2022
{
    public static class D17
    {
        private static readonly List<bool[,]> shapes = new()
        {
            new bool[1, 4]
            {
                { true, true, true, true }
            },
            new bool[3, 3]
            {
                { false, true, false },
                { true, true, true },
                { false, true, false }
            },
            new bool[3, 3]
            {
                { true, true, true },
                { false, false, true },
                { false, false, true }
            },
            new bool[4, 1]
            {
                { true },
                { true },
                { true },
                { true }
            },
            new bool[2, 2]
            {
                { true, true },
                { true, true },
            }
        };

        public static int PartOne(string[] input)
        {
            string movements = input[0];
            List<bool[]> chamber = new();
            int currentShape = 0;
            int shapeCount = shapes.Count;
            int currentMovement = 0;
            int movementCount = movements.Length;

            bool Collided(int x, int y)
            {
                if (x < 0 || y < 0 || x + shapes[currentShape].GetLength(1) - 1 >= 7)
                {
                    return true;
                }
                if (y >= chamber.Count)
                {
                    return false;
                }
                for (int cy = y; cy < y + shapes[currentShape].GetLength(0); cy++)
                {
                    if (cy >= chamber.Count)
                    {
                        break;
                    }
                    int sy = cy - y;
                    for (int cx = x; cx < x + shapes[currentShape].GetLength(1); cx++)
                    {
                        int sx = cx - x;
                        if (shapes[currentShape][sy, sx] && chamber[cy][cx])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            int completedRocks = 0;
            while (completedRocks < 2022)
            {
                int x = 2;
                int y = chamber.Count + 3;
                bool stillMoving = true;
                while (stillMoving)
                {
                    if (movements[currentMovement] == '>' && !Collided(x + 1, y))
                    {
                        x++;
                    }
                    else if (movements[currentMovement] == '<' && !Collided(x - 1, y))
                    {
                        x--;
                    }
                    currentMovement = (currentMovement + 1) % movementCount;

                    if (!Collided(x, y - 1))
                    {
                        y--;
                    }
                    else
                    {
                        while (chamber.Count < y + shapes[currentShape].GetLength(0))
                        {
                            chamber.Add(new bool[7]);
                        }
                        for (int cy = y; cy < y + shapes[currentShape].GetLength(0); cy++)
                        {
                            int sy = cy - y;
                            for (int cx = x; cx < x + shapes[currentShape].GetLength(1); cx++)
                            {
                                int sx = cx - x;
                                chamber[cy][cx] = shapes[currentShape][sy, sx] || chamber[cy][cx];
                            }
                        }
                        stillMoving = false;
                    }
                }
                completedRocks++;
                currentShape = (currentShape + 1) % shapeCount;
            }

            return chamber.Count;
        }

        public static long PartTwo(string[] input)
        {
            string movements = input[0];
            List<bool[]> chamber = new();
            int currentShape = 0;
            int shapeCount = shapes.Count;
            int currentMovement = 0;
            int movementCount = movements.Length;

            bool Collided(int x, int y)
            {
                if (x < 0 || y < 0 || x + shapes[currentShape].GetLength(1) - 1 >= 7)
                {
                    return true;
                }
                if (y >= chamber.Count)
                {
                    return false;
                }
                for (int cy = y; cy < y + shapes[currentShape].GetLength(0); cy++)
                {
                    if (cy >= chamber.Count)
                    {
                        break;
                    }
                    int sy = cy - y;
                    for (int cx = x; cx < x + shapes[currentShape].GetLength(1); cx++)
                    {
                        int sx = cx - x;
                        if (shapes[currentShape][sy, sx] && chamber[cy][cx])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            Dictionary<int, int> completedAtRow = new();
            Dictionary<int, int> rowsAtBlockDrop = new();
            int completedRocks = 0;
            while (completedRocks < 5000)
            {
                int x = 2;
                int y = chamber.Count + 3;
                bool stillMoving = true;
                while (stillMoving)
                {
                    if (movements[currentMovement] == '>' && !Collided(x + 1, y))
                    {
                        x++;
                    }
                    else if (movements[currentMovement] == '<' && !Collided(x - 1, y))
                    {
                        x--;
                    }
                    currentMovement = (currentMovement + 1) % movementCount;

                    if (!Collided(x, y - 1))
                    {
                        y--;
                    }
                    else
                    {
                        while (chamber.Count < y + shapes[currentShape].GetLength(0))
                        {
                            chamber.Add(new bool[7]);
                        }
                        for (int cy = y; cy < y + shapes[currentShape].GetLength(0); cy++)
                        {
                            int sy = cy - y;
                            for (int cx = x; cx < x + shapes[currentShape].GetLength(1); cx++)
                            {
                                int sx = cx - x;
                                chamber[cy][cx] = shapes[currentShape][sy, sx] || chamber[cy][cx];
                            }
                        }
                        stillMoving = false;
                    }
                }
                completedRocks++;
                rowsAtBlockDrop[completedRocks] = chamber.Count;
                for (int ry = y; ry < y + shapes[currentShape].GetLength(0); ry++)
                {
                    completedAtRow[ry] = completedRocks;
                }
                currentShape = (currentShape + 1) % shapeCount;
            }

            int periodHeight = 0;
            int completedPerPeriod = 0;
            for (int y1 = chamber.Count - 1; y1 >= 0; y1--)
            {
                int matched = 0;
                for (int y2 = y1 - 1; y2 >= 0; y2--)
                {
                    if (Enumerable.SequenceEqual(chamber[y1 - matched], chamber[y2]))
                    {
                        matched++;
                        if (matched == 20)
                        {
                            periodHeight = y1 - matched - y2 + 1;
                            completedPerPeriod = completedAtRow[y1 - matched + 1] - completedAtRow[y2];
                            break;
                        }
                    }
                    else
                    {
                        matched = 0;
                    }
                }
                if (periodHeight != 0)
                {
                    break;
                }
            }

            long fullPeriods = 1000000000000 / completedPerPeriod;
            long remainingBlockDrops = 1000000000000 % completedPerPeriod;
            long total = (fullPeriods * periodHeight) + rowsAtBlockDrop[(int)remainingBlockDrops];

            return total;
        }
    }
}
