using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent GameOverEvent;
    
    public void Call()
    {
        GameOverEvent?.Invoke();
    }
}
