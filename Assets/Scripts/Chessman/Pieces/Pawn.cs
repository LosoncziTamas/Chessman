using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

namespace Chessman.Pieces
{
    public class Pawn : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightPawn;
        [SerializeField] private Sprite _darkPawn;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Vector2Int Position { get; set; }
        
        public PieceColor Color { get; private set; }
        
        public bool IsCaptured { get; set; } = false;
        
        public Transform Transform => transform;

        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            if (IsCaptured)
            {
                return Enumerable.Empty<Tile>();
            }
            
            var walkableTilesToTest = GetTilesToTest(tileContainer, pieces);

            return walkableTilesToTest;
        }

        private IEnumerable<Tile> GetTilesToTest(TileContainer tileContainer, ChessPieces pieces)
        {
            var moveType = Color == PieceColor.Light ? Movements.MoveType.PawnForward : Movements.MoveType.PawnBackward;
            var moves = Movements.GetMoves(Position, moveType, tileContainer);
            return FilterWalkableTiles(this, moves, tileContainer, pieces);
        }
        
        private bool CanBePromoted()
        {
            return Color == PieceColor.Light && Position.y == TileContainer.BoardDimensionY || Color == PieceColor.Dark && Position.y == 0;
        }
        
        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            MovePieceCommon(this, from, to);
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            
            if (CanBePromoted())
            {
                // TODO: change sprite, use different movement pattern, set promoted flag to true
            }
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to)
        {
            return CapturePieceCommon(this, from, to);
        }
        
        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkPawn : _lightPawn;
            SetSortingOrderBasedOnPosition(_spriteRenderer, Position);
            gameObject.name = $"{color} Pawn {position.x}";
        }
    }
}