using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
   public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        Health health = target.GetComponent<Health>();
        if(health !=null)
        {
          health.RestoreHealth(healthToAdd, target.GetComponent<Pawn>());
          Debug.Log("Applied Health");
        }
      
        
    }
    public override void Remove(PowerupManager target)
    {
        return;
       // Debug.Log("Removed Health");
    }
}
