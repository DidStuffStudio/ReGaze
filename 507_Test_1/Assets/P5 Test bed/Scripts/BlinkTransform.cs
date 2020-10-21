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
    [SerializeField] private float acceleration;
    [SerializeField] private bool accelerationCalculated;
    public float height = 1.5f;

    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
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
            if (!accelerationCalculated)
            {
                CalculateSlopeAndMaxDistance();
                acceleration = (Mathf.Pow(topSpeed, 2) / 2 * maxDistance); // Acceleration
                accelerationCalculated = true;
                print("maxdist " + maxDistance);
            }
            
            timer += Time.fixedDeltaTime;
            step += acceleration * Mathf.Pow(timer, 2);


            /*if (CalculateDistance() < maxDistance/4 && step <= topSpeed)
            {
                step += acceleration * Mathf.Pow(timer, 2);
            }
            
            else if (CalculateDistance() >= 3 * maxDistance / 4 && step >= 0) //deceleration
            {

                step -= acceleration * Mathf.Pow(timer, 2);

            }*/
            /*else // Constant Speed
            {
                print("constant speed");
                step = topSpeed;
            }*/
            
            
            print(" step " + step);
            transform.position = Vector3.MoveTowards((transform.position), target, step);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }

        if (Vector3.Distance(transform.position, new Vector3(target.x, height, target.z)) < 0.001f)
        {
            triggered = false;
            slope = 0;
            step = 0;
            timer = 0;
            maxDistance = 0;
            accelerationCalculated = false;
        }
    }

    float CalculateSlopeAndMaxDistance()
    {
        var dis = Vector3.Distance(startingPosition, target);
        /*slope = (topSpeed - 0) / (dis - 0); // slope = y2-y1/x2-x1*/
        return dis;
    }

    private float CalculateDistance()
    {
        var dist = Vector3.Distance(transform.position, startingPosition);
        return dist;
    }

}
