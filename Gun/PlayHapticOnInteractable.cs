using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    public float amplitude = 0.5f; 
    public float duration = 0.05f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable target;

    private void Awake()
    {
        target = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    public void Call()
    {
        if (target == null)
            return;
        if (target.firstInteractorSelecting == null)
            return;
        if (!(target.firstInteractorSelecting is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor))
            return;

        var interactor = target.firstInteractorSelecting as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
        //if (interactor.xrController == null)
            return;

        //interactor.xrController.SendHapticImpulse(amplitude, duration);
    }
}
