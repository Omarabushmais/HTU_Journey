using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    [Header("Movement")]
    public float horizontal;
    public float vertical;

    [Header("Actions")]
    public bool jumpPressed;
    public bool runHeld;
    public bool interactPressed;
    public bool teleportPressed;

    [Header("Mobile")]
    [SerializeField] private Joystick joystick;

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

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
  
        float joystickX = 0f;
        float joystickY = 0f;

        if (joystick != null)
        {
            joystickX = joystick.Horizontal;
            joystickY = joystick.Vertical;
        }

        float keyboardX = Input.GetAxis("Horizontal");
        float keyboardY = Input.GetAxis("Vertical");

        horizontal = Mathf.Abs(joystickX) > Mathf.Abs(keyboardX) ? joystickX : keyboardX;
        vertical = Mathf.Abs(joystickY) > Mathf.Abs(keyboardY) ? joystickY : keyboardY;

        runHeld = Input.GetKey(KeyCode.LeftShift);
        
        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;

        if (Input.GetKeyDown(KeyCode.E))
            interactPressed = true;

        if (Input.GetKeyDown(KeyCode.T))
            teleportPressed = true;
            
    }


    public void JumpButton()
    {
        jumpPressed = true;
    }

    public void RunDown()
    {
        runHeld = true;
    }

    public void RunUp()
    {
        runHeld = false;
    }

    public void InteractButton()
    {
        interactPressed = true;
    }
    public void TeleportButton()
    {
        teleportPressed = true;
    }

 
    public void ResetOneFrameInputs()
    {
        jumpPressed = false;
        teleportPressed =false;
        // interactPressed = false;
    }
}