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
                if (CheckManualCorrect(manual))
                    score += manual.MiddlePageNumber ?? throw new Exception("Failed to find middle page number!");
            }
            return score;
        }

        public int CalculateCorrectedOrderScore()
        {

            int score = 0;
            foreach (var manual in _safetyManuals)
            {
                if (!CheckManualCorrect(manual))
                {
                    List<string> expectedNodes = manual.PageOrdering;
                    List<List<string>> orderedRules = OrderRules(_rules, expectedNodes);

                    manual.CorrectWithRules(orderedRules);
                    score += manual.MiddlePageNumber ?? throw new Exception("Failed to find middle page number!");
                }
            }
            return score;
        }

        private bool CheckManualCorrect(SafetyManual manual)
        {
            foreach (var rule in _rules)
            {
                if (!manual.PassesRule(rule))
                {
                    return false;
                }
            }
            return true;
        }

        private List<List<string>> OrderRules(List<Rule> rules, List<string> expectedNodes)
        {
            HashSet<string> nodes = new();
            foreach (var rule in rules)
            {
                nodes.Add(rule.FirstPageNumber);
                nodes.Add(rule.SecondPageNumber);
            }

            RuleGraph graph = new(nodes.ToList(), expectedNodes);
            foreach (var rule in rules)
            {
                graph.AddRule(rule);
            }

            List<List<string>> orderedRules = new();
            while (!graph.Empty)
            {
                orderedRules.Add(graph.RemoveLeaves());
            }
            orderedRules.Reverse();

            return orderedRules;
        }
    }
}
