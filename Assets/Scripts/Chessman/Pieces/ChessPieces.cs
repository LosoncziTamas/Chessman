using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Chessman.Pieces.GameUtils;

namespace Chessman.Pieces
{
    public class ChessPieces : MonoBehaviour
    {
        [SerializeField] private Pawn _pawnPrefab;
        [SerializeField] private King _kingPrefab;
        [SerializeField] private Bishop _bishopPrefab;
        [SerializeField] private Rook _rookPrefab;
        [SerializeField] private Knight _knightPrefab;
        [SerializeField] private Queen _queenPrefab;

        private TileContainer _tileContainer;
        
        public List<IChessPiece> DarkPieces { get; } = new List<IChessPiece>();
        public List<IChessPiece> LightPieces { get; } = new List<IChessPiece>();

        public IChessPiece GetKing(PieceColor playerColor)
        {
            if (playerColor == PieceColor.Dark)
            {
                var darkKing = DarkPieces.FirstOrDefault(piece => piece.GetType() == typeof(King));
                return darkKing;
            }
            
            var lightKing = LightPieces.FirstOrDefault(piece => piece.GetType() == typeof(King));
            return lightKing;
        }
        
        private void Start()
        {
            _tileContainer = FindObjectOfType<TileContainer>();
            Setup();
        }

        private void Setup()
        {
            SetPawns();
            SetKings();
            SetBishops();
            SetRooks();
            SetKnights();
            SetQueens();
        }

        private IChessPiece CreatePiece<T>(T prefab, Vector2Int position, PieceColor color) where T : Component, IChessPiece
        {
            var tile = _tileContainer.GetTile(position);
            var piece = Instantiate(prefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
            piece.Init(tile.Position, color);
            tile.ChessPiece = piece;
            return piece;
        }

        private void CreatePieceAndAddToList<T>(T prefab, Vector2Int position, PieceColor color) where T : Component, IChessPiece
        {
            var piece = CreatePiece(prefab, position, color);
            if (color == PieceColor.Dark)
            {
                DarkPieces.Add(piece);
            }
            else
            {
                LightPieces.Add(piece);
            }
        }
        
        private void SetQueens()
        {
            CreatePieceAndAddToList(_queenPrefab, new Vector2Int(3, 0), PieceColor.Light);
            CreatePieceAndAddToList(_queenPrefab, new Vector2Int(3, 7), PieceColor.Dark);
        }

        private void SetBishops()
        {
            CreatePieceAndAddToList(_bishopPrefab, new Vector2Int(2, 0), PieceColor.Light);
            CreatePieceAndAddToList(_bishopPrefab, new Vector2Int(5, 0), PieceColor.Light);
            CreatePieceAndAddToList(_bishopPrefab, new Vector2Int(2, 7), PieceColor.Dark);
            CreatePieceAndAddToList(_bishopPrefab, new Vector2Int(5, 7), PieceColor.Dark);
        }

        private void SetPawns()
        {
            for (var x = 0; x < 8; ++x)
            {
                CreatePieceAndAddToList(_pawnPrefab, new Vector2Int(x, 1), PieceColor.Light);
                CreatePieceAndAddToList(_pawnPrefab, new Vector2Int(x, 6), PieceColor.Dark);
            }
        }

        private void SetKings()
        {
            CreatePieceAndAddToList(_kingPrefab, new Vector2Int(4, 0), PieceColor.Light);
            CreatePieceAndAddToList(_kingPrefab, new Vector2Int(4, 7), PieceColor.Dark);
        }

        private void SetKnights()
        {
            CreatePieceAndAddToList(_knightPrefab, new Vector2Int(1, 0), PieceColor.Light);
            CreatePieceAndAddToList(_knightPrefab, new Vector2Int(6, 0), PieceColor.Light);
            CreatePieceAndAddToList(_knightPrefab, new Vector2Int(1, 7), PieceColor.Dark);
            CreatePieceAndAddToList(_knightPrefab, new Vector2Int(6, 7), PieceColor.Dark);
        }

        private void SetRooks()
        {
            CreatePieceAndAddToList(_rookPrefab, new Vector2Int(0, 0), PieceColor.Light);
            CreatePieceAndAddToList(_rookPrefab, new Vector2Int(7, 0), PieceColor.Light);
            CreatePieceAndAddToList(_rookPrefab, new Vector2Int(0, 7), PieceColor.Dark);
            CreatePieceAndAddToList(_rookPrefab, new Vector2Int(7, 7), PieceColor.Dark);
        }
    }
}