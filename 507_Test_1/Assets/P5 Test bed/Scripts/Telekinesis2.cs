using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples;
using UnityEngine;

public class Telekinesis2 : MonoBehaviour
{
    public Transform controllerTransform;


    void Update()
    {
        controllerTransform = ControllerManager.Instance.transform;
        print(controllerTransform.position.y);
    }
}
