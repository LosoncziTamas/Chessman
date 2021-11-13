using System;
using UnityEngine;

namespace Chessman.GUI
{
    public class GameCursor : MonoBehaviour
    {
        // TODO: fix 
        public static GameCursor Instance { get; private set; }

        public Vector3 Offset;

        [SerializeField] private Color _lightTintColor;
        [SerializeField] private Color _darkTintColor;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Camera _camera;
        private Camera Camera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = Camera.current;
                }

                return _camera;
            }
        }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Update()
        {
            if (Camera == null)
            {
                return;
            }
            gameObject.transform.position = Camera.ScreenToWorldPoint(Input.mousePosition + Offset);
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