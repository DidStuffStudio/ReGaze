using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class Testing : MonoBehaviour
 {
     private static Testing _instance;

     public static Testing Instance
     {
         get => _instance;
     }

     private void Awake()
     {
         if (_instance != null && _instance != this) {
             Destroy(this.gameObject);
         }
         else {
             _instance = this;
         }
     }

     public enum EyeTracking
    {
        Quest,
        Htc
    }

    public EyeTracking eyeTracking;
}