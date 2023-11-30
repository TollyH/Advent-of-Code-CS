using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D14
    {
        public static int PartOne(string[] input)
        {
            HashSet<Point> blocked = new();
            int highestY = 0;
            foreach (string line in input)
            {
                Point? lastPoint = null;
                foreach (string coordStr in line.Split(" -> "))
                {
                    string[] split = coordStr.Split(",");
                    Point pnt = new(int.Parse(split[0]), int.Parse(split[1]));
                    if (lastPoint is null)
                    {
                        _ = blocked.Add(pnt);
                    }
                    else
                    {
                        if (pnt.Y > lastPoint.Value.Y)
                        {
                            for (int y = lastPoint.Value.Y; y <= pnt.Y; y++)
                            {
                                _ = blocked.Add(new Point(pnt.X, y));
                            }
                        }
                        else if (pnt.Y < lastPoint.Value.Y)
                        {
                            for (int y = lastPoint.Value.Y; y >= pnt.Y; y--)
                            {
                                _ = blocked.Add(new Point(pnt.X, y));
                            }
                        }
                        else if (pnt.X > lastPoint.Value.X)
                        {
                            for (int x = lastPoint.Value.X; x <= pnt.X; x++)
                            {
                                _ = blocked.Add(new Point(x, pnt.Y));
                            }
                        }
                        else if (pnt.X < lastPoint.Value.X)
                        {
                            for (int x = lastPoint.Value.X; x >= pnt.X; x--)
                            {
                                _ = blocked.Add(new Point(x, pnt.Y));
                            }
                        }
                    }
                    lastPoint = pnt;
                    if (pnt.Y > highestY)
                    {
                        highestY = pnt.Y;
                    }
                }
            }

            int sandCount = 0;
            bool stillFalling = true;
            while (stillFalling)
            {
                sandCount++;
                Point sand = new(500, 0);
                while (true)
                {
                    Point newPnt = sand;
                    newPnt.Y++;
                    if (blocked.Contains(newPnt))
                    {
                        newPnt.X--;
                        if (blocked.Contains(newPnt))
                        {
                            newPnt.X += 2;
                            if (blocked.Contains(newPnt))
                            {
                                break;
                            }
                        }
                    }
                    sand = newPnt;
                    if (newPnt.Y > highestY)
                    {
                        sandCount--;
                        stillFalling = false;
                        break;
                    }
                }
                _ = blocked.Add(sand);
            }

            return sandCount;
        }

        public static int PartTwo(string[] input)
        {
            HashSet<Point> blocked = new();
            int highestY = 0;
            foreach (string line in input)
            {
                Point? lastPoint = null;
                foreach (string coordStr in line.Split(" -> "))
                {
                    string[] split = coordStr.Split(",");
                    Point pnt = new(int.Parse(split[0]), int.Parse(split[1]));
                    if (lastPoint is null)
                    {
                        _ = blocked.Add(pnt);
                    }
                    else
                    {
                        if (pnt.Y > lastPoint.Value.Y)
                        {
                            for (int y = lastPoint.Value.Y; y <= pnt.Y; y++)
                            {
                                _ = blocked.Add(new Point(pnt.X, y));
                            }
                        }
                        else if (pnt.Y < lastPoint.Value.Y)
                        {
                            for (int y = lastPoint.Value.Y; y >= pnt.Y; y--)
                            {
                                _ = blocked.Add(new Point(pnt.X, y));
                            }
                        }
                        else if (pnt.X > lastPoint.Value.X)
                        {
                            for (int x = lastPoint.Value.X; x <= pnt.X; x++)
                            {
                                _ = blocked.Add(new Point(x, pnt.Y));
                            }
                        }
                        else if (pnt.X < lastPoint.Value.X)
                        {
                            for (int x = lastPoint.Value.X; x >= pnt.X; x--)
                            {
                                _ = blocked.Add(new Point(x, pnt.Y));
                            }
                        }
                    }
                    lastPoint = pnt;
                    if (pnt.Y > highestY)
                    {
                        highestY = pnt.Y;
                    }
                }
            }

            int sandCount = 0;
            bool stillFalling = true;
            while (stillFalling)
            {
                sandCount++;
                Point sand = new(500, 0);
                while (true)
                {
                    Point newPnt = sand;
                    newPnt.Y++;
                    if (blocked.Contains(newPnt))
                    {
                        newPnt.X--;
                        if (blocked.Contains(newPnt))
                        {
                            newPnt.X += 2;
                            if (blocked.Contains(newPnt))
                            {
                                stillFalling = sand != new Point(500, 0);
                                break;
                            }
                        }
                    }
                    sand = newPnt;
                    if (sand.Y == highestY + 1)
                    {
                        break;
                    }
                }
                _ = blocked.Add(sand);
            }

            return sandCount;
        }
    }
}
