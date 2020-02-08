using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector=new Vector3(10f,10f,10f);
    [Range(0,1)] [SerializeField] float movementFactor;
    Vector3 startingPos;
    [SerializeField]float period=2f;
    const float rad=Mathf.PI*2;
    void Start()
    {
        startingPos=transform.position;
    }
    void Update()
    {
        float cycle=Time.time/period;
        float rawSinWave=Mathf.Sin(rad*cycle);
        movementFactor=rawSinWave / 2f + 0.5f;
        transform.position=startingPos+movementVector*movementFactor;
        
        
        //Debug.Log("Time.time"+Time.time);
        //Debug.Log("Time.deltaTime"+Time.deltaTime);
    }
}
