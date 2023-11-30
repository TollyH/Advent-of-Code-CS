using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2022
{
    public static class D15
    {
        private static int Distance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static int PartOne(string[] input)
        {
            int yToCheck = 2000000;
            Dictionary<Point, Point> sensors = new();
            HashSet<int> filledX = new();
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, "Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)").Groups;
                sensors[new Point(int.Parse(groups[1].Value), int.Parse(groups[2].Value))] = new Point(int.Parse(groups[3].Value), int.Parse(groups[4].Value));
            }
            foreach (Point sensor in sensors.Keys)
            {
                Point beacon = sensors[sensor];
                int distance = Distance(sensor, beacon);
                if (sensor.Y - distance <= yToCheck && sensor.Y + distance >= yToCheck)
                {
                    int widthOffset = Math.Abs(distance - (yToCheck - (sensor.Y - distance)));
                    for (int x = sensor.X - distance + widthOffset; x <= sensor.X + distance - widthOffset; x++)
                    {
                        if (x != beacon.X || yToCheck != beacon.Y)
                        {
                            _ = filledX.Add(x);
                        }
                    }
                }
            }
            return filledX.Count;
        }

        public static long PartTwo(string[] input)
        {
            int coordLimit = 4000000;
            Dictionary<Point, int> sensors = new();
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, "Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)").Groups;
                Point sensor = new(int.Parse(groups[1].Value), int.Parse(groups[2].Value));
                Point beacon = new(int.Parse(groups[3].Value), int.Parse(groups[4].Value));
                int distance = Distance(sensor, beacon);
                sensors[sensor] = distance;
            }
            foreach (Point sensor in sensors.Keys)
            {
                int distance = sensors[sensor];
                distance++;
                int i = 0;
                for (int y = Math.Max(0, sensor.Y - distance); y <= sensor.Y + distance; y++)
                {
                    if (y > coordLimit)
                    {
                        break;
                    }
                    Point pt1 = new(sensor.X - distance + Math.Abs(distance - i), y);
                    Point pt2 = new(sensor.X + distance - Math.Abs(distance - i), y);
                    bool pt1Answer = true;
                    bool pt2Answer = true;
                    foreach (Point otherSensor in sensors.Keys)
                    {
                        int otherDistance = sensors[otherSensor];
                        if (pt1Answer && (pt1.X < 0 || pt1.X > coordLimit || Distance(pt1, otherSensor) <= otherDistance))
                        {
                            pt1Answer = false;
                        }
                        if (pt2Answer && (pt2.X < 0 || pt2.X > coordLimit || Distance(pt2, otherSensor) <= otherDistance))
                        {
                            pt2Answer = false;
                        }
                    }
                    if (pt1Answer)
                    {
                        return (pt1.X * 4000000L) + pt1.Y;
                    }
                    if (pt2Answer)
                    {
                        return (pt2.X * 4000000L) + pt2.Y;
                    }
                    i++;
                }
            }
            return -1;
        }
    }
}
