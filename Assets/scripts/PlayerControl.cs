using System;
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

    private float _horizontalInput;
    private float _verticalInput;
    private bool _jumpPressed;
    private bool _runPressed;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        if (_controller == null)
            Debug.LogError("CharacterController is missing!");

        if (_animator == null)
            Debug.LogError("Animator is missing!");

        _playerSpeed = _walkSpeed;
    }

    public void TeleportTo(Transform targetPoint)
    {
        if (targetPoint == null) return;

        _controller.enabled = false;

        transform.position = targetPoint.position;
        transform.rotation = targetPoint.rotation;

        // Reset gravity / falling velocity
        _playerVelocity = Vector3.zero;

        // Enable CharacterController again
        _controller.enabled = true;
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        PlayerInputManager.Instance.ResetOneFrameInputs();

    }

    private void HandleInput()
    {
        var input = PlayerInputManager.Instance;

        _horizontalInput = input.horizontal;
        
        _verticalInput = input.vertical;

        _runPressed = input.runHeld;
        _jumpPressed = input.jumpPressed;
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;

        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        // Movement direction relative to camera
        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0)
            * new Vector3(_horizontalInput, 0, _verticalInput);

        Vector3 movementDirection = movementInput.normalized;

        bool isMoving = movementDirection.magnitude > 0.1f;
        bool isRunning = _runPressed && isMoving;

        _playerSpeed = isRunning ? _runSpeed : _walkSpeed;

        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

        float speedValue = movementDirection.magnitude * (isRunning ? 1f : 0.5f);
        _animator.SetFloat("Speed", speedValue);

        // Rotation
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        // Jump
        if (_jumpPressed && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        // Gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

    }

  
}