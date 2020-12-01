using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioToPlay;
    public UnityEvent OnTrigger;
    public TheMasterManager manager;
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            print("Entered");
            var src = GetComponent<AudioSource>();
            src.clip = audioToPlay;
            src.Play();
            OnTrigger?.Invoke();
            manager.progressLight(index);

        }
    }
}
