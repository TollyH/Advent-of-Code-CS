using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2023
{
    public static class D08
    {
        private static readonly Regex nodeRegex = new(@"(...) = \((...), (...)\)");

        private static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LowestCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        public static int PartOne(string[] input)
        {
            string turns = input[0];
            string currentNode = "AAA";
            Dictionary<string, (string Left, string Right)> nodes = new();
            foreach (string line in input[2..])
            {
                GroupCollection match = nodeRegex.Match(line).Groups;
                nodes[match[1].Value] = (match[2].Value, match[3].Value);
            }

            bool reachedEnd = false;
            int ti = 0;
            int i = 0;
            while (!reachedEnd)
            {
                currentNode = turns[i] == 'R' ? nodes[currentNode].Right : nodes[currentNode].Left;
                reachedEnd = currentNode == "ZZZ";
                i = (i + 1) % turns.Length;
                ti++;
            }

            return ti;
        }

        public static long PartTwo(string[] input)
        {
            string turns = input[0];
            Dictionary<string, (string Left, string Right)> nodes = new();
            foreach (string line in input[2..])
            {
                GroupCollection match = nodeRegex.Match(line).Groups;
                nodes[match[1].Value] = (match[2].Value, match[3].Value);
            }
            string[] currentNodes = nodes.Keys.Where(n => n[^1] == 'A').ToArray();
            long[] reachedEndIn = new long[currentNodes.Length];

            bool reachedEnd = false;
            long ti = 0;
            int i = 0;
            while (!reachedEnd)
            {
                ti++;
                for (int j = 0; j < currentNodes.Length; j++)
                {
                    if (reachedEndIn[j] != 0)
                    {
                        continue;
                    }
                    string thisNode = currentNodes[j];
                    currentNodes[j] = turns[i] == 'R' ? nodes[thisNode].Right : nodes[thisNode].Left;
                    if (currentNodes[j].EndsWith('Z'))
                    {
                        reachedEndIn[j] = ti;
                    }
                }
                reachedEnd = reachedEndIn.All(n => n != 0);
                i = (i + 1) % turns.Length;
            }

            return reachedEndIn.Aggregate(LowestCommonMultiple);
        }
    }
}
