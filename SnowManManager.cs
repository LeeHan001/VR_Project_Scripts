using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SnowManManager : MonoBehaviour
{
    public bool[,] CheckArray = new bool[3, 3];
    public int Index = 0;

    public GameObject[] SnowBall;

    public UnityEvent[] OnCallSnowMan;
    public UnityEvent OnCallGameEnd;
    void Start()
    {
        InitializeCheckArray();
        MoveSnowBallsRandomly();
    }

    void InitializeCheckArray()
    {
        for (int i = 0; i < CheckArray.GetLength(0); i++)
        {
            for (int j = 0; j < CheckArray.GetLength(1); j++)
            {
                CheckArray[i, j] = false; // Initial value false
            }
        }
    }

    public void CheckTrueBottom(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 0] = true;
        CheckGameClear(SnowManIndex);
    }

    public void CheckTrueMiddle(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 1] = true;
        CheckGameClear(SnowManIndex);
    }
    public void CheckTrueTop(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 2] = true;
        CheckGameClear(SnowManIndex);
    }
    public void CheckFalseBottom(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 0] = false;
    }

    public void CheckFalseMiddle(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 1] = false;

    }
    public void CheckFalseTop(int SnowManIndex)
    {
        CheckArray[SnowManIndex, 2] = false;
    }


    public void CheckGameClear(int SnowManIndex)
    {
        bool AllChecked = true;

        for (int j = 0; j < CheckArray.GetLength(1); j++)
        {
            if (!CheckArray[SnowManIndex, j])
            {
                AllChecked = false; // If there is one false, set it to false
                break; //End of repeat statement
            }
        }

        if (AllChecked)
        {
            Index++;
            Debug.Log("Index Up: " + Index);
            CallSnowMan(SnowManIndex);
            MoveSnowBallsRandomly();
        }

        // Clear the game when there are more than 3 checked states
        if (Index >= 3)
        {
            StartCoroutine(GameClearAfterDelay(3f));
            Debug.Log("Game Clear!");

        }
    }

    private void CallSnowMan(int SnowManIndex)
    {
        if (SnowManIndex >= 0 && SnowManIndex < OnCallSnowMan.Length)
        {
            OnCallSnowMan[SnowManIndex]?.Invoke();
        }
    }

    private IEnumerator GameClearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Game Clear Event 
        OnCallGameEnd?.Invoke();
       
    }

    private void MoveSnowBallsRandomly()
    {
        foreach (GameObject snowBall in SnowBall)
        {
            float randomX = Random.Range(-45f, 45f);
            float randomZ = Random.Range(-45f, 45f);
            Vector3 newPosition = new Vector3(randomX, 10f, randomZ);
            snowBall.transform.position = newPosition;
        }
    }
}
