using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;
    private Rigidbody rb;
    public float respawnDistance = 10;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, startPos) > respawnDistance)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = startPos;
            transform.rotation = startRot;
            transform.localScale = Vector3.zero;
            
        }

        if (transform.localScale.x < startScale.x)
        {
            transform.localScale += new Vector3(0.05f,0.05f,0.05f);
        }
    }
}
