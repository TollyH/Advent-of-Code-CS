using System.Drawing;

namespace AdventOfCode.Yr2023
{
    public static class D18
    {
        private static readonly Dictionary<char, Point> directions = new()
        {
            { 'U', new Point(0, -1) },
            { 'D', new Point(0, 1) },
            { 'L', new Point(-1, 0) },
            { 'R', new Point(1, 0) },
            { '3', new Point(0, -1) },
            { '1', new Point(0, 1) },
            { '2', new Point(-1, 0) },
            { '0', new Point(1, 0) },
        };

        public static int PartOne(string[] input)
        {
            Point currentPosition = new();
            List<Point> vertices = new();
            int dugPoints = 0;

            foreach (string line in input)
            {
                string[] components = line.Split(' ');
                Point direction = directions[components[0][0]];
                int count = int.Parse(components[1]);
                vertices.Add(currentPosition);
                currentPosition = new Point(currentPosition.X + (direction.X * count), currentPosition.Y + (direction.Y * count));
                dugPoints += count;
            }

            int area = 0;
            for (int i = 0; i < vertices.Count; i++)
            {
                int next = (i + 1) % vertices.Count;
                area += (vertices[i].X * vertices[next].Y) - (vertices[next].X * vertices[i].Y);
            }
            area = Math.Abs(area) / 2;
            return area + (dugPoints / 2) + 1;
        }

        private readonly struct LongPoint
        {
            public readonly long X;
            public readonly long Y;

            public LongPoint(long x, long y)
            {
                X = x;
                Y = y;
            }
        }

        public static long PartTwo(string[] input)
        {
            LongPoint currentPosition = new();
            List<LongPoint> vertices = new();
            long dugPoints = 0;

            foreach (string line in input)
            {
                string[] components = line.Split(' ');
                Point direction = directions[components[2][^2]];
                int count = Convert.ToInt32(components[2][2..^2], 16);
                vertices.Add(currentPosition);
                currentPosition = new LongPoint(currentPosition.X + (direction.X * count), currentPosition.Y + (direction.Y * count));
                dugPoints += count;
            }

            long area = 0;
            for (int i = 0; i < vertices.Count; i++)
            {
                int next = (i + 1) % vertices.Count;
                area += (vertices[i].X * vertices[next].Y) - (vertices[next].X * vertices[i].Y);
            }
            area = Math.Abs(area) / 2;
            return area + (dugPoints / 2) + 1;
        }
    }
}
