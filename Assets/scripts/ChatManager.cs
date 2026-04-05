using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Transform chatContent;
    public GameObject messagePrefab;

    private string webhookUrl = "https://n8n.ez-moh-n8n.online/webhook/1ff1e150-7674-45fe-8506-52b62ebe928f";

    private string currentNpcId;

    public void SetCurrentNpc(string npcId)
    {
        currentNpcId = npcId;

        Debug.Log("Current NPC set to: " + currentNpcId);

        string chatId = NPCChatSessionManager.Instance.GetOrCreateChatId(currentNpcId);
        Debug.Log("Current Chat ID: " + chatId);

        inputField.ActivateInputField();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (string.IsNullOrEmpty(currentNpcId))
            return;

        
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SendMessage();
            }
        
    }

    public void SendMessage()
    {
        string userMessage = inputField.text;

        if (string.IsNullOrEmpty(userMessage))
            return;

        if (string.IsNullOrEmpty(currentNpcId))
        {
            AddMessage("Error: No NPC selected.");
            return;
        }

        AddMessage("You: " + userMessage);
        StartCoroutine(SendToN8n(userMessage));

        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void ClearCurrentNpcChat()
    {
        if (string.IsNullOrEmpty(currentNpcId))
        {
            AddMessage("Error: No NPC selected.");
            return;
        }

        string newChatId = NPCChatSessionManager.Instance.ClearChatAndCreateNew(currentNpcId);

        ClearChatUI();

        AddMessage("System: Chat cleared.");
        Debug.Log("New Chat ID for " + currentNpcId + ": " + newChatId);

        inputField.ActivateInputField();
    }

    void AddMessage(string text)
    {
        GameObject msg = Instantiate(messagePrefab, chatContent);
        msg.GetComponent<TMP_Text>().text = text;
    }

    void ClearChatUI()
    {
        for (int i = chatContent.childCount - 1; i >= 0; i--)
        {
            Destroy(chatContent.GetChild(i).gameObject);
        }
    }

    IEnumerator SendToN8n(string message)
    {
        string playerId = NPCChatSessionManager.Instance.GetPlayerId();
        string chatId = NPCChatSessionManager.Instance.GetOrCreateChatId(currentNpcId);

        ChatRequest requestData = new ChatRequest
        {
            playerId = playerId,
            chatId = chatId,
            npcId = currentNpcId,
            message = message
        };

        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text.Trim();
            AddMessage("Bot: " + response);
        }
        else
        {
            AddMessage("Error: " + request.error);
        }
    }

    [System.Serializable]
    public class ChatRequest
    {
        public string playerId;
        public string chatId;
        public string npcId;
        public string message;
    }
}