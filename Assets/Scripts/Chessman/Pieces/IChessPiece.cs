using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public interface IChessPiece
    {
        Vector2Int Position { get; set; }
        PieceColor Color { get; }
        public bool IsCaptured { get; set; }
        Transform Transform { get; }
        
        void Init(Vector2Int position, PieceColor color);
        void MovePiece(TileContainer tileContainer, Tile from, Tile to);
        IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces);
        IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to);
    }
}