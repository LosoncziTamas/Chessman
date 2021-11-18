using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

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
            var possibleMoves = Movements.GetMoves(Position, Movements.MoveType.Bishop, tileContainer);
            var result = FilterWalkableTiles(this, possibleMoves, tileContainer, pieces);
            return result;
        }

        public Task MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            MovePieceCommon(this, from, to);
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return Task.CompletedTask;
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return CapturePieceCommon(this, from, to);
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkBishop : _lightBishop;
            SetSortingOrderBasedOnPosition(_spriteRenderer, position);
            Position = position;
            Color = color;
            gameObject.name = $"{color} Bishop {position.x}";
        }
    }
}