using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    //rotation time
    float rotTimer=0.0f;
    float rotWait=0.0005f;
    float visualTime=0.0f;

    //destroy time
    float destroyTimer=0.0f;
    float destroyWait=0.01f;
    
    //destroy check variable
    bool destroyCheck=false;
 
    // Update is called once per frame
    void Update()
    {
        rotTimer=Time.deltaTime;
        destroyTimer=Time.deltaTime;
        if(destroyTimer>destroyWait)
            {
                destroyTimer=visualTime;
                destroyTimer-=destroyWait;
                if(destroyCheck==true){
                    Destroy(gameObject);}
            }
        if(rotTimer>rotWait)
        {
            rotTimer=visualTime;
            rotTimer-=rotWait;
            transform.Rotate(0.0f,1.0f,0.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Rocket":
                destroyCheck=true;
                break;
            default:
                break;        
        }
    }
}
