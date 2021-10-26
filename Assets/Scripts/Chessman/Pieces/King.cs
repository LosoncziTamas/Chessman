using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chessman.Pieces
{
    public class King : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightKing;
        [SerializeField] private Sprite _darkKing;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            return Enumerable.Empty<Tile>();
        }

        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform { get; }
        
        public void MovePiece(TileContainer tileContainer, Tile @from, Tile to)
        {
            
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            throw null;
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkKing : _lightKing;
        }
    }
}