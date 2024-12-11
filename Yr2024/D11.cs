namespace AdventOfCode.Yr2024
{
    public static class D11
    {
        public static int PartOne(string[] input)
        {
            List<long> stones = input[0].Split(' ').Select(long.Parse).ToList();

            for (int i = 0; i < 25; i++)
            {
                List<long> newStones = new();
                foreach (long stone in stones)
                {
                    if (stone == 0)
                    {
                        newStones.Add(1);
                        continue;
                    }

                    string stoneStr = stone.ToString();
                    if (stoneStr.Length % 2 == 0)
                    {
                        newStones.Add(long.Parse(stoneStr[..(stoneStr.Length / 2)]));
                        newStones.Add(long.Parse(stoneStr[(stoneStr.Length / 2)..]));
                        continue;
                    }

                    newStones.Add(stone * 2024);
                }
                stones = newStones;
            }

            return stones.Count;
        }

        public static long PartTwo(string[] input)
        {
            Dictionary<long, long> stones = new();

            foreach (long stone in input[0].Split(' ').Select(long.Parse).ToList())
            {
                _ = stones.TryAdd(stone, 0);
                stones[stone]++;
            }

            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> newStones = new();
                foreach ((long stone, long count) in stones)
                {
                    if (stone == 0)
                    {
                        _ = newStones.TryAdd(1, 0);
                        newStones[1] += count;
                        continue;
                    }

                    string stoneStr = stone.ToString();
                    if (stoneStr.Length % 2 == 0)
                    {
                        long firstHalf = long.Parse(stoneStr[..(stoneStr.Length / 2)]);
                        long secondHalf = long.Parse(stoneStr[(stoneStr.Length / 2)..]);

                        _ = newStones.TryAdd(firstHalf, 0);
                        _ = newStones.TryAdd(secondHalf, 0);

                        newStones[firstHalf] += count;
                        newStones[secondHalf] += count;

                        continue;
                    }

                    long product = stone * 2024;
                    _ = newStones.TryAdd(product, 0);
                    newStones[product] += count;
                }
                stones = newStones;
            }

            return stones.Sum(kv => kv.Value);
        }
    }
}
