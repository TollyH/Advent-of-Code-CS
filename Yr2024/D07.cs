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

        public static long PartTwo(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                string[] components = line.Split(' ');

                long testValue = long.Parse(components[0][..^1]);

                long[] numbers = components.Skip(1).Select(long.Parse).ToArray();
                for (int i = 0; i < (1 << ((numbers.Length - 1) * 2)); i++)
                {
                    long val = numbers[0];
                    bool possible = true;
                    for (int j = 0; j < numbers.Length - 1;)
                    {
                        // Use bits of i to test every permutation of addition, multiplication, and concatenation
                        if (((i >> (j * 2)) & 1) != 0)
                        {
                            if (((i >> (j * 2 + 1)) & 1) != 0)
                            {
                                val *= numbers[++j];
                            }
                            else
                            {
                                val = val * (long)Math.Pow(10, numbers[++j].ToString().Length) + numbers[j];
                            }
                        }
                        else
                        {
                            val += numbers[++j];
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
                }
            }

            return total;
        }
    }
}
