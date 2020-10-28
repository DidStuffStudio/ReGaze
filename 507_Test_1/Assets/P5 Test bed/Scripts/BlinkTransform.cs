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
    private bool triggered;
    [SerializeField] private float topSpeed = 0.1f;
    private Vector3 startingPosition;
    private Vector3 direction;

    private float stepsum;
    private float step;

    private float timer;
  
    [SerializeField] private float maxDistance;
    [SerializeField] private float acceleration;

    private bool justJumped;
    [SerializeField] private GameObject triggerBox;
    private bool triggerMet;
    private GameObject camera;
    private CapsuleCollider capsule;
    

    private void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        camera = transform.GetChild(0).gameObject;
        eyeRaycast = GetComponent<EyeRaycast>();
    }

    public void Update()
    {
        UpdateCapsuleCollider();
        
        if (ControllerManager.Instance.GetButtonPress(TriggerButton) && eyeRaycast.hasHit && !triggered) //Trigger new jump if conditions are met
        {
            target = eyeRaycast.targetPos;
            triggered = true;
            maxDistance = Vector3.Distance(transform.position, target);
            triggerBox.transform.position = new Vector3(target.x, target.y + triggerBox.transform.localScale.y/2, target.z);
            triggerBox.SetActive(true);
            acceleration = Mathf.Pow(topSpeed, 2) / 2 * maxDistance;
        }

        if (!triggered) return;
        
        timer += Time.fixedDeltaTime;
        step += acceleration * Mathf.Pow(timer, 2);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (triggerMet)
        {
            StartCoroutine(WaitBro());
        }
    }
    
    IEnumerator WaitBro()
    {
        yield return new WaitForSeconds(0.2f);
        step = 0;
        timer = 0;
        maxDistance = 0;
        triggerBox.SetActive(false);
        triggerMet = false;
        triggered = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 11)
        {
            triggerMet = true;
        }
    }

    private void UpdateCapsuleCollider()
    {
        var height = camera.transform.position.y;
        capsule.center = new Vector3(0, height / 2, 0);
        capsule.height = height;
    }
    
}
