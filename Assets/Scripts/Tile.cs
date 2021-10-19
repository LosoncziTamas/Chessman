using DefaultNamespace;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static readonly Color HighlightColorRed = Color.red;
    public static readonly Color HighlightColorYellow = Color.yellow;
    
    [SerializeField] private SpriteRenderer _inside;
    [SerializeField] private SpriteRenderer _border;

    public bool HasPiece => ChessPiece != null;
    public IChessPiece ChessPiece { get; private set; }

    public void HighLightBorder(Color highlightColor)
    {
        _border.color = highlightColor;
    }

    public void OnSelected()
    {
        Debug.Log("Selected");
        HighLightBorder(HighlightColorYellow);
    }
}
