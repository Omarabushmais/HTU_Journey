using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;


public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Transform chatContent;
    public GameObject messagePrefab;

    private string webhookUrl = "https://n8n.ez-moh-n8n.online/webhook/1ff1e150-7674-45fe-8506-52b62ebe928f";

    public void SendMessage()
    {
        string userMessage = inputField.text;

        if (string.IsNullOrEmpty(userMessage))
            return;

        AddMessage("You: " + userMessage);

        StartCoroutine(SendToN8n(userMessage));

        inputField.text = "";
    }

    void AddMessage(string text)
    {
        GameObject msg = Instantiate(messagePrefab, chatContent);
        
        msg.GetComponent<TMP_Text>().text = text;
    }

    IEnumerator SendToN8n(string message)
    {
        string json = "{\"message\":\"" + message + "\"}";

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            response = response.Trim();
            AddMessage("Bot: "+response);
        }
        else
        {
            AddMessage("Error: " + request.error);
        }
    }
}
