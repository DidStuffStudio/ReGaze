using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;
using UnityEngine.Serialization;

public class Telekinesis : MonoBehaviour
{
    private EyeRaycast eyeRaycast;
    private const ControllerButton TriggerButton = ControllerButton.Trigger;
    private bool distanceCalculated = false;
    private bool isGrabbed;

    public Transform telekineticTransform;

    public GameObject grabbedObject;
    [SerializeField] private float depthMoveStrength;
    [SerializeField] private float moveConstant;
    private float moveStep = 0.5f;
    private float distance = 0.0f;
    private Vector3 grabbedTans;
    private Vector3 grabbedObjDir;

    public Vector3 pos = Vector3.zero, latePos = Vector3.zero, deltaPos = Vector3.zero;
    public float strength = 5;

    [SerializeField] private Material seethrough;
    private Material originalMat;
    


    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
    }

    private void Update()
    {
   
        
        if (eyeRaycast.raycastHitObject && (ControllerManager.Instance.GetButtonPressDown(TriggerButton) ||
                                            Input.GetButtonDown("Fire2")))
        {
            
            isGrabbed = true;
            grabbedObject = eyeRaycast.raycastHitObject;
            originalMat = grabbedObject.GetComponent<Renderer>().material;
            grabbedObject.GetComponent<Renderer>().material = seethrough;
            // grabbedTans = grabbedObject.transform.position;
            if (!distanceCalculated)
            {
                CalculateDistance();
            }
        }

        if (ControllerManager.Instance.GetButtonPressUp(TriggerButton) || Input.GetButtonUp("Fire2"))
        {
            // grabbedObject.GetComponent<Rigidbody>().AddForce(eyeRaycast.eyeTrackingData.GazeRay.Direction, ForceMode.Impulse);

            if (grabbedObject)
            {
                var rb = grabbedObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.AddForce(grabbedObjDir * strength, ForceMode.Force);
                grabbedObject.GetComponent<Renderer>().material = originalMat;
                grabbedObject = null;
            }

            isGrabbed = false;

            distanceCalculated = false;
        }
        print(Input.GetAxis("right joystick vertical"));

        if (!isGrabbed) return;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        // Vector3.MoveTowards(grabbedObject.transform.position, eyeRaycast.eyeTrackingData.GazeRay.Direction * distance, 2);
        
        
        

       if (Input.GetAxis("right joystick vertical") != 0) distance += Input.GetAxis("right joystick vertical") * depthMoveStrength;
        
        
        var telekineticTransformDist =
            Vector3.Distance(telekineticTransform.position, grabbedObject.transform.position);
        moveStep = telekineticTransformDist / moveConstant;



        if (eyeRaycast.eyeTrackingData.IsLeftEyeBlinking || eyeRaycast.eyeTrackingData.IsRightEyeBlinking) return;

        telekineticTransform.position = Camera.main.transform.position + eyeRaycast.eyeDirection * distance;
        
        grabbedObject.transform.position =
            Vector3.MoveTowards(grabbedObject.transform.position, telekineticTransform.position, moveStep);

        pos = grabbedObject.transform.position;
        grabbedObjDir = grabbedObject.transform.position - latePos;
        latePos = grabbedObject.transform.position;
    }
    
    void CalculateDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, grabbedObject.transform.position);
        distanceCalculated = true;
    }
}


/*
print(Input.GetAxis("Vertical"));
print(Input.GetButtonDown("Fire1")); // B
print(Input.GetButtonDown("Fire2")); // A
print(Input.GetButtonDown("Fire3")); // Y
*/