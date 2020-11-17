using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usability_StoryManager : MonoBehaviour
{

    public AudioSource voiceoverSource;
    public AudioClip[] voiceClips = new AudioClip[8];



    public void PlayVoice(int part)
    {
        voiceoverSource.clip = voiceClips[part];
        voiceoverSource.Play();
        print("PLAYED");
    }
    
}
