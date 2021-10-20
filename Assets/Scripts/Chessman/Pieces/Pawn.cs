using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Pawn : MonoBehaviour, IChessPiece
    {
        [SerializeField] private Sprite _lightPawn;
        [SerializeField] private Sprite _darkPawn;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Vector2Int Position { get; private set; }
        
        public PieceColor Color { get; private set; }

        
        public IEnumerable<Tile> GetWalkableTiles(TileContainer tileContainer)
        {
            var movingForward = Color == PieceColor.Light;
            if (movingForward)
            {
                var forwardTilePos = new Vector2Int(Position.x, Position.y + 1);
                yield return tileContainer.GetTile(forwardTilePos);
            }
            else
            {
                var forwardTilePos = new Vector2Int(Position.x, Position.y - 1);
                yield return tileContainer.GetTile(forwardTilePos);
            }
        }

        public void MovePiece(TileContainer tileContainer, Vector2Int to)
        {
            transform.position = tileContainer.GetTile(to).transform.position;
            Position = to;
        }

        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkPawn : _lightPawn;
        }
    }
}