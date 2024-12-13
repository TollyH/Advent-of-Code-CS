using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2024
{
    public static partial class D13
    {
        private readonly record struct Machine(Point AButton, Point BButton, Point Prize);

        [GeneratedRegex("([0-9]+).*?([0-9]+)")]
        public static partial Regex TwoNumberRegex();

        public static int PartOne(string[] input)
        {
            List<Machine> machines = new();

            for (int i = 0; i < input.Length; i++)
            {
                Match lineOne = TwoNumberRegex().Match(input[i++]);
                Match lineTwo = TwoNumberRegex().Match(input[i++]);
                Match lineThree = TwoNumberRegex().Match(input[i++]);

                machines.Add(new Machine(
                    new Point(int.Parse(lineOne.Groups[1].Value), int.Parse(lineOne.Groups[2].Value)),
                    new Point(int.Parse(lineTwo.Groups[1].Value), int.Parse(lineTwo.Groups[2].Value)),
                    new Point(int.Parse(lineThree.Groups[1].Value), int.Parse(lineThree.Groups[2].Value))));
            }

            int total = 0;

            foreach (Machine machine in machines)
            {
                for (int i = 0; i <= 200; i++)
                {
                    bool found = false;
                    int tokens = 0;
                    for (int aPresses = 0; aPresses <= i && aPresses <= 100 && !found; aPresses++)
                    {
                        int bPresses = i - aPresses;
                        if (bPresses > 100)
                        {
                            continue;
                        }
                        Point target = new(
                            (machine.AButton.X * aPresses) + (machine.BButton.X * bPresses),
                            (machine.AButton.Y * aPresses) + (machine.BButton.Y * bPresses));
                        if (target == machine.Prize)
                        {
                            tokens = aPresses * 3 + bPresses;
                            found = true;
                        }
                    }
                    if (found)
                    {
                        total += tokens;
                    }
                }
            }

            return total;
        }

        public static int PartTwo(string[] input)
        {
            return 0;
        }
    }
}
