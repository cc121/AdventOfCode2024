using AdventOfCode2024.Days.Day6.DataStructures;

namespace AdventOfCode2024.Days.Day6
{
    internal class Day6 : Day
    {
        public override string Part1(string input)
        {
            var map = new Map(input);

            return map.GetUniqueVisits().ToString();
        }

        public override string Part2(string input)
        {
            throw new NotImplementedException();
        }
    }
}
