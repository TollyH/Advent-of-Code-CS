namespace AdventOfCode.Yr2023
{
    public static class D01
    {
        public static int PartOne(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                int value = 0;
                foreach (char c in line.Where(char.IsDigit))
                {
                    value += (c - '0') * 10;
                    break;
                }
                foreach (char c in line.Where(char.IsDigit).Reverse())
                {
                    value += c - '0';
                    break;
                }
                sum += value;
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            int sum = 0;
            foreach (string rawLine in input)
            {
                string line = rawLine
                    .Replace("one", "o1e")
                    .Replace("two", "t2o")
                    .Replace("three", "t3e")
                    .Replace("four", "f4r")
                    .Replace("five", "f5e")
                    .Replace("six", "s6x")
                    .Replace("seven", "s7n")
                    .Replace("eight", "e8t")
                    .Replace("nine", "n9e");
                int value = 0;
                foreach (char c in line.Where(char.IsDigit))
                {
                    value += (c - '0') * 10;
                    break;
                }
                foreach (char c in line.Where(char.IsDigit).Reverse())
                {
                    value += c - '0';
                    break;
                }
                sum += value;
            }
            return sum;
        }
    }
}