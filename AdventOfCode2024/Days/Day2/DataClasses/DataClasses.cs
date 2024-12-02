namespace AdventOfCode2024.Days.Day2.DataClasses
{
    internal class Day2Data
    {
        private readonly List<Day2Reports> _reports = [];

        public Day2Data(string input)
        {
            var rows = input.Split("\n");
            foreach (var row in rows)
            {
                _reports.Add(new Day2Reports(row));
            }
        }

        public int GetSareReportsCount()
        {
            return _reports.Count(r => r.Safe);
        }
    }

    internal class Day2Reports
    {
        private readonly List<int> _report;

        public Day2Reports(string row)
        {
            _report = row.Split(" ").Select(int.Parse).ToList();

            _safe = DetermineSafety();
        }

        #region Safe Methods

        private bool _safe = false;
        public bool Safe => _safe;

        private readonly int[] _safeRange = [1, 3];

        private bool DetermineSafety()
        {
            var wasSafe = true;

            int index = 1;
            int previous = _report[0];
            int next;
            bool? isIncreasing = null;

            while (index < _report.Count)
            {
                next = _report[index];

                var delta = Math.Abs(next - previous);
                if (delta < _safeRange[0] || delta > _safeRange[1])
                {
                    wasSafe = false;
                    break;
                }

                if (isIncreasing == null && next != previous)
                {
                    isIncreasing = next > previous;
                }
                else if ((isIncreasing == true && next < previous) || (isIncreasing == false && next > previous))
                {
                    wasSafe = false;
                    break;
                }

                previous = next;
                index++;
            }

            return wasSafe;
        }

        #endregion
    }
}
