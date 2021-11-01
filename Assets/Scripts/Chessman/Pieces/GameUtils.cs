using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        
        public static IEnumerable<Tile> FilterWalkableTiles(IChessPiece forPiece, IEnumerable<Vector2Int> positionsToTest, TileContainer tileContainer)
        {
            var result = new List<Tile>();

            foreach (var position in positionsToTest)
            {
                if (tileContainer.OutsideOfBounds(position))
                {
                    continue;
                }
                
                var tile = tileContainer.GetTile(position);
                if (!tile.HasPiece || forPiece.IsEnemyPieceOnTile(tile))
                {
                    result.Add(tile);
                }
            }
            
            return result;
        }

        public static bool IsCheck(IEnumerable<Tile> walkableTiles, TileContainer tileContainer, ChessPieces chessPieces, PieceColor color)
        {
            var king = chessPieces.GetKing(color);
            var kingTile = tileContainer.GetTile(king.Position);
            return walkableTiles.Contains(kingTile);
        }
    }
}