using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D12
    {
        private struct SquareData
        {
            public int Distance { get; set; }
            public Point? Previous { get; set; }
        }

        public static int PartOne(string[] input)
        {
            char[,] map = new char[input[0].Length, input.Length];
            Dictionary<Point, SquareData> unvisited = new();
            Dictionary<Point, SquareData> visited = new();
            List<(int, Point)> queue = new();
            Point start = new();
            Point end = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        start.X = x;
                        start.Y = y;
                        map[x, y] = 'a';
                        unvisited[new Point(x, y)] = new() { Distance = 0 };
                        queue.Add((0, new Point(x, y)));
                    }
                    else if (input[y][x] == 'E')
                    {
                        end.X = x;
                        end.Y = y;
                        map[x, y] = 'z';
                        unvisited[new Point(x, y)] = new() { Distance = int.MaxValue };
                    }
                    else
                    {
                        map[x, y] = input[y][x];
                        unvisited[new Point(x, y)] = new() { Distance = int.MaxValue };
                    }
                }
            }

            while (unvisited.Count > 0)
            {
                (int distance, Point coord) = queue[0];
                SquareData currentData = unvisited[coord];
                _ = unvisited.Remove(coord);
                visited[coord] = currentData;
                queue.RemoveAt(0);

                if (coord == end)
                {
                    break;
                }

                List<Point> adjacentCoords = new();
                if (coord.X >= 1)
                {
                    adjacentCoords.Add(new Point(coord.X - 1, coord.Y));
                }
                if (coord.X < map.GetLength(0) - 1)
                {
                    adjacentCoords.Add(new Point(coord.X + 1, coord.Y));
                }
                if (coord.Y >= 1)
                {
                    adjacentCoords.Add(new Point(coord.X, coord.Y - 1));
                }
                if (coord.Y < map.GetLength(1) - 1)
                {
                    adjacentCoords.Add(new Point(coord.X, coord.Y + 1));
                }

                foreach (Point adj in adjacentCoords)
                {
                    if (visited.ContainsKey(adj))
                    {
                        continue;
                    }
                    SquareData adjacent = unvisited[adj];
                    int newDistance = distance + 1;
                    if (newDistance < unvisited[adj].Distance && (map[adj.X, adj.Y] - map[coord.X, coord.Y] <= 1))
                    {
                        adjacent.Distance = newDistance;
                        adjacent.Previous = coord;
                        unvisited[adj] = adjacent;
                        queue.Add((newDistance, adj));
                    }
                }

                queue.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            }

            List<Point> finalPath = new();
            Point? current = end;
            while (current is not null)
            {
                finalPath.Add(current.Value);
                current = visited[current.Value].Previous;
            }
            return finalPath.Count - 1;
        }

        public static int PartTwo(string[] input)
        {
            char[,] map = new char[input[0].Length, input.Length];
            List<Point> startPoints = new();
            Point end = new();
            int minPathLength = int.MaxValue;

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] is 'S' or 'a')
                    {
                        Point start = new(x, y);
                        map[x, y] = 'a';
                        startPoints.Add(start);
                    }
                    else if (input[y][x] == 'E')
                    {
                        end.X = x;
                        end.Y = y;
                        map[x, y] = 'z';
                    }
                    else
                    {
                        map[x, y] = input[y][x];
                    }
                }
            }

            foreach (Point start in startPoints)
            {
                Dictionary<Point, SquareData> unvisited = new();
                Dictionary<Point, SquareData> visited = new();
                List<(int, Point)> queue = new();
                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[0].Length; x++)
                    {
                        unvisited[new Point(x, y)] = new() { Distance = int.MaxValue };
                    }
                }
                unvisited[start] = new() { Distance = 0 };
                queue.Add((0, start));
                bool found = true;
                while (unvisited.Count > 0)
                {
                    if (!queue.Any())
                    {
                        found = false;
                        break;
                    }
                    (int distance, Point coord) = queue[0];
                    SquareData currentData = unvisited[coord];
                    _ = unvisited.Remove(coord);
                    visited[coord] = currentData;
                    queue.RemoveAt(0);

                    if (coord == end)
                    {
                        break;
                    }

                    List<Point> adjacentCoords = new();
                    if (coord.X >= 1)
                    {
                        adjacentCoords.Add(new Point(coord.X - 1, coord.Y));
                    }
                    if (coord.X < map.GetLength(0) - 1)
                    {
                        adjacentCoords.Add(new Point(coord.X + 1, coord.Y));
                    }
                    if (coord.Y >= 1)
                    {
                        adjacentCoords.Add(new Point(coord.X, coord.Y - 1));
                    }
                    if (coord.Y < map.GetLength(1) - 1)
                    {
                        adjacentCoords.Add(new Point(coord.X, coord.Y + 1));
                    }

                    foreach (Point adj in adjacentCoords)
                    {
                        if (visited.ContainsKey(adj))
                        {
                            continue;
                        }
                        SquareData adjacent = unvisited[adj];
                        int newDistance = distance + 1;
                        if (newDistance < unvisited[adj].Distance && (map[adj.X, adj.Y] - map[coord.X, coord.Y] <= 1))
                        {
                            adjacent.Distance = newDistance;
                            adjacent.Previous = coord;
                            unvisited[adj] = adjacent;
                            queue.Add((newDistance, adj));
                        }
                    }

                    queue.Sort((x, y) => x.Item1.CompareTo(y.Item1));
                }
                if (!found)
                {
                    continue;
                }

                List<Point> finalPath = new();
                Point? current = end;
                while (current is not null)
                {
                    finalPath.Add(current.Value);
                    current = visited[current.Value].Previous;
                }
                if (finalPath.Count - 1 < minPathLength)
                {
                    minPathLength = finalPath.Count - 1;
                }
            }
            return minPathLength;
        }
    }
}
