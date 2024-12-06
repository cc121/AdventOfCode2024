using AdventOfCode2024.Days.Day5.DataClasses;

namespace AdventOfCode2024.Days.Day5
{
    internal class Day5 : Day
    {
        public override string Part1(string input)
        {
            var printQueue = ParseInput(input);

            return printQueue.CalculateCorrectOrderScore().ToString();
        }

        public override string Part2(string input)
        {
            var printQueue = ParseInput(input);

            return printQueue.CalculateCorrectedOrderScore().ToString();
        }

        private static PrintQueue ParseInput(string input)
        {
            PrintQueue printQueue = new();

            var rows = input.Split("\r\n");
            bool endOfRules = false;
            foreach (var row in rows)
            {
                if (row == "")
                {
                    endOfRules = true;
                    continue;
                }

                if (!endOfRules)
                {
                    printQueue.AddRule(row);
                }
                else
                {
                    printQueue.AddManual(row);
                }
            }

            return printQueue;
        }
    }
}
