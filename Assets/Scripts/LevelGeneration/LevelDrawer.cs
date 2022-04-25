using System.ComponentModel;
using Entities.HitBoxes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelGeneration
{
    public class LevelDrawer
    {
        private RuleTile _floorTile;
        private RuleTile _wallTile;
        private GameObject _tilemapsParent;
        private Tilemap _floorsTilemap;
        private Tilemap _wallsTilemap;

        public void DrawLevel(CellType[,] level, Material wallsMaterial)
        {
            _floorTile = Resources.Load<RuleTile>("FloorTile");
            _wallTile = Resources.Load<RuleTile>("WallTile");

            _tilemapsParent = new GameObject("Grid");
            _tilemapsParent.AddComponent<Grid>();
            _floorsTilemap = CreateTilemap("Floors Tilemap", _tilemapsParent, -1);
            _wallsTilemap = CreateTilemapWithCollider("Walls Tilemap", _tilemapsParent, 0, wallsMaterial);

            DrawLayer(level, GetTilemapFromCellType(CellType.Wall), CellType.Wall);
            FillAllMap(level, GetTilemapFromCellType(CellType.Floor), CellType.Floor);
        }

        public void Clear()
        {
            Object.Destroy(_tilemapsParent);
        }
        
        private Tilemap CreateTilemap(string tilemapName, GameObject parent, int sortingOrder, Material material = null)
        {
            var go = new GameObject(tilemapName);
            go.transform.parent = parent.transform;
            var tm = go.AddComponent<Tilemap>();
            var tr = go.AddComponent<TilemapRenderer>();
            tr.sortingOrder = sortingOrder;
            if (material)
                tr.material = material;
            tm.tileAnchor = new Vector3(0, 0, 0);

            return tm;
        }

        private Tilemap CreateTilemapWithCollider(string tilemapName, GameObject parent, int sortingOrder, Material material = null)
        {
            var tilemap = CreateTilemap(tilemapName, parent, sortingOrder, material);
            var wallsRigidbody2D = tilemap.gameObject.AddComponent<Rigidbody2D>();
            wallsRigidbody2D.bodyType = RigidbodyType2D.Static;
            var wallsCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
            wallsCollider.usedByComposite = true;
            tilemap.gameObject.AddComponent<CompositeCollider2D>();
            tilemap.gameObject.AddComponent<WallHitBox>();

            return tilemap;
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

        private void FillAllMap(CellType[,] level, Tilemap map, CellType type)
        {
            var vectors = new Vector3Int[level.GetLength(0) * level.GetLength(1)];
            var tiles = new RuleTile[level.GetLength(0) * level.GetLength(1)];
            var tile = GetTileFromCellType(type);
            
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
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
