using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelGeneration
{
    public class LevelDrawer
    {
        private RuleTile _floorTile;
        private RuleTile _wallTile;
        private Tilemap _floorsTilemap;
        private Tilemap _wallsTilemap;

        public void DrawLevel(CellType[,] level)
        {
            _floorTile = Resources.Load<RuleTile>("FloorTile");
            _wallTile = Resources.Load<RuleTile>("WallTile");

            var grid = new GameObject("Grid");
            grid.AddComponent<Grid>();
            _floorsTilemap = CreateTilemap("Floors Tilemap", grid);
            _wallsTilemap = CreateTilemap("Walls Tilemap", grid);
            
            var wallsRigidbody2D = _wallsTilemap.gameObject.AddComponent<Rigidbody2D>();
            wallsRigidbody2D.bodyType = RigidbodyType2D.Static;
            _wallsTilemap.gameObject.AddComponent<TilemapCollider2D>();
            _wallsTilemap.gameObject.AddComponent<CompositeCollider2D>();

            DrawLayer(level, GetTilemapFromCellType(CellType.Wall), CellType.Wall);
            DrawLayer(level, GetTilemapFromCellType(CellType.Floor), CellType.Floor);
        }
        
        private Tilemap CreateTilemap(string tilemapName, GameObject parent)
        {
            var go = new GameObject(tilemapName);
            go.transform.parent = parent.transform;
            var tm = go.AddComponent<Tilemap>();
            var tr = go.AddComponent<TilemapRenderer>();
            tm.tileAnchor = new Vector3(0, 0, 0);

            return tm;
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
