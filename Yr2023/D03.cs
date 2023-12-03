using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D03
    {
        public static int PartOne(string[] input)
        {
            int sum = 0;
            for (int y = 0; y < input.Length; y++)
            {
                int currentNumber = 0;
                bool connectedToSymbol = false;
                for (int x = 0; x < input[0].Length; x++)
                {
                    char currentChar = input[y][x];
                    if (char.IsDigit(currentChar))
                    {
                        currentNumber *= 10;
                        currentNumber += currentChar - '0';
                        if (!connectedToSymbol)
                        {
                            for (int dy = y - 1; dy <= y + 1; dy++)
                            {
                                for (int dx = x - 1; dx <= x + 1; dx++)
                                {
                                    if (dx >= 0 && dy >= 0 && dx < input[0].Length && dy < input.Length
                                        && !char.IsDigit(input[dy][dx]) && input[dy][dx] != '.')
                                    {
                                        connectedToSymbol = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (connectedToSymbol)
                        {
                            sum += currentNumber;
                        }
                        currentNumber = 0;
                        connectedToSymbol = false;
                    }
                }
                if (connectedToSymbol)
                {
                    sum += currentNumber;
                }
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            Dictionary<Point, (int Count, int Product)> gears = new();

            for (int y = 0; y < input.Length; y++)
            {
                int currentNumber = 0;
                HashSet<Point> connectedGears = new();
                for (int x = 0; x < input[0].Length; x++)
                {
                    char currentChar = input[y][x];
                    if (char.IsDigit(currentChar))
                    {
                        currentNumber *= 10;
                        currentNumber += currentChar - '0';
                        for (int dy = y - 1; dy <= y + 1; dy++)
                        {
                            for (int dx = x - 1; dx <= x + 1; dx++)
                            {
                                if (dx >= 0 && dy >= 0 && dx < input[0].Length && dy < input.Length
                                    && input[dy][dx] == '*')
                                {
                                    _ = connectedGears.Add(new Point(dx, dy));
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Point gear in connectedGears)
                        {
                            if (gears.ContainsKey(gear))
                            {
                                (int count, int product) = gears[gear];
                                gears[gear] = (count + 1, product * currentNumber);
                            }
                            else
                            {
                                gears[gear] = (1, currentNumber);
                            }
                        }
                        currentNumber = 0;
                        connectedGears.Clear();
                    }
                }
                foreach (Point gear in connectedGears)
                {
                    if (gears.ContainsKey(gear))
                    {
                        (int count, int product) = gears[gear];
                        gears[gear] = (count + 1, product * currentNumber);
                    }
                    else
                    {
                        gears[gear] = (1, currentNumber);
                    }
                }
            }
            return gears.Values.Where(gear => gear.Count == 2).Sum(gear => gear.Product);
        }
    }
}
