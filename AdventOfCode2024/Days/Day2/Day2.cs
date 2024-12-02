using AdventOfCode2024.Days.Day2.DataClasses;

namespace AdventOfCode2024.Days.Day2
{
    internal class Day2 : Day
    {
        public override string Part1(string input)
        {
            Day2Data data = ParseInput(input);

            return data.GetSareReportsCount().ToString();
        }

        public override string Part2(string input)
        {
            Day2Data data = ParseInput(input, allowRemoval: true);

            return data.GetSareReportsCount().ToString();
        }

        private Day2Data ParseInput(string input, bool allowRemoval = false)
        {
            return new Day2Data(input, allowRemoval);
        }
    }
}
