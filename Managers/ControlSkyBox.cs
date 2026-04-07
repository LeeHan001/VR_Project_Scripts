using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkyBox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Rotate Skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
    }
}
