using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBoulderCheck : MonoBehaviour
{
   public Transform boulder, yPass;
   public Collider jumpCollider;

   private void Start()
   {
      jumpCollider.gameObject.layer = 0;
   }

   private void Update()
   {
      if (boulder.position.y < yPass.position.y)
      {
         jumpCollider.gameObject.layer = 8;
      }
      
   }
}
