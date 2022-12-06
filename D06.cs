namespace AdventOfCode.Yr2022
{
    public static class D06
    {
        public static int PartOne(string[] input)
        {
            string data = input[0];
            for (int i = 3; i < data.Length; i++)
            {
                string slice = data[(i - 3)..(i + 1)];
                if (slice.Length == slice.Distinct().Count())
                {
                    return i + 1;
                }
            }
            return -1;
        }

        public static int PartTwo(string[] input)
        {
            string data = input[0];
            for (int i = 13; i < data.Length; i++)
            {
                string slice = data[(i - 13)..(i + 1)];
                if (slice.Length == slice.Distinct().Count())
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
