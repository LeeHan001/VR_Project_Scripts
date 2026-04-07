using UnityEngine;

public class MoveOrigin : MonoBehaviour
{
    // Called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = Vector3.zero;
        }
    }
}
