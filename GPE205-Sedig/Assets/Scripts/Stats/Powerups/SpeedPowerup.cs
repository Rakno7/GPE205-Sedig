using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpeedPowerup : Powerup
{
   public float SpeedToAdd;

    public override void Apply(PowerupManager target)
    {
        Pawn Speed = target.GetComponent<Pawn>();
        
        if(Speed !=null)
        {
          if(Speed.GetComponent<HumanPawn>())
          {
            Speed.runSpeed += SpeedToAdd;
          }
          else
          {
            Speed.moveSpeed += SpeedToAdd;
          }
          
          Debug.Log("Applied Speed");
        }
    }
    public override void Remove(PowerupManager target)
    {
        Pawn Speed = target.GetComponent<Pawn>();
        if(Speed.GetComponent<HumanPawn>())
          {
            Speed.runSpeed -= SpeedToAdd;
          }
          else
          {
            Speed.moveSpeed -= SpeedToAdd;
          }
        Debug.Log("Removed Speed");
    }
}
