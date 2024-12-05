using AdventOfCode2024.Days.Day4.CeresGrid;

namespace AdventOfCode2024.Days.Day4
{
    internal class Day4 : Day
    {
        public override string Part1(string input)
        {
            var grid = ParseInput(input);

            return grid.GetWordCount().ToString();
        }

        public override string Part2(string input)
        {
            throw new NotImplementedException();
        }

        private static Grid ParseInput(string input)
        {
            int x = 0, y = 0;
            var grid = new Grid();

            var rows = input.Split("\r\n");
            foreach (var row in rows)
            {
                x = 0;
                foreach (var letter in row)
                {
                    grid.AddLetter(new GridLetter(letter.ToString(), x, y));
                    x++;
                }
                y++;
            }

            return grid;
        }
    }
}
