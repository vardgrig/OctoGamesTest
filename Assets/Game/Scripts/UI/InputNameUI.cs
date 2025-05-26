using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class InputNameUI : CustomUI
    {
        [SerializeField] private GameObject nameInputPanel;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private LabeledButton confirmButton;
        [SerializeField] private TMP_Text promptText;
    
        private UniTaskCompletionSource _inputCompletionSource;
    
        protected override void Awake()
        {
            base.Awake();
        
            if (confirmButton)
                confirmButton.onClick.AddListener(OnConfirmName);
        
            if (!promptText)
                promptText.text = "Please enter your name:";
        
            SetVisible(false);
        }
    
        public async UniTask ShowAndWaitForInput()
        {
            // Reset input
            if (nameInputField)
            {
                nameInputField.text = "";
                nameInputField.interactable = true;
            }
        
            if (confirmButton)
                confirmButton.interactable = true;
        
            // Show UI
            SetVisible(true);
        
            // Focus input field
            if (nameInputField)
            {
                await UniTask.DelayFrame(1); // Wait one frame
                nameInputField.Select();
                nameInputField.ActivateInputField();
            }
        
            // Create completion source
            _inputCompletionSource = new UniTaskCompletionSource();
        
            // Wait for completion
            await _inputCompletionSource.Task;
        }
    
        private void OnConfirmName()
        {
            var playerName = nameInputField?.text?.Trim() ?? "";
        
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Player";
            }
        
            // Store in Naninovel variables
            var customVariables = Engine.GetService<ICustomVariableManager>();
            customVariables.SetVariableValue("PlayerName", playerName);
            
            // Hide UI
            SetVisible(false);
        
            // Complete the task
            _inputCompletionSource?.TrySetResult();
        }
    
        private void SetVisible(bool visible)
        {
            if (nameInputPanel)
                nameInputPanel.SetActive(visible);
            else
                gameObject.SetActive(visible);
        }

        protected override void Update()
        {
            // Handle Enter key
            if (nameInputPanel && nameInputPanel.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    OnConfirmName();
                }
            }
        }
    }
}