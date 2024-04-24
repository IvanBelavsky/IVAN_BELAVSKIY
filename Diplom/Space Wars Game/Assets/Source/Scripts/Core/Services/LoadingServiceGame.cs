using UnityEngine;

public class LoadingServiceGame : MonoBehaviour
{
   [field: SerializeField] public bool IsLoad { get; private set; }

   public void Loaouding(bool isLoad)
   {
      IsLoad = isLoad;
   }
}