namespace AdventOfCode.Yr2023
{
    public static class D01
    {
        public static int PartOne(string[] input)
        {
            return input.Sum(line =>
                ((line.First(char.IsDigit) - '0') * 10) + (line.Last(char.IsDigit) - '0'));
        }

        public static int PartTwo(string[] input)
        {
            return input.Select(line => line
                    .Replace("one", "o1e")
                    .Replace("two", "t2o")
                    .Replace("three", "t3e")
                    .Replace("four", "f4r")
                    .Replace("five", "f5e")
                    .Replace("six", "s6x")
                    .Replace("seven", "s7n")
                    .Replace("eight", "e8t")
                    .Replace("nine", "n9e"))
                .Select(line =>
                    ((line.First(char.IsDigit) - '0') * 10) + (line.Last(char.IsDigit) - '0'))
                .Sum();
        }
    }
}