using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelGeneration
{
    public class LevelDrawer : MonoBehaviour
    {
        [SerializeField] private Tilemap _floorsTilemap;
        [SerializeField] private Tilemap _wallsTilemap;
        [SerializeField] private RuleTile _floorTile;
        [SerializeField] private RuleTile _wallTile;

        public void DrawLevel(CellType[,] level)
        {
            DrawLayer(level, GetTilemapFromCellType(CellType.Wall), CellType.Wall);
            DrawLayer(level, GetTilemapFromCellType(CellType.Floor), CellType.Floor);
        }

        private void DrawLayer(CellType[,] level, Tilemap map, CellType type)
        {
            var vectors = new Vector3Int[level.GetLength(0) * level.GetLength(1)];
            var tiles = new RuleTile[level.GetLength(0) * level.GetLength(1)];
            var tile = GetTileFromCellType(type);
            
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    var currentCell = level[i, j];
                    if (currentCell != type) continue;
                    vectors[j + i * level.GetLength(1)] = new Vector3Int(i, j, 0);
                    tiles[j + i * level.GetLength(1)] = tile;
                }    
            }
            map.ClearAllTiles();
            map.SetTiles(vectors, tiles);
        }

        private RuleTile GetTileFromCellType(CellType cellType)
        {
            return cellType switch
            {
                CellType.Wall => _wallTile,
                CellType.Empty => null,
                CellType.Floor => _floorTile,
                _ => throw new InvalidEnumArgumentException()
            };
        }

        private Tilemap GetTilemapFromCellType(CellType cellType)
        {
            return cellType switch
            {
                CellType.Wall => _wallsTilemap,
                _ => _floorsTilemap,
            };
        }
        
    }
}
