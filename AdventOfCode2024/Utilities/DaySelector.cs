using AdventOfCode2024.Days;
using AdventOfCode2024.Days.Day1;
using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day3;

namespace AdventOfCode2024.Utilities
{
    internal static class DaySelector
    {
        public static Day SelectDay(int dayNumber)
        {
            return dayNumber switch
            {
                1 => new Day1(),
                2 => new Day2(),
                3 => new Day3(),
                /*4 => new Day4(),
                5 => new Day5(),
                6 => new Day6(),
                7 => new Day7(),
                8 => new Day8(),
                9 => new Day9(),
                10 => new Day10(),
                11 => new Day11(),
                12 => new Day12(),
                13 => new Day13(),
                14 => new Day14(),
                15 => new Day15(),
                16 => new Day16(),
                17 => new Day17(),
                18 => new Day18(),
                19 => new Day19(),
                20 => new Day20(),
                21 => new Day21(),
                22 => new Day22(),
                23 => new Day23(),
                24 => new Day24(),
                25 => new Day25(),*/
                _ => throw new ArgumentOutOfRangeException(nameof(dayNumber), "Invalid day number.")
            };
        }
    }
}
