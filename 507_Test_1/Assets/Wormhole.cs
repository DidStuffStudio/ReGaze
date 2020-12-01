using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public string tagCheck;
    public bool correct;
    public bool onTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagCheck) && onTrigger)
        {
       
            correct = true;
            Testing.Instance.Telekinesis.ReleaseObject();
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if(onTrigger)
            OnTriggerFeedback();
        if (correct)
            CorrectFeedback();
    }

    void CorrectFeedback()
    {
        
    }

    void OnTriggerFeedback()
    {
        
    }
}
