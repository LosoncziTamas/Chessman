using System;
using UnityEngine;

namespace Chessman.GUI
{
    public class Panel : MonoBehaviour
    {
        private static readonly int Property = Animator.StringToHash("Panel Open");

        [SerializeField] private Animator _animator;

        private void Awake()
        {
            
        }

        private void OnGUI()
        {
            var opened = _animator.GetBool(Property);
            if (GUILayout.Button(opened ? "Close" : "Open"))
            {
                _animator.SetBool(Property, !opened);
            }
        }
    }
}
