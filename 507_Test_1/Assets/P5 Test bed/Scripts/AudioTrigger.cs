using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioToPlay;
    public UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            var src = GetComponent<AudioSource>();
            src.clip = audioToPlay;
            src.Play();
            OnTrigger?.Invoke();

        }
    }
}
