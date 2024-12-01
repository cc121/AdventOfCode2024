namespace AdventOfCode2024.Days
{
    internal abstract class Day
    {
        public string Run(string input, int partNumber)
        {
            return partNumber switch
            {
                1 => Part1(input),
                2 => Part2(input),
                _ => throw new System.ArgumentOutOfRangeException(nameof(partNumber), "Invalid part number.")
            };
        }

        public abstract string Part1(string input);

        public abstract string Part2(string input);
    }
}
