using System;
using System.Collections.Generic;
using Chessman.Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Chessman.GUI
{
    public class PromotePieceModal : MonoBehaviour
    {

        [Serializable]
        public class Promotion
        {
            [SerializeField] private Sprite _sprite;
            [SerializeField] private Movements.MoveType _movementType;
        }

        [SerializeField] private List<Promotion> _promotions;
        [SerializeField] private Sprites _sprites;

        private void Awake()
        {
            
        }
    }
}