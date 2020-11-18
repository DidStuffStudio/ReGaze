using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBoulderCheck : MonoBehaviour
{
   public Transform boulder, yPass;
   public GameObject jumpCollider;

   private void Start()
   {
      jumpCollider.SetActive(false);
   }

   private void Update()
   {
      if (boulder.position.y < yPass.position.y)
      {
         jumpCollider.SetActive(true);
      }
      
   }
}
