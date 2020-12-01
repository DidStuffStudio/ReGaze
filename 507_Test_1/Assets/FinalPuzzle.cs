using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FinalPuzzle : MonoBehaviour
{

    public MeshRenderer forcefield;

    public Wormhole leftWormhole, rightWormhole;

    public bool test;
    public VisualEffect core;

    private float coreSizeMultiplyer = 1.0f;
    public float sizeChangeInterval;
    private float coreSize = 2.0f;
    public GameObject endPanel;
    private bool finito;
    private void Update()
    {
        
    
        if (test)
        {
            //Forcefield half capacity
            forcefield.material.SetFloat("Forcefield_Speed", -0.02f);
            forcefield.material.SetFloat("Forcefield_Alpha", 0.1f);
            coreSizeMultiplyer += sizeChangeInterval;
            if (!finito)
            {
                StartCoroutine(WaitForEnd());
            }
        }
        
        
        if (leftWormhole.correct && rightWormhole.correct)
        {
            //Forcefield gone and core expand   
            forcefield.material.SetFloat("Forcefield_Speed", -0.00f);
            forcefield.material.SetFloat("Forcefield_Alpha", 0.0f);
            coreSizeMultiplyer += sizeChangeInterval;
            if (!finito)
            {
                StartCoroutine(WaitForEnd());
            }


        }
        
        else if (leftWormhole.correct || rightWormhole.correct)
        {
            //Forcefield half capacity
            forcefield.material.SetFloat("Forcefield_Speed", -0.02f);
            forcefield.material.SetFloat("Forcefield_Alpha", 0.1f);
        }

        coreSize = (Mathf.Sin(Time.time) + 1.5f) * coreSizeMultiplyer;
        core.SetFloat("SphereSize", coreSize);
        
    }

    IEnumerator WaitForEnd()
    {
        finito = true;
        yield return new WaitForSeconds(5.0f);
        endPanel.SetActive(true);
    }
}
