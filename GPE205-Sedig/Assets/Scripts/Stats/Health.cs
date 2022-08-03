using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
   public bool isCanTakeDamage;
   public float maxHealth;
   public float currentHealth;
   public virtual void Start()
   {
    
   }
   public abstract void ReduceHealth(float Amount, Pawn playerPawn);
   public abstract void RestoreHealth(float Amount, Pawn playerPawn);
   public abstract void Die();
   
}
