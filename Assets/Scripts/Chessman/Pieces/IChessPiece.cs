using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public interface IChessPiece
    {
        IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces);
        Vector2Int Position { get; }
        PieceColor Color { get; }
        public bool IsCaptured { get; set; }
        
        Transform Transform { get; }
        void MovePiece(TileContainer tileContainer, Tile from, Tile to);

        IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to);

        void Init(Vector2Int position, PieceColor color);
    }
}