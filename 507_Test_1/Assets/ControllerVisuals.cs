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
        spawn = Instantiate(controllerPrefab);
    }

    private void Update()
    {
        
        spawn.transform.position = ControllerManager.Instance.Position;
        spawn.transform.rotation = ControllerManager.Instance.Rotation;
    }
}
