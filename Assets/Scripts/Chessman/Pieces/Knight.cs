using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Knight : MonoBehaviour, IChessPiece
    {
        public Vector2Int Position { get; set; }
        public PieceColor Color { get; private set; }
        public bool IsCaptured { get; set; }
        public Transform Transform => gameObject.transform;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _lightKnight;
        [SerializeField] private Sprite _darkKnight;
        
        public void Init(Vector2Int position, PieceColor color)
        {
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkKnight : _lightKnight;
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, position);
            Position = position;
            Color = color;
            gameObject.name = $"{color} Knight {position.x}";
        }

        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            GameUtils.MovePieceCommon(this, from, to);
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile @from, Tile to)
        {
            GameUtils.SetSortingOrderBasedOnPosition(_spriteRenderer, to.Position);
            return GameUtils.CapturePieceCommon(this, from, to);
        }

        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer, ChessPieces pieces)
        {
            return Enumerable.Empty<Tile>();
        }
    }
}