using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;

public class EyeRaycast : MonoBehaviour
{
    public GameObject lightObject;

    public float eyeSpeed = 2.0f;

    public GameObject raycastHitObject;

    public Vector3 targetPos;

    private Testing.EyeTracking testSwitch;

    public bool hasHit = false;


    private void Update()
    {
        switch (Testing.Instance.eyeTracking)
        {
            case Testing.EyeTracking.Quest:

            {
                var headOrigin = Camera.main.transform.position;
                var headDir = Camera.main.transform.forward;
                GazeCast(headOrigin, headDir);
                break;
            }

            case Testing.EyeTracking.Htc:

            {
                var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
                var eyeOrigin = eyeTrackingData.GazeRay.Origin;
                var eyeDirection = eyeTrackingData.GazeRay.Direction;
                GazeCast(eyeOrigin, eyeDirection);
                break;
            }
        }
    }


    public void GazeCast(Vector3 startPoint, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(startPoint, direction, out hit, Mathf.Infinity, LayerMask.GetMask("Selectable")))
        {
            Debug.DrawRay(startPoint, direction * 1000, Color.red);
            raycastHitObject = hit.transform.gameObject;
        }

        if (Physics.Raycast(startPoint, direction, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(startPoint, direction * 1000, Color.red);
            targetPos = hit.point;
            lightObject.transform.position = hit.point += new Vector3(0, 0.1f, 0);
            lightObject.SetActive(true);
            hasHit = true;
        }
        else
        {
            hasHit = false;
            lightObject.SetActive(false);
        }
    }
}