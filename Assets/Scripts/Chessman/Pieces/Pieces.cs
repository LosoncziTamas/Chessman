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
            
            var pawn = Instantiate(_pawnPrefab, bottomLeftTile.transform.position, Quaternion.identity);
            pawn.Init(Vector2Int.zero, PieceColor.Light);
            bottomLeftTile.ChessPiece = pawn;
            
            
        }
    }
}