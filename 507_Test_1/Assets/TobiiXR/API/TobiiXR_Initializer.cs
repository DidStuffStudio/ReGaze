// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.XR;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Optional convenience initializer for TobiiXR. Used by the TobiiXR Initializer prefab.
///
/// Feel free to replace this script with a manual call to TobiiXR.Start.
/// 
/// Ideally TobiiXR.Start should be called after any required VR SDK has been
/// initialized but before any game object that use TobiiXR is called.
/// </summary>
[DefaultExecutionOrder(-10)]
public class TobiiXR_Initializer : MonoBehaviour
{
    public TobiiXR_Settings Settings;

    public bool isBlinking = false;

    // public TobiiXR_EyeTrackingData eyeTrackingData;

    public GameObject lightObject;

    public float speed = 2.0f;

    public GameObject raycastHitObject;

    public Vector3 targetPos;

    private void Awake()
    {
        TobiiXR.Start(Settings);
        Debug.Log(LayerMask.NameToLayer("Ground"));


        //layerMask = ~layerMask;
    }

    private void Update()
    {
        
        Blinking();
        EyeRayCast();
        
    }

    void Blinking()
    {
        // Get eye tracking data in local space
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);

        // The EyeBlinking bool is true when the eye is closed
        var isLeftEyeBlinking = eyeTrackingData.IsLeftEyeBlinking;
        var isRightEyeBlinking = eyeTrackingData.IsRightEyeBlinking;
        if (isLeftEyeBlinking && isRightEyeBlinking) isBlinking = true;
        else isBlinking = false;
        
       
        // Using gaze direction in local space makes it easier to apply a local rotation
        // to your virtual eye balls.
        var eyesDirection = eyeTrackingData.GazeRay.Direction;
    }

    void EyeRayCast()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

        var eyeOrigin = eyeTrackingData.GazeRay.Origin;
        var eyeDirection = eyeTrackingData.GazeRay.Direction;

        RaycastHit hit;

        if (Physics.Raycast(eyeOrigin, eyeDirection, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Selectable")))
        {
            Debug.DrawRay(eyeOrigin, eyeDirection * 1000, Color.red);
            raycastHitObject = hit.transform.gameObject;

        }

        if (Physics.Raycast(eyeOrigin, eyeDirection, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Ground")))
        {
            Debug.DrawRay(eyeOrigin, eyeDirection*1000, Color.red);
            
            lightObject.transform.position = hit.point;
            targetPos = hit.point;
            // Debug.Log(hit.point);
            Vector3.MoveTowards(lightObject.transform.position, hit.point, speed * Time.deltaTime);
            lightObject.transform.position = new Vector3(lightObject.transform.position.x, -1.65f, lightObject.transform.position.z);
        }


    }
}