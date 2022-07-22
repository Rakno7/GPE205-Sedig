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
        if(!isShotDelay)
        {
        Vector3 movementVector = transform.forward * speed;
        
        Vector3 pos = Firepoint.transform.position;
        Quaternion rotation = transform.rotation;
        GameObject Particles = Instantiate(ShotParticles,pos,rotation);
        GameObject Cannonball = Instantiate(CannonShot,pos,rotation);
        Cannonball.GetComponent<Rigidbody>().AddForce(movementVector, ForceMode.Impulse);
        //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-transform.forward,Firepoint.transform.position,ForceMode.Impulse);
        ParentRb.AddForce(-transform.forward * speed / 10 ,ForceMode.Impulse);

        //Debug.Log("attacked");

        Invoke("Cooldown", AttackCooldown);
        isShotDelay = true;
        }
    }
    public override void Cooldown()
    {
        isShotDelay = false;
    }
}
