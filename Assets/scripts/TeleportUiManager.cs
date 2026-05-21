using UnityEngine;

public class TeleportUIManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public PlayerControl playerControl;
    [SerializeField] private GameObject chatUI;

    [Header("UI")]
    public GameObject teleportPanel;

    [Header("Teleport Points")]
    public Transform libraryPoint;
    public Transform clubsRoomPoint;
    public Transform SciPoint;
    public Transform SetPoint;
    public Transform EntrancePoint;
    public Transform RegisterPoint;

    private void Start()
    {
        if (teleportPanel != null)
        {
            teleportPanel.SetActive(false);
        }
    }

    private void Update()
    {

        if (chatUI != null && chatUI.activeSelf)
        return;

        if (PlayerInputManager.Instance != null && PlayerInputManager.Instance.teleportPressed)
        {
            ToggleTeleportPanel();
        }
        
        
    }

    public void ToggleTeleportPanel()
    {
        if (teleportPanel == null) return;

        bool isOpen = teleportPanel.activeSelf;
        teleportPanel.SetActive(!isOpen);

        Cursor.visible = !isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void CloseTeleportPanel()
    {
        if (teleportPanel == null) return;

        teleportPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TeleportToLibrary()
    {
        TeleportPlayer(libraryPoint);
    }

    public void TeleportToClubsRoom()
    {
        TeleportPlayer(clubsRoomPoint);
    }

    public void TeleportSci()
    {
        TeleportPlayer(SciPoint);
    }
    public void TeleportRegisteration()
    {
        TeleportPlayer(RegisterPoint);
    }

    public void TeleportToEntrance()
    {
        TeleportPlayer(EntrancePoint);
    }

    public void TeleportToSet()
    {
        TeleportPlayer(SetPoint);
    }

    // public void TeleportToNorthBuilding()
    // {
    //     TeleportPlayer(northBuildingPoint);
    // }

    

    private void TeleportPlayer(Transform targetPoint)
    {
        if (playerControl == null || targetPoint == null) return;

        playerControl.TeleportTo(targetPoint);

        CloseTeleportPanel();
    }
}