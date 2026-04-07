using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public float LifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, LifeTime); 
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime); // bullet movement
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check whether the collided object can receive a hit.
        var hittable = other.GetComponent<Hittable>();
        if (hittable != null)
        {
            hittable.Hit(); 
        }

        Destroy(gameObject);
    }
}