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
            
            var walkableTilesToTest = GetTilesToTest(tileContainer);

            return walkableTilesToTest;
        }

        private IEnumerable<Tile> GetTilesToTest(TileContainer tileContainer)
        {
            var moveType = Color == PieceColor.Light ? Movements.MoveType.PawnForward : Movements.MoveType.PawnBackward;
            var moves = Movements.GetMoves(Position, moveType, tileContainer);
            return FilterWalkableTiles(this, moves, tileContainer);
        }
        
        // TODO: fix overflow
        private List<Tile> FilterWalkableTilesOld(TileContainer tileContainer, ChessPieces pieces, List<Tile> walkableTilesToTest)
        {
            var testedWalkableTiles = new List<Tile>();
            
            var playerKing = pieces.GetKing(Color);
            var playerKingTile = tileContainer.GetTile(playerKing.Position);
            var currentTile = tileContainer.GetTile(Position);
            var oldPosition = Position;
            
            foreach (var walkableTile in walkableTilesToTest)
            {
                var originalPieceOnWalkable = walkableTile.ChessPiece;
                
                currentTile.ChessPiece = null;
                Position = walkableTile.Position;
                walkableTile.ChessPiece = this;

                var enemyPieces = Color == PieceColor.Light ? pieces.DarkPieces : pieces.LightPieces;

                var isCheck = false;
                foreach (var enemyPiece in enemyPieces)
                {
                    if (enemyPiece.Position == walkableTile.Position)
                    {
                        continue;
                    }
                    
                    if (enemyPiece.GetWalkableTiles(tileContainer, pieces).Contains(playerKingTile))
                    {
                        isCheck = true;
                        break;
                    }
                }

                if (!isCheck)
                {
                    testedWalkableTiles.Add(walkableTile);
                }
                
                currentTile.ChessPiece = this;
                Position = oldPosition;
                walkableTile.ChessPiece = originalPieceOnWalkable;
            }

            return testedWalkableTiles;
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