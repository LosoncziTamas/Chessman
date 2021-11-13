using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.String;

namespace Chessman.GUI
{
    public class Panel : MonoBehaviour
    {
        private static readonly int PanelOpen = Animator.StringToHash("Panel Open");

        private const string WonText = "{0} player won!";

        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            SetInteractable(false);
        }

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(OnRetryClicked);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Show"))
            {
                ShowPanel(PieceColor.Light);
            }
            if (GUILayout.Button("Hide"))
            {
                HidePanel();
            }
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(OnRetryClicked);
        }

        private void OnRetryClicked()
        {
            HidePanel();
        }

        public void ShowPanel(PieceColor winner)
        {
            _mainText.text = Format(WonText, winner);
            _animator.SetBool(PanelOpen, true);
            SetInteractable(true);
        }

        public void HidePanel()
        {
            _animator.SetBool(PanelOpen, false);
            SetInteractable(false);
        }
        
        private void SetInteractable(bool interactable)
        {
            _canvasGroup.interactable = interactable;
            _canvasGroup.blocksRaycasts = interactable;
        }
    }
}
