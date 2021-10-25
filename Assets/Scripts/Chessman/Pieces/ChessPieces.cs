using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chessman.Pieces
{
    public class ChessPieces : MonoBehaviour
    {
        [SerializeField] private Pawn _pawnPrefab;
        [SerializeField] private King _kingPrefab;
        [SerializeField] private Bishop _bishopPrefab;

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

        public void Setup()
        {
            SetPawns();
            SetKings();
            SetBishops();
        }

        private void SetBishops()
        {
            var tile = _tileContainer.GetTile(new Vector2Int(2, 0));
            var lightBishop = Instantiate(_bishopPrefab, tile.transform.position, Quaternion.identity, transform);
            lightBishop.Init(tile.Position, PieceColor.Light);
            tile.ChessPiece = lightBishop;
            
            LightPieces.Add(lightBishop);
        }

        private void SetPawns()
        {
            for (var x = 0; x < 2; ++x)
            {
                var tile = _tileContainer.GetTile(new Vector2Int(x, 1));
                var lightPawn = Instantiate(_pawnPrefab, tile.transform.position, Quaternion.identity, transform);
                lightPawn.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightPawn;

                LightPieces.Add(lightPawn);
                
                tile = _tileContainer.GetTile(new Vector2Int(x, 6));
                var darkPawn = Instantiate(_pawnPrefab, tile.transform.position, Quaternion.identity, transform);
                darkPawn.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkPawn;
                
                DarkPieces.Add(darkPawn);
            }
        }

        private void SetKings()
        {
            var tile = _tileContainer.GetTile(new Vector2Int(4, 0));
            var lightKing = Instantiate(_kingPrefab, tile.transform.position, Quaternion.identity, transform);
            lightKing.Init(tile.Position, PieceColor.Light);
            tile.ChessPiece = lightKing;

            LightPieces.Add(lightKing);
            
            tile = _tileContainer.GetTile(new Vector2Int(4, 7));
            var darkKing = Instantiate(_kingPrefab, tile.transform.position, Quaternion.identity, transform);
            darkKing.Init(tile.Position, PieceColor.Dark);
            tile.ChessPiece = darkKing;

            DarkPieces.Add(darkKing);
        }
    }
}