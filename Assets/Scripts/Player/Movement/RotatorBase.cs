using UnityEngine;

public abstract class RotatorBase : IState
{
    protected Rigidbody rb;
    protected Vector3 lookInput;

    public abstract void Execute();

    public void SetLookInput(Vector3 updatedLookInput)
    {
        lookInput = updatedLookInput;
    }
}