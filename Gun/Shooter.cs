using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public GameObject bulletPrefab; 

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    public bool BulletShot = false;
    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        if(BulletShot)
        {
            while (true)
            {
                BulletShoot();

                yield return new WaitForSeconds(shootDelay);
            }
        }
        else
        {
            while (true)
            {
                Shoot();

                yield return new WaitForSeconds(shootDelay);
            }
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            var hitObject = hitInfo.transform.GetComponent<Hittable>(); 
            hitObject?.Hit(); //Hit Object Recasting Successful

            OnShootSuccess?.Invoke(hitInfo.point);
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitPoint);
        }
    }

    private void BulletShoot()
    {
        // Bullet generation and firing
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation); // Using Bullet Freefab
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.Speed = 20f; // Set the speed of the bullet
        }
    }
}
