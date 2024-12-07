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
                        var newObstacle = new Obstacle(x, y);
                        mapRow.Add(newObstacle);
                        if (x > 0)
                        {
                            var leftNeighbor = mapRow[x - 1];
                            if (leftNeighbor is EmptySpace leftEmptySpace)
                            {
                                leftEmptySpace.AddNeighbor(Direction.East, mapRow[x]);
                            }
                        }
                        if (y > 0)
                        {
                            var topNeighbor = _map[y - 1][x];
                            if (topNeighbor is EmptySpace topEmptySpace)
                            {
                                topEmptySpace.AddNeighbor(Direction.South, mapRow[x]);
                            }
                        }
                    }
                    else
                    {
                        // Either empty space or guard (who starts on an empty space)
                        var newEmptySpace = new EmptySpace(x, y);
                        mapRow.Add(newEmptySpace);
                        if (x > 0)
                        {
                            var leftNeighbor = mapRow[x - 1];
                            newEmptySpace.AddNeighbor(Direction.West, leftNeighbor);
                            if (leftNeighbor is EmptySpace leftEmptySpace)
                            {
                                leftEmptySpace.AddNeighbor(Direction.East, mapRow[x]);
                            }
                        }
                        if (y > 0)
                        {
                            var topNeighbor = _map[y - 1][x];
                            newEmptySpace.AddNeighbor(Direction.North, topNeighbor);
                            if (topNeighbor is EmptySpace topEmptySpace)
                            {
                                topEmptySpace.AddNeighbor(Direction.South, mapRow[x]);
                            }
                        }

                        if (character != '.')
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
                            newEmptySpace.IsVisited = true;
                        }
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

        public int GetLoops()
        {
            return RunSimulation();
        }

        private bool _simulationRan = false;

        private int RunSimulation()
        {
            int loops = 0;

            if (_simulationRan)
            {
                return -1;
            }

            while (true)
            {
                var (newX, newY) = _guard.GetNextPosition();
                if (newX < 0 || newX >= _maxX || newY < 0 || newY >= _maxY)
                {
                    break;
                }
                var newSpace = _map[newY][newX];

                // Check for possible loops
                if (newSpace is EmptySpace)
                {
                    if (DetectLoop())
                    {
                        loops++;
                    }
                }

                if (!_guard.MoveTo(newSpace))
                {
                    _guard.RotateCW();
                }
            }
            _simulationRan = true;

            return loops;
        }

        private bool DetectLoop()
        {
            // 
            var currentDirection = _guard.CurrentDirection;
            var rotatedDirection = currentDirection switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new Exception("Invalid direction")
            };

            var currentSpace = _map[_guard.Y][_guard.X];
            if (currentSpace is EmptySpace emptySpace)
            {
                DrawGraph();
                Obstacle? obstacle = emptySpace.GetObstacleInDirection(rotatedDirection);
                var result = obstacle?.HasGuardVisited(rotatedDirection) ?? false;
                return result;
            }
            else
                throw new Exception("Invalid state - guard current position not an empty space");
        }

        private void DrawGraph()
        {
            string filePath = "map_output.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int y = 0; y < _maxY; y++)
                {
                    for (int x = 0; x < _maxX; x++)
                    {
                        if (x == _guard.X && y == _guard.Y)
                        {
                            char guardChar = _guard.CurrentDirection switch
                            {
                                Direction.North => '^',
                                Direction.East => '>',
                                Direction.South => 'v',
                                Direction.West => '<',
                                _ => throw new Exception("Invalid direction")
                            };
                            writer.Write(guardChar);
                        }
                        else
                        {
                            var space = _map[y][x];
                            if (space is EmptySpace emptySpace)
                            {
                                if (emptySpace.IsVisited)
                                {
                                    writer.Write("X");
                                }
                                else
                                {
                                    writer.Write(".");
                                }
                            }
                            else if (space is Obstacle)
                            {
                                writer.Write("#");
                            }
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
