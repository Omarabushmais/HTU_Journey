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
        dialogueUI.SetActive(false);

        chatManager = dialogueUI.GetComponent<ChatManager>();
    }

    private void Update()
    {
        if (playerInZone && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            if (!isOpen)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            interactPrompt.SetActive(false);
        }
    }

    public void OpenDialogue()
    {
        isOpen = true;
        interactPrompt.SetActive(false);
        dialogueUI.SetActive(true);

        if (chatManager != null)
        {
            chatManager.SetCurrentNpc(npcId);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDialogue()
    {
        isOpen = false;
        dialogueUI.SetActive(false);

        if (playerInZone)
            interactPrompt.SetActive(true);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
