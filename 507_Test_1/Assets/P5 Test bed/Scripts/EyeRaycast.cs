using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;
using UnityEngine.VFX;

public class EyeRaycast : MonoBehaviour
{
    public GameObject lightObject;

    public float eyeSpeed = 2.0f;

    public GameObject raycastHitObject;

    public Vector3 targetPos;
    public RaycastHit raycastHit;

    public bool hasHit = false;

    public TobiiXR_EyeTrackingData eyeTrackingData;

    public Vector3 eyeOrigin;
    public Vector3 eyeDirection;

    [SerializeField] private float lightHeight = 0.05f;

    [SerializeField] private float lightMoveConstant = 0.2f;

    [SerializeField] private Transform jumpTransform;
    [SerializeField] private GameObject eyeSignifierPrefab;
    private GameObject eyeSignifier;

    private GameObject lastHitObject;

    private void Start()
    {
        eyeSignifier = Instantiate(eyeSignifierPrefab, lightObject.transform) as GameObject;
        eyeSignifier.SetActive(false);
    }

    private void Update()
    {
        switch (Testing.Instance.eyeTracking)
        {
            case Testing.EyeTracking.Quest:
            {
                eyeOrigin = Camera.main.transform.position;
                eyeDirection = Camera.main.transform.forward;
                break;
            }

            case Testing.EyeTracking.HTC:
            {
                eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
                if (eyeTrackingData.GazeRay.IsValid)
                {
                    eyeOrigin = Camera.main.transform.position;
                    eyeDirection = Vector3.Normalize(eyeTrackingData.GazeRay.Direction);
                }
                break;
            }
        }
        GazeCast(eyeOrigin, eyeDirection);
    }


    public void GazeCast(Vector3 startPoint, Vector3 direction)
    {
        RaycastHit hit;

        int layerMask = 1 << 0; // this would cast rays only against colliders in layer 0 --> the default one
        layerMask = ~layerMask; // invert the layer mask
        if (Physics.Raycast(startPoint, direction, out hit, Mathf.Infinity, layerMask))
        {
            print(hit.collider.gameObject.name);
            Debug.DrawRay(startPoint, direction * hit.distance, Color.cyan); 
            // targetPos = hit.point;

            if (hit.collider.gameObject.layer == 9) // if selectable              
            {
                /*if (raycastHitObject)
                {
                    lastHitObject = raycastHitObject;
                    if (raycastHitObject.GetComponent<Grabbable>().isSelected) return;
                }

                raycastHitObject = hit.transform.gameObject;
                if (raycastHitObject == lastHitObject || raycastHitObject.GetComponent<Grabbable>().isSelected)
                {
                    raycastHitObject.GetComponent<Grabbable>().Focused();
                    return;
                }

                // if it's not the same as the previous hit object, then set that one to default state
                if (lastHitObject) lastHitObject.GetComponent<Grabbable>().Default();

                // set the new one as focused
                raycastHitObject.GetComponent<Grabbable>().Focused();*/
                eyeSignifier.GetComponent<VisualEffect>().enabled = false;
                hasHit = false;
                lightObject.SetActive(false);
            }
            else
            {
                // if ground, turn on target pos and so on...
                if (hit.collider.gameObject.layer == 8) // if ground                
                {
                    targetPos = hit.point;
                    raycastHit = hit;
                    eyeSignifier.GetComponent<VisualEffect>().enabled = true;
                    lightObject.SetActive(true);
                    hasHit = true;
                    MoveLight(hit.point);
                }
                else
                {
                    eyeSignifier.GetComponent<VisualEffect>().enabled = false;
                    hasHit = false;
                    lightObject.SetActive(false);
                }

                if (raycastHitObject && !raycastHitObject.GetComponent<Grabbable>().isSelected)
                {
                    raycastHitObject.GetComponent<Grabbable>().Default();
                }
                eyeSignifier.GetComponent<VisualEffect>().enabled = false;
            }
        }
    }

    void MoveLight(Vector3 hitPoint)
    {
        //lightHeight += Input.GetAxis("Vertical");
        jumpTransform.position = hitPoint + new Vector3(0, lightHeight, 0);
        var lightToTargetDistance = Vector3.Distance(lightObject.transform.position, jumpTransform.position);
        var moveStep = lightToTargetDistance / lightMoveConstant;
        lightObject.transform.position =
            Vector3.MoveTowards(lightObject.transform.position, jumpTransform.position, moveStep);
    }
}