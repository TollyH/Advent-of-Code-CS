namespace AdventOfCode.Yr2023
{
    public static class D02
    {
        public static int PartOne(string[] input)
        {
            Dictionary<string, int> maxCubes = new()
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                bool possible = true;
                string line = input[i].Split(": ")[1];
                foreach (string draw in line.Split("; "))
                {
                    foreach (string blockDraw in draw.Split(", "))
                    {
                        string[] components = blockDraw.Split(' ');
                        int cubeCount = int.Parse(components[0]);
                        if (cubeCount > maxCubes[components[1]])
                        {
                            possible = false;
                            break;
                        }
                    }
                }
                if (possible)
                {
                    sum += i + 1;
                }
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                Dictionary<string, int> maxCubes = new()
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                string game = line.Split(": ")[1];
                foreach (string draw in game.Split("; "))
                {
                    foreach (string blockDraw in draw.Split(", "))
                    {
                        string[] components = blockDraw.Split(' ');
                        int cubeCount = int.Parse(components[0]);
                        if (cubeCount > maxCubes[components[1]])
                        {
                            maxCubes[components[1]] = cubeCount;
                        }
                    }
                }
                sum += maxCubes.Values.Aggregate(1, (a, b) => a * b);
            }
            return sum;
        }
    }
}
