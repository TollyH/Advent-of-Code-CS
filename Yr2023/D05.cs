namespace AdventOfCode.Yr2023
{
    public static class D05
    {
        public readonly struct Range
        {
            public readonly long Start;
            public readonly long End;

            public Range(long start, long end)
            {
                Start = start;
                End = end;
            }

            public bool Contains(long value)
            {
                return value >= Start && value < End;
            }

            public bool Overlaps(Range range)
            {
                return Start < range.End && range.Start < End;
            }
        }

        public class RangeDictionary
        {
            private readonly Dictionary<Range, Range> backingDictionary = new();

            public long this[long key]
            {
                get
                {
                    KeyValuePair<Range, Range> match = backingDictionary.FirstOrDefault(kv => kv.Key.Contains(key));
                    if (match.Key.Start != match.Key.End)
                    {
                        return match.Value.Start + (key - match.Key.Start);
                    }
                    return key;
                }
            }

            public Range this[Range key]
            {
                set => backingDictionary[key] = value;
            }

            /// <summary>
            /// Gets a list of ranges representing the mapped values of the input ranges.
            /// Accounts for default values where values in the input ranges are unmapped.
            /// </summary>
            public List<Range> GetMatchingRanges(List<Range> ranges)
            {
                List<Range> result = new();
                foreach (Range range in ranges)
                {
                    List<KeyValuePair<Range, Range>> matching = backingDictionary.Where(kv => kv.Key.Overlaps(range)).ToList();
                    for (int i = 0; i < matching.Count; i++)
                    {
                        // Make sure matching ranges only consider the portion that is actually overlapping with the range in the parameter
                        KeyValuePair<Range, Range> overlap = matching[i];
                        Range limitedKey = new(Math.Max(overlap.Key.Start, range.Start), Math.Min(overlap.Key.End, range.End));
                        long startCutoff = limitedKey.Start - overlap.Key.Start;
                        long endCutoff = overlap.Key.End - limitedKey.End;
                        Range limitedValue = new(overlap.Value.Start + startCutoff, overlap.Value.End - endCutoff);
                        matching[i] = new KeyValuePair<Range, Range>(limitedKey, limitedValue);
                    }
                    matching = matching.OrderBy(kv => kv.Key.Start).ToList();
                    if (matching.Count == 0)
                    {
                        matching.Add(new KeyValuePair<Range, Range>(range, range));
                    }
                    else
                    {
                        // Fill in areas where nothing matched for that portion of the parameter range with the indices FROM the parameter range
                        // as "source numbers that aren't mapped correspond to the same destination number"
                        for (int i = 0; i < matching.Count - 1; i++)
                        {
                            if (matching[i].Key.End < matching[i + 1].Key.Start)
                            {
                                Range newRange = new(matching[i].Key.End, matching[i + 1].Key.Start);
                                matching.Insert(i + 1, new KeyValuePair<Range, Range>(newRange, newRange));
                            }
                        }
                        if (range.Start < matching[0].Key.Start)
                        {
                            Range newRange = new(range.Start, matching[0].Key.Start);
                            matching.Add(new KeyValuePair<Range, Range>(newRange, newRange));
                        }
                        if (range.End > matching[^1].Key.End)
                        {
                            Range newRange = new(matching[^1].Key.End, range.End);
                            matching.Add(new KeyValuePair<Range, Range>(newRange, newRange));
                        }
                    }
                    result.AddRange(matching.Select(kv => kv.Value));
                }
                return result;
            }
        }

        public static long PartOne(string[] input)
        {
            RangeDictionary seedToSoil = new();
            RangeDictionary soilToFertilizer = new();
            RangeDictionary fertilizerToWater = new();
            RangeDictionary waterToLight = new();
            RangeDictionary lightToTemperature = new();
            RangeDictionary temperatureToHumidity = new();
            RangeDictionary humidityToLocation = new();
            RangeDictionary[] maps = { seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation };
            long currentMap = 0;

            IEnumerable<long> seeds = input[0].Split(": ")[1].Split(" ").Select(long.Parse);

            foreach (string line in input[3..])
            {
                if (line == "")
                {
                    currentMap++;
                    continue;
                }
                if (line[^1] == ':')
                {
                    continue;
                }
                RangeDictionary map = maps[currentMap];
                string[] components = line.Split(' ');
                long destination = long.Parse(components[0]);
                long source = long.Parse(components[1]);
                long length = long.Parse(components[2]);
                map[new Range(source, source + length)] = new Range(destination, destination + length);
            }

            return seeds
                .Select(seed => seedToSoil[seed])
                .Select(soil => soilToFertilizer[soil])
                .Select(fertilizer => fertilizerToWater[fertilizer])
                .Select(water => waterToLight[water])
                .Select(light => lightToTemperature[light])
                .Select(temperature => temperatureToHumidity[temperature])
                .Select(humidity => humidityToLocation[humidity])
                .Min();
        }

        public static long PartTwo(string[] input)
        {
            RangeDictionary seedToSoil = new();
            RangeDictionary soilToFertilizer = new();
            RangeDictionary fertilizerToWater = new();
            RangeDictionary waterToLight = new();
            RangeDictionary lightToTemperature = new();
            RangeDictionary temperatureToHumidity = new();
            RangeDictionary humidityToLocation = new();
            RangeDictionary[] maps = { seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation };
            long currentMap = 0;

            long[] seedComponents = input[0].Split(": ")[1].Split(" ").Select(long.Parse).ToArray();
            List<Range> seeds = new();
            for (int i = 0; i < seedComponents.Length; i += 2)
            {
                seeds.Add(new Range(seedComponents[i], seedComponents[i] + seedComponents[i + 1]));
            }

            foreach (string line in input[3..])
            {
                if (line == "")
                {
                    currentMap++;
                    continue;
                }
                if (line[^1] == ':')
                {
                    continue;
                }
                RangeDictionary map = maps[currentMap];
                string[] components = line.Split(' ');
                long destination = long.Parse(components[0]);
                long source = long.Parse(components[1]);
                long length = long.Parse(components[2]);
                map[new Range(source, source + length)] = new Range(destination, destination + length);
            }

            return humidityToLocation.GetMatchingRanges(
                    temperatureToHumidity.GetMatchingRanges(
                    lightToTemperature.GetMatchingRanges(
                    waterToLight.GetMatchingRanges(
                    fertilizerToWater.GetMatchingRanges(
                    soilToFertilizer.GetMatchingRanges(
                    seedToSoil.GetMatchingRanges(seeds)))))))
                .Min(r => r.Start);
        }
    }
}