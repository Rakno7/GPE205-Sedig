using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttacker : Attacker
{
    public Transform Firepoint;
    public Rigidbody ParentRb;
    public GameObject CannonShot;
    public GameObject ShotParticles;
    public bool isShotDelay = false;
    public float AttackCooldown = 1;
    public override void Start()
    {
        
    }
    public override void Attack(Vector3 direction, float speed)
    {
         //dont fire while shot delay is act active
        if(!isShotDelay)
        {
            //create a new movement vector forward * speed
        Vector3 movementVector = transform.forward * speed;
           //create a position vector to fire from
        Vector3 pos = Firepoint.transform.position;
        //create rotation vector equal to transform rotation
        Quaternion rotation = transform.rotation;
        //create shot particles
        GameObject Particles = Instantiate(ShotParticles,pos,rotation);
        //spawn the cannon at the position and rotation of the vectors 
        GameObject Cannonball = Instantiate(CannonShot,pos,rotation);
        //add force upon the movement vector
        Cannonball.GetComponent<Rigidbody>().AddForce(movementVector, ForceMode.Impulse);
        //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-transform.forward,Firepoint.transform.position,ForceMode.Impulse);
        ParentRb.AddForce(-transform.forward * speed / 10 ,ForceMode.Impulse);

        //Debug.Log("attacked");
        
        //invoke the delay reset
        Invoke("Cooldown", AttackCooldown);
        //track that the shot was taken
        //if(GetComponentInParent<PlayerController>() || GetComponent<PlayerController>())
        //{
        isTookShot = true;
        //}
        //stop firing until the delay is reset
        isShotDelay = true;
        }
    }
    public override void Cooldown()
    {
        //reset the delay and shot detection
        isTookShot = false;
        isShotDelay = false;
    }
}
