using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

class Testing : MonoBehaviour
 {
     private static Testing _instance;
     public static Testing Instance => _instance;

     public EyeRaycast EyeRaycast;
     public Telekinesis Telekinesis;
     public BlinkTransform BlinkTransform;
     public Hand rightHand;
     [SerializeField] private GameObject wayFindingAudio;

     private void Awake()
     {
         if (_instance != null && _instance != this) {
             Destroy(this.gameObject);
         }
         else {
             _instance = this;
         }
     }

     private void Start()
     {
         EyeRaycast = GetComponent<EyeRaycast>();
         Telekinesis = GetComponent<Telekinesis>();
         BlinkTransform = GetComponent<BlinkTransform>();
     }

     public enum EyeTracking
    {
        Quest,
        HTC, 
        Controllers
    }
     public EyeTracking eyeTracking;

     /// <summary>
     /// Each of the trigger boxes will allow for the use of certain telekinesis interactions.
     /// 1 - Eye
     /// 2 - Eye + Gesture
     /// 3 - Eye  Gesture + Touchpad
     /// </summary>
     public enum TutorialInteractionMethods
     {
         EyeOnly,
         Gestures,
         TouchPad
     }

     public TutorialInteractionMethods tutorialInteractionMethods;

     public void MoveWayFindingAudioSource(Vector3 newPosition)
     {
         wayFindingAudio.transform.position = newPosition;
         if (wayFindingAudio.GetComponent<AudioSource>())
         {
             var audio = wayFindingAudio.GetComponent<AudioSource>();
             audio.Play();
         }
     }
 }