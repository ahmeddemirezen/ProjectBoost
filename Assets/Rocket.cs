using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigidBody;
    //rocket pyhsics
    float rocketMass=80.0f;
    float fuelMass=120.0f;
    //********************
    //Timer for rotation
    private float rotTimer= 0.0f;
    const float rotWaitTime=0.01f;
    //timer for location(thrust)
    private float locTimer=0.0f;
    const float locWaitTime=0.05f;
    private float visualTime=0.0f;
    //*******************************
    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
        rigidBody.mass=rocketMass+fuelMass;
    }
    
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        rotTimer += Time.deltaTime;
        locTimer += Time.deltaTime;
        if(locTimer>locWaitTime)
        {
            locTimer=visualTime;
            locTimer-=locWaitTime;
            if(Input.GetKey(KeyCode.Space))
            {    
                if(fuelMass>0)
                {
                    rigidBody.AddRelativeForce(0.0f,10000.0f,0.0f);
                    AudioController("play");
                    SettingRocketMass();
                    print(rigidBody.mass);
                }
            }
            else
            {
                AudioController("stop");
            }
        }
        
        if(rotTimer>rotWaitTime)
        {
            rotTimer=visualTime;
            rotTimer-=rotWaitTime;
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward);
            }
        }
    }

    void SettingRocketMass()
    {
        fuelMass-=1.0f;
        rigidBody.mass=rocketMass+fuelMass;
    }

    void AudioController(string audioFuncCheck)
    {
        if(audioFuncCheck=="play")
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if(audioFuncCheck=="stop")
        {
            audioSource.Stop();
        }
    }
}
