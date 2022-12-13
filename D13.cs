namespace AdventOfCode.Yr2022
{
    public static class D13
    {
        private static List<object> ParseLine(string line)
        {
            if (line[1] == ']')
            {
                return new List<object>();
            }
            List<object> result = new() { "" };
            int currentIndex = 0;
            for (int i = 1; i < line.Length - 1; i++)
            {
                char character = line[i];
                if (character == '[')
                {
                    int closingIndex = i + 1;
                    int nest = 1;
                    while (nest > 0)
                    {
                        if (line[closingIndex] == '[')
                        {
                            nest++;
                        }
                        else if (line[closingIndex] == ']')
                        {
                            nest--;
                        }
                        closingIndex++;
                    }
                    result[currentIndex] = closingIndex - i <= 1 ? new List<object>() : ParseLine(line[i..closingIndex]);
                    i = closingIndex - 1;
                }
                else if (character == ',')
                {
                    if (result[currentIndex].GetType() == typeof(string))
                    {
                        result[currentIndex] = int.Parse((string)result[currentIndex]);
                    }
                    currentIndex++;
                    result.Add("");
                }
                else
                {
                    result[currentIndex] = ((string)result[currentIndex]) + character;
                }
            }
            if (result[currentIndex].GetType() == typeof(string))
            {
                result[currentIndex] = int.Parse((string)result[currentIndex]);
            }
            return result;
        }

        private static bool? Compare(List<object> first, List<object> second)
        {
            int minLength = Math.Min(first.Count, second.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (first[i].GetType() == typeof(int) && second[i].GetType() == typeof(int))
                {
                    if ((int)first[i] < (int)second[i])
                    {
                        return true;
                    }
                    if ((int)first[i] > (int)second[i])
                    {
                        return false;
                    }
                    continue;
                }

                List<object> convertedFirst;
                List<object> convertedSecond;
                if (first[i].GetType() == typeof(int))
                {
                    convertedFirst = new List<object>() { first[i] };
                    convertedSecond = (List<object>)second[i];
                }
                else if (second[i].GetType() == typeof(int))
                {
                    convertedFirst = (List<object>)first[i];
                    convertedSecond = new List<object>() { second[i] };
                }
                else
                {
                    convertedFirst = (List<object>)first[i];
                    convertedSecond = (List<object>)second[i];
                }

                bool? result = Compare(convertedFirst, convertedSecond);
                if (result is not null)
                {
                    return result;
                }
            }
            return first.Count == second.Count ? null : first.Count < second.Count;
        }

        public static int PartOne(string[] input)
        {
            int totalCorrect = 0;
            for (int i = 0; i < input.Length; i += 3)
            {
                List<object> first = ParseLine(input[i]);
                List<object> second = ParseLine(input[i + 1]);

                if (Compare(first, second) is null or true)
                {
                    totalCorrect += (i / 3) + 1;
                }
            }
            return totalCorrect;
        }

        public static int PartTwo(string[] input)
        {
            List<List<object>> packets = new();
            foreach (string line in input)
            {
                if (line != "")
                {
                    packets.Add(ParseLine(line));
                }
            }
            List<object> dividerFirst = new() { new List<object>() { 2 } };
            List<object> dividerSecond = new() { new List<object>() { 6 } };
            packets.Add(dividerFirst);
            packets.Add(dividerSecond);
            packets.Sort((x, y) => Compare(x, y) is null ? 0 : Compare(x, y)!.Value ? -1 : 1);
            return (packets.IndexOf(dividerFirst) + 1) * (packets.IndexOf(dividerSecond) + 1);
        }
    }
}
