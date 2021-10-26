using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var result = FilterWalkableTiles(tileContainer, pieces, walkableTilesToTest);

            return result;
        }

        private List<Tile> GetTilesToTest(TileContainer tileContainer)
        {
            var walkableTilesToTest = new List<Tile>();
            
            if (Color == PieceColor.Light)
            {
                var y = Math.Min(TileContainer.BoardDimensionY - 1, Position.y + 1);
                var forwardTilePos = new Vector2Int(Position.x,  y);
                walkableTilesToTest.Add(tileContainer.GetTile(forwardTilePos));
            }
            else
            {
                var y = Math.Max(0, Position.y - 1);
                var forwardTilePos = new Vector2Int(Position.x, y);
                walkableTilesToTest.Add(tileContainer.GetTile(forwardTilePos));
            }

            return walkableTilesToTest;
        }
        // TODO: fix overflow

        private List<Tile> FilterWalkableTiles(TileContainer tileContainer, ChessPieces pieces, List<Tile> walkableTilesToTest)
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
            transform.position = to.transform.position;
            Position = to.Position;
            to.ChessPiece = this;
            from.ChessPiece = null;

            if (CanBePromoted())
            {
                // TODO: change sprite, use different movement pattern, set promoted flag to true
            }
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to)
        {
            return GameUtils.CapturePieceCommon(this, from, to);
        }
        

        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkPawn : _lightPawn;
        }
    }
}