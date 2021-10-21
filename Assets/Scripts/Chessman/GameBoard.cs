using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chessman
{
    public class GameBoard : MonoBehaviour
    {
        public PieceColor CurrentTurnColor { get; private set; } = PieceColor.Light;
    
        [SerializeField] private TileContainer _tileContainer;
        
        private Camera _camera;

        private List<Tile> _walkableTiles;
        private Tile _selectedTile;

        private void Awake()
        {
            _camera = Camera.main;
        }
    
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
                if (hit.collider == null)
                {
                    return;
                }
            
                var go = hit.collider.gameObject;
                if (go.CompareTag("Tile"))
                {
                    var tile = go.GetComponent<Tile>();
                    Debug.Assert(tile != null);
                    SelectTile(tile);
                }
            }
        }

        private void SelectTile(Tile tile)
        {
            if (_selectedTile == null)
            {
                if (tile.HasPiece && tile.ChessPiece.Color == CurrentTurnColor)
                {
                    var chessPiece = tile.ChessPiece;
                    tile.HighLightBorder(Tile.HighlightColorYellow);
                    _walkableTiles = chessPiece.GetWalkableTiles(_tileContainer).ToList();
                    foreach (var walkableTile in _walkableTiles)
                    {
                        switch (walkableTile.HasPiece)
                        {
                            case true when walkableTile.ChessPiece.Color != CurrentTurnColor:
                                walkableTile.HighLightBorder(Tile.HighlightColorRed);
                                break;
                            case false:
                                walkableTile.HighLightBorder(Tile.HighlightColorYellow);
                                break;
                        }
                    }
                    _selectedTile = tile;
                }
            }

            if (_selectedTile != null && _walkableTiles.Contains(tile))
            {
                _selectedTile.ChessPiece.MovePiece(_tileContainer, _selectedTile, tile);
                foreach (var walkableTile in _walkableTiles)
                {
                    walkableTile.ClearHighlight();
                }
                _selectedTile.ClearHighlight();
                _selectedTile = null;
                _walkableTiles.Clear();

                CurrentTurnColor = CurrentTurnColor == PieceColor.Dark ? PieceColor.Light : PieceColor.Dark;
            }
        }
    }
}
