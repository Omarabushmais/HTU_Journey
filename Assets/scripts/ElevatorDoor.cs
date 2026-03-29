using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    [Header("Left Doors")]
    public Transform leftDoor1;
    public Transform leftDoor2;
    [Header("Right Doors")]
    public Transform rightDoor1;
    public Transform rightDoor2;
    [Header("Speeds")]
    public float leftDoor1Speed = 3f;
    public float leftDoor2Speed = 3f;
    public float rightDoor1Speed = 3f;
    public float rightDoor2Speed = 3f;
    [Header("Settings")]
    public float openDistance = 2f;

    private Vector3 leftClosed1, leftClosed2;
    private Vector3 rightClosed1, rightClosed2;
    private Vector3 leftOpen1, leftOpen2;
    private Vector3 rightOpen1, rightOpen2;
    public bool reverseOrder = false;
    private bool isOpen = false;

    void Start()
    {
        leftClosed1 = leftDoor1.position;
        leftClosed2 = leftDoor2.position;
        rightClosed1 = rightDoor1.position;
        rightClosed2 = rightDoor2.position;

        if (reverseOrder)
        {
            leftOpen1 = leftClosed1 + leftDoor1.forward * openDistance;
            leftOpen2 = leftClosed2 + leftDoor2.forward * openDistance;
            rightOpen1 = rightClosed1 - rightDoor1.forward * openDistance;
            rightOpen2 = rightClosed2 - rightDoor2.forward * openDistance;
        }
        else
        {
            leftOpen1 = leftClosed1 - leftDoor1.forward * openDistance;
            leftOpen2 = leftClosed2 - leftDoor2.forward * openDistance;
            rightOpen1 = rightClosed1 + rightDoor1.forward * openDistance;
            rightOpen2 = rightClosed2 + rightDoor2.forward * openDistance;
        }
    }

    void Update()
    {
        Vector3 targetLeft1 = isOpen ? leftOpen1 : leftClosed1;
        Vector3 targetLeft2 = isOpen ? leftOpen2 : leftClosed2;
        Vector3 targetRight1 = isOpen ? rightOpen1 : rightClosed1;
        Vector3 targetRight2 = isOpen ? rightOpen2 : rightClosed2;

        leftDoor1.position = Vector3.Lerp(leftDoor1.position, targetLeft1, Time.deltaTime * leftDoor1Speed);
        leftDoor2.position = Vector3.Lerp(leftDoor2.position, targetLeft2, Time.deltaTime * leftDoor2Speed);
        rightDoor1.position = Vector3.Lerp(rightDoor1.position, targetRight1, Time.deltaTime * rightDoor1Speed);
        rightDoor2.position = Vector3.Lerp(rightDoor2.position, targetRight2, Time.deltaTime * rightDoor2Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isOpen = false;
    }
}
