using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D11
    {
        public static int PartOne(string[] input)
        {
            List<List<bool>> image = input.Select(s => s.Select(c => c == '#').ToList()).ToList();

            for (int y = 0; y < image.Count; y++)
            {
                if (!image[y].Contains(true))
                {
                    image.Insert(y, Enumerable.Repeat(false, image[y].Count).ToList());
                    y++;
                }
            }

            for (int x = 0; x < image[0].Count; x++)
            {
                if (image.All(r => !r[x]))
                {
                    foreach (List<bool> row in image)
                    {
                        row.Insert(x, false);
                    }
                    x++;
                }
            }

            List<Point> galaxies = new();
            for (int y = 0; y < image.Count; y++)
            {
                for (int x = 0; x < image[0].Count; x++)
                {
                    if (image[y][x])
                    {
                        galaxies.Add(new Point(x, y));
                    }
                }
            }

            List<(Point, Point)> pairs = new();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    pairs.Add((galaxies[i], galaxies[j]));
                }
            }

            return pairs.Sum(g => Math.Abs(g.Item1.X - g.Item2.X) + Math.Abs(g.Item1.Y - g.Item2.Y));
        }

        private static readonly int expansionFactor = 1000000;
        public static long PartTwo(string[] input)
        {
            List<List<bool>> image = input.Select(s => s.Select(c => c == '#').ToList()).ToList();

            List<int> expandedRows = new();
            List<int> expandedColumns = new();

            for (int y = 0; y < image.Count; y++)
            {
                if (!image[y].Contains(true))
                {
                    expandedRows.Add(y);
                }
            }

            for (int x = 0; x < image[0].Count; x++)
            {
                if (image.All(r => !r[x]))
                {
                    expandedColumns.Add(x);
                }
            }

            List<Point> galaxies = new();
            int passedRows = 0;
            for (int y = 0; y < image.Count; y++)
            {
                if (passedRows < expandedRows.Count && y >= expandedRows[passedRows])
                {
                    passedRows++;
                }
                int passedColumns = 0;
                for (int x = 0; x < image[0].Count; x++)
                {
                    if (passedColumns < expandedColumns.Count && x >= expandedColumns[passedColumns])
                    {
                        passedColumns++;
                    }
                    if (image[y][x])
                    {
                        galaxies.Add(new Point(
                            x + (passedColumns * (expansionFactor - 1)),
                            y + (passedRows * (expansionFactor - 1))));
                    }
                }
            }

            List<(Point, Point)> pairs = new();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    pairs.Add((galaxies[i], galaxies[j]));
                }
            }

            return pairs.Sum(g => (long)Math.Abs(g.Item1.X - g.Item2.X) + Math.Abs(g.Item1.Y - g.Item2.Y));
        }
    }
}