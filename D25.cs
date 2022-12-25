namespace AdventOfCode.Yr2022
{
    public static class D25
    {
        private static readonly Dictionary<char, int> digits = new()
        {
            { '2', 2 },
            { '1', 1 },
            { '0', 0 },
            { '-', -1 },
            { '=', -2 },
        };

        public static string PartOne(string[] input)
        {
            List<long> numbers = new();

            foreach (string line in input)
            {
                long newNum = 0;
                for (int i = 1; i <= line.Length; i++)
                {
                    newNum += digits[line[^i]] * (long)Math.Pow(5, i - 1);
                }
                numbers.Add(newNum);
            }

            long sum = numbers.Sum();

            string snafu = "";
            long remaining = sum;
            bool done = false;
            while (!done)
            {
                long remainingMod = remaining % 5;
                if (remainingMod == 0)
                {
                    snafu = "0" + snafu;
                    remaining /= 5;
                }
                else if (remainingMod == 1)
                {
                    snafu = "1" + snafu;
                    remaining -= 1;
                    remaining /= 5;
                }
                else if (remainingMod == 2)
                {
                    snafu = "2" + snafu;
                    remaining -= 2;
                    remaining /= 5;
                }
                else if (remainingMod == 3)
                {
                    snafu = "=" + snafu;
                    remaining += 2;
                    remaining /= 5;
                }
                else if (remainingMod == 4)
                {
                    snafu = "-" + snafu;
                    remaining += 1;
                    remaining /= 5;
                }

                if (remaining == 0)
                {
                    done = true;
                }
            }

            return snafu;
        }

        public static string PartTwo(string[] input)
        {
            return "Hope you have all the other stars!";
        }
    }
}
