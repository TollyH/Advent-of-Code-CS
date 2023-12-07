namespace AdventOfCode.Yr2023
{
    public static class D07
    {
        private enum CardType
        {
            FiveOfAKind,
            FourOfAKind,
            FullHouse,
            ThreeOfAKind,
            TwoPair,
            OnePair,
            HighCard
        }

        private static CardType GetCardType(string hand, bool doJokerCheck)
        {
            Dictionary<char, int> charCounts = hand.Distinct().ToDictionary(c => c, c => hand.Count(h => h == c));
            Dictionary<char, int> charCountsNoJoker = new(charCounts);
            _ = charCountsNoJoker.Remove('J');
            bool hasJoker = doJokerCheck && charCounts.ContainsKey('J');
            if (charCounts.Count == 1 || (hasJoker && charCounts.Count == 2))
            {
                return CardType.FiveOfAKind;
            }
            if (charCounts.ContainsValue(4) || (hasJoker && charCountsNoJoker.ContainsValue(4 - charCounts['J'])))
            {
                return CardType.FourOfAKind;
            }
            if ((charCounts.Count == 2 && charCounts.ContainsValue(3))
                || (hasJoker && charCounts.Count <= 3 && charCountsNoJoker.ContainsValue(3 - charCounts['J'])))
            {
                return CardType.FullHouse;
            }
            if (charCounts.ContainsValue(3) || (hasJoker && charCountsNoJoker.ContainsValue(3 - charCounts['J'])))
            {
                return CardType.ThreeOfAKind;
            }
            if (charCounts.Count(kv => kv.Value == 2) == 2 || (hasJoker && charCountsNoJoker.ContainsValue(2)))
            {
                return CardType.TwoPair;
            }
            if (charCounts.ContainsValue(2) || hasJoker)
            {
                return CardType.OnePair;
            }
            return CardType.HighCard;
        }

        public static int PartOne(string[] input)
        {
            char[] cardOrder = new[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

            List<(string Hand, int Bid)> hands = input.Select(h => h.Split(' '))
                .Select(split => (split[0], int.Parse(split[1]))).ToList();
            hands.Sort((a, b) =>
            {
                CardType aType = GetCardType(a.Hand, false);
                CardType bType = GetCardType(b.Hand, false);
                if (aType == bType)
                {
                    for (int i = 0; i < a.Hand.Length; i++)
                    {
                        int aIndex = Array.IndexOf(cardOrder, a.Hand[i]);
                        int bIndex = Array.IndexOf(cardOrder, b.Hand[i]);
                        if (aIndex != bIndex)
                        {
                            return bIndex.CompareTo(aIndex);
                        }
                    }
                }
                return bType.CompareTo(aType);
            });

            int winnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                winnings += hands[i].Bid * (i + 1);
            }
            return winnings;
        }

        public static int PartTwo(string[] input)
        {
            char[] cardOrder = new[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

            List<(string Hand, int Bid)> hands = input.Select(h => h.Split(' '))
                .Select(split => (split[0], int.Parse(split[1]))).ToList();
            hands.Sort((a, b) =>
            {
                CardType aType = GetCardType(a.Hand, true);
                CardType bType = GetCardType(b.Hand, true);
                if (aType == bType)
                {
                    for (int i = 0; i < a.Hand.Length; i++)
                    {
                        int aIndex = Array.IndexOf(cardOrder, a.Hand[i]);
                        int bIndex = Array.IndexOf(cardOrder, b.Hand[i]);
                        if (aIndex != bIndex)
                        {
                            return bIndex.CompareTo(aIndex);
                        }
                    }
                }
                return bType.CompareTo(aType);
            });

            int winnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                winnings += hands[i].Bid * (i + 1);
            }
            return winnings;
        }
    }
}