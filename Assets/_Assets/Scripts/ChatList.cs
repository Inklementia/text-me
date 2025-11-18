using UnityEngine;
using System.Collections.Generic;

namespace Neocortex.Samples
{
    public class ChatList : MonoBehaviour
    {
        [SerializeField] private List<ChatCharacter> chatCharacters = new List<ChatCharacter>();
        
        [Header("UI References")]
        [SerializeField] private Transform chatItemsContainer;
        [SerializeField] private GameObject chatItemPrefab;

        private readonly List<ChatItem> _chatItems = new List<ChatItem>();
        private ChatCharacter _currentActiveChat;

        void Start()
        {
            InitializeChatList();
        }

        private void InitializeChatList()
        {
            // Clear existing items
            foreach (var item in _chatItems)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }
            _chatItems.Clear();

            // Create chat items for each chat character
            foreach (var chatChar in chatCharacters)
            {
                if (chatChar == null) continue;

                // Create chat item UI
                GameObject itemObj = Instantiate(chatItemPrefab, chatItemsContainer);
                ChatItem chatItem = itemObj.GetComponent<ChatItem>();
                
                if (chatItem != null)
                {
                    chatItem.Initialize(chatChar);
                    chatItem.OnChatSelected.AddListener(SelectChat);
                    _chatItems.Add(chatItem);

                    // Subscribe to message updates
                    chatChar.OnMessageUpdated.AddListener(() => UpdateChatItem(chatChar));
                }

                // Hide all chats initially
                chatChar.Hide();
            }
        }

        private void SelectChat(ChatCharacter chatCharacter)
        {
            if (chatCharacter == null) return;

            // Hide current active chat
            if (_currentActiveChat != null)
            {
                _currentActiveChat.Hide();
            }

            // Show selected chat
            _currentActiveChat = chatCharacter;
            _currentActiveChat.Show();

            Debug.Log($"Selected chat: {chatCharacter.CharacterData.CharacterName}");
        }

        private void UpdateChatItem(ChatCharacter chatCharacter)
        {
            // Find the chat item index that corresponds to this chat character
            int index = chatCharacters.IndexOf(chatCharacter);
            if (index >= 0 && index < _chatItems.Count)
            {
                ChatItem item = _chatItems[index];
                if (item != null)
                {
                    item.UpdateUI();
                }
            }
        }
        

        private void OnDestroy()
        {
            foreach (var item in _chatItems)
            {
                if (item != null)
                {
                    item.OnChatSelected.RemoveListener(SelectChat);
                }
            }
        }
    }
}

