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
                GazeCast(eyeOrigin, eyeDirection);
                break;
            }

            case Testing.EyeTracking.HTC:

            {
                eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
                eyeOrigin = Camera.main.transform.position;
                eyeDirection = Vector3.Normalize(eyeTrackingData.GazeRay.Direction);
                GazeCast(eyeOrigin, eyeDirection);
                break;
            }
        }
    }


    public void GazeCast(Vector3 startPoint, Vector3 direction)
    {
        RaycastHit hitGround;
        RaycastHit hitSelectable;
        Debug.DrawRay(startPoint, direction, Color.cyan);

        if (Physics.Raycast(startPoint, direction, out hitSelectable, Mathf.Infinity, LayerMask.GetMask("Selectable")))
        {
            targetPos = hitSelectable.point;
            if (!raycastHitObject) raycastHitObject = hitSelectable.transform.gameObject;
            var g = raycastHitObject.GetComponent<Grabbable>();
            Debug.DrawRay(startPoint, hitSelectable.point - startPoint, Color.red);
            if (hitSelectable.transform.gameObject != raycastHitObject && !g.isSelected)
            {
                raycastHitObject.GetComponent<Grabbable>().Default();
                raycastHitObject = hitSelectable.transform.gameObject;
                raycastHitObject.GetComponent<Grabbable>().Focused();
            }
            return;
        }
        // if the user is not looking at a selectable object but another one was hovered/selected
        /*if (raycastHitObject)
        {
            var g = raycastHitObject.GetComponent<Grabbable>();
            if (!g.isSelected)
            {
                g.Default();
                raycastHitObject = null;
            }
        }*/
        
        // if there's another selectable object behind the already selected one, it apparently sets it to focused or selected (one of both)

        if (Physics.Raycast(startPoint, direction, out hitGround, Mathf.Infinity, LayerMask.GetMask("Ground", "Walls")))
        {
            if (hitGround.collider.gameObject.layer == 10) return; // Wall layer
            targetPos = hitGround.point;
            raycastHit = hitGround;
            if(!eyeSignifier.activeSelf) eyeSignifier.SetActive(true);
            lightObject.SetActive(true);
            hasHit = true;
            MoveLight(hitGround.point);
        }
        else
        {
            if(eyeSignifier.activeSelf) eyeSignifier.SetActive(false);
            hasHit = false;
            lightObject.SetActive(false);
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