namespace AdventOfCode2024.Days.Day1.LocationLists
{
    internal class LocationList
    {
        private readonly List<int> _locations = [];

        public void AddLocation(int locationId)
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
    }
}
