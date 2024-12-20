﻿using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day3
{
    internal class Day3 : Day
    {
        public override string Part1(string input)
        {
            string pattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)";
            MatchCollection matches = Regex.Matches(input, pattern);

            int result = 0;
            foreach (Match match in matches)
            {
                int num1 = int.Parse(match.Groups[1].Value);
                int num2 = int.Parse(match.Groups[2].Value);
                result += num1 * num2;
            }

            return result.ToString();
        }

        public override string Part2(string input)
        {
            string pattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)|don\'t\(\)|do\(\)";
            MatchCollection matches = Regex.Matches(input, pattern);

            int result = 0;
            int factor = 1;
            foreach (Match match in matches)
            {
                if (match.ToString().Contains("don't"))
                {
                    factor = 0;
                }
                else if (match.ToString().Contains("do"))
                {
                    factor = 1;
                }
                else
                {
                    int num1 = int.Parse(match.Groups[1].Value);
                    int num2 = int.Parse(match.Groups[2].Value);
                    result += factor * num1 * num2;
                }
            }

            return result.ToString();
        }
    }
}
