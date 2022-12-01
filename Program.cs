namespace AdventOfCode
{
    public static class Program
    {
        public static readonly int DaysToRun = 1;

        public static void Main(string[] args)
        {
            for (int day = 1; day <= DaysToRun; day++)
            {
                // Reflection is used to dynamically access each day's class, instead of specifying each one individually
                Console.WriteLine($"==== Day {day:00} ====");
                string[] input = File.ReadAllText($"input{day:00}.txt").Trim().Split('\n');
                Console.WriteLine(Type.GetType($"AdventOfCode.D{day:00}")!.GetMethod("PartOne")!.Invoke(null, new object[] { input }));
                Console.WriteLine(Type.GetType($"AdventOfCode.D{day:00}")!.GetMethod("PartTwo")!.Invoke(null, new object[] { input }));
                Console.WriteLine();
            }
        }
    }
}
