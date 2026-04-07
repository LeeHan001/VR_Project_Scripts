using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{

    public UnityEvent GameOver;
    public UnityEvent GameClear;

    public GameObject[] Object;
    public GameObject BossObject;

    public bool BossSpawnInSpanwer = false;
    public bool BossSpawnInCanDestroy = false;

    private float TimeInterval = 5f;
    public Transform[] SpawnPoint;

    private bool Check = true; // Check if spawning is in progress
    private int Count = 30; // Fixed count 

    public int ObjectSpawnCount;
    public int ObjectDestroyCount;

    private List<GameObject> SpawnedObjects = new List<GameObject>();

    private float BossSpawnTime; 
    private float BossAliveTime; 
    public void ResetGame()
    {
        StopAllCoroutines();
        BossSpawnInSpanwer = false;
        BossSpawnInCanDestroy = false;
        Check = true;
        ObjectSpawnCount = 0;
        ObjectDestroyCount = 0;
        BossSpawnTime = 0;
        BossAliveTime = 0;
    }
    public void GameStart()
    {
        ResetGame();
        StartCoroutine(SpawnObjects());
        Debug.Log("Start");
    }

    private void FixedUpdate()
    {
        // Check if the boss has spawned and track how long it has been alive
        if (BossSpawnInSpanwer && !BossSpawnInCanDestroy)
        {
            BossAliveTime = Time.time - BossSpawnTime; // Calculate time since the boss was spawned

            // If the boss has been alive for more than 5 seconds, trigger game over
            if (BossAliveTime > 5f)
            {
                CallGameOver();
            }
            //Debug.Log(BossAliveTime);
        }
        else if(BossSpawnInSpanwer && BossSpawnInCanDestroy)
        {
            BossAliveTime = Time.time - BossSpawnTime; // Calculate time since the boss was spawned

            // If the boss has been alive for more than 5 seconds, trigger game clear
            if (BossAliveTime > 5f)
            {
                CallGameClear();
            }
            Debug.Log(BossAliveTime + "DSf");
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (Check)
            {
                if (Count == ObjectSpawnCount)
                {
                    yield return new WaitForSeconds(3f);
                    SpawnBossObject();
                }
                else
                    SpawnObject();

                float ratio = (float)ObjectSpawnCount / Count;
                TimeInterval = Mathf.Lerp(5f, 0.5f, ratio); 

                yield return new WaitForSeconds(TimeInterval);

            }
            else
                yield return null;
        }
    }

    private void SpawnObject()
    {
        if (Object.Length == 0 || SpawnPoint.Length == 0) 
            return; 

        int RandomObjectIndex = Random.Range(0, Object.Length);
        GameObject objectToSpawn = Object[RandomObjectIndex];

        int RandomPositionIndex = Random.Range(0, SpawnPoint.Length);
        GameObject spawnedObject = Instantiate(objectToSpawn, SpawnPoint[RandomPositionIndex].position, SpawnPoint[RandomPositionIndex].rotation);

        SpawnedObjects.Add(spawnedObject);

        ObjectSpawnCount++;

        //If more than 10 objects are piled up, game over
        if (ObjectSpawnCount - ObjectDestroyCount >= 10)
        {
            StopAllCoroutines();
            CallGameOver();
        }

    }

    private void SpawnBossObject()
    {
        GameObject boss = Instantiate(BossObject, SpawnPoint[0].position, SpawnPoint[0].rotation);
        SpawnedObjects.Add(boss);
        Check = false;
        BossSpawnInSpanwer = true;
        BossSpawnTime = Time.time;
    }

    public void DestroyAllObject()
    {
        // Create a copy to safely remove during foreach loop
        List<GameObject> objectsToDestroy = new List<GameObject>(SpawnedObjects);

        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null) // Null check
            {
                CanDestroy canDestroy = obj.GetComponent<CanDestroy>();
                if (canDestroy != null)
                {
                    canDestroy.OnDestroyed?.Invoke(); // Call OnDestroyed event
                }
                Destroy(obj,1f);// Destroy object
            }
        }

        objectsToDestroy.Clear();
        SpawnedObjects.Clear(); // Clear the list
        BossSpawnInSpanwer = false;
    }


    public void CallGameOver()
    {
        GameOver?.Invoke();
    }
    public void CallGameClear()
    {
        GameClear?.Invoke();
    }

}