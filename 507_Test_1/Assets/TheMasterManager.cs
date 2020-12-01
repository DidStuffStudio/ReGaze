using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMasterManager : MonoBehaviour
{
    public int positionIndex = 0;
    public Light[] lights;
    public float lightIntensity = 0.01f;

    private void Start()
    {

        foreach (var light in lights)
        {
            light.transform.parent.gameObject.SetActive(false);
        }
        lights[0].transform.parent.gameObject.SetActive(true);
    }
    

    public void progressLight(int i)
    {
        lights[i].transform.parent.gameObject.SetActive(false);
        if (i < lights.Length)
        {
            positionIndex = i;
            //lights[i].intensity = 0.0f;
            //lights[i+1].intensity = lightIntensity;
           
            lights[i + 1].transform.parent.gameObject.SetActive(true);
        }

    }
    
}
