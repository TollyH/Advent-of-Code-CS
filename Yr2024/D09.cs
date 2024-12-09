namespace AdventOfCode.Yr2024
{
    public static class D09
    {
        public static long PartOne(string[] input)
        {
            int[] diskMap = input[0].Select(c => c - '0').ToArray();

            int[] disk = new int[diskMap.Sum()];

            bool isFile = true;
            int fileId = 0;
            int diskIndex = 0;
            foreach (int blockSize in diskMap)
            {
                int setValue = isFile ? fileId++ : -1;
                for (int i = 0; i < blockSize; i++)
                {
                    disk[diskIndex++] = setValue;
                }
                isFile = !isFile;
            }

            int freeIndex = 0;
            int blockIndex = disk.Length - 1;
            while (true)
            {
                bool end = false;
                while (disk[freeIndex] != -1)
                {
                    if (++freeIndex >= disk.Length || freeIndex >= blockIndex)
                    {
                        end = true;
                        break;
                    }
                }
                while (disk[blockIndex] == -1)
                {
                    if (--blockIndex < 0 || blockIndex <= freeIndex)
                    {
                        end = true;
                        break;
                    }
                }

                if (end)
                {
                    break;
                }

                disk[freeIndex] = disk[blockIndex];
                disk[blockIndex] = -1;
            }

            long sum = 0;
            for (int i = 0; i < disk.Length && disk[i] != -1; i++)
            {
                sum += disk[i] * i;
            }
            return sum;
        }

        public static long PartTwo(string[] input)
        {
            int[] diskMap = input[0].Select(c => c - '0').ToArray();

            int[] disk = new int[diskMap.Sum()];

            bool isFile = true;
            int fileId = 0;
            int diskIndex = 0;
            foreach (int blockSize in diskMap)
            {
                int setValue = isFile ? fileId++ : -1;
                for (int i = 0; i < blockSize; i++)
                {
                    disk[diskIndex++] = setValue;
                }
                isFile = !isFile;
            }

            int blockIndexEnd = disk.Length - 1;
            while (true)
            {
                bool end = false;
                while (disk[blockIndexEnd] == -1)
                {
                    if (--blockIndexEnd < 0)
                    {
                        end = true;
                        break;
                    }
                }

                int blockIndexStart = blockIndexEnd;
                while (disk[blockIndexStart] == disk[blockIndexEnd])
                {
                    if (--blockIndexStart < 0)
                    {
                        end = true;
                        break;
                    }
                }
                blockIndexStart++;

                if (end)
                {
                    break;
                }

                int freeIndexStart = 0;
                int blockLength = blockIndexEnd - blockIndexStart + 1;
                while (true)
                {
                    while (disk[freeIndexStart] != -1)
                    {
                        if (++freeIndexStart >= disk.Length || freeIndexStart >= blockIndexEnd)
                        {
                            end = true;
                            break;
                        }
                    }

                    int freeIndexEnd = freeIndexStart;
                    while (disk[freeIndexEnd] == -1)
                    {
                        if (++freeIndexEnd >= disk.Length || freeIndexStart >= blockIndexEnd)
                        {
                            end = true;
                            break;
                        }
                    }
                    freeIndexEnd--;

                    if (end)
                    {
                        break;
                    }

                    int freeLength = freeIndexEnd - freeIndexStart + 1;
                    if (blockLength > freeLength)
                    {
                        freeIndexStart = freeIndexEnd + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (end)
                {
                    blockIndexEnd = blockIndexStart - 1;
                    continue;
                }

                for (int i = 0; i < blockLength; i++)
                {
                    disk[freeIndexStart + i] = disk[blockIndexStart + i];
                    disk[blockIndexStart + i] = -1;
                }
            }

            long sum = 0;
            for (int i = 0; i < disk.Length; i++)
            {
                if (disk[i] == -1)
                {
                    continue;
                }
                sum += disk[i] * i;
            }
            return sum;
        }
    }
}
