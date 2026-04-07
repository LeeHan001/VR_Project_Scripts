using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
   
    public Animator Fade_animator;

    public void LoadScene(int SceneIndex)
    {
        StartCoroutine(TransitionToScene(SceneIndex));
    }

    private IEnumerator TransitionToScene(int SceneIndex)
    {
        // Play Appear Animation
        Fade_animator.SetBool("isFaded", false);
        yield return new WaitForSeconds(2f); // Wait for Appear Animation

        // Scene Load
        SceneManager.LoadScene(SceneIndex);
    }
}