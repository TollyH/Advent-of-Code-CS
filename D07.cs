namespace AdventOfCode.Yr2022
{
    public static class D07
    {
        private class File
        {
            public string Name { get; private set; }
            public uint Size { get; private set; }

            public File(string name, uint size)
            {
                Name = name;
                Size = size;
            }
        }

        private class Directory
        {
            public string Name { get; set; }
            public uint Size { get; private set; }
            public Directory? Parent { get; private set; }
            public List<Directory> Directories { get; private set; }
            public List<File> Files { get; private set; }

            public Directory(string name, Directory? parent = null)
            {
                Name = name;
                Parent = parent;
                Directories = new();
                Files = new();
            }

            public void AddFile(File file)
            {
                Files.Add(file);
                IncrementSize(file.Size);
            }

            public void IncrementSize(uint amount)
            {
                Size += amount;
                Parent?.IncrementSize(amount);
            }

            public List<Directory> SearchDirectories(Func<Directory, bool> filter)
            {
                List<Directory> matched = new();
                if (filter(this))
                {
                    matched.Add(this);
                }
                foreach (Directory directory in Directories)
                {
                    matched.AddRange(directory.SearchDirectories(filter));
                }
                return matched;
            }
        }

        public static long PartOne(string[] input)
        {
            Directory rootDir = new("/");
            Directory currentDir = rootDir;
            string? activeCommand = null;
            foreach (string line in input[1..])
            {
                if (line.StartsWith('$'))
                {
                    string[] args = line.Split(' ')[1..];
                    activeCommand = args[0];
                    if (activeCommand == "cd")
                    {
                        if (args[1] == "..")
                        {
                            if (currentDir.Parent is not null)
                            {
                                currentDir = currentDir.Parent;
                            }
                        }
                        else if (args[1] == "/")
                        {
                            currentDir = rootDir;
                        }
                        else
                        {
                            IEnumerable<Directory> matches = currentDir.Directories.Where(d => d.Name == args[1]);
                            if (matches.Any())
                            {
                                currentDir = matches.First();
                            }
                            else
                            {
                                currentDir.Directories.Add(new Directory(args[1], currentDir));
                            }
                        }
                        activeCommand = null;
                    }
                }
                else if (activeCommand == "ls")
                {
                    if (line.StartsWith("dir"))
                    {
                        currentDir.Directories.Add(new Directory(line.Split(" ")[1], currentDir));
                    }
                    else
                    {
                        string[] split = line.Split(" ");
                        currentDir.AddFile(new File(split[1], uint.Parse(split[0])));
                    }
                }
            }
            return rootDir.SearchDirectories(d => d.Size <= 100000).Sum(d => d.Size);
        }

        public static uint PartTwo(string[] input)
        {
            Directory rootDir = new("/");
            Directory currentDir = rootDir;
            string? activeCommand = null;
            foreach (string line in input[1..])
            {
                if (line.StartsWith('$'))
                {
                    string[] args = line.Split(' ')[1..];
                    activeCommand = args[0];
                    if (activeCommand == "cd")
                    {
                        if (args[1] == "..")
                        {
                            if (currentDir.Parent is not null)
                            {
                                currentDir = currentDir.Parent;
                            }
                        }
                        else if (args[1] == "/")
                        {
                            currentDir = rootDir;
                        }
                        else
                        {
                            IEnumerable<Directory> matches = currentDir.Directories.Where(d => d.Name == args[1]);
                            if (matches.Any())
                            {
                                currentDir = matches.First();
                            }
                            else
                            {
                                currentDir.Directories.Add(new Directory(args[1], currentDir));
                            }
                        }
                        activeCommand = null;
                    }
                }
                else if (activeCommand == "ls")
                {
                    if (line.StartsWith("dir"))
                    {
                        currentDir.Directories.Add(new Directory(line.Split(" ")[1], currentDir));
                    }
                    else
                    {
                        string[] split = line.Split(" ");
                        currentDir.AddFile(new File(split[1], uint.Parse(split[0])));
                    }
                }
            }
            uint totalSpace = 70000000;
            uint neededSpace = 30000000;
            long minClear = neededSpace - (totalSpace - rootDir.Size);
            List<Directory> bigEnough = rootDir.SearchDirectories(d => d.Size >= minClear);
            bigEnough.Sort((x, y) => x.Size.CompareTo(y.Size));
            return bigEnough[0].Size;
        }
    }
}
