using System;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
    private const int BoardDimensionX = 8;
    private const int BoardDimensionY = 8;
    private const float OffsetX = -3.5f;
    private const float OffsetY = -3.5f;
    
    [SerializeField] private Tile _darkTilePrefab;
    [SerializeField] private Tile _lightTilePrefab;
    
    public Tile[] Tiles  = new Tile[BoardDimensionX * BoardDimensionY];

    private void Start()
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
                Tiles[x * BoardDimensionX + y] = tile;
            }
        }
    }

    public Tile GetTileByPosition(Vector2Int pos)
    {
        Debug.Assert(pos.x > 0 && pos.x < BoardDimensionX);
        Debug.Assert(pos.y > 0 && pos.y < BoardDimensionY);
        var result = Tiles[pos.x * BoardDimensionX + pos.y];
        return result;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Get Tile"))
        {
            var tile = GetTileByPosition(new Vector2Int(3, 3));
            tile.HighLightBorder(Tile.HighlightColorRed);
            tile = GetTileByPosition(new Vector2Int(7, 5));
            tile.HighLightBorder(Tile.HighlightColorYellow);
        }
    }
}
