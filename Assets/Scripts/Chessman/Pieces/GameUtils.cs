using UnityEngine;

namespace Chessman.Pieces
{
    public static class GameUtils
    {
        public static void MovePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            piece.Transform.position = to.transform.position;
            piece.Position = to.Position;
            to.ChessPiece = piece;
            from.ChessPiece = null;
        }

        public static IChessPiece CapturePieceCommon(IChessPiece piece, Tile from, Tile to)
        {
            Debug.Assert(to.HasPiece);
            var capturedPiece = to.ChessPiece;
            
            piece.Transform.position = to.transform.position;
            piece.Position = to.Position;
            to.ChessPiece = piece;
            from.ChessPiece = null;

            return capturedPiece;
        }
    }
}