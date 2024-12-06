namespace AdventOfCode2024.Days.Day6.DataStructures
{
    internal class Map
    {
        private readonly List<List<MapSpace>> _map = [];
        private int _maxX, _maxY;
        private readonly Guard _guard;

        public Map(string mapString)
        {
            int x = 0, y = 0;
            Guard? guard = null;

            var rows = mapString.Split("\r\n");
            foreach (var row in rows)
            {
                x = 0;
                var mapRow = new List<MapSpace>();
                foreach (var character in row)
                {
                    if (character == '#')
                    {
                        mapRow.Add(new Obstacle(x, y));
                    }
                    else if (character == '.')
                    {
                        mapRow.Add(new EmptySpace(x, y));
                    }
                    else
                    {
                        Direction direction;
                        switch (character)
                        {
                            case '^':
                                direction = Direction.North;
                                break;
                            case '>':
                                direction = Direction.East;
                                break;
                            case 'v':
                                direction = Direction.South;
                                break;
                            case '<':
                                direction = Direction.West;
                                break;
                            default:
                                throw new Exception("Invalid character in map string");
                        }
                        guard = new Guard(x, y, direction);

                        var guardSpace = new EmptySpace(x, y);
                        guardSpace.IsVisited = true;

                        mapRow.Add(guardSpace);
                    }
                    x++;
                }
                _map.Add(mapRow);
                y++;
            }

            _maxX = x;
            _maxY = y;

            if (guard == null)
            {
                throw new Exception("No guard found in map string");
            }
            else
            {
                _guard = guard;
            }
        }

        public int GetUniqueVisits()
        {
            RunSimulation();

            int uniqueVisits = 0;
            foreach (var row in _map)
            {
                foreach (var mapSpace in row)
                {
                    if (mapSpace is EmptySpace emptySpace && emptySpace.IsVisited)
                    {
                        uniqueVisits++;
                    }
                }
            }
            return uniqueVisits;
        }

        private bool _simulationRan = false;
        private void RunSimulation()
        {
            if (_simulationRan)
            {
                return;
            }

            while (true)
            {
                var (newX, newY) = _guard.GetNextPosition();
                if (newX < 0 || newX >= _maxX || newY < 0 || newY >= _maxY)
                {
                    break;
                }
                var newSpace = _map[newY][newX];
                if (!_guard.MoveTo(newSpace))
                {
                    _guard.RotateCW();
                }
            }
            _simulationRan = true;
        }
    }
}
