namespace AdventOfCode2024.Days.Day6.DataStructures
{
    internal class MapSpace
    {
        private int _x, _y;

        public MapSpace(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
    }

    internal class Obstacle : MapSpace
    {
        public Obstacle(int x, int y) : base(x, y) { }

        private readonly HashSet<Direction> _guardVisitHistory = new HashSet<Direction>();

        public void AddGuardVisit(Direction direction)
        {
            _guardVisitHistory.Add(direction);
        }

        public bool HasGuardVisited(Direction direction)
        {
            return _guardVisitHistory.Contains(direction);
        }
    }

    internal class EmptySpace : MapSpace
    {
        public bool IsVisited { get; set; }

        public EmptySpace(int x, int y) : base(x, y)
        {
            IsVisited = false;
        }

        private readonly Dictionary<Direction, MapSpace> _neighbors = [];
        public void AddNeighbor(Direction direction, MapSpace neighbor)
        {
            _neighbors[direction] = neighbor;
        }

        public (Obstacle? Obstacle, bool WasStopped) GetObstacleInDirection(Direction direction, int? stopX = null, int? stopY = null)
        {
            if (stopX != null && stopY == null || stopX == null && stopY != null)
            {
                throw new Exception("stopX and stopY must both be null or both be set");
            }

            // Check for map edge
            if (!_neighbors.ContainsKey(direction))
            {
                return (null, false);
            }

            // Handle the different neighbor types
            var neighbor = _neighbors[direction];
            if (neighbor.X == stopX && neighbor.Y == stopY)
            {
                return (null, true);
            }
            else if (neighbor is Obstacle obstacleNeighbor)
            {
                return (obstacleNeighbor, false);
            }
            else if (neighbor is EmptySpace emptyNeighbor)
            {
                var result = emptyNeighbor.GetObstacleInDirection(direction, stopX, stopY);
                return result;
            }
            else
                throw new Exception("invalid state - neighbor is not an obstacle or empty space");
        }
    }

    internal class Guard : MapSpace
    {
        public Direction CurrentDirection { get; set; }

        public Guard(int x, int y, Direction initialDirection) : base(x, y)
        {
            CurrentDirection = initialDirection;
        }

        public (int newX, int newY) GetNextPosition()
        {
            int newX = X, newY = Y;

            switch (CurrentDirection)
            {
                case Direction.North:
                    newY -= 1;
                    break;
                case Direction.East:
                    newX += 1;
                    break;
                case Direction.South:
                    newY += 1;
                    break;
                case Direction.West:
                    newX -= 1;
                    break;
            }

            return (newX, newY);
        }

        public void RotateCW()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new System.Exception("Invalid direction")
            };
        }

        public bool MoveTo(MapSpace targetSpace)
        {
            if (targetSpace is Obstacle obstacle)
            {
                obstacle.AddGuardVisit(CurrentDirection);
                return false; // Can't move to an obstacle
            }

            if (targetSpace is EmptySpace emptySpace)
            {
                emptySpace.IsVisited = true; // Mark the empty space as visited
                base.X = emptySpace.X;
                base.Y = emptySpace.Y;
            }

            return true;
        }
    }

    internal enum Direction
    {
        North,
        East,
        South,
        West
    }
}
