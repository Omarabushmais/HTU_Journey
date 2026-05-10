using System.Collections;
using UnityEngine;
using TMPro;

public class PlaceMessageTrigger : MonoBehaviour
{
    [Header("UI References")]
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    [Header("Message Settings")]
    [TextArea(2, 5)]
    public string placeMessage;

    public float displayTime = 5f;

    private bool hasShown = false;
    private Coroutine hideCoroutine;

    private void Start()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (hasShown) return;

        ShowMessage();
        hasShown = true;
    }

    private void ShowMessage()
    {
        if (messagePanel == null || messageText == null) return;

        messageText.text = placeMessage;
        messagePanel.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);

        messagePanel.SetActive(false);
    }
}