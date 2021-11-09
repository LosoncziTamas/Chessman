using UnityEngine;

namespace Chessman.GUI
{
    public class GameCursor : MonoBehaviour
    {
        // TODO: should be singleton
        
        public Vector3 Offset;

        [SerializeField] private Color _lightTintColor;
        [SerializeField] private Color _darkTintColor;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            gameObject.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition + Offset);
            if (Input.GetMouseButton(0))
            {
                
            }
        }

        public void SetColor(PieceColor pieceColor)
        {
            _spriteRenderer.color = pieceColor == PieceColor.Dark ? _darkTintColor : _lightTintColor;
        }
    }
}