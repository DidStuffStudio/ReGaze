using System;
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
    public int index = 0;
    public float lightIntensity;


    private void Start()
    {
        lightIntensity = GetComponentInChildren<Light>().intensity;
        GetComponentInChildren<Light>().intensity = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12) // player layer
        {
            print("Entered boulder with index " + index);
            var src = GetComponent<AudioSource>();
            //src.clip = audioToPlay;
            src.Play();
            OnTrigger?.Invoke();
            manager.progressLight(index);
            if (index < manager.helpers.Length)
            {
                Testing.Instance.MoveWayFindingAudioSource(manager.helpers[index + 1].transform.position);
            }
        }
    }
}