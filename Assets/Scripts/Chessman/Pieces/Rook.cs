using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Rook : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightRook;
        [SerializeField] private Sprite _darkRook;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform => gameObject.transform;
        
        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkRook : _lightRook;
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, position);
            Position = position;
            Color = color;
            gameObject.name = $"{color} Rook {position.x}";
        }

        public void MovePiece(TileContainer tileContainer, Tile @from, Tile to)
        {
            GameUtils.MovePieceCommon(this, from, to);
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
        }

        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            var walkableTiles = new List<Tile>();
            
            // forward
            for (var y = Position.y + 1; y < TileContainer.BoardDimensionY; y++)
            {
                var positionToTest = new Vector2Int(Position.x, y);
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

            // backward
            for (var y = Position.y - 1; y >= 0; y--)
            {
                var positionToTest = new Vector2Int(Position.x, y);
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
            
            // left
            for (var x = Position.x - 1; x >= 0; x--)
            {
                var positionToTest = new Vector2Int(x, Position.y);
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
            
            // right
            for (var x = Position.x + 1; x < TileContainer.BoardDimensionX; x++)
            {
                var positionToTest = new Vector2Int(x, Position.y);
                var tileToTest = tileContainer.GetTile(positionToTest);

                if (tileToTest.HasPiece)
                {
                    {
                        walkableTiles.Add(tileToTest);
                    }
                    break;
                }
                walkableTiles.Add(tileToTest);
            }

            return walkableTiles;
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return GameUtils.CapturePieceCommon(this, from, to);
        }
    }
}