using System.Text.RegularExpressions;

namespace AdventOfCode.Yr2022
{
    public static class D19
    {
        private class Blueprint
        {
            public int BlueprintID { get; set; }
            public int OreRobotOreCost { get; set; }
            public int ClayRobotOreCost { get; set; }
            public int ObsidianRobotOreCost { get; set; }
            public int ObsidianRobotClayCost { get; set; }
            public int GeodeRobotOreCost { get; set; }
            public int GeodeRobotObsidianCost { get; set; }

            public Blueprint(int blueprintID, int oreRobotOreCost, int clayRobotOreCost, int obsidianRobotOreCost,
                int obsidianRobotClayCost, int geodeRobotOreCost, int geodeRobotObsidianCost)
            {
                BlueprintID = blueprintID;
                OreRobotOreCost = oreRobotOreCost;
                ClayRobotOreCost = clayRobotOreCost;
                ObsidianRobotOreCost = obsidianRobotOreCost;
                ObsidianRobotClayCost = obsidianRobotClayCost;
                GeodeRobotOreCost = geodeRobotOreCost;
                GeodeRobotObsidianCost = geodeRobotObsidianCost;
            }
        }

        private struct ResourceTypeCount
        {
            public int Ore { get; set; }
            public int Clay { get; set; }
            public int Obsidian { get; set; }
            public int Geode { get; set; }
        }

        public static int PartOne(string[] input)
        {
            List<Blueprint> blueprints = new();
            foreach (string line in input)
            {
                GroupCollection groups = Regex.Match(line, @"Blueprint ([0-9]+):\s*Each ore robot costs ([0-9]+) ore\.\s*" +
                    @"Each clay robot costs ([0-9]+) ore\.\s*Each obsidian robot costs ([0-9]+) ore and ([0-9]+) clay\.\s*" +
                    @"Each geode robot costs ([0-9]+) ore and ([0-9]+) obsidian\.").Groups;
                blueprints.Add(new Blueprint(int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[3].Value),
                    int.Parse(groups[4].Value), int.Parse(groups[5].Value), int.Parse(groups[6].Value), int.Parse(groups[7].Value)));
            }

