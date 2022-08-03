using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Powerup
{
   public float duration;
   public bool isPermanate;
   public abstract void Apply(PowerupManager target);
   public abstract void Remove(PowerupManager target);

  
   private void Start()
   {
      
   }
}
