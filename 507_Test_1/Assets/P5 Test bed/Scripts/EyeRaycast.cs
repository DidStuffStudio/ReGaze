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


    public bool hasHit = false;

    public TobiiXR_EyeTrackingData eyeTrackingData;

    public Vector3 eyeOrigin;
    public Vector3 eyeDirection;

    [SerializeField] private float lightHeight = 0.2f;


    private void Update()
    {
        switch (Testing.Instance.eyeTracking)
        {
            case Testing.EyeTracking.Quest:

            {
                eyeOrigin = Camera.main.transform.position;
                eyeDirection = Camera.main.transform.forward;
                GazeCast(eyeOrigin, eyeDirection);
                break;
            }

            case Testing.EyeTracking.HTC:

            {
                eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
                eyeOrigin = eyeTrackingData.GazeRay.Origin;
                eyeDirection = eyeTrackingData.GazeRay.Direction;
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
            //Debug.DrawRay(startPoint, hit.point, Color.red);
           if (hit.transform.gameObject != raycastHitObject)
           {
                raycastHitObject = hit.transform.gameObject;
                targetPos = hit.point;
                raycastHitObject.GetComponent<Grabbable>().focused = true;
           }
        }
        else
        {
            if (raycastHitObject != null)
            {
                raycastHitObject.GetComponent<Grabbable>().focused = false;
                raycastHitObject = null;
            }   
        }

        if (Physics.Raycast(startPoint, direction, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            //Debug.DrawRay(startPoint, direction * 1000, Color.red);
            targetPos = hit.point;
            lightObject.transform.position = hit.point += new Vector3(0, lightHeight, 0);
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