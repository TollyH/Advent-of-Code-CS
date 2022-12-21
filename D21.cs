using System.Numerics;

namespace AdventOfCode.Yr2022
{
    public static class D21
    {
        public static long PartOne(string[] input)
        {
            Dictionary<string, string> monkeys = new();
            foreach (string line in input)
            {
                string[] split = line.Split(": ");
                monkeys[split[0]] = split[1];
            }

            long ResolveMonkey(string monkey)
            {
                string call = monkeys[monkey];
                if (long.TryParse(call, out long number))
                {
                    return number;
                }
                string[] split = call.Split(' ');
                long monkeyOne = ResolveMonkey(split[0]);
                long monkeyTwo = ResolveMonkey(split[2]);
                return split[1] switch
                {
                    "+" => monkeyOne + monkeyTwo,
                    "-" => monkeyOne - monkeyTwo,
                    "*" => monkeyOne * monkeyTwo,
                    "/" => monkeyOne / monkeyTwo,
                    _ => 0,
                };
            }

            return ResolveMonkey("root");
        }

        public static BigInteger PartTwo(string[] input)
        {
            Dictionary<string, string> monkeys = new();
            foreach (string line in input)
            {
                string[] split = line.Split(": ");
                monkeys[split[0]] = split[1];
            }

            bool ContainsHuman(string monkey)
            {
                if (monkey == "humn")
                {
                    return true;
                }
                if (!BigInteger.TryParse(monkeys[monkey], out _))
                {
                    string[] split = monkeys[monkey].Split(" ");
                    return ContainsHuman(split[0]) || ContainsHuman(split[2]);
                }
                return false;
            }

            (BigInteger, bool) ResolveMonkey(string monkey, BigInteger humanValue, bool cleanDivide)
            {
                if (monkey == "humn")
                {
                    return (humanValue, cleanDivide);
                }
                string call = monkeys[monkey];
                if (BigInteger.TryParse(call, out BigInteger number))
                {
                    return (number, cleanDivide);
                }
                string[] split = call.Split(' ');
                (BigInteger monkeyOne, bool newCleanDivideOne) = ResolveMonkey(split[0], humanValue, cleanDivide);
                (BigInteger monkeyTwo, bool newCleanDivideTwo) = ResolveMonkey(split[2], humanValue, cleanDivide);
                cleanDivide = newCleanDivideOne && newCleanDivideTwo && cleanDivide;
                if (split[1] == "/" && monkeyOne % monkeyTwo != 0)
                {
                    cleanDivide = false;
                }
                return split[1] switch
                {
                    "+" => (monkeyOne + monkeyTwo, cleanDivide),
                    "-" => (monkeyOne - monkeyTwo, cleanDivide),
                    "*" => (monkeyOne * monkeyTwo, cleanDivide),
                    "/" => (monkeyOne / monkeyTwo, cleanDivide),
                    _ => (0, cleanDivide),
                };
            }

            string[] root = monkeys["root"].Split(" ");
            string rootBranchOne = root[0];
            string rootBranchTwo = root[2];
            bool branchOneHuman = ContainsHuman(rootBranchOne);
            BigInteger resolvedOne = 0;
            BigInteger resolvedTwo = 0;
            bool ascending;
            if (branchOneHuman)
            {
                resolvedTwo = ResolveMonkey(rootBranchTwo, 0, true).Item1;
                ascending = ResolveMonkey(rootBranchOne, int.MaxValue, true).Item1 > ResolveMonkey(rootBranchOne, 0, true).Item1;
            }
            else
            {
                resolvedOne = ResolveMonkey(rootBranchOne, 0, true).Item1;
                ascending = ResolveMonkey(rootBranchTwo, int.MaxValue, true).Item1 > ResolveMonkey(rootBranchTwo, 0, true).Item1;
            }

            BigInteger low = 0;
            BigInteger high = ulong.MaxValue;
            while (true)
            {
                BigInteger m = (BigInteger)Math.Ceiling((decimal)(low + high) / 2);
                bool cleanDivide;
                if (branchOneHuman)
                {
                    (resolvedOne, cleanDivide) = ResolveMonkey(rootBranchOne, m, true);
                }
                else
                {
                    (resolvedTwo, cleanDivide) = ResolveMonkey(rootBranchTwo, m, true);
                }
                
                if (resolvedOne == resolvedTwo)
                {
                    while (!cleanDivide)
                    {
                        m--;
                        if (branchOneHuman)
                        {
                            (resolvedOne, cleanDivide) = ResolveMonkey(rootBranchOne, m, true);
                        }
                        else
                        {
                            (resolvedTwo, cleanDivide) = ResolveMonkey(rootBranchTwo, m, true);
                        }
                        if (resolvedOne != resolvedTwo)
                        {
                            m++;
                            break;
                        }
                    }
                    while (!cleanDivide)
                    {
                        m++;
                        if (branchOneHuman)
                        {
                            (resolvedOne, cleanDivide) = ResolveMonkey(rootBranchOne, m, true);
                        }
                        else
                        {
                            (resolvedTwo, cleanDivide) = ResolveMonkey(rootBranchTwo, m, true);
                        }
                        if (resolvedOne != resolvedTwo)
                        {
                            return -1;
                        }
                    }
                    return m;
                }
                if (ascending ^ (!branchOneHuman) ^ (resolvedOne < resolvedTwo))
                {
                    high = m - 1;
                }
                else
                {
                    low = m + 1;
                }
            }
        }
    }
}
