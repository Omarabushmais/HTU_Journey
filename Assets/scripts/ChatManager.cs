using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Transform chatContent;
    public GameObject messagePrefab;
    public GameObject dialogueUI;
    bool isOpen;
    public static ChatManager Instance;
    private string webhookUrl = "https://mustafa2004.app.n8n.cloud/webhook/15d0ec83-3023-4871-8b5a-8bd3344f4266";

    private string currentNpcId;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
        Debug.Log("what the fffff id is"+currentNpcId);
        if (string.IsNullOrEmpty(userMessage))
            return;

        if (string.IsNullOrEmpty(currentNpcId))
        {
            AddMessage("Error: No NPC selected.");
            return;
        }

        //AddMessage("You: " + userMessage);
        AddMessage("<align=right><color=#4997fc><b>You:</b></color> " + userMessage + "</align>");
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
    public void OpenChat(string npcId)
    {
        currentNpcId = npcId;
        Debug.Log("id in open chat is  is"+currentNpcId);

        dialogueUI.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inputField.ActivateInputField();
    }

    public void CloseChat()
    {
        ClearCurrentNpcChat();
        isOpen = false;
        currentNpcId = null;
        dialogueUI.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                    Debug.Log("we are sending to this url" + webhookUrl);

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text.Trim();
            //AddMessage("Bot: " + response);
            AddMessage("<color=#ff5555><b>Bot:</b></color> " + response);
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