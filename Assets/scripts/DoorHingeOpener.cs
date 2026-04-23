using UnityEngine;

public class DoorHingeOpener : MonoBehaviour
{
    public HingeJoint hinge;
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float motorSpeed = 120f;
    public float motorForce = 100f;

    private JointMotor motor;
    private bool isOpening = false;
    private bool isClosing = false;

    private void Start()
    {
        if (hinge == null)
            hinge = GetComponent<HingeJoint>();

        motor = hinge.motor;
        hinge.useMotor = false;
    }

    private void Update()
    {
        float currentAngle = hinge.angle;
        if (hinge == null)
        {
        }
        if (isOpening)
        {
            

                motor.targetVelocity = motorSpeed;
                motor.force = motorForce;
                hinge.motor = motor;
                hinge.useMotor = true;
            
        }

        if (isClosing)
        {
            
                motor.targetVelocity = -motorSpeed;
                motor.force = motorForce;
                hinge.motor = motor;
                hinge.useMotor = true;
            
        }
    }

    public void OpenDoor()
    {

        isOpening = true;
        isClosing = false;
    }

    public void CloseDoor()
    {

        isClosing = true;
        isOpening = false;
    }

    public void ToggleDoor(bool open)
    {
        if (open)
            OpenDoor();
        else
            CloseDoor();
    }

    private void StopDoor()
    {
        isOpening = false;
        isClosing = false;
        hinge.useMotor = false;
    }
}