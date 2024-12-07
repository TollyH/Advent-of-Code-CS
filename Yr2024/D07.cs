namespace AdventOfCode.Yr2024
{
    public static class D07
    {
        public static long PartOne(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                string[] components = line.Split(' ');

                long testValue = long.Parse(components[0][..^1]);

                long[] numbers = components.Skip(1).Select(long.Parse).ToArray();
                for (int i = 0; i < (1 << (numbers.Length - 1)); i++)
                {
                    long val = numbers[0];
                    bool possible = true;
                    for (int j = 0; j < numbers.Length - 1;)
                    {
                        // Use bits of i to test every permutation of addition and multiplication
                        if (((i >> j) & 1) != 0)
                        {
                            val *= numbers[++j];
                        }
                        else
                        {
                            val += numbers[++j];
                        }

                        if (val > testValue)
                        {
                            // Because we're only adding and multiplying,
                            // if the value ends up bigger than the test value
                            // we can guarantee this permutation will not end up being possible
                            possible = false;
                            break;
                        }
                    }

                    if (possible && val == testValue)
                    {
                        total += testValue;
                        break;
                    }
                }
            }

            return total;
        }

        private enum Operation
        {
            Add,
            Multiply,
            Concat
        }

        public static long PartTwo(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                string[] components = line.Split(' ');

                long testValue = long.Parse(components[0][..^1]);

                long[] numbers = components.Skip(1).Select(long.Parse).ToArray();
                Operation[] operations = new Operation[numbers.Length - 1];
                bool end = false;
                while (!end)
                {
                    long val = numbers[0];
                    bool possible = true;
                    for (int j = 0; j < operations.Length;)
                    {
                        switch (operations[j])
                        {
                            case Operation.Add:
                                val += numbers[++j];
                                break;
                            case Operation.Multiply:
                                val *= numbers[++j];
                                break;
                            case Operation.Concat:
                                val = val * GetBase10Pow(numbers[++j]) + numbers[j];
                                break;
                        }

                        if (val > testValue)
                        {
                            // Because we're only adding, multiplying, and concatenating,
                            // if the value ends up bigger than the test value
                            // we can guarantee this permutation will not end up being possible
                            possible = false;
                            break;
                        }
                    }

                    if (possible && val == testValue)
                    {
                        total += testValue;
                        break;
                    }

                    for (int k = 0; ; k++)
                    {
                        if (k >= operations.Length)
                        {
                            end = true;
                            break;
                        }

                        if (++operations[k] > Operation.Concat)
                        {
                            operations[k] = Operation.Add;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return total;
        }

        // This sacrifices program size for speed
        private static long GetBase10Pow(long num)
        {
            return num switch
            {
                < 1 => 1,
                < 10 => 10,
                < 100 => 100,
                < 1000 => 1000,
                < 10000 => 10000,
                < 100000 => 100000,
                < 1000000 => 1000000,
                < 10000000 => 10000000,
                < 100000000 => 100000000,
                < 1000000000 => 1000000000,
                < 10000000000 => 10000000000,
                < 100000000000 => 100000000000,
                < 1000000000000 => 1000000000000,
                < 10000000000000 => 10000000000000,
                < 100000000000000 => 100000000000000,
                < 1000000000000000 => 1000000000000000,
                < 10000000000000000 => 10000000000000000,
                < 100000000000000000 => 100000000000000000,
                _ => 0
            };
        }
    }
}
