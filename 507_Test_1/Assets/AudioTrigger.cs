using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioToPlay;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            var src = GetComponent<AudioSource>();
            src.clip = audioToPlay;
            src.Play();

        }
    }
}
