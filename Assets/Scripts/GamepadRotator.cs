using UnityEngine;

public class GamepadRotator : RotatorBase, IState
{
    public GamepadRotator(Rigidbody rb, Vector3 lookInput)
    {
        this.rb = rb;
        this.lookInput = lookInput;
    }

    public override void Execute()
    {
        if (lookInput.sqrMagnitude < 0.1f)
            return;

        Vector3 lookDir = Vector3.left * lookInput.y + Vector3.forward * lookInput.x;

        Quaternion newRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        rb.MoveRotation(newRotation);
    }
}