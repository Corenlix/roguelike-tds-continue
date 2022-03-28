using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LevelGeneration
{
    class Walker
    {
        const int MaxErrorSteps = 3000;
        
        private readonly int _rotate90Chance;
        private readonly int _rotate180Chance;
        private readonly CellType _cellType;
        
        private int _madeErrorSteps;
        private Direction _currentDirection;
        private Vector2Int _position;
        private CellType[,] _level;

        public Walker(CellType cellType, int rotate90Chance, int rotate180Chance)
        {
            _cellType = cellType;
            _rotate90Chance = rotate90Chance;
            _rotate180Chance = rotate180Chance;
        }

        public CellType[,] Walk(Dungeon dungeon, CellType[,] levelTable, float roomPart)
        {
            var rooms = dungeon.Rooms;
            foreach (var room in rooms)
            {
                _madeErrorSteps = 0;
                
                var roomRect = room.Rect;
                int roomSquare = roomRect.size.x * roomRect.size.y;
                int stepsCount = (int) (roomSquare * roomPart);
                levelTable = Walk(levelTable, new Vector2Int((int) roomRect.center.x, (int) roomRect.center.y), stepsCount);
            }

            return levelTable;
        }
        
        private CellType[,] Walk(CellType[,] levelTable, Vector2Int startPosition, int steps) 
        {
            _level = levelTable;
            _position = startPosition;

            SetRandomDirection();
            while (steps > 0 && _madeErrorSteps < MaxErrorSteps)
            {
                Move();
                if (ChangeCell())
                    steps -= 1;
            }

            return levelTable;
        }

        private void Move()
        {
            _position += GetMoveDelta();
            if (Random.Range(0, 100) < _rotate90Chance)
                Rotate90();
            else if (Random.Range(0, 100) < _rotate180Chance)
                Rotate180();

            if (_position.x >= _level.GetLength(0) || _position.y >= _level.GetLength(1) || _position.x < 0 || _position.y < 0)
            {
                _position.x = Mathf.Clamp(_position.x, 0, _level.GetLength(0) -1);
                _position.y = Mathf.Clamp(_position.y, 0, _level.GetLength(1) - 1);
                Rotate90();
            }
        }
        
        private bool ChangeCell()
        {
            _madeErrorSteps++;
            if (_level[_position.x, _position.y] != (int)CellType.Wall) return false;
            
            _level[_position.x, _position.y] = _cellType;
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
            _currentDirection = (Direction)Random.Range(0, 3);
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
