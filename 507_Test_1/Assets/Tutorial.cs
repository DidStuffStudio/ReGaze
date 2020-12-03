using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioSource tutorialAudioSrc;

    public AudioClip welcome,getStarted ,jumpAround, telekinesis;

    private EyeRaycast eyeRaycast;

    public bool jumped;

    public bool called;

    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
        StartCoroutine(WaitForWelcome());
    }

    public void Update()
    {
        if (jumped && !called)
        {
            
            called = true;
            tutorialAudioSrc.clip = jumpAround;
            tutorialAudioSrc.Play();
            StartCoroutine(Wait(jumpAround.length + 5.0f));
        }
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayPart3();
    }
    
    IEnumerator WaitForWelcome()
    {
        yield return new WaitForSeconds(welcome.length);
        tutorialAudioSrc.clip = getStarted;
        tutorialAudioSrc.Play();
        eyeRaycast.welcomeAudioFinito = true;
    }
    

    void PlayPart3()
    {
        tutorialAudioSrc.clip = telekinesis;
        tutorialAudioSrc.Play();
    }
}
