using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerBlade : MonoBehaviour
{
    public UnityEvent onBladeCollision;

    public bool hasCollided = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade") && !hasCollided)
        {
            onBladeCollision.Invoke();
            hasCollided = true;
        }
    }

    public void HasColliderFalse()
    {
        hasCollided = false;
    }
}
