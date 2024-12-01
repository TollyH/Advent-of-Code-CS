namespace AdventOfCode.Yr2024
{
    public static class D01
    {
        public static int PartOne(string[] input)
        {
            List<int> firstCol = new();
            List<int> secondCol = new();

            foreach (string line in input)
            {
                string[] lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                firstCol.Add(int.Parse(lineSplit[0]));
                secondCol.Add(int.Parse(lineSplit[1]));
            }

            firstCol.Sort();
            secondCol.Sort();

            int total = 0;

            for (int i = 0; i < firstCol.Count; i++)
            {
                total += Math.Abs(firstCol[i] - secondCol[i]);
            }

            return total;
        }

        public static int PartTwo(string[] input)
        {
            List<int> firstCol = new();
            List<int> secondCol = new();

            foreach (string line in input)
            {
                string[] lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                firstCol.Add(int.Parse(lineSplit[0]));
                secondCol.Add(int.Parse(lineSplit[1]));
            }

            int total = 0;

            foreach (int num in firstCol)
            {
                total += num * secondCol.Count(n => n == num);
            }

            return total;
        }
    }
}
