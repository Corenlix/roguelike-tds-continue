using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace LevelGeneration
{
    public class LevelDrawer : MonoBehaviour
    {
        [SerializeField] private Tilemap _floorsTilemap;
        [SerializeField] private Tilemap _wallsTilemap;
        [FormerlySerializedAs("_floorTale")] [SerializeField] private RuleTile _floorTile;
        [SerializeField] private RuleTile _wallTile;
        [SerializeField] private int _offset;
        
        public void DrawLevel(CellType[,] level)
        {
            DrawLevelBackground(level.GetLength(0), level.GetLength(1));
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(0); j++)
                {
                    var currentCell = level[i, j];
                    var tile = GetTileFromCellType(currentCell);
                    var palette = GetTilemapFromCellType(currentCell);
                    palette.SetTile(new Vector3Int(i, j, 0), tile);
                }    
            }
        }

        private void DrawLevelBackground(int levelWidth, int levelHeight)
        {
            _floorsTilemap.ClearAllTiles();
            _wallsTilemap.ClearAllTiles();
            for (int i = -_offset; i < levelWidth + _offset; i++)
            {
                for (int j = -_offset; j < levelHeight + _offset; j++)
                {
                    if(i < 0 || i >= levelWidth || j < 0 || j >= levelHeight)
                        GetTilemapFromCellType(CellType.Wall).SetTile(new Vector3Int(i, j, 0), GetTileFromCellType(CellType.Wall));
                    else
                        GetTilemapFromCellType(CellType.Floor).SetTile(new Vector3Int(i, j, 0), GetTileFromCellType(CellType.Floor));
                }
            }
        }
        
        
        private RuleTile GetTileFromCellType(CellType cellType)
        {
            return cellType switch
            {
                CellType.Wall => _wallTile,
                CellType.Empty => null,
                _ => _floorTile,
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
