using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : Health
{
    private Rigidbody rb;
   
    private void Update()
    {
        //for testing!//for testing!//for testing!
        if(Input.GetKeyDown(KeyCode.X))
        {
            Die();
        }
    }
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
    }

    public override void Die()
    {
        if(GetComponent<HumanPawn>().controller !=null)
        {
          if(GetComponent<HumanPawn>().controller.GetComponent<AiController>())
          {
            GameManager.instance.ResetAiSpawns();

                GameManager.instance.ResetPlayerSpawns();
                HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
                GameManager.instance.humans.Remove(thisPawn);
                GameManager.instance.aiPlayers.Remove(GetComponent<HumanPawn>().controller.GetComponent<AiController>());
                Destroy(thisPawn.controller.gameObject);
                Destroy(thisPawn.anim);
                thisPawn.SetRagdollColliderState(true);
                thisPawn.SetRagdollRigidbodyState(false);
                
                Destroy(thisPawn);
                Destroy(this);
          }
          if(GetComponent<HumanPawn>().controller.GetComponent<PlayerController>())
          {
                GameManager.instance.ResetPlayerSpawns();

                HumanPawn thisPawn = gameObject.GetComponent<HumanPawn>();
                GameManager.instance.humans.Remove(thisPawn);
                GameManager.instance.players.Remove(GetComponent<HumanPawn>().controller.GetComponent<PlayerController>());
                Destroy(thisPawn.controller.gameObject);
                Destroy(thisPawn.anim);
                thisPawn.SetRagdollColliderState(true);
                thisPawn.SetRagdollRigidbodyState(false);
                
                Destroy(thisPawn);
                Destroy(this);
          }
        }   
    }
}
