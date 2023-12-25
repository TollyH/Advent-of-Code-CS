namespace AdventOfCode.Yr2023
{
    public static class D25
    {
        public static int PartOne(string[] input)
        {
            // Found manually with GraphViz (will vary based on input)
            List<(string, string)> cutWires = new() { ("kzh", "rks"), ("tnz", "dgt"), ("ddc", "gqm") };

            HashSet<string> allComponents = new();
            Dictionary<string, List<string>> componentMap = new();

            foreach (string line in input)
            {
                string[] components = line.Replace(":", "").Split(' ');
                allComponents.UnionWith(components);
                componentMap.TryAdd(components[0], new List<string>());
                foreach (string otherComponent in components[1..])
                {
                    componentMap.TryAdd(otherComponent, new List<string>());
                    if (!cutWires.Contains((components[0], otherComponent))
                        && !cutWires.Contains((otherComponent, components[0])))
                    {
                        componentMap[components[0]].Add(otherComponent);
                        componentMap[otherComponent].Add(components[0]);
                    }
                }
            }

            HashSet<string> firstGroup = new();
            Stack<string> resolverStack = new();
            resolverStack.Push(allComponents.First());

            while (resolverStack.TryPop(out string? src))
            {
                if (!firstGroup.Add(src))
                {
                    continue;
                }
                foreach (string dst in componentMap[src])
                {
                    resolverStack.Push(dst);
                }
            }

            return firstGroup.Count * (allComponents.Count - firstGroup.Count);
        }

        public static string PartTwo(string[] input)
        {
            return "Merry Christmas :)";
        }
    }
}
