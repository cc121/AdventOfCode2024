﻿using AdventOfCode2024.Days.Day1.LocationLists;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day1
{
    internal class Day1 : IDay
    {
        public string Run(string input)
        {
            (LocationList list1, LocationList list2) = ParseInput(input);

            return list1.CalculateDistance(list2).ToString();
        }

        private (LocationList, LocationList) ParseInput(string input)
        {
            var pattern = @"(\d+)\s+(\d+)";
            var regex = new Regex(pattern);

            LocationList list1 = new LocationList();
            LocationList list2 = new LocationList();

            foreach (Match match in regex.Matches(input))
            {
                list1.AddLocation(int.Parse(match.Groups[1].Value));
                list2.AddLocation(int.Parse(match.Groups[2].Value));
            }
            return (list1, list2);
        }
    }
}
