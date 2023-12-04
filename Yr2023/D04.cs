namespace AdventOfCode.Yr2023
{
    public static class D04
    {
        public static int PartOne(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                string[] card = line.Split(": ")[1].Split(" | ");
                HashSet<int> winningNumbers = card[0].Split(' ').Where(num => num != "").Select(int.Parse).ToHashSet();
                HashSet<int> ourNumbers = card[1].Split(' ').Where(num => num != "").Select(int.Parse).ToHashSet();
                ourNumbers.IntersectWith(winningNumbers);
                if (ourNumbers.Count > 0)
                {
                    sum += (int)Math.Pow(2, ourNumbers.Count - 1);
                }
            }
            return sum;
        }

        public static int PartTwo(string[] input)
        {
            int[] cards = Enumerable.Repeat(1, input.Length).ToArray();
            for (int i = 0; i < input.Length; i++)
            {
                string[] card = input[i].Split(": ")[1].Split(" | ");
                HashSet<int> winningNumbers = card[0].Split(' ').Where(num => num != "").Select(int.Parse).ToHashSet();
                HashSet<int> ourNumbers = card[1].Split(' ').Where(num => num != "").Select(int.Parse).ToHashSet();
                ourNumbers.IntersectWith(winningNumbers);
                int numOfCards = cards[i];
                for (int di = i + 1; di <= i + ourNumbers.Count; di++)
                {
                    cards[di] += numOfCards;
                }
            }
            return cards.Sum();
        }
    }
}
