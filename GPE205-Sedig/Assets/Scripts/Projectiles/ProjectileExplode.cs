using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : MonoBehaviour
{
public GameObject ExplosionParticles;
public GameObject ExplosionSmokeParticles;
public float explosiveForce;
public float explosionRadius;

public float ProjectileDamage;   

    private void OnCollisionEnter(Collision other)
    {
        Vector3 Pos = transform.position;
        Quaternion Rotation = transform.rotation;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
       
        foreach(Collider hitObjects in colliders)
        {
            Rigidbody rb = hitObjects.GetComponent<Rigidbody>();
            if(rb != null && !hitObjects.gameObject.GetComponent<HumanHealth>() && !hitObjects.gameObject.GetComponent<TankHealth>())
            {
            rb.AddExplosionForce(explosiveForce,transform.position,explosionRadius);
            }
        }
        
        Instantiate(ExplosionParticles, Pos, Rotation);
        Instantiate(ExplosionSmokeParticles, Pos, Rotation);
    }
}
