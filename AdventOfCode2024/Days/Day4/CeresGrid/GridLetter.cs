namespace AdventOfCode2024.Days.Day4.CeresGrid
{
    internal class GridLetter
    {
        private static readonly Dictionary<string, string> _nextLetter = new()
        {
            { "X", "M" },
            { "M", "A" },
            { "A", "S" }
        };

        private string _letter;
        public string Letter => _letter;

        private int _x, _y;
        public int X => _x;
        public int Y => _y;

        private List<GridLetter> _neighbors;

        public GridLetter(string letter, int x, int y)
        {
            _letter = letter;
            _x = x;
            _y = y;
            _neighbors = new List<GridLetter>();
        }

        public void AddNeighbor(GridLetter neighbor)
        {
            _neighbors.Add(neighbor);
        }

        public int GetWordCount()
        {
            if (_letter != "X")
            {
                return 0;
            }
            else
            {
                var count = 0;
                var nextLetter = _nextLetter[_letter];
                foreach (var neighbor in _neighbors)
                {
                    if (neighbor.Letter == nextLetter)
                    {
                        int i = neighbor.X - X, j = neighbor.Y - Y;
                        if (neighbor.RecursiveWordSearch(i, j))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        private bool RecursiveWordSearch(int i, int j)
        {
            if (_letter == "S")
            {
                return true;
            }
            else
            {
                var nextLetter = _nextLetter[_letter];
                int nextX = X + i, nextY = Y + j;
                foreach (var neighbor in _neighbors)
                {
                    // Only search in straight lines so return the word search result of the neighbor in the right direction with the correct letter
                    if (neighbor.Letter == nextLetter && neighbor.X == nextX && neighbor.Y == nextY)
                    {
                        return neighbor.RecursiveWordSearch(i, j);
                    }
                }
            }
            return false;
        }

        public bool IsNeighbor(GridLetter other)
        {
            return Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
        }

        public bool IsX
        {
            get
            {
                if (_letter != "A")
                {
                    return false;
                }
                else
                {
                    return CheckForX();
                }
            }
        }

        private bool CheckForX()
        {
            int[] topLeft = [-1, -1], topRight = [-1, 1], bottomLeft = [1, -1], bottomRight = [1, 1];
            int[][] top = [topLeft, topRight], bottom = [bottomLeft, bottomRight], left = [topLeft, bottomLeft], right = [topRight, bottomRight];
            int[][][][] pairs = [[top, bottom], [left, right]];

            foreach (var pair in pairs)
            {
                if ((MatchingPair(pair[0], "M") && MatchingPair(pair[1], "S")) || (MatchingPair(pair[0], "S") && MatchingPair(pair[1], "M")))
                {
                    return true;
                }
            }

            return false;
        }

        private bool MatchingPair(int[][] direction, string letter)
        {
            return CheckDirection(direction[0], letter) && CheckDirection(direction[1], letter);
        }

        private bool CheckDirection(int[] direction, string letter)
        {
            int x = X + direction[0], y = Y + direction[1];
            foreach (var neighbor in _neighbors)
            {
                if (neighbor.X == x && neighbor.Y == y && neighbor.Letter == letter)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
