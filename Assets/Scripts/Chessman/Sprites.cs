using UnityEngine;

namespace Chessman
{
    [CreateAssetMenu(fileName = "Sprites", menuName = "ScriptableObjects/Sprite Holder", order = 1)]
    public class Sprites : ScriptableObject
    {
        [SerializeField] private Sprite _lightPawn;
        [SerializeField] private Sprite _darkPawn;
        [SerializeField] private Sprite _lightRook;
        [SerializeField] private Sprite _darkRook;
        [SerializeField] private Sprite _darkQueen;
        [SerializeField] private Sprite _lightQueen;

        public Sprite LightPawn => _lightPawn;
        public Sprite DarkPawn => _darkPawn;

        public Sprite LightRook => _lightRook;

        public Sprite DarkRook => _darkRook;

        public Sprite DarkQueen => _darkQueen;

        public Sprite LightQueen => _lightQueen;
    }
}