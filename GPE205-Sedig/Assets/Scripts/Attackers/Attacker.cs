using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    public bool isTookShot;
    public abstract void Start();
    
    public abstract void Attack(Vector3 direction, float speed);
    public abstract void Cooldown();
}
