using System;
using Chessman.Pieces;
using UnityEngine;

namespace Chessman
{
    public class Tile : MonoBehaviour
    {
        public static readonly Color HighlightColorRed = Color.red;
        public static readonly Color HighlightColorYellow = Color.yellow;
    
        [SerializeField] private SpriteRenderer _inside;
        [SerializeField] private SpriteRenderer _border;

        public bool HasPiece => ChessPiece != null;
        public IChessPiece ChessPiece { get; set; }
        public Vector2Int Position { get; set; }

        private Color _defaultColor;

        private void Start()
        {
            _defaultColor = _inside.color;
        }

        public void HighLightBorder(Color highlightColor)
        {
            _border.color = highlightColor;
        }

        public void ClearHighlight()
        {
            _border.color = _defaultColor;
        }
    }
}
