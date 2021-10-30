using System.Collections.Generic;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

namespace Chessman.Pieces
{
    public class King : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightKing;
        [SerializeField] private Sprite _darkKing;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform => gameObject.transform;
        
        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            var left = new Vector2Int(Position.x - 1, Position.y);
            var right = new Vector2Int(Position.x + 1, Position.y);
            var forward = new Vector2Int(Position.x, Position.y + 1);
            var backward = new Vector2Int(Position.x, Position.y - 1);
            var leftForward = new Vector2Int(Position.x - 1, Position.y + 1);
            var leftBackward = new Vector2Int(Position.x - 1, Position.y - 1);
            var rightBackward = new Vector2Int(Position.x + 1, Position.y - 1);
            var rightForward = new Vector2Int(Position.x + 1, Position.y + 1);

            var result = FilterWalkableTiles(this, new List<Vector2Int>
            {
                left,
                right,
                forward,
                backward,
                leftForward,
                leftBackward,
                rightBackward,
                rightForward,
            }, tileContainer);
            
            return result;
        }

        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            MovePieceCommon(this, from, to);
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return CapturePieceCommon(this, from, to);
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkKing : _lightKing;
            SetSortingOrderBasedOnPosition(_spriteRenderer, Position);
            gameObject.name = $"{color} King";
        }
    }
}