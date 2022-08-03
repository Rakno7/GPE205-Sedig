using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : Health
{
    public float wallCollisionDamage = 5;
    private Rigidbody rb;
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isCanTakeDamage = true;
    }
    public override void ReduceHealth(float Amount, Pawn playerPawn)
    {
        if(!isCanTakeDamage) return;
        
        
        playerPawn = GetComponent<Pawn>();
        
        currentHealth -= Amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public override void RestoreHealth(float Amount, Pawn playerPawn)
    {
        
        playerPawn = GetComponent<Pawn>();
        
        currentHealth += Amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Projectile")
        {
            float DamageTaken = other.transform.gameObject.GetComponent<ProjectileExplode>().ProjectileDamage;
            ReduceHealth(DamageTaken,GetComponent<Pawn>());
        }
       
        //else if(rb.velocity.magnitude > 2) //To Do: figure out why wall collision damage only works if the collision is on angle.
        //{
        //   ReduceHealth(wallCollisionDamage,GetComponent<Pawn>());
        //}
    }

    public override void Die()
    {
            TankPawn thisPawn = gameObject.GetComponent<TankPawn>();
            GameManager.instance.Vehicles.Remove(thisPawn);
            GameManager.instance.Destroyedtanks.Add(thisPawn);
            if(thisPawn.Driver != null)
            {
                Destroy(thisPawn.Driver.gameObject);
                GameManager.instance.humans.Remove(thisPawn.Driver.GetComponent<HumanPawn>());
                GameManager.instance.DeadAIPlayers.Add(thisPawn.Driver.GetComponent<HumanPawn>());
            }
            if(gameObject.GetComponentInParent<TankPawn>().controller !=null)
            {
            Destroy(gameObject.GetComponentInParent<TankPawn>().controller.gameObject);
            }
            Destroy(gameObject);
    }
}
