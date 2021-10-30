using System.Collections.Generic;
using System.Linq;
using Chessman.Pieces;
using DG.Tweening;
using UnityEngine;

namespace Chessman
{
    public class GameBoard : MonoBehaviour
    {
        private static readonly Vector3 ElevationOffset = Vector3.up * 0.3f;
        public PieceColor CurrentTurnColor { get; private set; } = PieceColor.Light;
    
        [SerializeField] private TileContainer _tileContainer;
        [SerializeField] private Transform _capturedPieceContainer;
        [SerializeField] private ChessPieces _chessPieces;
        
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
                    OnTileClicked(tile);
                }
            }
        }

        private void HighlightWalkableTiles(Tile tile)
        {
            var chessPiece = tile.ChessPiece;
            tile.HighLightBorder(Tile.HighlightColorYellow);
            _walkableTiles = chessPiece.GetWalkableTiles(_tileContainer, _chessPieces).ToList();
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
        }

        private void MovePieceToNewPosition(Tile tile)
        {
            if (tile.HasPiece)
            {
                var capturedPiece = _selectedTile.ChessPiece.MoveAndCapture(_tileContainer, _selectedTile, tile);
                capturedPiece.Transform.SetParent(_capturedPieceContainer, false);
                capturedPiece.IsCaptured = true;
            }
            else
            {
                _selectedTile.ChessPiece.MovePiece(_tileContainer, _selectedTile, tile);
            }
            
            Unselect();
        }

        private void Unselect()
        {
            foreach (var walkableTile in _walkableTiles)
            {
                walkableTile.ClearHighlight();
            }
            
            _selectedTile.ClearHighlight();
            _selectedTile = null;
            _walkableTiles.Clear();
        }
        
        private void OnTileClicked(Tile tile)
        {
            if (_selectedTile == null)
            {
                if (tile.HasPiece && tile.ChessPiece.Color == CurrentTurnColor)
                {
                    var elevatedPos = tile.ChessPiece.Transform.position + ElevationOffset;
                    tile.ChessPiece.Transform.DOMove(elevatedPos, 0.6f);
                    HighlightWalkableTiles(tile);
                    _selectedTile = tile;
                }
            }
            else
            {
                if (_walkableTiles.Contains(tile))
                {
                    MovePieceToNewPosition(tile);
                    CurrentTurnColor = CurrentTurnColor == PieceColor.Dark ? PieceColor.Light : PieceColor.Dark;
                }
                else
                {
                    _selectedTile.ChessPiece.Transform.DOMove(_selectedTile.ChessPiece.Transform.position - ElevationOffset, 0.6f);
                    Unselect();
                }
            }
        }
    }
}
