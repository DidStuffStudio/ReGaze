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

    private void Update()
    {

       EyeRayCast();

    }


    public Vector3 EyeRayCast()
    {
        Vector3 hitPoint;

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
            Debug.DrawRay(eyeOrigin, eyeDirection * 1000, Color.red);

            lightObject.transform.position = hit.point;
            targetPos = hit.point;
            
            

            // Debug.Log(hit.point);
            Vector3.MoveTowards(lightObject.transform.position, hit.point, eyeSpeed * Time.deltaTime);
            lightObject.transform.position = new Vector3(lightObject.transform.position.x, -1.65f, lightObject.transform.position.z);
        }

        hitPoint = hit.point;

        return hitPoint;
    }


}
