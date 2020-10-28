using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;

public class ControllerGesture : MonoBehaviour
{
    private Vector3 controllerPosition;
    private Vector3 storedControllerPosition;
    public float zMovement;

    private void Update()
    {
        controllerPosition = ControllerManager.Instance.Position;
        if (Vector3.Distance(controllerPosition, Camera.main.transform.position) < Vector3.Distance(Camera.main.transform.position, storedControllerPosition))
        {
            zMovement = -Vector3.Distance(controllerPosition,storedControllerPosition);
        }
        else
        {
            zMovement = Vector3.Distance(controllerPosition,storedControllerPosition);
        }
        
        //zMovement = Camera.main.transform.position + Camera.main.transform.forward * Vector3.Distance(Camera.main.transform.position, controllerPosition);
        //zMovement = Vector3.Distance(Camera.main.transform.position, controllerPosition);

    }

    public void StoreVector(Vector3 pos)
    {
        storedControllerPosition = pos;
    }

}
