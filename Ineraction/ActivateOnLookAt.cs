using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookAt : MonoBehaviour
{
    public new Camera camera; // Reference to the camera
    public Behaviour Target; // Target component to activate

    public float ThresholdAngle = 30f; // Angle threshold for activation
    public float ThresholdDuration = 2f; // Duration for which the angle must be maintained

    private bool IsLooking = false; // Check if looking at the target
    private float ShowingTime; // Time to activate the target

    private void Awake()
    {
        Target.enabled = false; // Disable target initially
    }

    private void Update()
    {
        var dir = Target.transform.position - camera.transform.position;
        var angle = Vector3.Angle(camera.transform.forward, dir);

        if (angle <= ThresholdAngle)
        {
            if (!IsLooking)
            {
                IsLooking = true;
                ShowingTime = Time.time + ThresholdDuration; // Set activation time
            }
            else if (!Target.enabled && Time.time > ShowingTime)
            {
                Target.enabled = true; // Enable target component
            }
        }
        else if (IsLooking)
        {
            IsLooking = false; // Reset if not looking
            Target.enabled = false; // Disable target component
        }
    }
}