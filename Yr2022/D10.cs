namespace AdventOfCode.Yr2022
{
    public static class D10
    {
        public static int PartOne(string[] input)
        {
            int cycle = 0;
            int x = 1;
            List<int> signalStrengths = new();

            void IncrementCycle(int amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    cycle++;
                    if ((cycle + 20) % 40 == 0)
                    {
                        signalStrengths.Add(x * cycle);
                    }
                }
            }
            
            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                string mnemonic = split[0];
                string[] operands = split[1..];
                switch (mnemonic)
                {
                    case "noop":
                        IncrementCycle(1);
                        break;
                    case "addx":
                        IncrementCycle(2);
                        x += int.Parse(operands[0]);
                        break;
                    default:
                        throw new InvalidOperationException($"{mnemonic} is not a recognised instruction.");
                }
            }

            return signalStrengths.Sum();
        }

        public static string PartTwo(string[] input)
        {
            int cycle = 0;
            int x = 1;
            string display = "";

            void IncrementCycle(int amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    int screenX = cycle % 40;
                    display += x > screenX - 2 && x < screenX + 2 ? "█" : " ";
                    if (screenX == 39)
                    {
                        display += "\n";
                    }
                    cycle++;
                }
            }

            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                string mnemonic = split[0];
                string[] operands = split[1..];
                switch (mnemonic)
                {
                    case "noop":
                        IncrementCycle(1);
                        break;
                    case "addx":
                        IncrementCycle(2);
                        x += int.Parse(operands[0]);
                        break;
                    default:
                        throw new InvalidOperationException($"{mnemonic} is not a recognised instruction.");
                }
            }

            return display;
        }
    }
}
