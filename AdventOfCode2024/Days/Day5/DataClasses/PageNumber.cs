namespace AdventOfCode2024.Days.Day5.DataClasses
{
    internal class PageNumber(string number)
    {
        private readonly string _number = number;

        public string Number => _number;

        private readonly List<PageNumber> _tail = new();
        public void AddTail(IEnumerable<PageNumber> tail)
        {
            _tail.AddRange(tail);
        }

        public bool TailContains(string number)
        {
            return _tail.Any(pageNumber => pageNumber.Number == number);
        }
    }
}
