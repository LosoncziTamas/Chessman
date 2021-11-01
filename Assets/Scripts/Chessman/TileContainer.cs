using System.Collections.Generic;
using UnityEngine;

namespace Chessman
{
    public class TileContainer : MonoBehaviour
    {
        public const int BoardDimensionX = 8;
        public const int BoardDimensionY = 8;
        private const float OffsetX = -3.5f;
        private const float OffsetY = -3.5f;
    
        [SerializeField] private Tile _darkTilePrefab;
        [SerializeField] private Tile _lightTilePrefab;
    
        public Tile[] Tiles  = new Tile[BoardDimensionX * BoardDimensionY];

        private void Awake()
        {
            Build();
        }

        public void Build()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        
            for (var x = 0; x < BoardDimensionX; x++)
            {
                for (var y = 0; y < BoardDimensionY; y++)
                {
                    var position = new Vector3(x + OffsetX, y + OffsetY, 0);
                    var tile = Instantiate((x + y) % 2 == 0 ? _darkTilePrefab : _lightTilePrefab, position, Quaternion.identity, transform);
                    tile.Position = new Vector2Int(x, y);
                    Tiles[x * BoardDimensionX + y] = tile;
                }
            }
        }

        public bool OutsideOfBounds(Vector2Int position)
        {
            return position.x < 0 || position.x >= BoardDimensionX || position.y < 0 || position.y >= BoardDimensionY;
        }
        
        public bool InsideBounds(Vector2Int position)
        {
            return !OutsideOfBounds(position);
        }

        public Tile GetTile(Vector2Int pos)
        {
            Debug.Assert(pos.x >= 0 && pos.x < BoardDimensionX, $"x: {pos.x}");
            Debug.Assert(pos.y >= 0 && pos.y < BoardDimensionY, $"y: {pos.y}");
            var result = Tiles[pos.x * BoardDimensionX + pos.y];
            return result;
        }

        public IEnumerable<Tile> GetTiles(IEnumerable<Vector2Int> positions)
        {
            var result = new List<Tile>();

            foreach (var pos in positions)
            {
                result.Add(GetTile(pos));
            }

            return result;
        }
    }
}
