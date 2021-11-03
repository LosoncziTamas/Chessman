using UnityEngine;

namespace Chessman
{
    public class AlignChildren : MonoBehaviour
    {
        private const int RowCount = 8;
        private const int ColumnCount = 2;
        
        public Vector2 Offset = new Vector2(0, 0);
        private Vector2 Diff = new Vector2(1, -1);
        
        private int _childCount;
        
        public void Add(Transform child)
        {
            var pos = transform.position;
            var childPosX = pos.x + _childCount / RowCount * Diff.x;
            var childPosy = pos.y + _childCount % RowCount * Diff.y;
            child.position = new Vector3(childPosX, childPosy, pos.z);
        }
        
    }
}