using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public interface IChessPiece
    {
        IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer);
        Vector2Int Position { get; }
        PieceColor Color { get; }
        void MovePiece(TileContainer tileContainer, Tile from, Tile to);

        void Init(Vector2Int position, PieceColor color);
    }
}