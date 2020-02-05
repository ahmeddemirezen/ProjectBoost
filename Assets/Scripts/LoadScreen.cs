using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    //rotation time
    float rotTimer=0.0f;
    float rotWait=0.0005f;
    float visualTime=0.0f;
    
    void Start()
    {
        Invoke("LoadNextLevel",3.0f);
    }
    // Update is called once per frame
    void Update()
    {
        rotTimer=Time.deltaTime;
        
        if(rotTimer>rotWait)
        {
            rotTimer=visualTime;
            rotTimer-=rotWait;
            transform.Rotate(0.0f,1.0f,0.0f);
        }
    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
