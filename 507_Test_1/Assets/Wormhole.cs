using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public string tagCheck;
    public bool correct;
    public bool onTrigger;
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagCheck) && onTrigger)
        {
       
            correct = true;
            Testing.Instance.Telekinesis.ReleaseObject();
            Destroy(other.gameObject);
            CorrectFeedback();
        }
    }

    /*void Update()
    {
        if (correct)
            CorrectFeedback();
    }*/

    void CorrectFeedback()
    {
        mr.material.SetFloat("NS",15.0f);
        mr.material.SetFloat("Em", 5.0f);
        mr.material.SetFloat("VO", 0.0f);
        mr.material.SetFloat("FresnelPower", 0.0f);
    }

}
