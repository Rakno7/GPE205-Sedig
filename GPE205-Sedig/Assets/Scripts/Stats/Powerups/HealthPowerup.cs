using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
   public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        TankHealth targetHealth = target.GetComponent<TankHealth>();
        if(targetHealth !=null)
        {
          targetHealth.RestoreHealth(healthToAdd, target.GetComponent<Pawn>());
          Debug.Log("Applied Health");
        }
        
    }
    public override void Remove(PowerupManager target)
    {
        return;
       // Debug.Log("Removed Health");
    }
}
