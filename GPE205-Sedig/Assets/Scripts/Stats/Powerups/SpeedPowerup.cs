using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpeedPowerup : Powerup
{
   public float SpeedToAdd;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if(targetSpeed !=null)
        {
          targetSpeed.moveSpeed += SpeedToAdd;
          Debug.Log("Applied Speed");
        }
        
    }
    public override void Remove(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        targetSpeed.moveSpeed -= SpeedToAdd;
         Debug.Log("Removed Speed");
    }
}
