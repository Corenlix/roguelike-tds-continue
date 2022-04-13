using UnityEngine;

namespace LevelGeneration.WalkerGenerator
{
    class WalkerLevelGenerator
    {
        private Vector2Int StartPosition =>
            new Vector2Int(_levelTable.GetLength(0) / 2, _levelTable.GetLength(1) / 2);
            
        private const int MapOffset = 5;
        private const int MaxErrorSteps = 100000;

        private readonly WalkerLevelStaticData _levelData;
        private const CellType CellType = LevelGeneration.CellType.Floor;
        private int _madeErrorSteps;
        private Direction _currentDirection;
        private Vector2Int _position;
        private CellType[,] _levelTable;

        public WalkerLevelGenerator(WalkerLevelStaticData levelData)
        {
            _levelData = levelData;
        }
        
        public Level Generate()
        {
            _levelTable = new CellType[_levelData.LevelSize.x + MapOffset, _levelData.LevelSize.y + MapOffset];
            Walk(_levelData.Steps);
            var level = new Level(_levelTable);
            LevelScaler.Scale(level, _levelData.Scale);
            return level;
        }

        private void Walk(int steps) 
        {
            _position = StartPosition;
            SetRandomDirection();
            while (steps > 0 && _madeErrorSteps < MaxErrorSteps)
            {
                Move();
                if (ChangeCell())
                    steps -= 1;
            }
        }

        private void Move()
        {
            _position += GetMoveDelta();
            if (Random.Range(0, 100) < _levelData.Rotate90Chance)
                Rotate90();
            else if (Random.Range(0, 100) < _levelData.Rotate180Chance)
                Rotate180();

            if (_position.x >= _levelData.LevelSize.x || _position.y >= _levelData.LevelSize.y || _position.x < MapOffset || _position.y < MapOffset)
            {
                _position = StartPosition;
                Rotate90();
            }
        }
        
        private bool ChangeCell()
        {
            _madeErrorSteps++;
            if (_levelTable[_position.x, _position.y] != (int)CellType.Wall) return false;
            
            _levelTable[_position.x, _position.y] = CellType;
            _madeErrorSteps = 0;
            return true;
        }

        private void Rotate90() 
        {
            var leftOrRight = Random.Range(0, 100);
            if (leftOrRight > 50)
                _currentDirection = (Direction)(((int)_currentDirection + 1) % 4);
            else
                _currentDirection = (Direction)(((int)_currentDirection - 1) % 4);
        }
        
        private void Rotate180() 
        {
            _currentDirection = (Direction)(((int)_currentDirection + 2) % 4);
        }

        private Vector2Int GetMoveDelta()
        {
            return _currentDirection switch
            {
                Direction.Up => new Vector2Int(0, 1),
                Direction.Down => new Vector2Int(0, -1),
                Direction.Left => new Vector2Int(-1, 0),
                Direction.Right => new Vector2Int(1, 0),
                _ => new Vector2Int(0, 0)
            };
        }
        
        private void SetRandomDirection() 
        {
            _currentDirection = (Direction)Random.Range(0, 4);
        }
        
        private enum Direction 
        {
            Up,
            Right,
            Down,
            Left
        }
    }
}