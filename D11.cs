namespace AdventOfCode.Yr2022
{
    public static class D11
    {
        private class Monkey
        {
            public List<ulong> Items { get; private set; }
            public Func<ulong, ulong> Operation { get; private set; }
            public ulong Test { get; private set; }
            public int MonkeyIfTrue { get; private set; }
            public int MonkeyIfFalse { get; private set; }
            public ulong TotalInspected { get; private set; }

            public Monkey(List<ulong> items, Func<ulong, ulong> operation, ulong test, int monkeyIfTrue, int monkeyIfFalse)
            {
                Items = items;
                Operation = operation;
                Test = test;
                MonkeyIfTrue = monkeyIfTrue;
                MonkeyIfFalse = monkeyIfFalse;
                TotalInspected = 0;
            }

            public ulong PopItem()
            {
                TotalInspected++;
                ulong first = Items[0];
                Items.RemoveAt(0);
                return first;
            }
        }

        private static ulong OperatorParse(ulong a, ulong b, string op)
        {
            return op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => 0
            };
        }

        private static ulong ParamParse(string param, ulong old)
        {
            return ulong.TryParse(param, out ulong result) ? result : old;
        }

        public static ulong PartOne(string[] input)
        {
            List<Monkey> monkeys = new();

            int i = 1;
            while (i < input.Length)
            {
                List<ulong> items = input[i][18..].Split(", ").Select(x => ulong.Parse(x)).ToList();
                i++;
                string[] operationStr = input[i][19..].Split(' ');
                Func<ulong, ulong> operation = x => OperatorParse(ParamParse(operationStr[0], x), ParamParse(operationStr[2], x), operationStr[1]);
                i++;
                ulong test = ulong.Parse(input[i][21..]);
                i++;
                int trueMonkey = int.Parse(input[i][29..]);
                i++;
                int falseFonkey = int.Parse(input[i][30..]);
                monkeys.Add(new Monkey(items, operation, test, trueMonkey, falseFonkey));
                i += 3;
            }
            for (int round = 0; round < 20; round++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    Monkey monkey = monkeys[m];
                    while (monkey.Items.Any())
                    {
                        ulong newWorry = monkey.Operation(monkey.PopItem()) / 3;
                        if (newWorry % monkey.Test == 0)
                        {
                            monkeys[monkey.MonkeyIfTrue].Items.Add(newWorry);
                        }
                        else
                        {
                            monkeys[monkey.MonkeyIfFalse].Items.Add(newWorry);
                        }
                    }
                }
            }

            List<ulong> inspected = monkeys.Select(m => m.TotalInspected).ToList();
            inspected.Sort((x, y) => y.CompareTo(x));
            return inspected[0] * inspected[1];
        }

        public static ulong PartTwo(string[] input)
        {
            List<Monkey> monkeys = new();
            ulong commonMultiple = 1;

            int i = 1;
            while (i < input.Length)
            {
                List<ulong> items = input[i][18..].Split(", ").Select(x => ulong.Parse(x)).ToList();
                i++;
                string[] operationStr = input[i][19..].Split(' ');
                Func<ulong, ulong> operation = x => OperatorParse(ParamParse(operationStr[0], x), ParamParse(operationStr[2], x), operationStr[1]);
                i++;
                ulong test = ulong.Parse(input[i][21..]);
                commonMultiple *= test;
                i++;
                int trueMonkey = int.Parse(input[i][29..]);
                i++;
                int falseFonkey = int.Parse(input[i][30..]);
                monkeys.Add(new Monkey(items, operation, test, trueMonkey, falseFonkey));
                i += 3;
            }
            for (int round = 0; round < 10000; round++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    Monkey monkey = monkeys[m];
                    while (monkey.Items.Any())
                    {
                        ulong newWorry = monkey.Operation(monkey.PopItem()) % commonMultiple;
                        if (newWorry % monkey.Test == 0)
                        {
                            monkeys[monkey.MonkeyIfTrue].Items.Add(newWorry);
                        }
                        else
                        {
                            monkeys[monkey.MonkeyIfFalse].Items.Add(newWorry);
                        }
                    }
                }
            }

            List<ulong> inspected = monkeys.Select(m => m.TotalInspected).ToList();
            inspected.Sort((x, y) => y.CompareTo(x));
            return inspected[0] * inspected[1];
        }
    }
}
