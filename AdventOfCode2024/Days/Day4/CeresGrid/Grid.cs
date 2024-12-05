namespace AdventOfCode2024.Days.Day4.CeresGrid
{
    internal class Grid
    {
        private List<GridLetter> _letters = [];

        public void AddLetter(GridLetter newLetter)
        {
            foreach (var letter in _letters)
            {
                if (letter.IsNeighbor(newLetter))
                {
                    letter.AddNeighbor(newLetter);
                    newLetter.AddNeighbor(letter);
                }
            }

            _letters.Add(newLetter);
        }

        public int GetWordCount()
        {
            var count = 0;
            foreach (var letter in _letters)
            {
                count += letter.GetWordCount();
            }
            return count;
        }

        public int GetXCount()
        {
            var count = 0;
            foreach (var letter in _letters)
            {
                if (letter.Letter == "A")
                {
                    var isX = letter.IsX;
                    if (isX)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
