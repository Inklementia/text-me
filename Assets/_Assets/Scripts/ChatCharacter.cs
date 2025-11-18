using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Neocortex.Data;

namespace Neocortex.Samples
{
    public class ChatCharacter : MonoBehaviour
    {
        [SerializeField] private NeocortexChatPanel chatPanel;
        [SerializeField] private NeocortexTextChatInput chatInput;
        [SerializeField] private Character character;
        [SerializeField] private Canvas chatCanvas;
        
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private Image characterImage;
        [SerializeField] private Button backButton;

        public UnityEvent OnMessageUpdated = new UnityEvent();

        private OllamaRequest _request;
        private string _lastMessage = "";

        public Character CharacterData => character;
        public string LastMessage => _lastMessage;

        void Start()
        {
            if (character == null)
            {
                Debug.LogError("Character is not assigned in ChatCharacter!");
                return;
            }

            Debug.Log($"Character: {character.CharacterName}");
            Debug.Log($"Model: {character.Model}");

            // Initialize UI
            if (characterNameText != null)
            {
                characterNameText.text = character.CharacterName;
            }

            if (characterImage != null && character.CharacterImage != null)
            {
                characterImage.sprite = character.CharacterImage;
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(OnBackButtonClicked);
            }

            string modelName = character.GetSelectedModel();
            if (string.IsNullOrEmpty(modelName))
            {
                Debug.LogError($"Model is not set in Character: {character.CharacterName}!");
                return;
            }

            Debug.Log($"Creating OllamaRequest with model: {modelName}");
            _request = new OllamaRequest();
            _request.OnChatResponseReceived += OnChatResponseReceived;
            _request.ModelName = modelName;
            chatInput.OnSendButtonClicked.AddListener(OnUserMessageSent);

            // Set system prompt
            if (!string.IsNullOrEmpty(character.SystemPrompt))
            {
                _request.AddSystemMessage(character.SystemPrompt);
            }
            else
            {
                Debug.LogWarning($"System prompt is empty for {character.CharacterName}!");
            }
        }

        private void OnBackButtonClicked()
        {
            Hide();
        }

        private void OnChatResponseReceived(ChatResponse response)
        {
            chatPanel.AddMessage(response.message, false);
            _lastMessage = response.message;
            OnMessageUpdated?.Invoke();
        }

        private void OnUserMessageSent(string message)
        {
            _request.Send(message);
            chatPanel.AddMessage(message, true);
            _lastMessage = message;
            OnMessageUpdated?.Invoke();
        }

        public void Show()
        {
            if (chatCanvas != null)
            {
                chatCanvas.gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            if (chatCanvas != null)
            {
                chatCanvas.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (_request != null)
            {
                _request.OnChatResponseReceived -= OnChatResponseReceived;
            }
            
            if (chatInput != null)
            {
                chatInput.OnSendButtonClicked.RemoveListener(OnUserMessageSent);
            }

            if (backButton != null)
            {
                backButton.onClick.RemoveListener(OnBackButtonClicked);
            }
        }
    }
}
