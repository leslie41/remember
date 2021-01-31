using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour
{
    public Transform PlayerTransform;
   


    private void Start()
    {
        //transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (PlayerTransform.position.y < 0)
        {
           transform.position = new Vector3(0,0.55f ,0);
        }
        else if (PlayerTransform.position.y >57 )
        {
            transform.position = new Vector3(0, 60.5f, 0);
        }
        else
        {
            
           // transform.position =  PlayerTransform.position;
            transform.position = new Vector3(0,PlayerTransform.position.y,0);
        }
    }



}

