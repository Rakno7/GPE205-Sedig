using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
   public float maxHealth = 100;
   public virtual void Start()
   {
    
   }
   public abstract void ReduceHealth(float Amount);
   public abstract void RestoreHealth(float Amount);
   
}
