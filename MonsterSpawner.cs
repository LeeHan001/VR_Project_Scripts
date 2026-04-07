using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MonsterSpawner : MonoBehaviour
{
    public TextMeshProUGUI HpText; // UI for displaying player HP

    public UnityEvent GameOver; // Event for game over
    public UnityEvent GameClear; // Event for game clear

    public GameObject[] Object; // Array of monster prefabs
    private float TimeInterval = 5f; // Time interval between spawns
    public Transform[] SpawnPoint; // Points where monsters will spawn

    public int PlayerHp = 3; // Player's health

    private bool Check = true; // Check if spawning is in progress
    private int Count = 30; // Fixed count of monsters to spawn
    public int ObjectSpawnCount; // Count of spawned monsters
    public int ObjectDestroyCount; // Count of destroyed monsters

    private bool canDecreaseHp = true; // Check if HP can be decreased
    private List<GameObject> SpawnedObjects = new List<GameObject>(); // List of spawned monsters

    public void ResetGame()
    {
        StopAllCoroutines();
        PlayerHp = 3;
        HpText.text = "HP - " + PlayerHp;
        Check = true;
        canDecreaseHp = true;
        ObjectSpawnCount = 0;
        ObjectDestroyCount = 0;
    }

    public void GameStart()
    {
        ResetGame();
        StartCoroutine(SpawnObjects());
        //Debug.Log("Start");
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (ObjectSpawnCount >= Count) // Check if the spawn limit is reached
            {
                Check = false; // Stop spawning

                // Check if all monsters are destroyed
                if (ObjectDestroyCount >= Count)
                {
                    CallGameClear();
                    yield break; // Exit coroutine
                }
            }

            if (Check)
            {
                SpawnObject(); // Spawn a monster

                float ratio = (float)ObjectSpawnCount / Count; // Calculate spawn ratio
                TimeInterval = Mathf.Lerp(5f, 0.2f, ratio); // Adjust spawn interval

                yield return new WaitForSeconds(TimeInterval); // Wait before next spawn
            }
            else
            {
                yield return null;
            }
        }
    }

    private void SpawnObject()
    {
        if (Object.Length == 0 || SpawnPoint.Length == 0) return; // Check for valid arrays

        int RandomObjectIndex = Random.Range(0, Object.Length);
        GameObject objectToSpawn = Object[RandomObjectIndex];

        int RandomPositionIndex = Random.Range(0, SpawnPoint.Length);
        GameObject spawnedObject = Instantiate(objectToSpawn, SpawnPoint[RandomPositionIndex].position, SpawnPoint[RandomPositionIndex].rotation);

        SpawnedObjects.Add(spawnedObject); // Add to the list
        ObjectSpawnCount++; // Increment spawn count
    }

    public void DestroyAllObject()
    {
        List<GameObject> objectsToDestroy = new List<GameObject>(SpawnedObjects); // Copy list

        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null) // Check for null
            {
                CanDestroy canDestroy = obj.GetComponent<CanDestroy>();
                canDestroy?.OnDestroyed?.Invoke(); // Invoke event if exists
                Destroy(obj, 1f); // Destroy object after 1 second
            }
        }

        objectsToDestroy.Clear();
        SpawnedObjects.Clear(); // Clear the list
    }

    public void DecreseHp()
    {
        if (canDecreaseHp) // Allow HP decrease
        {
            StartCoroutine(DecreaseHpCoroutine());
        }
    }

    private IEnumerator DecreaseHpCoroutine()
    {
        canDecreaseHp = false; // Prevent further decreases
        PlayerHp--;
        HpText.text = "HP - " + PlayerHp;

        //Debug.Log(PlayerHp);

        if (PlayerHp <= 0) // Check for game over
        {
            CallGameOver();
        }

        yield return new WaitForSeconds(1f); // Wait before allowing next decrease
        canDecreaseHp = true; // Allow HP decrease again
    }

    public void CallGameOver()
    {
        GameOver?.Invoke(); // Invoke game over event
    }

    public void CallGameClear()
    {
        GameClear?.Invoke(); // Invoke game clear event
    }
}
