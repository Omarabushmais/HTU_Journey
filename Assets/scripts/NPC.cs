using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcId = "guard_01";

    public GameObject interactPrompt;   // "Press E" UI
    public GameObject dialogueUI;       // Main UI panel

    private bool playerInZone = false;
    private bool isOpen = false;
    private ChatManager chatManager;

    private void Start()
    {
        interactPrompt.SetActive(false);
    }

    private void Update()
    {
        var input = PlayerInputManager.Instance;
            
        if (playerInZone && input.interactPressed)
        {
            ChatManager.Instance.OpenChat(npcId);
            input.interactPressed = false;
            interactPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInZone = true;
        interactPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInZone = false;
        interactPrompt.SetActive(false);
    }

   


}
