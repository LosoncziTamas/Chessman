using System.Collections.Generic;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

namespace Chessman.Pieces
{
    public class Knight : MonoBehaviour, IChessPiece
    {
        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform => gameObject.transform;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _lightKnight;
        [SerializeField] private Sprite _darkKnight;
        
        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkKnight : _lightKnight;
            SetSortingOrderBasedOnPosition(_spriteRenderer, position);
            Position = position;
            Color = color;
            gameObject.name = $"{color} Knight {position.x}";
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

        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            var possibleMoves = Movements.GetMoves(Position, Movements.MoveType.Knight, tileContainer);
            var result = FilterWalkableTiles(this, possibleMoves, tileContainer);
            return result;
        }
    }
}