using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2024
{
    public static partial class D03
    {
        [GeneratedRegex(@"mul\(([0-9]+),([0-9]+)\)")]
        private static partial Regex ValidInstructionP1();

        [GeneratedRegex(@"mul\(([0-9]+),([0-9]+)\)|do\(\)|don't\(\)")]
        private static partial Regex ValidInstructionP2();

        public static int PartOne(string[] input)
        {
            return ValidInstructionP1().Matches(string.Join('\n', input))
                .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
        }

        public static int PartTwo(string[] input)
        {
            int total = 0;
            bool enabled = true;
            foreach (Match instruction in ValidInstructionP2().Matches(string.Join('\n', input)))
            {
                if (instruction.Value == "do()")
                {
                    enabled = true;
                }
                else if (instruction.Value == "don't()")
                {
                    enabled = false;
                }
                else if (enabled)
                {
                    total += int.Parse(instruction.Groups[1].Value) * int.Parse(instruction.Groups[2].Value);
                }
            }
            return total;
        }
    }
}
