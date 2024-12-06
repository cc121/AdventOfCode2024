namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class SafetyManual
    {
        private readonly Dictionary<string, PageNumber> _pageNumbers = new();

        public SafetyManual(string manual)
        {
            var pages = manual.Split(",").ToList();
            pages.Reverse();

            int middleIndex = pages.Count / 2;
            for (int i = 0; i < pages.Count; i++)
            {
                var page = pages[i];
                var pageNumber = new PageNumber(page);
                pageNumber.AddTail(_pageNumbers.Values);
                _pageNumbers.Add(page, pageNumber);

                if (i == middleIndex)
                {
                    _middlePageNumber = int.Parse(page);
                }
            }
        }

        private int? _middlePageNumber = null;

        public int? MiddlePageNumber => _middlePageNumber;

        public bool PassesRule(Rule rule)
        {
            if (_pageNumbers.ContainsKey(rule.FirstPageNumber) && _pageNumbers.ContainsKey(rule.SecondPageNumber))
            {
                var pageNumber = _pageNumbers[rule.FirstPageNumber];
                return pageNumber.TailContains(rule.SecondPageNumber);
            }
            else
                return true;
        }
    }
}
