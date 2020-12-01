using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMasterManager : MonoBehaviour
{
    public int positionIndex = 0;
    public GameObject[] helpers;
    private Light[] lights;
    public float lightIntensity = 0.01f;

    public float fadeSpeed;

    private void Start()
    {
        lights = new Light[helpers.Length];
        int counter = 0;
        foreach (var helper in helpers)
        {
            lights[counter] = helper.GetComponentInChildren<Light>();
            counter++;
        }

        //lights[0].intensity = helpers[0].transform.GetComponent<AudioTrigger>().lightIntensity;
    }

    private void Update()
    {
        if (lights[positionIndex + 1].intensity <
            helpers[positionIndex + 1].transform.GetComponent<AudioTrigger>().lightIntensity)
        {
            lights[positionIndex].intensity += fadeSpeed;
        }

        if (positionIndex >= 0)
        {
            if (lights[positionIndex].intensity > 0) lights[positionIndex].intensity-=fadeSpeed;
            else lights[positionIndex].transform.parent.gameObject.SetActive(false);
        }
        
    }

    public void progressLight(int i)
    {
        positionIndex = i;
        /*lights[i].transform.parent.gameObject.SetActive(false);
        if (i < lights.Length)
        {
            lights[i + 1].transform.parent.gameObject.SetActive(true);
        }*/
    }
}