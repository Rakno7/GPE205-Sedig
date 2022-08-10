using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttacker : Attacker
{
    public Transform Firepoint;
    public GameObject Bullet;
    public GameObject ShotParticles;
    public bool isShotDelay = false;
    public float AttackCooldown = 0.3f;
    public override void Start()
    {
        
    }
    public override void Attack(Vector3 direction, float speed)
    {
       
         //dont fire while shot delay is act active
        if(!isShotDelay)
        {
            thisController = pawn.GetComponent<Pawn>().controller; 
        //create a new movement vector forward * speed
        Vector3 movementVector = transform.forward * speed;
           //create a position vector to fire from
        Vector3 pos = Firepoint.transform.position;
        //create rotation vector equal to transform rotation
        Quaternion rotation = transform.rotation;
        //create shot particles
        GameObject Particles = Instantiate(ShotParticles,pos,rotation);
        //spawn the bullet at the position and rotation of the vectors 
        GameObject bullet = Instantiate(Bullet,pos,rotation);
        ProjectileExplode projectile = bullet.GetComponent<ProjectileExplode>();
        projectile.whoTookThisShot = thisController;
        //add force upon the movement vector
        bullet.GetComponent<Rigidbody>().AddForce(movementVector, ForceMode.Impulse);
        
    

        
        
        //invoke the delay reset
        Invoke("Cooldown", AttackCooldown);
        //track that the shot was taken
        isTookShot = true;
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
