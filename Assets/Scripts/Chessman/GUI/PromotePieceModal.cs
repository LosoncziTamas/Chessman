using System.Threading.Tasks;
using Chessman.Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Chessman.GUI
{
    public class PromotePieceModal : MonoBehaviour
    {
        public static PromotePieceModal Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Assuming object is in the scene.
                    _instance = FindObjectOfType<PromotePieceModal>(includeInactive: true);
                }

                return _instance;
            }
        }

        private static PromotePieceModal _instance;
        
        public class Promotion
        {
            public readonly Sprite sprite;
            public readonly Movements.MoveType movementType;

            public Promotion(Sprite sprite, Movements.MoveType movementType)
            {
                this.sprite = sprite;
                this.movementType = movementType;
            }
        }
        
        [SerializeField] private Sprites _sprites;
        [SerializeField] private Button _knightButton;
        [SerializeField] private Button _queenButton;
        [SerializeField] private Button _rookButton;
        [SerializeField] private Button _bishopButton;

        private TaskCompletionSource<Promotion> _tcs;
        private PieceColor? _forColor;

        private void OnEnable()
        {
            _knightButton.onClick.AddListener(OnKnightButtonClick);
            _queenButton.onClick.AddListener(OnQueenButtonClick);
            _rookButton.onClick.AddListener(OnRookButtonClick);
            _bishopButton.onClick.AddListener(OnBishopButtonClick);
        }

        private void OnKnightButtonClick()
        {
            var sprite = _forColor == PieceColor.Dark ? _sprites.DarkKnight : _sprites.LightKnight;
            _tcs.SetResult(new Promotion(sprite, Movements.MoveType.Knight));
        }
        
        private void OnQueenButtonClick()
        {
            var sprite = _forColor == PieceColor.Dark ? _sprites.DarkQueen : _sprites.LightQueen;
            _tcs.SetResult(new Promotion(sprite, Movements.MoveType.Queen));
        }
        
        private void OnRookButtonClick()
        {
            var sprite = _forColor == PieceColor.Dark ? _sprites.DarkRook : _sprites.LightRook;
            _tcs.SetResult(new Promotion(sprite, Movements.MoveType.Rook));
        }
        
        private void OnBishopButtonClick()
        {
            var sprite = _forColor == PieceColor.Dark ? _sprites.DarkBishop : _sprites.LightBishop;
            _tcs.SetResult(new Promotion(sprite, Movements.MoveType.Bishop));
        }

        private void OnDisable()
        {
            _knightButton.onClick.RemoveListener(OnKnightButtonClick);
            _queenButton.onClick.RemoveListener(OnQueenButtonClick);
            _rookButton.onClick.RemoveListener(OnRookButtonClick);
            _bishopButton.onClick.RemoveListener(OnBishopButtonClick);
        }
        
        public Task<Promotion> Show(PieceColor forColor)
        {
            gameObject.SetActive(true);
            Debug.Assert(_tcs == null);
            _forColor = forColor;
            _tcs = new TaskCompletionSource<Promotion>();
            return _tcs.Task;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}