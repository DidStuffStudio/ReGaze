using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usability_StoryManager : MonoBehaviour
{

    public AudioSource voiceoverSource;
    public AudioClip[] voiceClips = new AudioClip[8];

    public bool jumped;
    private bool hasJumpedFirstTime;


    public void PlayVoice(int part)
    {
        voiceoverSource.clip = voiceClips[part];
        voiceoverSource.PlayOneShot(voiceClips[part]);
        print("PLAYED");
    }

    private void Update()
    {
        if (jumped && !hasJumpedFirstTime)
        {
            PlayVoice(2);
            hasJumpedFirstTime = true;
        }
    }
}
