using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using Tobii.XR.Examples;

public class ControllerVisuals : MonoBehaviour
{
    public GameObject controllerPrefab;
    private GameObject spawn;
    private void Start()
    {
        spawn = Instantiate(controllerPrefab, transform);
        spawn.transform.position = transform.position;
    }

    private void Update()
    {
        
        spawn.transform.localPosition = ControllerManager.Instance.Position - transform.position;
        spawn.transform.localRotation = ControllerManager.Instance.Rotation;
    }
}
