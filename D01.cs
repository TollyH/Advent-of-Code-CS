namespace AdventOfCode
{
    public static class D01
    {
        public static int PartOne(string[] input)
        {
            List<int> elves = new() { 0 };
            int currentElf = 0;
            foreach (string line in input)
            {
                if (line == "")
                {
                    currentElf++;
                    elves.Add(0);
                }
                else
                {
                    elves[currentElf] += int.Parse(line);
                }
            }
            return elves.Max();
        }

        public static int PartTwo(string[] input)
        {
            List<int> elves = new() { 0 };
            int currentElf = 0;
            foreach (string line in input)
            {
                if (line == "")
                {
                    currentElf++;
                    elves.Add(0);
                }
                else
                {
                    elves[currentElf] += int.Parse(line);
                }
            }
            elves.Sort((x, y) => y - x);
            return elves.Take(3).Sum();
        }
    }
}
