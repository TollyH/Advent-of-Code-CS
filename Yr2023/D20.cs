using System.Data;

namespace AdventOfCode.Yr2023
{
    public static class D20
    {
        private readonly struct Pulse
        {
            public readonly string Source;
            public readonly string Destination;
            public readonly bool IsHigh;

            public Pulse(string source, string destination, bool isHigh)
            {
                Source = source;
                Destination = destination;
                IsHigh = isHigh;
            }
        }

        private interface IModule
        {
            public string Name { get; }
            public string[] OutputModules { get; }
            public List<string> InputModules { get; }
            public List<Pulse> ReceivePulse(Pulse pulse);
        }

        private class BroadcasterModule : IModule
        {
            public string Name { get; }

            public string[] OutputModules { get; }
            public List<string> InputModules { get; } = new();

            public BroadcasterModule(string name, string[] outputModules)
            {
                Name = name;
                OutputModules = outputModules;
            }

            public List<Pulse> ReceivePulse(Pulse pulse)
            {
                return OutputModules.Select(m => new Pulse(Name, m, pulse.IsHigh)).ToList();
            }
        }

        private class FlipFlopModule : IModule
        {
            public string Name { get; }

            public string[] OutputModules { get; }
            public List<string> InputModules { get; } = new();

            public bool IsPowered = false;

            public FlipFlopModule(string name, string[] outputModules)
            {
                Name = name;
                OutputModules = outputModules;
            }

            public List<Pulse> ReceivePulse(Pulse pulse)
            {
                if (pulse.IsHigh)
                {
                    return new List<Pulse>();
                }
                IsPowered = !IsPowered;
                return OutputModules.Select(m => new Pulse(Name, m, IsPowered)).ToList();
            }
        }

        private class ConjunctionModule : IModule
        {
            public string Name { get; }

            public string[] OutputModules { get; }
            public List<string> InputModules { get; } = new();

            public Dictionary<string, bool> RememberedPulses { get; } = new();

            public ConjunctionModule(string name, string[] outputModules)
            {
                Name = name;
                OutputModules = outputModules;
            }

            public List<Pulse> ReceivePulse(Pulse pulse)
            {
                PopulateDictionary();
                RememberedPulses[pulse.Source] = pulse.IsHigh;
                bool sendHigh = !RememberedPulses.Values.All(p => p);
                return OutputModules.Select(m => new Pulse(Name, m, sendHigh)).ToList();
            }

            private void PopulateDictionary()
            {
                foreach (string input in InputModules)
                {
                    _ = RememberedPulses.TryAdd(input, false);
                }
            }
        }

        private class OutputModule : IModule
        {
            public string Name { get; }

            public string[] OutputModules { get; }
            public List<string> InputModules { get; } = new();

            public OutputModule(string name, string[] outputModules)
            {
                Name = name;
                OutputModules = outputModules;
            }

            public List<Pulse> ReceivePulse(Pulse pulse)
            {
                // Console.WriteLine($"Received {(pulse.IsHigh ? "high" : "low")} pulse from {pulse.Source}");
                return new List<Pulse>();
            }
        }

        public static int PartOne(string[] input)
        {
            Dictionary<string, IModule> modules = new();

            foreach (string line in input)
            {
                string[] components = line.Split(" -> ");
                string[] connectedModules = components[1].Split(", ");
                switch (components[0][0])
                {
                    case '%':
                        modules[components[0][1..]] = new FlipFlopModule(components[0][1..], connectedModules);
                        break;
                    case '&':
                        modules[components[0][1..]] = new ConjunctionModule(components[0][1..], connectedModules);
                        break;
                    default:
                        modules[components[0]] = components[0] switch
                        {
                            "broadcaster" => new BroadcasterModule(components[0], connectedModules),
                            "output" => new OutputModule(components[0], connectedModules),
                            _ => modules[components[0]]
                        };
                        break;
                }

            }
            _ = modules.TryAdd("output", new OutputModule("output", Array.Empty<string>()));

            foreach (IModule module in modules.Values)
            {
                foreach (string output in module.OutputModules)
                {
                    if (!modules.ContainsKey(output))
                    {
                        continue;
                    }
                    modules[output].InputModules.Add(module.Name);
                }
            }

            int lowPulsesSent = 0;
            int highPulsesSent = 0;
            for (int i = 0; i < 1000; i++)
            {
                Queue<Pulse> pulseQueue = new();
                pulseQueue.Enqueue(new Pulse("button", "broadcaster", false));

                while (pulseQueue.TryDequeue(out Pulse pulse))
                {
                    // Console.WriteLine($"{pulse.Source} -{(pulse.IsHigh ? "high" : "low")}-> {pulse.Destination}");
                    if (pulse.IsHigh)
                    {
                        highPulsesSent++;
                    }
                    else
                    {
                        lowPulsesSent++;
                    }
                    if (!modules.ContainsKey(pulse.Destination))
                    {
                        continue;
                    }
                    foreach (Pulse newPulse in modules[pulse.Destination].ReceivePulse(pulse))
                    {
                        pulseQueue.Enqueue(newPulse);
                    }
                }
                // Console.WriteLine();
            }

            return highPulsesSent * lowPulsesSent;
        }

        private static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LowestCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        public static long PartTwo(string[] input)
        {
            Dictionary<string, IModule> modules = new();

            foreach (string line in input)
            {
                string[] components = line.Split(" -> ");
                string[] connectedModules = components[1].Split(", ");
                switch (components[0][0])
                {
                    case '%':
                        modules[components[0][1..]] = new FlipFlopModule(components[0][1..], connectedModules);
                        break;
                    case '&':
                        modules[components[0][1..]] = new ConjunctionModule(components[0][1..], connectedModules);
                        break;
                    default:
                        modules[components[0]] = components[0] switch
                        {
                            "broadcaster" => new BroadcasterModule(components[0], connectedModules),
                            "output" => new OutputModule(components[0], connectedModules),
                            _ => modules[components[0]]
                        };
                        break;
                }

            }
            _ = modules.TryAdd("rx", new OutputModule("rx", Array.Empty<string>()));

            foreach (IModule module in modules.Values)
            {
                foreach (string output in module.OutputModules)
                {
                    modules[output].InputModules.Add(module.Name);
                }
            }

            List<long> loopAmounts = new();
            foreach (string loopStart in modules["broadcaster"].OutputModules)
            {
                string endLowNode = modules[loopStart].OutputModules.Select(m => modules[m]).OfType<ConjunctionModule>().First()
                    .OutputModules.Select(m => modules[m]).OfType<ConjunctionModule>().First().Name;

                bool done = false;
                for (int i = 0; !done; i++)
                {
                    Queue<Pulse> pulseQueue = new();
                    pulseQueue.Enqueue(new Pulse("loopTest", loopStart, false));

                    while (pulseQueue.TryDequeue(out Pulse pulse))
                    {
                        if (pulse.Destination == endLowNode && !pulse.IsHigh)
                        {
                            loopAmounts.Add(i + 1);
                            done = true;
                            break;
                        }
                        foreach (Pulse newPulse in modules[pulse.Destination].ReceivePulse(pulse))
                        {
                            pulseQueue.Enqueue(newPulse);
                        }
                    }
                }
            }

            return loopAmounts.Aggregate(LowestCommonMultiple);
        }
    }
}
