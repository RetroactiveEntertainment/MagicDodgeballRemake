using System;
using UnityEngine;

public class MouseRotator : RotatorBase, IState
{
    //public const int GROUND_LAYER_MASK = 7;
    public const float RAYCAST_MAXS_DISTANCE = 100f;

    public MouseRotator(Rigidbody rb, Vector3 lookInput)
    {
        this.rb = rb;
        this.lookInput = lookInput;
    }

    public override void Execute()
    {
        Vector3 currentPos = rb.position;
        Ray ray = Camera.main.ScreenPointToRay(lookInput);
        RaycastHit hit;
        Vector3 rayHitPos = new Vector3();

        if (Physics.Raycast(ray, out hit, RAYCAST_MAXS_DISTANCE))
        {
            rayHitPos = hit.point;
            rayHitPos.y = currentPos.y;
        }
        else
        {
            Debug.LogError("Ray did not hit");
        }

        Vector3 lookDir = (rayHitPos - currentPos).normalized;
        Quaternion rotQuat = Quaternion.LookRotation(lookDir, Vector3.up);
        rb.MoveRotation(rotQuat);


        /*Debug.Log($"Mouse screen position: {lookInput}");
        Vector3 currentPos = rb.position;
        Vector3 mouseToWorldPos = Camera.main.ScreenToWorldPoint(lookInput);
        //mouseToWorldPos = new Vector3(mouseToWorldPos.x, mouseToWorldPos.y, rb.position.z);
        mouseToWorldPos = new Vector3(mouseToWorldPos.x, mouseToWorldPos.y, Camera.main.nearClipPlane + 1f);
        Debug.Log($"Mouse WORLD position: {mouseToWorldPos}");
        Vector3 lookDirection = (mouseToWorldPos - currentPos).normalized;


        /*Vector3 lookDir = lookInput - rb.position;
        lookDir = new Vector3(lookDir.x, rb.position.y, lookDir.z);#1#
        Quaternion lookRot = Quaternion.LookRotation(lookDirection, Vector3.up);
        rb.MoveRotation(lookRot);*/
    }
}