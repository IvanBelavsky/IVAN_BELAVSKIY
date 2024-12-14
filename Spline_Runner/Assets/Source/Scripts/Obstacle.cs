using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out SplineFollower follower))
      {
         if (other.TryGetComponent(out SplineFollower splineFollower))
         {
            Debug.Log("Obstacle");
         }
      }
   }
}