namespace AdventOfCode.Yr2022
{
    public static class D03
    {
        public static int PartOne(string[] input)
        {
            int totalPriorities = 0;
            foreach (string line in input)
            {
                string compartmentOne = line[..(line.Length / 2)];
                string compartmentTwo = line[(line.Length / 2)..];
                foreach (char letter in compartmentOne)
                {
                    if (compartmentTwo.Contains(letter))
                    {
                        totalPriorities += letter >= 97 ? letter - 96 : letter - 38;
                        break;
                    }
                }
            }
            return totalPriorities;
        }

        public static int PartTwo(string[] input)
        {
            int totalPriorities = 0;
            for (int i = 0; i < input.Length; i += 3)
            {
                string[] groups = input[i..(i + 3)];
                foreach (char letter in groups[0])
                {
                    if (groups[1].Contains(letter) && groups[2].Contains(letter))
                    {
                        totalPriorities += letter >= 97 ? letter - 96 : letter - 38;
                        break;
                    }
                }
            }
            return totalPriorities;
        }
    }
}
