using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    [Header("Ray")]//Separate
    public LineRenderer Ray;
    public LayerMask HitRayMask;
    public float Distance = 100f;

    [Header("ReticlePoint")] //Separate
    public GameObject ReticlePoint;
    public bool ShowReticel = true;

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Off()
    {
        StopAllCoroutines();

        Ray.enabled = false;
        ReticlePoint.SetActive(false);
    }

    public IEnumerator Process()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, Distance, HitRayMask))
            {
                Ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));//Calculating the end of the light
                Ray.enabled = true;

                ReticlePoint.transform.position = hitInfo.point;
                ReticlePoint.SetActive(ShowReticel);
            }
            else
            {
                Ray.enabled = false;

                ReticlePoint.SetActive(false);
            }
            yield return null;
        }
    }
}
