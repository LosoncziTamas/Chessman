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
        public Transform Transform => transform;


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

        public void MovePiece(TileContainer tileContainer, Tile from, Tile to)
        {
            transform.position = to.transform.position;
            Position = to.Position;
            to.ChessPiece = this;
            from.ChessPiece = null;
        }

        public IChessPiece MoveAndCapture(TileContainer tileContainer, Tile from, Tile to)
        {
            Debug.Assert(to.HasPiece);
            var capturedPiece = to.ChessPiece;
            
            transform.position = to.transform.position;
            Position = to.Position;
            to.ChessPiece = this;
            from.ChessPiece = null;

            return capturedPiece;
        }
        

        public void Init(Vector2Int position, PieceColor color)
        {
            Position = position;
            Color = color;
            _spriteRenderer.sprite = color == PieceColor.Dark ? _darkPawn : _lightPawn;
        }
    }
}