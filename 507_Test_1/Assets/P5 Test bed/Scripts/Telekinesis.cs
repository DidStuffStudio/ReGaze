using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;
using UnityEngine.Serialization;

public class Telekinesis : MonoBehaviour
{
    private EyeRaycast eyeRaycast;
    private const ControllerButton TouchpadButton = ControllerButton.Touchpad;
    private bool distanceCalculated = false;
    private bool isGrabbed;

    public Transform telekineticTransform;

    public GameObject grabbedObject;
    [SerializeField] private float depthMoveStrength;
    [SerializeField] private float moveConstant;
    private float moveStep = 0.5f;
    private float distance = 0.0f;

    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
    }

    private void Update()
    {
        if (eyeRaycast.raycastHitObject && (ControllerManager.Instance.GetButtonPressDown(TouchpadButton) ||
                                            Input.GetButtonDown("Fire2")))
        {
            isGrabbed = true;
            grabbedObject = eyeRaycast.raycastHitObject;
            if (!distanceCalculated)
            {
                CalculateDistance();
            }
        }

        if (ControllerManager.Instance.GetButtonPressUp(TouchpadButton) || Input.GetButtonUp("Fire2"))
        {
            // grabbedObject.GetComponent<Rigidbody>().AddForce(eyeRaycast.eyeTrackingData.GazeRay.Direction, ForceMode.Impulse);
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            isGrabbed = false;
            grabbedObject = null;
            distanceCalculated = false;
        }


        if (!isGrabbed) return;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        // Vector3.MoveTowards(grabbedObject.transform.position, eyeRaycast.eyeTrackingData.GazeRay.Direction * distance, 2);

        distance += Input.GetAxis("Vertical") * depthMoveStrength;
        var telekineticTransformDist =
            Vector3.Distance(telekineticTransform.position, grabbedObject.transform.position);
        moveStep = telekineticTransformDist / moveConstant;

        telekineticTransform.position = eyeRaycast.eyeDirection * distance;
        Debug.DrawRay(eyeRaycast.eyeOrigin, eyeRaycast.eyeDirection * 5, Color.green);
        grabbedObject.transform.position =
            Vector3.MoveTowards(grabbedObject.transform.position, telekineticTransform.position, moveStep);
    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(eyeRaycast.eyeOrigin, grabbedObject.transform.position);
        distanceCalculated = true;
    }
}


/*
print(Input.GetAxis("Vertical"));
print(Input.GetButtonDown("Fire1")); // B
print(Input.GetButtonDown("Fire2")); // A
print(Input.GetButtonDown("Fire3")); // Y
*/