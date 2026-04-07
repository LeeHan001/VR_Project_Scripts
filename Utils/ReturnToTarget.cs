using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform Target;

    public float Duration = 1f;
    public AnimationCurve Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public void Call()
    {
        if (!gameObject.activeInHierarchy)
            return;

        StopAllCoroutines();
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        if (Target == null)
        {
            yield break;
        }

        // Validate Target.position
        if (float.IsNaN(Target.position.x) || float.IsNaN(Target.position.y) || float.IsNaN(Target.position.z))
        {
            Debug.LogError("Target position is invalid (NaN).");
            yield break;
        }

        // Validate Duration
        if (Duration <= 0)
        {
            Debug.LogError("Duration must be greater than 0.");
            yield break;
        }

        var BeginTime = Time.time;

        while (true)
        {
            var T = (Time.time - BeginTime) / Duration;

            if (T >= 1f)
                break;

            T = Curve.Evaluate(T);

            // Validate before using Lerp
            var NewPosition = Vector3.Lerp(transform.position, Target.position, T);

            // Check for NaN values
            if (float.IsNaN(NewPosition.x) || float.IsNaN(NewPosition.y) || float.IsNaN(NewPosition.z))
            {
                Debug.LogError("Calculated position is invalid (NaN).");
                yield break;
            }

            transform.position = NewPosition;

            yield return null;
        }

        // Set final position
        transform.position = Target.position;
    }
}