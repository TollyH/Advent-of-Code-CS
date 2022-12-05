using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2022
{
    public static class D05
    {
        public static string PartOne(string[] input)
        {
            List<List<char>> crateStacks = new();
            bool parsingCrates = true;
            foreach (string line in input)
            {
                if (parsingCrates)
                {
                    if (line.Length >= 2 && int.TryParse(line[1..2], out _))
                    {
                        parsingCrates = false;
                        continue;
                    }
                    for (int i = 1; i < line.Length; i += 4)
                    {
                        int stackIndex = (i - 1) / 4;
                        if (line[i] != ' ')
                        {
                            while (crateStacks.Count < (stackIndex + 1))
                            {
                                crateStacks.Add(new List<char>());
                            }
                            crateStacks[stackIndex].Insert(0, line[i]);
                        }
                    }
                }
                else
                {
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    GroupCollection groups = Regex.Match(line, "move ([0-9]+) from ([0-9]+) to ([0-9]+)").Groups;
                    int count = int.Parse(groups[1].Value);
                    int source = int.Parse(groups[2].Value) - 1;
                    int destination = int.Parse(groups[3].Value) - 1;
                    for (int i = 0; i < count; i++)
                    {
                        crateStacks[destination].Add(crateStacks[source][^1]);
                        crateStacks[source].RemoveAt(crateStacks[source].Count - 1);
                    }
                }
            }
            string result = "";
            foreach (List<char> stack in crateStacks)
            {
                result += stack[^1];
            }
            return result;
        }

        public static string PartTwo(string[] input)
        {
            List<List<char>> crateStacks = new();
            bool parsingCrates = true;
            foreach (string line in input)
            {
                if (parsingCrates)
                {
                    if (line.Length >= 2 && int.TryParse(line[1..2], out _))
                    {
                        parsingCrates = false;
                        continue;
                    }
                    for (int i = 1; i < line.Length; i += 4)
                    {
                        int stackIndex = (i - 1) / 4;
                        if (line[i] != ' ')
                        {
                            while (crateStacks.Count < (stackIndex + 1))
                            {
                                crateStacks.Add(new List<char>());
                            }
                            crateStacks[stackIndex].Insert(0, line[i]);
                        }
                    }
                }
                else
                {
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    GroupCollection groups = Regex.Match(line, "move ([0-9]+) from ([0-9]+) to ([0-9]+)").Groups;
                    int count = int.Parse(groups[1].Value);
                    int source = int.Parse(groups[2].Value) - 1;
                    int destination = int.Parse(groups[3].Value) - 1;
                    crateStacks[destination].AddRange(crateStacks[source].TakeLast(count));
                    crateStacks[source].RemoveRange(crateStacks[source].Count - count, count);
                }
            }
            string result = "";
            foreach (List<char> stack in crateStacks)
            {
                result += stack[^1];
            }
            return result;
        }
    }
}
