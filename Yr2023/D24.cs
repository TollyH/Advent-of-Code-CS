namespace AdventOfCode.Yr2023
{
    public static class D24
    {
        public readonly struct Vector2 : IEquatable<Vector2>
        {
            public readonly decimal X;
            public readonly decimal Y;

            public Vector2(decimal x, decimal y)
            {
                X = x;
                Y = y;
            }

            public static Vector2 operator -(Vector2 left, Vector2 right)
            {
                return new Vector2(
                    left.X - right.X,
                    left.Y - right.Y
                );
            }

            public static bool operator ==(Vector2 left, Vector2 right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Vector2 left, Vector2 right)
            {
                return !left.Equals(right);
            }

            public override bool Equals(object? obj)
            {
                return obj is Vector2 other && Equals(other);
            }

            public bool Equals(Vector2 other)
            {
                return X.Equals(other.X) && Y.Equals(other.Y);
            }
        }

        public readonly struct Vector3 : IEquatable<Vector3>
        {
            public readonly decimal X;
            public readonly decimal Y;
            public readonly decimal Z;

            public Vector3(decimal x, decimal y, decimal z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public static Vector3 operator -(Vector3 left, Vector3 right)
            {
                return new Vector3(
                    left.X - right.X,
                    left.Y - right.Y,
                    left.Z - right.Z
                );
            }

            public static bool operator ==(Vector3 left, Vector3 right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Vector3 left, Vector3 right)
            {
                return !left.Equals(right);
            }

            public override bool Equals(object? obj)
            {
                return obj is Vector3 other && Equals(other);
            }

            public bool Equals(Vector3 other)
            {
                return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
            }
        }

        private readonly struct Hailstone2D
        {
            public readonly Vector2 Position;
            public readonly Vector2 Velocity;

            public Hailstone2D(Vector2 position, Vector2 velocity)
            {
                Position = position;
                Velocity = velocity;
            }
        }

        private readonly struct Hailstone3D
        {
            public readonly Vector3 Position;
            public readonly Vector3 Velocity;

            public Hailstone3D(Vector3 position, Vector3 velocity)
            {
                Position = position;
                Velocity = velocity;
            }
        }

        private static readonly long TestAreaMin = 200000000000000;
        private static readonly long TestAreaMax = 400000000000000;

        public static int PartOne(string[] input)
        {
            List<Hailstone2D> hailstones = new();
            foreach (string line in input)
            {
                string[] components = line.Split('@');
                long[] position = components[0].Split(',').Select(s => long.Parse(s.Trim())).ToArray();
                long[] velocity = components[1].Split(',').Select(s => long.Parse(s.Trim())).ToArray();
                hailstones.Add(new Hailstone2D(new Vector2(position[0], position[1]), new Vector2(velocity[0], velocity[1])));
            }

            int collisions = 0;
            for (int i = 0; i < hailstones.Count; i++)
            {
                for (int j = i + 1; j < hailstones.Count; j++)
                {
                    Hailstone2D first = hailstones[i];
                    Hailstone2D second = hailstones[j];

                    if (first.Velocity == second.Velocity)
                    {
                        continue;
                    }

                    decimal m1 = first.Velocity.Y / first.Velocity.X;
                    decimal m2 = second.Velocity.Y / second.Velocity.X;

                    decimal b1 = first.Position.Y - (m1 * first.Position.X);
                    decimal b2 = second.Position.Y - (m2 * second.Position.X);

                    if (m1 == m2)
                    {
                        continue;
                    }

                    decimal x = (b2 - b1) / (m1 - m2);
                    decimal y = (m1 * x) + b1;

                    if ((first.Velocity.X > 0 && x < first.Position.X)
                        || (first.Velocity.X < 0 && x > first.Position.X)
                        || (first.Velocity.Y < 0 && y > first.Position.Y)
                        || (first.Velocity.Y > 0 && y < first.Position.Y)
                        || (second.Velocity.X > 0 && x < second.Position.X)
                        || (second.Velocity.X < 0 && x > second.Position.X)
                        || (second.Velocity.Y < 0 && y > second.Position.Y)
                        || (second.Velocity.Y > 0 && y < second.Position.Y)
                        || x < TestAreaMin || x > TestAreaMax
                        || y < TestAreaMin || y > TestAreaMax)
                    {
                        continue;
                    }

                    collisions++;
                }
            }
            return collisions;
        }

        public static long PartTwo(string[] input)
        {
            List<Hailstone2D> hailstone2Ds = new();
            List<Hailstone3D> hailstone3Ds = new();
            foreach (string line in input)
            {
                string[] components = line.Split('@');
                long[] position = components[0].Split(',').Select(s => long.Parse(s.Trim())).ToArray();
                long[] velocity = components[1].Split(',').Select(s => long.Parse(s.Trim())).ToArray();
                hailstone2Ds.Add(new Hailstone2D(new Vector2(position[0], position[1]), new Vector2(velocity[0], velocity[1])));
                hailstone3Ds.Add(new Hailstone3D(new Vector3(position[0], position[1], position[2]),
                    new Vector3(velocity[0], velocity[1], velocity[2])));
            }

            Vector2 collisionPoint2D = new();
            Vector2 matching2DVelocity = new();
            bool match = false;
            for (decimal tryVelX = hailstone2Ds.Min(h => h.Velocity.X) * 10; !match && tryVelX <= hailstone2Ds.Max(h => h.Velocity.X) * 10; tryVelX++)
            {
                for (decimal tryVelY = hailstone2Ds.Min(h => h.Velocity.Y) * 10; !match && tryVelY <= hailstone2Ds.Max(h => h.Velocity.Y) * 10; tryVelY++)
                {
                    Vector2 rockVelocity = new(tryVelX, tryVelY);

                    Hailstone2D first = new(hailstone2Ds[0].Position, hailstone2Ds[0].Velocity - rockVelocity);
                    Hailstone2D second = new(hailstone2Ds[1].Position, hailstone2Ds[1].Velocity - rockVelocity);
                    Hailstone2D third = new(hailstone2Ds[2].Position, hailstone2Ds[2].Velocity - rockVelocity);

                    if (first.Velocity == second.Velocity
                        || first.Velocity.X == 0
                        || second.Velocity.X == 0
                        || third.Velocity.X == 0)
                    {
                        continue;
                    }

                    decimal m1 = first.Velocity.Y / first.Velocity.X;
                    decimal m2 = second.Velocity.Y / second.Velocity.X;

                    decimal b1 = first.Position.Y - (m1 * first.Position.X);
                    decimal b2 = second.Position.Y - (m2 * second.Position.X);

                    if (m1 == m2)
                    {
                        continue;
                    }

                    decimal x = (b2 - b1) / (m1 - m2);
                    decimal y = (m1 * x) + b1;

                    Vector2 xyCollision1 = new(x, y);

                    if (second.Velocity == third.Velocity)
                    {
                        continue;
                    }

                    m1 = second.Velocity.Y / second.Velocity.X;
                    m2 = third.Velocity.Y / third.Velocity.X;

                    b1 = second.Position.Y - (m1 * second.Position.X);
                    b2 = third.Position.Y - (m2 * third.Position.X);

                    if (m1 == m2)
                    {
                        continue;
                    }

                    x = (b2 - b1) / (m1 - m2);
                    y = (m1 * x) + b1;

                    Vector2 xyCollision2 = new(x, y);

                    if (Math.Abs(xyCollision1.X - xyCollision2.X) < 10 && Math.Abs(xyCollision1.Y - xyCollision2.Y) < 10)
                    {
                        collisionPoint2D = xyCollision1;
                        matching2DVelocity = rockVelocity;
                        match = true;
                        break;
                    }
                }
            }
            if (!match)
            {
                throw new Exception();
            }

            decimal mr = matching2DVelocity.Y / matching2DVelocity.X;
            decimal mh = hailstone2Ds[0].Velocity.Y / hailstone2Ds[0].Velocity.X;

            decimal br = collisionPoint2D.Y - (mr * collisionPoint2D.X);
            decimal bh = hailstone2Ds[0].Position.Y - (mh * hailstone2Ds[0].Position.X);

            decimal collideX = (bh - br) / (mr - mh);

            decimal t1 = (collideX - collisionPoint2D.X) / matching2DVelocity.X;

            decimal collideZ1 = hailstone3Ds[0].Position.Z + (hailstone3Ds[0].Velocity.Z * t1);

            mr = matching2DVelocity.Y / matching2DVelocity.X;
            mh = hailstone2Ds[1].Velocity.Y / hailstone2Ds[1].Velocity.X;

            br = collisionPoint2D.Y - (mr * collisionPoint2D.X);
            bh = hailstone2Ds[1].Position.Y - (mh * hailstone2Ds[1].Position.X);

            collideX = (bh - br) / (mr - mh);

            decimal t2 = (collideX - collisionPoint2D.X) / matching2DVelocity.X;

            decimal collideZ2 = hailstone3Ds[1].Position.Z + (hailstone3Ds[1].Velocity.Z * t2);

            decimal rockVelocityZ = (collideZ2 - collideZ1) / (t2 - t1);
            decimal rockPositionZ = collideZ1 - (t1 * rockVelocityZ);

            return (long)(Math.Round(collisionPoint2D.X) + Math.Round(collisionPoint2D.Y) + Math.Round(rockPositionZ));
        }
    }
}
