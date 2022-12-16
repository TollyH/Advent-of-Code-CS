using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2022
{
    public static class D16
    {
        public static int PartOne(string[] input)
        {
            Dictionary<string, (int, string[])> valves = new();
            List<string> nonZero = new();
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, "Valve ([A-Z]{2}) has flow rate=([0-9]+); tunnels? leads? to valves? ([A-Z, ]+)").Groups;
                string valve = groups[1].Value;
                int flowRate = int.Parse(groups[2].Value);
                valves[valve] = (flowRate, groups[3].Value.Split(", "));
                if (flowRate > 0)
                {
                    nonZero.Add(valve);
                }
            }

            Dictionary<(string, string), int> pathLengths = new();

            foreach (string start in valves.Keys)
            {
                foreach (string destination in valves.Keys)
                {
                    Dictionary<string, int> distances = new();
                    Dictionary<string, string> previous = new();

                    HashSet<string> unvisited = new();
                    HashSet<string> visited = new();

                    foreach (string v in valves.Keys)
                    {
                        distances[v] = int.MaxValue;
                        previous[v] = "";
                        _ = unvisited.Add(v);
                    }
                    distances[start] = 0;

                    Queue<string> queue = new();
                    queue.Enqueue(start);

                    while (unvisited.Count > 0)
                    {
                        string currentValve = queue.Dequeue();

                        if (currentValve == destination)
                        {
                            break;
                        }

                        foreach (string neighbour in valves[currentValve].Item2)
                        {
                            if (visited.Contains(neighbour))
                            {
                                continue;
                            }
                            int newDistance = distances[currentValve] + 1;
                            if (newDistance < distances[neighbour])
                            {
                                distances[neighbour] = newDistance;
                                previous[neighbour] = currentValve;
                                _ = unvisited.Remove(neighbour);
                                _ = visited.Add(neighbour);
                                queue.Enqueue(neighbour);
                            }
                        }
                    }

                    List<string> finalPath = new();
                    string current = destination;
                    while (current != "")
                    {
                        finalPath.Add(current);
                        current = previous[current];
                    }
                    pathLengths[(start, destination)] = finalPath.Count;
                }
            }

            List<List<string>> GetAllValveOrders(string currentValve, List<string> openValves, int remainingMinutes)
            {
                List<List<string>> orders = new();
                foreach (string valve in nonZero)
                {
                    if (!openValves.Contains(valve) && pathLengths[(currentValve, valve)] < remainingMinutes)
                    {
                        List<string> newValves = new(openValves) { valve };
                        orders.Add(new List<string>(newValves));
                        orders.AddRange(GetAllValveOrders(valve, newValves, remainingMinutes - pathLengths[(currentValve, valve)]));
                    }
                }
                return orders;
            }

            int best = 0;
            foreach (List<string> order in GetAllValveOrders("AA", new List<string>(), 30))
            {
                int remainingMinutes = 30;
                string currentValve = "AA";
                int pressure = 0;
                foreach (string valve in order)
                {
                    remainingMinutes -= pathLengths[(currentValve, valve)];
                    pressure += valves[valve].Item1 * remainingMinutes;
                    currentValve = valve;
                }
                if (pressure > best)
                {
                    best = pressure;
                }
            }
            return best;
        }

        public static int PartTwo(string[] input)
        {
            Dictionary<string, (int, string[])> valves = new();
            List<string> nonZero = new();
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, "Valve ([A-Z]{2}) has flow rate=([0-9]+); tunnels? leads? to valves? ([A-Z, ]+)").Groups;
                string valve = groups[1].Value;
                int flowRate = int.Parse(groups[2].Value);
                valves[valve] = (flowRate, groups[3].Value.Split(", "));
                if (flowRate > 0)
                {
                    nonZero.Add(valve);
                }
            }

            Dictionary<(string, string), int> pathLengths = new();

            foreach (string start in valves.Keys)
            {
                foreach (string destination in valves.Keys)
                {
                    Dictionary<string, int> distances = new();
                    Dictionary<string, string> previous = new();

                    HashSet<string> unvisited = new();
                    HashSet<string> visited = new();

                    foreach (string v in valves.Keys)
                    {
                        distances[v] = int.MaxValue;
                        previous[v] = "";
                        _ = unvisited.Add(v);
                    }
                    distances[start] = 0;

                    Queue<string> queue = new();
                    queue.Enqueue(start);

                    while (unvisited.Count > 0)
                    {
                        string currentValve = queue.Dequeue();

                        if (currentValve == destination)
                        {
                            break;
                        }

                        foreach (string neighbour in valves[currentValve].Item2)
                        {
                            if (visited.Contains(neighbour))
                            {
                                continue;
                            }
                            int newDistance = distances[currentValve] + 1;
                            if (newDistance < distances[neighbour])
                            {
                                distances[neighbour] = newDistance;
                                previous[neighbour] = currentValve;
                                _ = unvisited.Remove(neighbour);
                                _ = visited.Add(neighbour);
                                queue.Enqueue(neighbour);
                            }
                        }
                    }

                    List<string> finalPath = new();
                    string current = destination;
                    while (current != "")
                    {
                        finalPath.Add(current);
                        current = previous[current];
                    }
                    pathLengths[(start, destination)] = finalPath.Count;
                }
            }

            List<List<string>> GetAllValveOrders(string currentValve, List<string> openValves, int remainingMinutes)
            {
                List<List<string>> orders = new();
                foreach (string valve in nonZero)
                {
                    if (!openValves.Contains(valve) && pathLengths[(currentValve, valve)] < remainingMinutes)
                    {
                        List<string> newValves = new(openValves) { valve };
                        orders.Add(new List<string>(newValves));
                        orders.AddRange(GetAllValveOrders(valve, newValves, remainingMinutes - pathLengths[(currentValve, valve)]));
                    }
                }
                return orders;
            }

            List<(List<string>, int)> orderScores = new();
            foreach (List<string> order in GetAllValveOrders("AA", new List<string>(), 26))
            {
                int remainingMinutes = 26;
                string currentValve = "AA";
                int pressure = 0;
                foreach (string valve in order)
                {
                    remainingMinutes -= pathLengths[(currentValve, valve)];
                    pressure += valves[valve].Item1 * remainingMinutes;
                    currentValve = valve;
                }
                orderScores.Add((order, pressure));
            }

            int[] best = new int[Environment.ProcessorCount];
            int perThread = orderScores.Count / Environment.ProcessorCount;
            int completedProcessors = 0;
            for (int processor = 0; processor < Environment.ProcessorCount; processor++)
            {
                int thisProcessor = processor;
                Thread processThread = new(() =>
                {
                    for (int i = perThread * thisProcessor; i < perThread * (thisProcessor + 1); i++)
                    {
                        for (int e = i + 1; e < orderScores.Count; e++)
                        {
                            (List<string> iOrder, int iScore) = orderScores[i];
                            (List<string> eOrder, int eScore) = orderScores[e];
                            if (!iOrder.Intersect(eOrder).Any())
                            {
                                if (iScore + eScore > best[thisProcessor])
                                {
                                    best[thisProcessor] = iScore + eScore;
                                }
                            }
                        }
                    }
                    completedProcessors++;
                });
                processThread.Start();
            }

            while (completedProcessors != Environment.ProcessorCount) { }

            return best.Max();
        }
    }
}
