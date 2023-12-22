using System.Numerics;

namespace AdventOfCode.Yr2023
{
    public static class D22
    {
        private readonly struct Brick
        {
            public readonly int ID;
            public readonly Vector3 Start;
            public readonly Vector3 End;

            public Brick(int id, Vector3 start, Vector3 end)
            {
                ID = id;
                Start = start;
                End = end;
            }

            public bool Overlaps(Brick other)
            {
                return Start.X < other.End.X && other.Start.X < End.X
                    && Start.Y < other.End.Y && other.Start.Y < End.Y
                    && Start.Z < other.End.Z && other.Start.Z < End.Z;
            }
        }

        private static HashSet<int> MoveBricks(List<Brick> bricks)
        {
            HashSet<int> moved = new();
            for (int i = 0; i < bricks.Count; i++)
            {
                Brick brick = bricks[i];
                Brick newBrick = new(brick.ID, brick.Start - Vector3.UnitZ, brick.End - Vector3.UnitZ);
                if (newBrick.Start.Z <= 0 || newBrick.End.Z <= 0)
                {
                    continue;
                }
                bool overlap = false;
                for (int j = i + 1; j < bricks.Count; j++)
                {
                    if (bricks[j].Overlaps(newBrick))
                    {
                        overlap = true;
                        break;
                    }
                }
                if (overlap)
                {
                    continue;
                }
                bricks[i] = newBrick;
                _ = moved.Add(brick.ID);
            }
            return moved;
        }

        public static int PartOne(string[] input)
        {
            List<Brick> bricks = new();
            for (int i = 0; i < input.Length; i++)
            {
                string[] components = input[i].Split('~');
                int[] start = components[0].Split(',').Select(int.Parse).ToArray();
                int[] end = components[1].Split(',').Select(int.Parse).ToArray();
                bricks.Add(new Brick(i, new Vector3(start[0], start[1], start[2]), new Vector3(end[0], end[1], end[2]) + Vector3.One));
            }
            bricks = bricks.OrderBy(b => -Math.Min(b.Start.Z, b.End.Z - 1)).ToList();

            while (MoveBricks(bricks).Count != 0) { }

            int result = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                List<Brick> bricksClone = new(bricks);
                bricksClone.RemoveAt(i);
                if (MoveBricks(bricksClone).Count == 0)
                {
                    result++;
                }
            }

            return result;
        }

        public static int PartTwo(string[] input)
        {
            List<Brick> bricks = new();
            for (int i = 0; i < input.Length; i++)
            {
                string[] components = input[i].Split('~');
                int[] start = components[0].Split(',').Select(int.Parse).ToArray();
                int[] end = components[1].Split(',').Select(int.Parse).ToArray();
                bricks.Add(new Brick(i, new Vector3(start[0], start[1], start[2]), new Vector3(end[0], end[1], end[2]) + Vector3.One));
            }
            bricks = bricks.OrderBy(b => -Math.Min(b.Start.Z, b.End.Z - 1)).ToList();

            while (MoveBricks(bricks).Count != 0) { }

            int result = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                List<Brick> bricksClone = new(bricks);
                bricksClone.RemoveAt(i);
                int moved = -1;
                HashSet<int> movedBricks = new();
                while (moved != 0)
                {
                    HashSet<int> newMoved = MoveBricks(bricksClone);
                    moved = newMoved.Count;
                    movedBricks.UnionWith(newMoved);
                }
                result += movedBricks.Count;
            }

            return result;
        }
    }
}