            List<int> blueprintQualities = new();
            int b = 1;
            foreach (Blueprint blueprint in blueprints)
            {
                ResourceTypeCount robots = new() { Ore = 1 };
                ResourceTypeCount resources = new();

                int maxOreSpend = Math.Max(blueprint.OreRobotOreCost, Math.Max(blueprint.ClayRobotOreCost,
                    Math.Max(blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost)));
                int maxClaySpend = blueprint.ObsidianRobotClayCost;
                int maxObsidianSpend = blueprint.GeodeRobotObsidianCost;

                int maxFinish = 0;

                void GetBestFinish(ResourceTypeCount robots, ResourceTypeCount resources, int minutesRemaining)
                {
                    if (minutesRemaining <= 0
                        || resources.Geode + (robots.Geode * minutesRemaining) + ((minutesRemaining - 1) * (minutesRemaining / 2)) < maxFinish)
                    {
                        if (resources.Geode > maxFinish)
                        {
                            maxFinish = resources.Geode;
                        }
                        return;
                    }
                    minutesRemaining--;
                    if (resources.Ore >= blueprint.GeodeRobotOreCost && resources.Obsidian >= blueprint.GeodeRobotObsidianCost)
                    {
                        ResourceTypeCount newRobots = robots;
                        newRobots.Geode++;
                        ResourceTypeCount newResources = resources;
                        newResources.Ore -= blueprint.GeodeRobotOreCost;
                        newResources.Obsidian -= blueprint.GeodeRobotObsidianCost;
                        newResources.Ore += robots.Ore;
                        newResources.Clay += robots.Clay;
                        newResources.Obsidian += robots.Obsidian;
                        newResources.Geode += robots.Geode;
                        GetBestFinish(newRobots, newResources, minutesRemaining);
                    }
                    else
                    {
                        if (resources.Ore >= blueprint.OreRobotOreCost && robots.Ore < maxOreSpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Ore++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.OreRobotOreCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                        if (resources.Ore >= blueprint.ClayRobotOreCost && robots.Clay < maxClaySpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Clay++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.ClayRobotOreCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                        if (resources.Ore >= blueprint.ObsidianRobotOreCost && resources.Clay >= blueprint.ObsidianRobotClayCost
                            && robots.Obsidian < maxObsidianSpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Obsidian++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.ObsidianRobotOreCost;
                            newResources.Clay -= blueprint.ObsidianRobotClayCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                    }
                    resources.Ore += robots.Ore;
                    resources.Clay += robots.Clay;
                    resources.Obsidian += robots.Obsidian;
                    resources.Geode += robots.Geode;
                    GetBestFinish(robots, resources, minutesRemaining);
                }
                GetBestFinish(robots, resources, 24);

                blueprintQualities.Add(maxFinish * b);
                b++;
            }

            return blueprintQualities.Sum();
        }

        public static int PartTwo(string[] input)
        {
            List<Blueprint> blueprints = new();
            foreach (string line in input[..3])
            {
                GroupCollection groups = Regex.Match(line, @"Blueprint ([0-9]+):\s*Each ore robot costs ([0-9]+) ore\.\s*" +
                    @"Each clay robot costs ([0-9]+) ore\.\s*Each obsidian robot costs ([0-9]+) ore and ([0-9]+) clay\.\s*" +
                    @"Each geode robot costs ([0-9]+) ore and ([0-9]+) obsidian\.").Groups;
                blueprints.Add(new Blueprint(int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[3].Value),
                    int.Parse(groups[4].Value), int.Parse(groups[5].Value), int.Parse(groups[6].Value), int.Parse(groups[7].Value)));
            }

            List<int> blueprintsMax = new();
            foreach (Blueprint blueprint in blueprints)
            {
                ResourceTypeCount robots = new() { Ore = 1 };
                ResourceTypeCount resources = new();

                int maxOreSpend = Math.Max(blueprint.OreRobotOreCost, Math.Max(blueprint.ClayRobotOreCost,
                    Math.Max(blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost)));
                int maxClaySpend = blueprint.ObsidianRobotClayCost;
                int maxObsidianSpend = blueprint.GeodeRobotObsidianCost;

                int maxFinish = 0;

                void GetBestFinish(ResourceTypeCount robots, ResourceTypeCount resources, int minutesRemaining)
                {
                    if (minutesRemaining <= 0
                        || resources.Geode + (robots.Geode * minutesRemaining) + ((minutesRemaining - 1) * (minutesRemaining / 2)) < maxFinish)
                    {
                        if (resources.Geode > maxFinish)
                        {
                            maxFinish = resources.Geode;
                        }
                        return;
                    }
                    minutesRemaining--;
                    if (resources.Ore >= blueprint.GeodeRobotOreCost && resources.Obsidian >= blueprint.GeodeRobotObsidianCost)
                    {
                        ResourceTypeCount newRobots = robots;
                        newRobots.Geode++;
                        ResourceTypeCount newResources = resources;
                        newResources.Ore -= blueprint.GeodeRobotOreCost;
                        newResources.Obsidian -= blueprint.GeodeRobotObsidianCost;
                        newResources.Ore += robots.Ore;
                        newResources.Clay += robots.Clay;
                        newResources.Obsidian += robots.Obsidian;
                        newResources.Geode += robots.Geode;
                        GetBestFinish(newRobots, newResources, minutesRemaining);
                    }
                    else
                    {
                        if (resources.Ore >= blueprint.OreRobotOreCost && robots.Ore < maxOreSpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Ore++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.OreRobotOreCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                        if (resources.Ore >= blueprint.ClayRobotOreCost && robots.Clay < maxClaySpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Clay++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.ClayRobotOreCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                        if (resources.Ore >= blueprint.ObsidianRobotOreCost && resources.Clay >= blueprint.ObsidianRobotClayCost
                            && robots.Obsidian < maxObsidianSpend)
                        {
                            ResourceTypeCount newRobots = robots;
                            newRobots.Obsidian++;
                            ResourceTypeCount newResources = resources;
                            newResources.Ore -= blueprint.ObsidianRobotOreCost;
                            newResources.Clay -= blueprint.ObsidianRobotClayCost;
                            newResources.Ore += robots.Ore;
                            newResources.Clay += robots.Clay;
                            newResources.Obsidian += robots.Obsidian;
                            newResources.Geode += robots.Geode;
                            GetBestFinish(newRobots, newResources, minutesRemaining);
                        }
                    }
                    resources.Ore += robots.Ore;
                    resources.Clay += robots.Clay;
                    resources.Obsidian += robots.Obsidian;
                    resources.Geode += robots.Geode;
                    GetBestFinish(robots, resources, minutesRemaining);
                }
                GetBestFinish(robots, resources, 32);

                blueprintsMax.Add(maxFinish);
            }

            return blueprintsMax[0] * blueprintsMax[1] * blueprintsMax[2];
        }
    }
}
