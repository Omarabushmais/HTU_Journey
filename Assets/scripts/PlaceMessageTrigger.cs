using System.Collections;
using UnityEngine;
using TMPro;

public class PlaceMessageTrigger : MonoBehaviour
{
    [Header("UI References")]
    public GameObject fullMessageUI;   // Parent object that contains bot + box + text
    public TextMeshProUGUI messageText;

    [Header("Message Settings")]
    [TextArea(2, 5)]
    public string placeMessage;

    public float displayTime = 5f;

    private bool hasShown = false;
    private Coroutine hideCoroutine;

    private void Start()
    {
        if (fullMessageUI != null)
        {
            fullMessageUI.SetActive(false);
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
        if (fullMessageUI == null || messageText == null) return;

        messageText.text = placeMessage;
        fullMessageUI.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);

        fullMessageUI.SetActive(false);
    }
}