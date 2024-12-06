namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class PrintQueue
    {
        private readonly List<Rule> _rules = new();

        public void AddRule(string rule)
        {
            _rules.Add(new Rule(rule));
        }

        private readonly List<SafetyManual> _safetyManuals = new();

        public void AddManual(string manual)
        {
            _safetyManuals.Add(new SafetyManual(manual));
        }

        public int CalculateCorrectOrderScore()
        {
            int score = 0;
            foreach (var manual in _safetyManuals)
            {
                bool updateCorrect = true;
                foreach (var rule in _rules)
                {
                    if (!manual.PassesRule(rule))
                    {
                        updateCorrect = false;
                        break;
                    }
                }
                if (updateCorrect)
                    score += manual.MiddlePageNumber ?? throw new Exception("Failed to find middle page number!");
            }
            return score;
        }
    }
}
