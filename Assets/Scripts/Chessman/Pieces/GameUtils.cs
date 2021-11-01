using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;

namespace Chessman.Pieces
{
    public static class GameUtils
    {
        public static readonly Vector3 PieceOffset = new Vector3(0f, 0.5f, 0f);

        public static void MovePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            piece.Transform.DOJump(to.transform.position + PieceOffset, 0.6f, 1, 0.6f);
            piece.Position = to.Position;
            to.ChessPiece = piece;
            from.ChessPiece = null;
        }

        public static IChessPiece CapturePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            Debug.Assert(to.HasPiece);
            var capturedPiece = to.ChessPiece;

            MovePieceCommon(piece, from, to);

            return capturedPiece;
        }

        public static void SetSortingOrderBasedOnPosition(SpriteRenderer spriteRenderer, Vector2Int position)
        {
            spriteRenderer.sortingOrder = (TileContainer.BoardDimensionY - position.y) + 1;
        }

        public static bool IsEnemyPieceOnTile(this IChessPiece piece, Tile tileToTest)
        {
            return tileToTest.HasPiece && tileToTest.ChessPiece.Color != piece.Color;
        }

        public static IEnumerable<Tile> FilterWalkableTiles(IChessPiece forPiece, IEnumerable<Vector2Int> positionsToTest, TileContainer tileContainer, ChessPieces chessPieces)
        {
            var walkableTilesToTest = new List<Tile>();
            
            foreach (var position in positionsToTest)
            {
                if (tileContainer.OutsideOfBounds(position))
                {
                    continue;
                }
                
                var tile = tileContainer.GetTile(position);
                if (!tile.HasPiece || forPiece.IsEnemyPieceOnTile(tile))
                {
                    walkableTilesToTest.Add(tile);
                }
            }
            
            var playerKing = chessPieces.GetKing(forPiece.Color);
            var currentTile = tileContainer.GetTile(forPiece.Position);
            var oldPosition = forPiece.Position;

            var result = new List<Tile>();
            
            foreach (var walkableTile in walkableTilesToTest)
            {
                var originalPieceOnWalkable = walkableTile.ChessPiece;
                
                currentTile.ChessPiece = null;
                forPiece.Position = walkableTile.Position;
                walkableTile.ChessPiece = forPiece;
                
                var bishopMoves = Movements.GetMoves(playerKing.Position, Movements.MoveType.Bishop, tileContainer).Where(tileContainer.InsideBounds);
                var bishopTiles = tileContainer.GetTiles(bishopMoves);
                if (bishopTiles.FirstOrDefault(t=> t.HasPiece && playerKing.IsEnemyPieceOnTile(t) && (t.ChessPiece.GetType() == typeof(Bishop) || t.ChessPiece.GetType() == typeof(Queen))))
                {
                    currentTile.ChessPiece = forPiece;
                    forPiece.Position = oldPosition;
                    walkableTile.ChessPiece = originalPieceOnWalkable;
                    continue;
                }
                
                var rookMoves = Movements.GetMoves(playerKing.Position, Movements.MoveType.Rook, tileContainer).Where(tileContainer.InsideBounds);
                var rookTiles = tileContainer.GetTiles(rookMoves);
                if (rookTiles.FirstOrDefault(t=> t.HasPiece && playerKing.IsEnemyPieceOnTile(t) && (t.ChessPiece.GetType() == typeof(Rook) || t.ChessPiece.GetType() == typeof(Queen))))
                {
                    currentTile.ChessPiece = forPiece;
                    forPiece.Position = oldPosition;
                    walkableTile.ChessPiece = originalPieceOnWalkable;
                    continue;
                }
                
                var knightMoves = Movements.GetMoves(playerKing.Position, Movements.MoveType.Knight, tileContainer).Where(tileContainer.InsideBounds);
                var knightTiles = tileContainer.GetTiles(knightMoves);
                if (knightTiles.FirstOrDefault(t=> t.HasPiece && playerKing.IsEnemyPieceOnTile(t) && t.ChessPiece.GetType() == typeof(Knight)))
                {
                    currentTile.ChessPiece = forPiece;
                    forPiece.Position = oldPosition;
                    walkableTile.ChessPiece = originalPieceOnWalkable;
                    continue;
                }
                
                var pawnMoves = Movements.GetMoves(playerKing.Position, forPiece.Color == PieceColor.Light ? Movements.MoveType.PawnForward : Movements.MoveType.PawnBackward, tileContainer).Where(tileContainer.InsideBounds);
                var pawnTiles = tileContainer.GetTiles(pawnMoves);
                if (pawnTiles.FirstOrDefault(t=> t.HasPiece && playerKing.IsEnemyPieceOnTile(t) && t.ChessPiece.GetType() == typeof(Pawn)))
                {
                    currentTile.ChessPiece = forPiece;
                    forPiece.Position = oldPosition;
                    walkableTile.ChessPiece = originalPieceOnWalkable;
                    continue;
                }

                result.Add(walkableTile);
                
                currentTile.ChessPiece = forPiece;
                forPiece.Position = oldPosition;
                walkableTile.ChessPiece = originalPieceOnWalkable;
            }
            
            return result;
        }
    }
}