using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorManager : MonoBehaviour
{
    public int KeyCheck;

    public UnityEvent KeyCheckEvent;

    public void KeyCheckUp()
    {
        KeyCheck++;
        if(KeyCheck >= 3)
        {
            GameClear();
        }
    }

    public void GameClear()
    {
        KeyCheckEvent?.Invoke();
    }
}
