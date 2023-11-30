using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D09
    {
        public static int PartOne(string[] input)
        {
            Point head = new();
            Point tail = new();
            HashSet<Point> tailVisited = new() { tail };
            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                string directionLetter = split[0];
                int distance = int.Parse(split[1]);
                Point movementAmount = directionLetter switch
                {
                    "L" => new Point(-1, 0),
                    "R" => new Point(1, 0),
                    "U" => new Point(0, 1),
                    "D" => new Point(0, -1),
                    _ => new Point()
                };
                for (int i = 0; i < distance; i++)
                {
                    head.X += movementAmount.X;
                    head.Y += movementAmount.Y;
                    int tailXDiff = head.X - tail.X;
                    int tailYDiff = head.Y - tail.Y;
                    if ((tailXDiff is < -1 or > 1 && tailYDiff == 0) ^ (tailYDiff is < -1 or > 1 && tailXDiff == 0))
                    {
                        tail.X += movementAmount.X;
                        tail.Y += movementAmount.Y;
                        _ = tailVisited.Add(tail);
                    }
                    else if (tailXDiff is < -1 or > 1 || tailYDiff is < -1 or > 1)
                    {
                        tail.X += movementAmount.X;
                        tail.Y += movementAmount.Y;
                        if (movementAmount.X != 0)
                        {
                            tail.Y = head.Y;
                        }
                        else
                        {
                            tail.X = head.X;
                        }
                        _ = tailVisited.Add(tail);
                    }
                }
            }
            return tailVisited.Count;
        }

        public static int PartTwo(string[] input)
        {
            Point[] knots = new Point[10];
            HashSet<Point> tailVisited = new() { knots[9] };
            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                string directionLetter = split[0];
                int distance = int.Parse(split[1]);
                Point headMovementAmount = directionLetter switch
                {
                    "L" => new Point(-1, 0),
                    "R" => new Point(1, 0),
                    "U" => new Point(0, 1),
                    "D" => new Point(0, -1),
                    _ => new Point()
                };
                for (int i = 0; i < distance; i++)
                {
                    knots[0].X += headMovementAmount.X;
                    knots[0].Y += headMovementAmount.Y;
                    Point lastMovementAmount = headMovementAmount;
                    for (int ik = 1; ik < knots.Length; ik++)
                    {
                        int knotXDiff = knots[ik - 1].X - knots[ik].X;
                        int knotYDiff = knots[ik - 1].Y - knots[ik].Y;
                        if ((knotXDiff is < -1 or > 1 && knotYDiff == 0) ^ (knotYDiff is < -1 or > 1 && knotXDiff == 0))
                        {
                            if (knotXDiff == 0)
                            {
                                lastMovementAmount.X = 0;
                                lastMovementAmount.Y = knotYDiff > 0 ? 1 : -1;
                                knots[ik].Y += lastMovementAmount.Y;
                            }
                            else
                            {
                                lastMovementAmount.X = knotXDiff > 0 ? 1 : -1;
                                lastMovementAmount.Y = 0;
                                knots[ik].X += lastMovementAmount.X;
                            }
                            if (ik == knots.Length - 1)
                            {
                                _ = tailVisited.Add(knots[ik]);
                            }
                        }
                        else if (knotXDiff is < -1 or > 1 || knotYDiff is < -1 or > 1)
                        {
                            knots[ik].X += lastMovementAmount.X;
                            knots[ik].Y += lastMovementAmount.Y;
                            if (lastMovementAmount.X != 0)
                            {
                                knotYDiff = knots[ik - 1].Y - knots[ik].Y;
                                knots[ik].Y += knotYDiff > 0 ? 1 : -1;
                            }
                            else
                            {
                                knotXDiff = knots[ik - 1].X - knots[ik].X;
                                knots[ik].X += knotXDiff > 0 ? 1 : -1;
                            }
                            if (ik == knots.Length - 1)
                            {
                                _ = tailVisited.Add(knots[ik]);
                            }
                        }
                    }
                }
            }
            return tailVisited.Count;
        }
    }
}
