using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out Player player))
      {
         player.Finished();
      }
   }
}