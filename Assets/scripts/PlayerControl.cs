using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 9f;
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;

    [Header("Camera")]
    [SerializeField] private Camera _followCamera;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private float _playerSpeed;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is missing on this GameObject!");
        }

        if (_animator == null)
        {
            Debug.LogError("Animator is missing on this GameObject!");
        }

        _playerSpeed = _walkSpeed;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;

        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) 
            * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        // Running
        bool isRunning = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && movementDirection.magnitude > 0.1f;
        _playerSpeed = isRunning ? _runSpeed : _walkSpeed;

        // Move character
        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

        // Animator
        bool isMoving = movementDirection.magnitude > 0.1f;
        _animator.SetBool("walking", isMoving);
        _animator.SetBool("running", isRunning&&isMoving);
        

        // Rotate player
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        // Gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}