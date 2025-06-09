using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float rotationSpeed = 0.1f; // Obsolete when using HandleLookInstant()
    private IState rotator;

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector3(-_moveInput.y, 0f, _moveInput.x).normalized * baseSpeed;
    }
}