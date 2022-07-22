using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : Health
{
    public float currentHealth;
    public float wallCollisionDamage = 5;
    private Rigidbody rb;
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    public override void ReduceHealth(float Amount)
    {
        currentHealth -= Amount;
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    public override void RestoreHealth(float Amount)
    {
        currentHealth += Amount;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Projectile")
        {
            ReduceHealth(50);
        }
       
        else if(rb.velocity.magnitude > 2) //To Do: figure out why wall collisions only work if the collision is on angle.
        {
           ReduceHealth(wallCollisionDamage);
        }
    }
}
