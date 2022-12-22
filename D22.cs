using System.Drawing;

namespace AdventOfCode.Yr2022
{
    public static class D22
    {
        private static int Mod(int a, int b)
        {
            // Needed because C#'s % operator finds the remainder, not modulo
            return ((a % b) + b) % b;
        }

        private static readonly Point[] movements = new Point[4] { new(1, 0), new(0, 1), new(-1, 0), new(0, -1) };

        public static int PartOne(string[] input)
        {
            int width = input[..^3].MaxBy(x => x.Length)!.Length + 2;
            int height = input.Length;
            string instructions = input[^1];
            bool?[,] map = new bool?[width, height];
            for (int y = 0; y < input.Length - 2; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map[x + 1, y + 1] = input[y][x] == ' ' ? null : input[y][x] == '#';
                }
            }

            Point position = new(1, 1);
            int rotation = 0;
            while (map[position.X, position.Y] is null)
            {
                position.X++;
            }

            for (int i = 0; i < instructions.Length; i++)
            {
                string amountString = "";
                while (i < instructions.Length && char.IsDigit(instructions[i]))
                {
                    amountString += instructions[i];
                    i++;
                }
                int amount = int.Parse(amountString);
                Point movement = movements[rotation];
                for (int j = 0; j < amount; j++)
                {
                    Point newPos = new(Mod(position.X + movement.X, width), Mod(position.Y + movement.Y, height));
                    while (map[newPos.X, newPos.Y] is null)
                    {
                        newPos = new(Mod(newPos.X + movement.X, width), Mod(newPos.Y + movement.Y, height));
                    }
                    if (!map[newPos.X, newPos.Y]!.Value)
                    {
                        position = newPos;
                    }
                }
                if (i < instructions.Length)
                {
                    rotation = Mod(rotation + (instructions[i] == 'R' ? 1 : -1), 4);
                }
            }

            return (1000 * position.Y) + (4 * position.X) + rotation;
        }

        public static int PartTwo(string[] input)
        {
            int cubeSize = 50;

            int GetFace(Point position)
            {
                if (position.Y <= cubeSize)
                {
                    return position.X <= cubeSize * 2 ? 1 : 2;
                }
                if (position.Y <= cubeSize * 2)
                {
                    return 3;
                }
                if (position.Y <= cubeSize * 3)
                {
                    return position.X <= cubeSize ? 4 : 5;
                }
                return 6;
            }

            int width = input[..^3].MaxBy(x => x.Length)!.Length + 2;
            int height = input.Length;
            string instructions = input[^1];
            bool?[,] map = new bool?[width, height];
            for (int y = 0; y < input.Length - 2; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map[x + 1, y + 1] = input[y][x] == ' ' ? null : input[y][x] == '#';
                }
            }

            Point position = new(1, 1);
            int rotation = 0;
            while (map[position.X, position.Y] is null)
            {
                position.X++;
            }

            for (int i = 0; i < instructions.Length; i++)
            {
                string amountString = "";
                while (i < instructions.Length && char.IsDigit(instructions[i]))
                {
                    amountString += instructions[i];
                    i++;
                }
                int amount = int.Parse(amountString);
                for (int j = 0; j < amount; j++)
                {
                    Point movement = movements[rotation];
                    Point newPos = new(position.X + movement.X, position.Y + movement.Y);
                    int newRotation = rotation;
                    if (map[newPos.X, newPos.Y] is null)
                    {
                        int face = GetFace(position);
                        // There's gotta be a better way than this...
                        switch (face)
                        {
                            case 1:
                                switch (rotation)
                                {
                                    case 2:
                                        newRotation = 0;
                                        newPos.Y = (cubeSize * 3) - position.Y + 1;
                                        newPos.X = 1;
                                        break;
                                    case 3:
                                        newRotation = 0;
                                        newPos.Y = (cubeSize * 3) + (position.X - cubeSize);
                                        newPos.X = 1;
                                        break;
                                    default: break;
                                }
                                break;
                            case 2:
                                switch (rotation)
                                {
                                    case 0:
                                        newRotation = 2;
                                        newPos.X = cubeSize * 2;
                                        newPos.Y = (cubeSize * 3) - position.Y + 1;
                                        break;
                                    case 1:
                                        newRotation = 2;
                                        newPos.Y = position.X - cubeSize;
                                        newPos.X = cubeSize * 2;
                                        break;
                                    case 3:
                                        newPos.Y = cubeSize * 4;
                                        newPos.X -= cubeSize * 2;
                                        break;
                                    default: break;
                                }
                                break;
                            case 3:
                                switch (rotation)
                                {
                                    case 0:
                                        newRotation = 3;
                                        newPos.X = position.Y + cubeSize;
                                        newPos.Y = cubeSize;
                                        break;
                                    case 2:
                                        newRotation = 1;
                                        newPos.X = position.Y - cubeSize;
                                        newPos.Y = (cubeSize * 2) + 1;
                                        break;
                                    default: break;
                                }
                                break;
                            case 4:
                                switch (rotation)
                                {
                                    case 2:
                                        newRotation = 0;
                                        newPos.X = cubeSize + 1;
                                        newPos.Y = (cubeSize * 3) - position.Y + 1;
                                        break;
                                    case 3:
                                        newRotation = 0;
                                        newPos.Y = position.X + cubeSize;
                                        newPos.X = cubeSize + 1;
                                        break;
                                    default: break;
                                }
                                break;
                            case 5:
                                switch (rotation)
                                {
                                    case 0:
                                        newRotation = 2;
                                        newPos.Y = (cubeSize * 3) - position.Y + 1;
                                        newPos.X = cubeSize * 3;
                                        break;
                                    case 1:
                                        newRotation = 2;
                                        newPos.Y = (cubeSize * 2) + position.X;
                                        newPos.X = cubeSize;
                                        break;
                                    default: break;
                                }
                                break;
                            case 6:
                                switch (rotation)
                                {
                                    case 0:
                                        newRotation = 3;
                                        newPos.X = position.Y - (cubeSize * 2);
                                        newPos.Y = cubeSize * 3;
                                        break;
                                    case 1:
                                        newPos.X += cubeSize * 2;
                                        newPos.Y = 1;
                                        break;
                                    case 2:
                                        newRotation = 1;
                                        newPos.X = position.Y - (cubeSize * 2);
                                        newPos.Y = 1;
                                        break;
                                    default: break;
                                }
                                break;
                            default: break;
                        }
                    }
                    if (!map[newPos.X, newPos.Y]!.Value)
                    {
                        position = newPos;
                        rotation = newRotation;
                    }
                }

                if (i < instructions.Length)
                {
                    rotation = Mod(rotation + (instructions[i] == 'R' ? 1 : -1), 4);
                }
            }

            return (1000 * position.Y) + (4 * position.X) + rotation;
        }
    }
}
