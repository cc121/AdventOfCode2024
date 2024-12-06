namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class SafetyManual
    {
        private readonly Dictionary<string, PageNumber> _pageNumbers = [];
        private List<string> _pageOrdering;

        public List<string> PageOrdering => _pageOrdering;

        public SafetyManual(string manual)
        {
            var pages = manual.Split(",").ToList();
            _pageOrdering = new List<string>(pages);

            SetPageNumbers(pages);
        }

        private void SetPageNumbers(List<string> pages)
        {
            _pageNumbers.Clear();
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

        public void CorrectWithRules(List<List<string>> orderedRules)
        {
            var newPageOrdering = new List<string>();
            foreach (var group in orderedRules)
            {
                foreach (var item in group)
                {
                    if (_pageOrdering.Contains(item))
                    {
                        newPageOrdering.Add(item);
                    }
                }
            }

            _pageOrdering = newPageOrdering;
            SetPageNumbers(newPageOrdering);
        }
    }
}
