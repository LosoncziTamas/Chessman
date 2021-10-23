using System;
using UnityEngine;

namespace Chessman.Pieces
{
    public class Pieces : MonoBehaviour
    {
        [SerializeField] private Pawn _pawnPrefab;

        private TileContainer _tileContainer;

        private void Start()
        {
            _tileContainer = FindObjectOfType<TileContainer>();
            Setup();
        }

        public void Setup()
        {
            SetPawns();
        }

        private void SetPawns()
        {
            for (var x = 0; x < 8; ++x)
            {
                var tile = _tileContainer.GetTile(new Vector2Int(x, 1));
                var lightPawn = Instantiate(_pawnPrefab, tile.transform.position, Quaternion.identity, transform);
                lightPawn.Init(tile.Position, PieceColor.Light);
                tile.ChessPiece = lightPawn;
                
                tile = _tileContainer.GetTile(new Vector2Int(x, 6));
                var darkPawn = Instantiate(_pawnPrefab, tile.transform.position, Quaternion.identity, transform);
                darkPawn.Init(tile.Position, PieceColor.Dark);
                tile.ChessPiece = darkPawn;
            }
        }
    }
}