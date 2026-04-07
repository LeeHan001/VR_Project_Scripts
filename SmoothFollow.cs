using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    [Range(0, 1)]
    public float positionDamping;

    [Range(0,1)]
    public float rotationDamping;

    void OnEnable()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, positionDamping);

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping);
    }
}
