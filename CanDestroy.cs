using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanDestroy : MonoBehaviour
{

    public Spawner TheSpawner;

    public float DestroyDelay = 1f;

    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    public int BossHp = 15;

    private bool IsDestroyed = false;

    private void Start()
    {
        TheSpawner = FindObjectOfType<Spawner>();
        OnCreated?.Invoke();
    }

    public void Destroy()
    {
        //Debug.Log("Destroy");

        if (IsDestroyed)
            return;
        IsDestroyed = true;

        TheSpawner.ObjectDestroyCount++;

        Destroy(gameObject, DestroyDelay);


        OnDestroyed?.Invoke();
    }

    public void BossDestroy()
    {
        BossHp--;

        //Debug.Log("BossDestroy");

        if (IsDestroyed)
            return;
        if(BossHp <= 0)
        {
            IsDestroyed = true;

            Destroy(gameObject, DestroyDelay);

            OnDestroyed?.Invoke();

            TheSpawner.BossSpawnInCanDestroy = true;
        }
    }

}
