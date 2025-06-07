using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotator : MonoBehaviour
{
    public const string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";
    public const string KEYBOARD_CONTROL_SCHEME_NAME = "Keyboard&Mouse";

    private Vector2 _lookInput;
    [SerializeField] private Rigidbody rb;

    private RotatorBase rotator;

    //[SerializeField] private float rotationSpeed = 0.1f; // Obsolete when using HandleLookInstant()
    private RotatorBase _currentRotator = null;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        Debug.Log($"Current device: {playerInput.currentControlScheme}");
        string currentControlScheme = playerInput.currentControlScheme;

        switch (currentControlScheme)
        {
            case GAMEPAD_CONTROL_SCHEME_NAME:
                Debug.Log($"Setting to gamepad scheme rotator");
                _currentRotator = new GamepadRotator(rb, _lookInput);
                break;
            case KEYBOARD_CONTROL_SCHEME_NAME:
                Debug.Log($"Setting to keyboard scheme rotator");
                _currentRotator = new MouseRotator(rb, _lookInput);
                break;
            default:
                Debug.LogError("Unknown or null control scheme!");
                break;
        }
    }

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
        _currentRotator.SetLookInput(_lookInput);
        //Debug.Log(_lookInput);
    }

    private void FixedUpdate()
    {
        _currentRotator.Execute();
    }
}