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

        private void SetQueens()
        {
            {
                var tile = _tileContainer.GetTile(new Vector2Int(3, 0));
                var lightQueen = Instantiate(_queenPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightQueen.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightQueen;

                LightPieces.Add(lightQueen);
            }
            
            {
                var tile = _tileContainer.GetTile(new Vector2Int(3, 7));
                var darkQueen = Instantiate(_queenPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkQueen.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkQueen;

                DarkPieces.Add(darkQueen);
            }
        }

        private void SetBishops()
        {
            {
                var tile = _tileContainer.GetTile(new Vector2Int(2, 0));
                var lightBishop = Instantiate(_bishopPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightBishop.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightBishop;

                LightPieces.Add(lightBishop);
            }
            {
                var tile = _tileContainer.GetTile(new Vector2Int(5, 0));
                var lightBishop = Instantiate(_bishopPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightBishop.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightBishop;

                LightPieces.Add(lightBishop);
            }

            { 
                var tile = _tileContainer.GetTile(new Vector2Int(2, 7));
                var darkBishop = Instantiate(_bishopPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkBishop.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkBishop;
                
                DarkPieces.Add(darkBishop);
            }
            { 
                var tile = _tileContainer.GetTile(new Vector2Int(5, 7));
                var darkBishop = Instantiate(_bishopPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkBishop.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkBishop;
                
                DarkPieces.Add(darkBishop);
            }
        }

        private void SetPawns()
        {
            for (var x = 0; x < 8; ++x)
            {
                var tile = _tileContainer.GetTile(new Vector2Int(x, 1));
                var lightPawn = Instantiate(_pawnPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightPawn.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightPawn;

                LightPieces.Add(lightPawn);
                
                tile = _tileContainer.GetTile(new Vector2Int(x, 6));
                var darkPawn = Instantiate(_pawnPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkPawn.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkPawn;
                
                DarkPieces.Add(darkPawn);
            }
        }

        private void SetKings()
        {
            var tile = _tileContainer.GetTile(new Vector2Int(4, 0));
            var lightKing = Instantiate(_kingPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
            lightKing.Init(tile.Position, PieceColor.Light);
            tile.ChessPiece = lightKing;

            LightPieces.Add(lightKing);
            
            tile = _tileContainer.GetTile(new Vector2Int(4, 7));
            var darkKing = Instantiate(_kingPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
            darkKing.Init(tile.Position, PieceColor.Dark);
            tile.ChessPiece = darkKing;

            DarkPieces.Add(darkKing);
        }

        private void SetKnights()
        {
            {
                var tile = _tileContainer.GetTile(new Vector2Int(1, 0));
                var lightKnight = Instantiate(_knightPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightKnight.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightKnight;

                LightPieces.Add(lightKnight);
            }
            {
                var tile = _tileContainer.GetTile(new Vector2Int(6, 0));
                var lightKnight = Instantiate(_knightPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightKnight.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightKnight;

                LightPieces.Add(lightKnight);
            }
            
            {
                var tile = _tileContainer.GetTile(new Vector2Int(1, 7));
                var darkKnight = Instantiate(_knightPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkKnight.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkKnight;

                DarkPieces.Add(darkKnight);
            }
            {
                var tile = _tileContainer.GetTile(new Vector2Int(6, 7));
                var darkKnight = Instantiate(_knightPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkKnight.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkKnight;

                DarkPieces.Add(darkKnight);
            }
        }

        private void SetRooks()
        {
            {
                var tile = _tileContainer.GetTile(new Vector2Int(0, 0));
                var lightRook = Instantiate(_rookPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightRook.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightRook;

                LightPieces.Add(lightRook);
            }
            {
                var tile = _tileContainer.GetTile(new Vector2Int(7, 0));
                var lightRook = Instantiate(_rookPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                lightRook.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightRook;

                LightPieces.Add(lightRook);
            }
            
            {
                var tile = _tileContainer.GetTile(new Vector2Int(0, 7));
                var darkRook = Instantiate(_rookPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkRook.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkRook;

                LightPieces.Add(darkRook);
            }
            {
                var tile = _tileContainer.GetTile(new Vector2Int(7, 7));
                var darkRook = Instantiate(_rookPrefab, tile.transform.position + PieceOffset, Quaternion.identity, transform);
                darkRook.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkRook;

                LightPieces.Add(darkRook);
            }
        }
    }
}