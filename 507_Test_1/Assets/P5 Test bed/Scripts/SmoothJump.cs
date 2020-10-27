using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR.Examples;
using Tobii.XR.GazeModifier;

public class SmoothJump : MonoBehaviour
{
    private EyeRaycast eyeRaycast;
    private const ControllerButton TriggerButton = ControllerButton.Trigger;
    [SerializeField]
    private TobiiXR_Initializer tobiiXR_Initializer;

    public float speed = 1.0f;
    private Vector3 target = Vector3.zero;
    private bool triggered = false;
    float height = 0.0f;
    private float prevTime;
    private float distance = 0;
    private Vector3 originalPos = Vector3.zero;
    private float timer = 0;
    private bool justJumped;
    public bool triggerBoxMet = false;

    [SerializeField] private float topSpeed = 12;
    [SerializeField] private float acceleration = 7;
    [SerializeField] private float distanceMultiplier = 0.1f;
    [SerializeField] private GameObject triggerBox;


    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
        height = transform.GetChild(0).position.y;
    }
    public void Update()
    {
        
        print(ControllerManager.Instance.GetButtonPress(TriggerButton));
        //print(transform.position + "Current Location" + target + "Target Position");
        
        if (ControllerManager.Instance.GetButtonPress(TriggerButton) && eyeRaycast.hasHit && !triggered && !justJumped)
        {
            
            timer = 0;
            triggered = true;
            target = eyeRaycast.targetPos;
            target = target + transform.position - transform.GetChild(0).position; // Camera must be first child!!!!!!!!!!!!!!!!!!!!! 
            target += new Vector3(0,height,0);
            prevTime = Time.time;
            distance = Vector3.Distance(new Vector3(target.x, 0.0f, target.z),
                new Vector3(transform.position.x, 0.0f, transform.position.z));
            originalPos = transform.position;
            triggerBox.transform.position = target;

        }
        if (triggered)
        {
            CalculateSpeed();
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            justJumped = true;
        }

        var newTarget = target - (transform.position - transform.GetChild(0).position);
        
        if (triggerBoxMet)
        {
            StartCoroutine(WaitBro());
            triggered = false;
            print("triggered false");
            triggerBoxMet = false;
        }
        
    }

    private void CalculateSpeed()
    {

        var distanceToTarget = Vector3.Distance(originalPos, target);
        if(distanceToTarget < (distance / 4)) // acceleration
        {
            timer += Time.fixedDeltaTime;
            speed = timer * acceleration;
        }
        else if (distanceToTarget >= (distance / 4) * 3) // deceleration
        {
            timer -= Time.fixedDeltaTime;
            speed = Mathf.Abs(timer * acceleration);
            
        }
    }

    IEnumerator WaitBro()
    {
        print("started coroutine");
        
        justJumped = true;
        yield return new WaitForSeconds(2f);
        justJumped = false;
        print("finished coroutine");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            triggerBoxMet = true;
        }
    }
}
