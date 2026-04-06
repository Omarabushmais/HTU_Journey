using UnityEngine;

public class DoorRotationTriger : MonoBehaviour
{
    public DoorHingeOpener door;
    private bool playerInZone = false;

    // private void Update()
    // {
    //     if (playerInZone && Input.GetKeyDown(KeyCode.E))
    //     {
    //         door.ToggleDoor();
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
             door.ToggleDoor(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
         door.ToggleDoor(false);
    }
}
