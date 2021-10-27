using UnityEngine;

namespace Chessman.Pieces
{
    public static class GameUtils
    {
        public static readonly Vector3 PieceOffset = new Vector3(0f, 0.5f, 0f);

        public static void MovePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            piece.Transform.position = to.transform.position + PieceOffset;
            piece.Position = to.Position;
            to.ChessPiece = piece;
            from.ChessPiece = null;
        }

        public static IChessPiece CapturePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            Debug.Assert(to.HasPiece);
            var capturedPiece = to.ChessPiece;
            
            piece.Transform.position = to.transform.position + PieceOffset;
            piece.Position = to.Position;
            to.ChessPiece = piece;
            from.ChessPiece = null;

            return capturedPiece;
        }

        public static void SetSortingOrderBasedOnPosition(SpriteRenderer spriteRenderer, Vector2Int position)
        {
            spriteRenderer.sortingOrder = (TileContainer.BoardDimensionY - position.y) + 1;
        }
    }
}