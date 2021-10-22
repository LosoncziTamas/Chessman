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
            var bottomLeftTile = _tileContainer.GetTile(Vector2Int.zero);
            
            var lightPawn = Instantiate(_pawnPrefab, bottomLeftTile.transform.position, Quaternion.identity, transform);
            lightPawn.Init(bottomLeftTile.Position, PieceColor.Light);
            bottomLeftTile.ChessPiece = lightPawn;
            
            var topLeftTile = _tileContainer.GetTile(new Vector2Int(0, 7));
            
            var darkPawn = Instantiate(_pawnPrefab, topLeftTile.transform.position, Quaternion.identity, transform);
            darkPawn.Init(topLeftTile.Position, PieceColor.Dark);
            topLeftTile.ChessPiece = darkPawn;
        }
    }
}