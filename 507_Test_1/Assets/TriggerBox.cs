using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    public UnityEvent TriggerEnter;

    public UnityEvent TriggerExit;

    public string tag;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            TriggerEnter?.Invoke();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            TriggerExit?.Invoke();
        }

    }
}
