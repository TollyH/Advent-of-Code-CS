namespace AdventOfCode.Yr2023
{
    public static class D06
    {
        public static int PartOne(string[] input)
        {
            int[] times = input[0].Split(':')[1].Split(' ').Where(t => t != "").Select(int.Parse).ToArray();
            int[] distances = input[1].Split(':')[1].Split(' ').Where(t => t != "").Select(int.Parse).ToArray();

            int product = 1;
            for (int i = 0; i < times.Length; i++)
            {
                int time = times[i];
                int targetDistance = distances[i] + 1;
                // Get difference between roots of quadratic equation "(-x^2) + (time)x - (targetDistance)"
                product *= (int)(Math.Floor((-time - Math.Sqrt((time * time) - (4 * targetDistance))) / -2)
                    - Math.Ceiling((-time + Math.Sqrt((time * time) - (4 * targetDistance))) / -2) + 1);
            }
            return product;
        }

        public static long PartTwo(string[] input)
        {
            long time = long.Parse(input[0].Split(':')[1].Replace(" ", ""));
            long targetDistance = long.Parse(input[1].Split(':')[1].Replace(" ", "")) + 1;
            // Get difference between roots of quadratic equation "(-x^2) + (time)x - (targetDistance)"
            return (long)(Math.Floor((-time - Math.Sqrt((time * time) - (4 * targetDistance))) / -2)
                - Math.Ceiling((-time + Math.Sqrt((time * time) - (4 * targetDistance))) / -2) + 1);
        }
    }
}
