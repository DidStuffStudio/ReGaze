using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR.Examples;

public class SmoothJump : MonoBehaviour
{
    private EyeRaycast focusPoint;
    private const ControllerButton TriggerButton = ControllerButton.Trigger;
    [SerializeField]
    private TobiiXR_Initializer tobiiXR_Initializer;
    public float speed = 1.0f, acceleration = 1.0f;
    private Vector3 target = Vector3.zero;
    private bool triggered = false;
    float height = 0.0f;


    private void Start()
    {
        focusPoint = GetComponent<EyeRaycast>();
        height = transform.position.y;
    }
    public void Update()
    {

        if (ControllerManager.Instance.GetButtonPress(TriggerButton) && focusPoint.EyeRayCast() != null && !triggered)
        {
            triggered = true;
            target = focusPoint.EyeRayCast();
            print("1");
        }
        if (triggered)
        {
            print("2");
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
        if (Vector3.Distance(new Vector3(target.x, 0.0f, target.z), new Vector3(transform.position.x,0.0f,transform.position.z)) < 0.5f)
        {
            print("3");
            triggered = false;
        }

        print(Vector3.Distance(target, transform.position));


        //if (ControllerManager.Instance.GetButtonPressDown(TriggerButton))
        //    print("Trigger Down");


        //if (ControllerManager.Instance.GetButtonPressUp(TriggerButton))
        //    print("Trigger Up");

    }

    public void Jump()
    {
       
    }





}
