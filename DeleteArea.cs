using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleteArea : MonoBehaviour
{

    public UnityEvent OnDestroyed;

    private bool IsDestroyed = false;

    public void Destroy1()
    {
        Debug.Log("Destroy");
        if (IsDestroyed)
            return;
        IsDestroyed = true;


        OnDestroyed?.Invoke();
    }
}
