namespace AdventOfCode2024.Days.Day1.LocationLists
{
    internal class LocationList
    {
        #region Operations

        private readonly List<int> _locations = [];

        public void Add(int locationId)
        {
            _locations.Add(locationId);
        }

        public int this[int index]
        {
            get { return _locations[index]; }
        }

        public void Sort()
        {
            _locations.Sort();
        }

        #endregion

        #region Calculations

        public Dictionary<int, int> CalculateOccurrences()
        {
            Dictionary<int, int> occurrences = [];
            foreach (var location in _locations)
            {
                if (occurrences.TryGetValue(location, out int value))
                {
                    occurrences[location] = value + 1;
                }
                else
                {
                    occurrences.Add(location, 1);
                }
            }
            return occurrences;
        }

        public int CalculateDistance(LocationList locationList)
        {
            Sort();
            locationList.Sort();

            int distance = 0;
            for (int i = 0; i < _locations.Count; i++)
            {
                distance += Math.Abs(_locations[i] - locationList[i]);
            }

            return distance;
        }

        public int CalculateSimilarity(LocationList locationList)
        {
            Dictionary<int, int> occurrences = CalculateOccurrences();
            Dictionary<int, int> locationListOccurrences = locationList.CalculateOccurrences();

            int similarity = 0;
            foreach (var key in occurrences.Keys)
            {
                if (locationListOccurrences.TryGetValue(key, out int value))
                {
                    similarity += (key * occurrences[key] * value);
                }
            }
            return similarity;
        }

        #endregion
    }
}
