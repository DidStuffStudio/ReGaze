using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR.Examples;
using Tobii.XR;

public class SpawnTargets : MonoBehaviour
{
    public Transform prefab;
    public GameObject[] blinkTargets;
    public float height = 1.5f;
    private Vector3 startingPosition;
    private float numTargets = 10;
    Camera mainCamera;
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, height, transform.position.z);
        //Debug.Log(blinkTargets[1].transform.position);

        for (int i = 1; i < blinkTargets.Length; i++)
        {
            blinkTargets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.GetChild(0).position);




        /*

        if (Vector3.Distance(blinkTargets[i].transform.position, transform.GetChild(0).position) < 5)
        {
            
            for (i = 1; i < numTargets;)
                blinkTargets[i].SetActive(true);
            
        }
        */

        

        checkPosition();
        Debug.Log(checkPosition());

        blinkTargets[checkPosition()].SetActive(true);
        
        
    }

    private int checkPosition()
    {
        int pos=0;
        for (int i = 0; i < numTargets; i++)
        {
            if (Vector3.Distance(blinkTargets[i].transform.position, transform.GetChild(0).position) < 5)
            {
                
            }
            
        }

        if (Vector3.Distance(blinkTargets[0].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 1;
        }
        if (Vector3.Distance(blinkTargets[1].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 2;
        }
        if (Vector3.Distance(blinkTargets[2].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 3;
        }
        if (Vector3.Distance(blinkTargets[3].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 4;
        }
        if (Vector3.Distance(blinkTargets[4].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 5;
        }
        if (Vector3.Distance(blinkTargets[5].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 6;
        }
        if (Vector3.Distance(blinkTargets[6].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 7;
        }
        if (Vector3.Distance(blinkTargets[7].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 8;
        }
        if (Vector3.Distance(blinkTargets[8].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 9;
        }
        if (Vector3.Distance(blinkTargets[9].transform.position, transform.GetChild(0).position) < 5)
        {
            pos = 10;
        }

        return pos;
    }
    /*
    public bool shouldSpawn()
    {
        if ((blinkTargets[0].transform.position - transform.GetChild(0).position).magnitude < 5.0f)
        {
            return true;
        }
        else return false;
    }
    */
}
