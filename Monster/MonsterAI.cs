using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // Player's Transform
    private NavMeshAgent agent;

    public float DestroyDelay = 0.2f;
    public float AttackDistance = 0.5f; // Attack distance to player
    public UnityEvent onBladeCollision; // Event for blade collision

    private MonsterSpawner monsterSpawner; // Reference to MonsterSpawner

    private bool HasDecreasedHp = false; // Check if HP has decreased
    private bool HasTriggered = false; // Check if blade collision occurred

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterSpawner = FindObjectOfType<MonsterSpawner>(); // Find MonsterSpawner
        // Find player by tag if not assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        // Warn if NavMeshAgent is missing
        if (agent == null)
        {
            Debug.LogWarning("There is No NavMeshAgent Component");
        }
    }

    void Update()
    {
        if (agent != null && player != null && !HasTriggered)
        {
            // Move towards the player's position
            agent.SetDestination(player.position);
        }
    }

    private void FixedUpdate()
    {
        if (HasDecreasedHp) return; // Exit if HP has decreased

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= AttackDistance)
        {
            if (!HasTriggered) // Check for blade collision
            {
                onBladeCollision.Invoke();
                HasTriggered = true; // Mark as triggered

                if (monsterSpawner != null) // Decrease HP if not already done
                {
                    monsterSpawner.DecreseHp();
                    MonsterDestroy();
                    HasDecreasedHp = true; // Mark HP as decreased
                }
            }
        }
    }

    // Handle blade collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade") && !HasTriggered)
        {
            onBladeCollision.Invoke(); // Invoke collision event
            HasTriggered = true;
            MonsterDestroy();
        }
    }

    public void MonsterDestroy()
    {
        Destroy(gameObject, DestroyDelay); // Destroy monster after delay
        monsterSpawner.ObjectDestroyCount++; // Increment destroy count
    }
}
