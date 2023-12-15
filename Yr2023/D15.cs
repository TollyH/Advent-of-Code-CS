using System.Collections.Specialized;

namespace AdventOfCode.Yr2023
{
    public static class D15
    {
        private static int Hash(string str)
        {
            // `unchecked` is used as the project is built with runtime overflow checking, but that is intended here
            return str.Aggregate(0, (v, c) => unchecked((byte)((v + c) * 17)));
        }

        public static int PartOne(string[] input)
        {
            return input[0].Split(',').Sum(Hash);
        }

        public static int PartTwo(string[] input)
        {
            OrderedDictionary[] boxes = new OrderedDictionary[256];
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new OrderedDictionary();
            }

            foreach (string instruction in input[0].Split(','))
            {
                if (instruction.EndsWith('-'))
                {
                    string label = instruction[..^1];
                    boxes[Hash(label)].Remove(label);
                }
                else
                {
                    string[] components = instruction.Split('=');
                    string label = components[0];
                    boxes[Hash(label)][label] = int.Parse(components[1]);
                }
            }

            int sum = 0;
            for (int bi = 0; bi < boxes.Length; bi++)
            {
                OrderedDictionary box = boxes[bi];
                for (int li = 0; li < box.Count; li++)
                {
                    sum += (bi + 1) * (li + 1) * (int)box[li]!;
                }
            }
            return sum;
        }
    }
}
