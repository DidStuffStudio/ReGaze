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

    [SerializeField] private float topSpeed = 12;
    [SerializeField] private float acceleration = 7;
    [SerializeField] private float distanceMultiplier = 0.1f;


    private void Start()
    {
        eyeRaycast = GetComponent<EyeRaycast>();
        height = transform.position.y;
    }
    public void Update()
    {
   

        if (ControllerManager.Instance.GetButtonPress(TriggerButton) && eyeRaycast.hasHit && !triggered)
        {
            timer = 0;
            triggered = true;
            target = eyeRaycast.targetPos;
            target += new Vector3(0,height,0);
            prevTime = Time.time;
            distance = Vector3.Distance(new Vector3(target.x, 0.0f, target.z),
                new Vector3(transform.position.x, 0.0f, transform.position.z));
            originalPos = transform.position;

        }
        if (triggered)
        {
            CalculateSpeed();
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
        if (Vector3.Distance(new Vector3(target.x, 0.0f, target.z), new Vector3(transform.position.x,0.0f,transform.position.z)) < 0.1f)
        {
            triggered = false;
            speed = 0;
        }
    }

    private void CalculateSpeed()
    {
        
        if(Vector3.Distance(originalPos, transform.position) < (distance / 4))
        {
            speed = timer * acceleration;
            timer += Time.deltaTime;
        }
        else if (Vector3.Distance(originalPos, transform.position) >= (distance / 4) * 3)
        {
            timer -= Time.deltaTime;
            
            speed = timer * acceleration;
            if (speed <= 0)
            {
                speed = 0;
            }
        }
    }
}
