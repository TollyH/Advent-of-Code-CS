namespace AdventOfCode.Yr2023
{
    public static class D19
    {
        private enum PartCategory
        {
            X, M, A, S
        }

        private readonly struct Part
        {
            public readonly int X;
            public readonly int M;
            public readonly int A;
            public readonly int S;

            public Part(int x, int m, int a, int s)
            {
                X = x;
                M = m;
                A = a;
                S = s;
            }

            public int GetFromCategory(PartCategory category)
            {
                return category switch
                {
                    PartCategory.X => X,
                    PartCategory.M => M,
                    PartCategory.A => A,
                    PartCategory.S => S,
                    _ => 0,
                };
            }
        }

        private class WorkflowCondition
        {
            public readonly bool AlwaysTrue;
            public readonly bool GreaterThan;
            public readonly PartCategory Category;
            public readonly int Value;
            public readonly string Result;

            public WorkflowCondition(bool alwaysTrue, bool greaterThan, PartCategory category, int value, string result)
            {
                AlwaysTrue = alwaysTrue;
                GreaterThan = greaterThan;
                Category = category;
                Value = value;
                Result = result;
            }

            public bool Test(Part part)
            {
                if (AlwaysTrue)
                {
                    return true;
                }
                int testValue = part.GetFromCategory(Category);
                return GreaterThan ? testValue > Value : testValue < Value;
            }
        }

        private static bool RunWorkflow(Dictionary<string, List<WorkflowCondition>> workflows, string startWorkflow, Part part)
        {
            List<WorkflowCondition> conditions = workflows[startWorkflow];
            foreach (WorkflowCondition condition in conditions)
            {
                if (condition.Test(part))
                {
                    if (condition.Result is "R" or "A")
                    {
                        return condition.Result == "A";
                    }
                    return RunWorkflow(workflows, condition.Result, part);
                }
            }
            throw new Exception();
        }

        public static int PartOne(string[] input)
        {
            Dictionary<string, List<WorkflowCondition>> workflows = new();

            int sum = 0;
            bool parsingWorkflows = true;
            foreach (string line in input)
            {
                if (line == "")
                {
                    parsingWorkflows = false;
                    continue;
                }
                if (parsingWorkflows)
                {
                    List<WorkflowCondition> newWorkflow = new();
                    string[] components = line.Split('{');
                    workflows[components[0]] = newWorkflow;
                    foreach (string condition in components[1][..^1].Split(','))
                    {
                        string[] conditionComponents = condition.Split(':');
                        string check = conditionComponents[0];
                        if (conditionComponents.Length > 1)
                        {
                            PartCategory category = check[0] switch
                            {
                                'x' => PartCategory.X,
                                'm' => PartCategory.M,
                                'a' => PartCategory.A,
                                's' => PartCategory.S,
                                _ => throw new Exception()
                            };
                            string result = conditionComponents[1];
                            newWorkflow.Add(new WorkflowCondition(false, check[1] == '>', category, int.Parse(check[2..]), result));
                        }
                        else
                        {
                            newWorkflow.Add(new WorkflowCondition(true, false, PartCategory.X, 0, check));
                        }
                    }
                }
                else
                {
                    string[] components = line.Split(',');
                    Part part = new(int.Parse(components[0][3..]), int.Parse(components[1][2..]), int.Parse(components[2][2..]), int.Parse(components[3][2..^1]));
                    if (RunWorkflow(workflows, "in", part))
                    {
                        sum += part.X + part.M + part.A + part.S;
                    }
                }
            }
            return sum;
        }

        public readonly struct Range
        {
            public readonly long Start;
            public readonly long End;

            public long Length => End - Start;

            public Range(long start, long end)
            {
                Start = start;
                End = end;
            }

            public Range UpperLimit(long limit)
            {
                if (limit < Start)
                {
                    throw new Exception();
                }
                return new Range(Start, Math.Min(limit, End));
            }

            public Range LowerLimit(long limit)
            {
                if (limit > End)
                {
                    throw new Exception();
                }
                return new Range(Math.Max(limit, Start), End);
            }

            public override string ToString()
            {
                return $"Start = {Start}, End = {End}";
            }
        }

        private readonly struct PartRange
        {
            public readonly Range X;
            public readonly Range M;
            public readonly Range A;
            public readonly Range S;

            public PartRange(Range x, Range m, Range a, Range s)
            {
                X = x;
                M = m;
                A = a;
                S = s;
            }

            public long Product()
            {
                return X.Length * M.Length * A.Length * S.Length;
            }

            public Range GetFromCategory(PartCategory category)
            {
                return category switch
                {
                    PartCategory.X => X,
                    PartCategory.M => M,
                    PartCategory.A => A,
                    PartCategory.S => S,
                    _ => throw new Exception(),
                };
            }

            public PartRange UpperLimitCategory(PartCategory category, long limit)
            {
                return category switch
                {
                    PartCategory.X => new PartRange(X.UpperLimit(limit), M, A, S),
                    PartCategory.M => new PartRange(X, M.UpperLimit(limit), A, S),
                    PartCategory.A => new PartRange(X, M, A.UpperLimit(limit), S),
                    PartCategory.S => new PartRange(X, M, A, S.UpperLimit(limit)),
                    _ => throw new Exception(),
                };
            }

            public PartRange LowerLimitCategory(PartCategory category, long limit)
            {
                return category switch
                {
                    PartCategory.X => new PartRange(X.LowerLimit(limit), M, A, S),
                    PartCategory.M => new PartRange(X, M.LowerLimit(limit), A, S),
                    PartCategory.A => new PartRange(X, M, A.LowerLimit(limit), S),
                    PartCategory.S => new PartRange(X, M, A, S.LowerLimit(limit)),
                    _ => throw new Exception(),
                };
            }
        }

        private static long GetPossiblePermutations(Dictionary<string, List<WorkflowCondition>> workflows, string startWorkflow, PartRange currentPossible)
        {
            long permutations = 0;
            foreach (WorkflowCondition condition in workflows[startWorkflow])
            {
                if (condition.AlwaysTrue)
                {
                    if (condition.Result is "R" or "A")
                    {
                        if (condition.Result == "A")
                        {
                            permutations += currentPossible.Product();
                        }
                    }
                    else
                    {
                        permutations += GetPossiblePermutations(workflows, condition.Result, currentPossible);
                    }
                }
                else
                {
                    PartRange limited = condition.GreaterThan
                        ? currentPossible.LowerLimitCategory(condition.Category, condition.Value + 1)
                        : currentPossible.UpperLimitCategory(condition.Category, condition.Value);
                    if (condition.Result is "R" or "A")
                    {
                        if (condition.Result == "A")
                        {
                            permutations += limited.Product();
                        }
                    }
                    else
                    {
                        permutations += GetPossiblePermutations(workflows, condition.Result, limited);
                    }
                    currentPossible = condition.GreaterThan
                        ? currentPossible.UpperLimitCategory(condition.Category, limited.GetFromCategory(condition.Category).Start)
                        : currentPossible.LowerLimitCategory(condition.Category, limited.GetFromCategory(condition.Category).End);
                }
            }
            return permutations;
        }

        public static long PartTwo(string[] input)
        {
            Dictionary<string, List<WorkflowCondition>> workflows = new();

            foreach (string line in input)
            {
                if (line == "")
                {
                    break;
                }
                List<WorkflowCondition> newWorkflow = new();
                string[] components = line.Split('{');
                workflows[components[0]] = newWorkflow;
                foreach (string condition in components[1][..^1].Split(','))
                {
                    string[] conditionComponents = condition.Split(':');
                    string check = conditionComponents[0];
                    if (conditionComponents.Length > 1)
                    {
                        PartCategory category = check[0] switch
                        {
                            'x' => PartCategory.X,
                            'm' => PartCategory.M,
                            'a' => PartCategory.A,
                            's' => PartCategory.S,
                            _ => throw new Exception()
                        };
                        string result = conditionComponents[1];
                        newWorkflow.Add(new WorkflowCondition(false, check[1] == '>', category, int.Parse(check[2..]), result));
                    }
                    else
                    {
                        newWorkflow.Add(new WorkflowCondition(true, false, PartCategory.X, 0, check));
                    }
                }
            }
            
            return GetPossiblePermutations(workflows, "in", new PartRange(new Range(1, 4001), new Range(1, 4001), new Range(1, 4001), new Range(1, 4001)));
        }
    }
}
