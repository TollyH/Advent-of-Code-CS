using System.Numerics;

namespace AdventOfCode.Yr2022
{
    public static class D18
    {
        private static readonly Vector3[] adjacent = new Vector3[6]
        {
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
        };

        public static int PartOne(string[] input)
        {
            Dictionary<Vector3, int> exposed = new();
            HashSet<Vector3> cubes = new();

            foreach (string line in input)
            {
                string[] split = line.Split(',');
                Vector3 newPos = new(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
                _ = cubes.Add(newPos);
                _ = exposed.Remove(newPos);
                foreach (Vector3 adj in adjacent)
                {
                    Vector3 side = newPos + adj;
                    if (!cubes.Contains(side))
                    {
                        if (exposed.ContainsKey(side))
                        {
                            exposed[side]++;
                        }
                        else
                        {
                            exposed[side] = 1;
                        }
                    }
                }
            }

            return exposed.Sum(x => x.Value);
        }

        public static int PartTwo(string[] input)
        {
            Dictionary<Vector3, int> exposed = new();
            HashSet<Vector3> cubes = new();
            Vector3 searchStart = new();
            Vector3 searchBound = new();

            foreach (string line in input)
            {
                string[] split = line.Split(',');
                Vector3 newPos = new(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
                _ = cubes.Add(newPos);
                _ = exposed.Remove(newPos);
                foreach (Vector3 adj in adjacent)
                {
                    Vector3 side = newPos + adj;
                    Vector3 newStart = side - new Vector3(1);
                    if (newStart.X < searchStart.X)
                    {
                        searchStart.X = newStart.X;
                    }
                    if (newStart.Y < searchStart.Y)
                    {
                        searchStart.Y = newStart.Y;
                    }
                    if (newStart.Z < searchStart.Z)
                    {
                        searchStart.Z = newStart.Z;
                    }
                    Vector3 newBound = side + new Vector3(1);
                    if (newBound.X > searchBound.X)
                    {
                        searchBound.X = newBound.X;
                    }
                    if (newBound.Y > searchBound.Y)
                    {
                        searchBound.Y = newBound.Y;
                    }
                    if (newBound.Z > searchBound.Z)
                    {
                        searchBound.Z = newBound.Z;
                    }
                    if (!cubes.Contains(side))
                    {
                        if (exposed.ContainsKey(side))
                        {
                            exposed[side]++;
                        }
                        else
                        {
                            exposed[side] = 1;
                        }
                    }
                }
            }

            HashSet<Vector3> searched = new();
            Dictionary<Vector3, int> outsideExposed = new();
            Queue<Vector3> searchQueue = new();
            searchQueue.Enqueue(searchStart);

            while (searchQueue.TryDequeue(out Vector3 position))
            {
                _ = searched.Add(position);
                if (exposed.ContainsKey(position))
                {
                    outsideExposed[position] = exposed[position];
                }
                foreach (Vector3 adj in adjacent)
                {
                    Vector3 toSearch = position + adj;
                    if (!searchQueue.Contains(toSearch) && !searched.Contains(toSearch) && !cubes.Contains(toSearch)
                        && toSearch.X <= searchBound.X && toSearch.Y <= searchBound.Y && toSearch.Z <= searchBound.Z
                        && toSearch.X >= searchStart.X && toSearch.Y >= searchStart.Y && toSearch.Z >= searchStart.Z)
                    {
                        searchQueue.Enqueue(toSearch);
                    }
                }
            }

            return outsideExposed.Sum(x => x.Value);
        }
    }
}
