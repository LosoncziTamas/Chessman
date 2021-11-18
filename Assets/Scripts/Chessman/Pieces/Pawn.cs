using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chessman.GUI;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

namespace Chessman.Pieces
{
    public class Pawn : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprites _sprites;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Vector2Int Position { get; set; }
        
        public PieceColor Color { get; private set; }
        
        public bool IsCaptured { get; set; } = false;
        
        public Transform Transform => transform;

        private Movements.MoveType _promotedMoveType = Movements.MoveType.None;

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
            if (_promotedMoveType != Movements.MoveType.None)
            {
                moveType = _promotedMoveType;
            }
            var moves = Movements.GetMoves(Position, moveType, tileContainer);
            return FilterWalkableTiles(this, moves, tileContainer, pieces);
        }
        
        private bool CanBePromoted()
        {
            return Color == PieceColor.Light && Position.y == TileContainer.BoardDimensionY - 1 || Color == PieceColor.Dark && Position.y == 1;
        }
        
        public async Task MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            MovePieceCommon(this, from, to);
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            
            if (CanBePromoted())
            {
                // TODO: ignore game events
                var modal = PromotePieceModal.Instance;
                var promotion = await modal.Show(Color);
                modal.Hide();
                _promotedMoveType = promotion.movementType;
                _spriteRenderer.sprite = promotion.sprite;
            }
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to)
        {
            SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return CapturePieceCommon(this, from, to);
        }
        
        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _sprites.DarkPawn : _sprites.LightPawn;
            SetSortingOrderBasedOnPosition(_spriteRenderer, Position);
            gameObject.name = $"{color} Pawn {position.x}";
        }
    }
}