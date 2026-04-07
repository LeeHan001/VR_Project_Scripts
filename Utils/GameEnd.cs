using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public Animator GameOver_animator;
    public Animator GameClear_animator;
    public void GameOverAnimation()
    {
        GameOver_animator.SetTrigger("AnimTrigger");
    }

    public void GameClearAnimation()
    {
        GameClear_animator.SetTrigger("AnimTrigger");
    }
}
