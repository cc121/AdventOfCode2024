namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class Rule
    {
        private readonly string _firstPageNumber, _secondPageNumber;

        public string FirstPageNumber => _firstPageNumber;
        public string SecondPageNumber => _secondPageNumber;

        public Rule(string rule)
        {
            var parts = rule.Split("|");
            _firstPageNumber = parts[0];
            _secondPageNumber = parts[1];
        }
    }
}
