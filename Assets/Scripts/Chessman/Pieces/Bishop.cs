using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Bishop : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightBishop;
        [SerializeField] private Sprite _darkBishop;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector2Int Position { get; private set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform { get; }
        
        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            //TileContainer.BoardDimensionX;
            //TileContainer.BoardDimensionY;

            var walkableTiles = new List<Tile>();
            
            // Forward right
            for (int x = Position.x + 1, y = Position.y + 1; x < TileContainer.BoardDimensionX && y < TileContainer.BoardDimensionY; x++, y++)
            {
                var positionToTest = new Vector2Int(x, y);
                var tileToTest = tileContainer.GetTile(positionToTest);

                if (tileToTest.HasPiece)
                {
                    if (tileToTest.ChessPiece.Color != Color)
                    {
                        walkableTiles.Add(tileToTest);
                    }
                    break;
                }

                walkableTiles.Add(tileToTest);
            }
            
            // Forward left
            for (int x = Position.x - 1, y = Position.y + 1; x > 0 && y < TileContainer.BoardDimensionY; x--, y++)
            {
                var positionToTest = new Vector2Int(x, y);
                var tileToTest = tileContainer.GetTile(positionToTest);

                if (tileToTest.HasPiece)
                {
                    if (tileToTest.ChessPiece.Color != Color)
                    {
                        walkableTiles.Add(tileToTest);
                    }
                    break;
                }
                walkableTiles.Add(tileToTest);
            }

             // TODO: add rest
            
            return walkableTiles;
        }
        
        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            return null;
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkBishop : _lightBishop;
            Position = position;
            Color = color;

        }
    }
}