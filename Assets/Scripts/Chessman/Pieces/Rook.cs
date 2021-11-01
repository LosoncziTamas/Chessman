using System.Collections.Generic;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

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
            MovePieceCommon(this, from, to);
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
        }

        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            var possibleMoves = Movements.GetMoves(Position, Movements.MoveType.Rook, tileContainer);
            var result = FilterWalkableTiles(this, possibleMoves, tileContainer, pieces);
            return result;
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return CapturePieceCommon(this, from, to);
        }
    }
}