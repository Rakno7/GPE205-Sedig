using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : MonoBehaviour
{
public GameObject ExplosionParticles;
public GameObject ExplosionSmokeParticles;
   
    private void OnCollisionEnter(Collision other)
    {
        Vector3 Pos = transform.position;
        Quaternion Rotation = transform.rotation;
        Instantiate(ExplosionParticles, Pos, Rotation);
        Instantiate(ExplosionSmokeParticles, Pos, Rotation);
    }
}
