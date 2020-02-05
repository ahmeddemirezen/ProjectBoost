using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //component variables
    AudioSource audioSource;
    Rigidbody rigidBody;

    //rocket pyhsics
    float rocketMass=80.0f;
    [SerializeField] float fuelMass=120.0f;

    //Timer for rotation
    private float rotTimer= 0.0f;
    [SerializeField] float rotWaitTime=0.005f;

    //timer for location(thrust)
    private float locTimer=0.0f;
    [SerializeField] float locWaitTime=0.05f;
    private float visualTime=0.0f;

    enum State {Alive,Dying,Transcending};
    State state=State.Alive;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
        rigidBody.mass=rocketMass+fuelMass;
        state=State.Alive;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(state==State.Alive)
        {
            Rotate();
            Thrust();
        }
    }
    
    //rotate is changed rocket rotation
    void Rotate()
    {
        rotTimer += Time.deltaTime;//set current frame time
        if (rotTimer > rotWaitTime)
        {
            rotTimer = visualTime;
            rotTimer -= rotWaitTime;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward*2);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-2*Vector3.forward);
            }
        }
    }
    
    //thrust is changed rocket location
    void Thrust()
    {
        locTimer += Time.deltaTime;//set current frame time
        if (locTimer > locWaitTime)
        {
            locTimer = visualTime;
            locTimer -= locWaitTime;
            if (Input.GetKey(KeyCode.Space))
            {
                if (fuelMass > 0)
                {
                    rigidBody.AddRelativeForce(0.0f, 11000.0f, 0.0f);
                    AudioController("play");
                    SettingRocketMass();
                    //print(rigidBody.mass);
                }
            }
            else
            {
                AudioController("stop");
            }
        }
    }
    
    //mass calculation for rocket pyhsics
    void SettingRocketMass()
    {
        fuelMass-=1.0f;
        rigidBody.mass=rocketMass+fuelMass;
    }
    
    //thursting audio control
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

    void OnCollisionEnter(Collision collision)
    {
        if(state!=State.Alive){ return; }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");//todo delete
                break;
            case "Fuel":
                fuelMass=120;
                break;
            case "Finish":
                state=State.Transcending;
                AudioController("stop");
                Invoke("LoadNextLevel",1.0f);
                break;    
            default:
                state=State.Dying;
                AudioController("stop");
                Invoke("LoadCurrentLevel",1.0f);
                break;        
        }   
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(2);
    }
    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(0);
    }
}
