using DG.Tweening;
using UnityEngine;

namespace Chessman
{
    public class AlignChildren : MonoBehaviour
    {
        private const int RowCount = 8;
        private const float MoveDuration = 1.0f;
        private static readonly Vector2 Padding = new Vector2(1, -1);
        
        public Vector2 Offset;
        
        private int _childCount;
        
        public void AddChild(Transform child)
        {
            child.SetParent(transform);
            var pos = transform.position;
            var childPosX = pos.x + Offset.x + _childCount / RowCount * Padding.x;
            var childPosy = pos.y + Offset.y + _childCount % RowCount * Padding.y;
            child.DOJump(new Vector3(childPosX, childPosy, pos.z), 0.6f, 1, MoveDuration)
                .AppendCallback(() =>
                {
                    var spriteRenderer = child.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sortingOrder =  _childCount % RowCount;
                    }
                });

            _childCount++;
        }
    }
}