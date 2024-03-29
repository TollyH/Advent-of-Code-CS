﻿using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D12
    {
        public static int PartOne(string[] input)
        {
            char[,] map = new char[input[0].Length, input.Length];
            Dictionary<Point, Point?> unvisited = new();
            Dictionary<Point, Point?> visited = new();
            Queue<Point> queue = new();
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
                        visited[new Point(x, y)] = null;
                        queue.Enqueue(new Point(x, y));
                    }
                    else if (input[y][x] == 'E')
                    {
                        end.X = x;
                        end.Y = y;
                        map[x, y] = 'z';
                        unvisited[new Point(x, y)] = null;
                    }
                    else
                    {
                        map[x, y] = input[y][x];
                        unvisited[new Point(x, y)] = null;
                    }
                }
            }

            while (unvisited.Count > 0)
            {
                Point coord = queue.Dequeue();

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
                    if (map[adj.X, adj.Y] - map[coord.X, coord.Y] <= 1)
                    {
                        visited[adj] = coord;
                        _ = unvisited.Remove(adj);
                        queue.Enqueue(adj);
                    }
                }
            }

            List<Point> finalPath = new();
            Point? current = end;
            while (current is not null)
            {
                finalPath.Add(current.Value);
                current = visited[current.Value];
            }
            return finalPath.Count - 1;
        }

        public static int PartTwo(string[] input)
        {
            char[,] map = new char[input[0].Length, input.Length];
            Dictionary<Point, Point?> unvisited = new();
            Dictionary<Point, Point?> visited = new();
            Queue<Point> queue = new();
            Point end = new();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        map[x, y] = 'a';
                        unvisited[new Point(x, y)] = null;
                    }
                    else if (input[y][x] == 'E')
                    {
                        end.X = x;
                        end.Y = y;
                        map[x, y] = 'z';
                        visited[new Point(x, y)] = null;
                        queue.Enqueue(new Point(x, y));
                    }
                    else
                    {
                        map[x, y] = input[y][x];
                        unvisited[new Point(x, y)] = null;
                    }
                }
            }

            Point start = new();
            while (unvisited.Count > 0)
            {
                Point coord = queue.Dequeue();

                if (map[coord.X, coord.Y] == 'a')
                {
                    start = coord;
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
                    if (map[adj.X, adj.Y] - map[coord.X, coord.Y] >= -1)
                    {
                        visited[adj] = coord;
                        _ = unvisited.Remove(adj);
                        queue.Enqueue(adj);
                    }
                }
            }

            List<Point> finalPath = new();
            Point? current = start;
            while (current is not null)
            {
                finalPath.Add(current.Value);
                current = visited[current.Value];
            }
            return finalPath.Count - 1;
        }
    }
}
