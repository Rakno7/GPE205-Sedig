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
        Pawn pawn = target.GetComponent<Pawn>();
        Transform targetTransform = target.GetComponent<Transform>();
        Health targetHealth = target.GetComponent<Health>();
        if(pawn !=null)
        {
          targetHealth.isCanTakeDamage = false;  
          ShieldToRemove = GameObject.Instantiate(ShieldPrefabToAdd,targetTransform.position,Quaternion.identity);
          ShieldToRemove.transform.parent = targetTransform;
        }
        
    }
    public override void Remove(PowerupManager target)
    { 
        Health targetHealth = target.GetComponent<Health>();
        targetHealth.isCanTakeDamage = true;
        GameObject.Destroy(ShieldToRemove);  
    }
}
