using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    private EyeRaycast eyeRaycast;
    private const ControllerButton TriggerButton = ControllerButton.Trigger;
    private bool isGrabbed;
    //public Rigidbody telekineticParent;
    public GameObject grabbedObject;
    
    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
    }

    void Update()
    {
        if (eyeRaycast.raycastHitObject && ControllerManager.Instance.GetButtonPressDown(TriggerButton))
        {
            isGrabbed = true;
            grabbedObject = eyeRaycast.raycastHitObject;
        }

        if (ControllerManager.Instance.GetButtonPressUp(TriggerButton))
        {
            isGrabbed = false;
            //grabbedObject.GetComponent<SpringJoint>().connectedBody = null;
            grabbedObject = null;
        }

        if (isGrabbed)
        {
            
            //grabbedObject.GetComponent<SpringJoint>().connectedBody =
                //telekineticParent.GetComponent<Rigidbody>();
            grabbedObject.transform.position = Camera.main.transform.position +
                                                             Camera.main.transform.forward *
                                                             Vector3.Distance(Camera.main.transform.position,
                                                                 eyeRaycast.raycastHitObject.transform
                                                                     .position);
        }
    }
}

