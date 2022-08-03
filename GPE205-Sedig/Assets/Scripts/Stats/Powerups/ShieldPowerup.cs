using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShieldPowerup : Powerup
{
   public GameObject ShieldPrefabToAdd;
   private GameObject ShieldToRemove;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        Transform targetTransform = target.GetComponent<Transform>();
        TankHealth targetHealth = target.GetComponent<TankHealth>();
        if(targetPawn !=null)
        {
          targetHealth.isCanTakeDamage = false;  
          ShieldToRemove = GameObject.Instantiate(ShieldPrefabToAdd,targetTransform.position,Quaternion.identity);
          ShieldToRemove.transform.parent = targetTransform;
        }
        
    }
    public override void Remove(PowerupManager target)
    { 
        TankHealth targetHealth = target.GetComponent<TankHealth>();
        targetHealth.isCanTakeDamage = true;
        GameObject.Destroy(ShieldToRemove);  
    }
}
