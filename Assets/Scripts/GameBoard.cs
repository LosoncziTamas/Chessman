using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private TileContainer _tileContainer;
    private Camera _camera;

    private void Awake()
    {
        _tileContainer = GetComponent<TileContainer>();
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
                tile.OnSelected();
            }
        }
    }
}
