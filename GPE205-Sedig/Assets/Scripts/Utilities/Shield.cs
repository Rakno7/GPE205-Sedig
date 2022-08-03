using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
   {
      if(other.tag == "Projectile")
      {
         Debug.Log("destroyed Projectile");
        Destroy(other.gameObject);
      }
   }
}
