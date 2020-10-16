using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public enum TelekineticMethod {ControllerManipulation, Copy, Touchpad}
    public TelekineticMethod telekineticMethod;
    private EyeRaycast eyeRaycast;
    private const ControllerButton TouchpadButton = ControllerButton.Touchpad;
    private bool distanceCalculated = false;
    private bool isGrabbed;
    private float distance = 0.0f;
    //public Rigidbody telekineticParent;
    public GameObject grabbedObject;
    [SerializeField] private float grabbableObjectOffset;

    private void Start()
    {
        
        eyeRaycast = GetComponent<EyeRaycast>();
    }

    void Update()
    {
        if (eyeRaycast.raycastHitObject && ControllerManager.Instance.GetButtonPressDown(TouchpadButton))
        {
            isGrabbed = true;
            grabbedObject = eyeRaycast.raycastHitObject;
            if (!distanceCalculated)
            {
                CalculateDistance();
            }
        }

        if (ControllerManager.Instance.GetButtonPressUp(TouchpadButton))
        {
            // grabbedObject.GetComponent<Rigidbody>().AddForce(eyeRaycast.eyeTrackingData.GazeRay.Direction, ForceMode.Impulse);
            isGrabbed = false;
            grabbedObject = null;
            distanceCalculated = false;
        }

        if (isGrabbed)
        {
            grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            // Vector3.MoveTowards(grabbedObject.transform.position, eyeRaycast.eyeTrackingData.GazeRay.Direction * distance, 2);
            grabbedObject.transform.position = eyeRaycast.eyeTrackingData.GazeRay.Direction * distance;
            
            switch (telekineticMethod)
            {
                case TelekineticMethod.Copy:
                {


                    break;
                }
            }
        }

    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(eyeRaycast.eyeTrackingData.GazeRay.Origin, grabbedObject.transform.position);
        distanceCalculated = true;
    }
        
        
}