using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMasterManager : MonoBehaviour
{
    public int positionIndex = 0;
    public Light[] lights;
    public float lightIntensity = 0.01f;
    
    void Update()
    {
        print(positionIndex);
    }

    private void Start()
    {
        progressLight(0);
    }

    public void progressLight(int i)
    {
        if (i > 0)
        {
            lights[i - 1].intensity = 0.0f;
        }
        
        lights[i].intensity = lightIntensity;
    }
    
}
