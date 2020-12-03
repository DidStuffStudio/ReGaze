using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FinalPuzzle : MonoBehaviour
{

    public MeshRenderer forcefieldR, leftPlatformR, rightPlatformR, leftWormholeR, rightWormholeR, leftOrbR, rightOrbR;
    public Grabbable leftOrbG, rightOrbG;

    public Wormhole leftWormhole, rightWormhole;

    public bool test;
    public VisualEffect core;

    private float coreSizeMultiplyer = 1.0f;
    public float sizeChangeInterval;
    private float coreSize = 2.0f;
    public GameObject endPanel;
    private bool finito;
    public AudioSource orbLeftAudioSrc, orbRightAudioSrc, shieldSrc;
    public AudioClip shieldDecrease, complete;
    private bool half, full;
    
    private void Update()
    {


        if (rightWormhole.onTrigger)
        {
            rightPlatformR.materials[1].SetFloat("Em", 1.5f);
            if(rightOrbR) rightOrbR.material.SetFloat("Em", 6.0f);
            if (rightOrbG.isSelected)
            {
                orbRightAudioSrc.Play();
                rightWormholeR.material.SetFloat("FresnelPower", 1.5f);
                rightWormholeR.material.SetFloat("Em", 10.0f);
            }
            else
            {
                if(orbRightAudioSrc) orbRightAudioSrc.Stop();
                rightWormholeR.material.SetFloat("FresnelPower", 0f);
                rightWormholeR.material.SetFloat("Em", 5.0f);
            }

        }

        else
        {
            rightWormholeR.material.SetFloat("FresnelPower", 0);
            rightPlatformR.materials[1].SetFloat("Em", 0.5f);
            if(rightOrbR) rightOrbR.material.SetFloat("Em", 3.5f);
            rightWormholeR.material.SetFloat("Em", 5.0f);
        }

        if (leftWormhole.onTrigger)
        {
            leftPlatformR.materials[1].SetFloat("Em", 1.5f);
            if(leftOrbR) leftOrbR.material.SetFloat("Em", 6.0f);
            if (leftOrbG.isSelected)
            {
                
                orbLeftAudioSrc.Play();
                leftWormholeR.material.SetFloat("FresnelPower", 1.5f);
                leftWormholeR.material.SetFloat("Em", 12.0f);
            }
            else
            {
                if(orbLeftAudioSrc) orbLeftAudioSrc.Stop();
                leftWormholeR.material.SetFloat("FresnelPower", 0f);
                leftWormholeR.material.SetFloat("Em", 5.0f);
            }

        }
        else
        {
            leftWormholeR.material.SetFloat("FresnelPower", 0);
            leftPlatformR.materials[1].SetFloat("Em", 0.5f);
            if(leftOrbR) leftOrbR.material.SetFloat("Em", 3.5f);
            leftWormholeR.material.SetFloat("Em", 5.0f);
        }



        if (test)
        {
            //Forcefield half capacity
            forcefieldR.material.SetFloat("Forcefield_Speed", -0.02f);
            forcefieldR.material.SetFloat("Forcefield_Alpha", 0.1f);
            coreSizeMultiplyer += sizeChangeInterval;
            if (!finito)
            {
                StartCoroutine(WaitForEnd());
            }
        }
        
        
        if (leftWormhole.correct && rightWormhole.correct)
        {
            //Forcefield gone and core expand   
            forcefieldR.material.SetFloat("Forcefield_Speed", -0.00f);
            forcefieldR.material.SetFloat("Forcefield_Alpha", 0.0f);
            coreSizeMultiplyer += sizeChangeInterval;
            if (!finito)
            {
                StartCoroutine(WaitForEnd());
            }

            if (!full)
            {
                full = true;
                shieldSrc.clip = complete;
                shieldSrc.loop = true;
                shieldSrc.Play();
            }


        }
        
        else if ((leftWormhole.correct || rightWormhole.correct ) && !half)
        {
            half = true;
            shieldSrc.clip = shieldDecrease;
            shieldSrc.Play();
            //Forcefield half capacity
            forcefieldR.material.SetFloat("Forcefield_Speed", -0.02f);
            forcefieldR.material.SetFloat("Forcefield_Alpha", 0.1f);
        }

        coreSize = (Mathf.Sin(Time.time) + 1.5f) * coreSizeMultiplyer;
        core.SetFloat("SphereSize", coreSize);
        
    }

    IEnumerator WaitForEnd()
    {
        finito = true;
        yield return new WaitForSeconds(10.0f);
        endPanel.SetActive(true);
    }
}
