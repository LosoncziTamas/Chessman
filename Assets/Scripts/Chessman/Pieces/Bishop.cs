using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Bishop : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightBishop;
        [SerializeField] private Sprite _darkBishop;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform => gameObject.transform;
        
        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
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
            for (int x = Position.x - 1, y = Position.y + 1; x >= 0 && y < TileContainer.BoardDimensionY; x--, y++)
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
            
            // Backward right
            for (int x = Position.x + 1, y = Position.y - 1; x < TileContainer.BoardDimensionX && y >= 0; x++, y--)
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
            
            // Backward left
            for (int x = Position.x - 1, y = Position.y - 1; x >= 0 && y >= 0; x--, y--)
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
            
            return walkableTiles;
        }

        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            GameUtils.MovePieceCommon(this, from, to);
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return GameUtils.CapturePieceCommon(this, from, to);
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkBishop : _lightBishop;
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, position);
            Position = position;
            Color = color;
        }
    }
}