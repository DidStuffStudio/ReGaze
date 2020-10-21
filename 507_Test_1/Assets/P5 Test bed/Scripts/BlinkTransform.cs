using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR.Examples;
using Tobii.XR.GazeModifier;

public class BlinkTransform : MonoBehaviour
{
    private EyeRaycast eyeRaycast;
    private const ControllerButton TriggerButton = ControllerButton.Trigger;
    private Vector3 target = Vector3.zero;
    private bool triggered = false;
    float height = 0.0f;
    [SerializeField] private float topSpeed = 0.1f;
    private Vector3 startingPosition;
    private float slope;
    private Vector3 direction;

    private float stepsum;
    private float step;

    private float timer;

    public float multiplyer;
    [SerializeField] private float multiplier = 2;
    [SerializeField] private float maxDistance;

    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
        height = transform.GetChild(0).position.y;
    }

    public void Update()
    {
        if (ControllerManager.Instance.GetButtonPress(TriggerButton) && eyeRaycast.hasHit && !triggered)
        {
            triggered = true;
            
            target = eyeRaycast.targetPos;
            target = target + transform.position -
                     transform.GetChild(0).position; // Camera must be first child!!!!!!!!!!!!!!!!!!!!! 
            target += new Vector3(0, height, 0);
            startingPosition = transform.position;
            direction = eyeRaycast.eyeDirection;
            maxDistance = CalculateSlopeAndMaxDistance();
        }

        if (triggered)
        {

            /*ar step = 0.0f;
            step += CalculateSpeed();
            print(step);*/
            if (slope == 0)
            {
                CalculateSlopeAndMaxDistance();

            }


            // var step = Vector3.Distance(transform.position, target) * slope * Time.deltaTime; // decelerated thing
            // var step = Mathf.Abs(Vector3.Distance(transform.position, target) * slope);
            // var step = Mathf.Abs(Vector3.Distance(transform.position, target) / slope);
            // stepsum += step;
            timer += Time.fixedDeltaTime;
            // step += slope / CalculateSlope() / timer;
            
            var acceleration = (Mathf.Pow(topSpeed, 2) / 2 * maxDistance);
            print("acceleration = " + acceleration + ", timer = " + timer);
            // var acceleration = (CalculateSlopeAndMaxDistance()*2 / Mathf.Pow(multiplier * Time.fixedDeltaTime, 2));
            step += acceleration * Mathf.Pow(timer, 2);
            
            print(" step " + step);
            // transform.position = Vector3.MoveTowards(transform.position, target, step / 10); // decelerating
            transform.position = Vector3.MoveTowards((transform.position), target, step);
            
            
            
        }

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            triggered = false;
            slope = 0;
            step = 0;
            timer = 0;
            maxDistance = 0;
            //print("Hit Target");
            // print("stepsum: " + stepsum);
        }

        /*
        if (slope * Vector3.Distance(startingPosition, target) != 0)
        {

        

        }*/
    }

    float CalculateSlopeAndMaxDistance()
    {
        var dis = Vector3.Distance(startingPosition, target);
        slope = (topSpeed - 0) / (dis - 0); // slope = y2-y1/x2-x1
        // print(slope);
        return dis;
    }

}
