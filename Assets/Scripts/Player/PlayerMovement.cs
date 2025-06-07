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

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
        //Debug.Log(_lookInput);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        //HandleLookInstant();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector3(-_moveInput.y, 0f, _moveInput.x).normalized * baseSpeed;
    }

    private void HandleLookInstant()
    {
        if (_lookInput.sqrMagnitude < 0.1f)
            return;

        Vector3 lookDir = Vector3.left * _lookInput.y + Vector3.forward * _lookInput.x;

        Quaternion newRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        rb.MoveRotation(newRotation);
    }

    private void HandleLookSmooth()
    {
        if (_lookInput.sqrMagnitude < 0.1f)
            return;

        Vector3 lookDir = Vector3.left * _lookInput.y + Vector3.forward * _lookInput.x;

        Quaternion newRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, newRotation, rotationSpeed));
    }
}