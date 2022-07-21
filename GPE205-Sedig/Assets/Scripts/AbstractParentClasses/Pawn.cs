using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract classes act as an interface and cannot be a component on a gameObject.
// pawn child classes will inherit from this abstract class.
//the abstract class acts as a template for subclasses to inherit from, 
//and is not able to be instanced itself. 
public abstract class Pawn : MonoBehaviour
{  
    public Mover mover;
    public Attacker attacker;
   public float moveSpeed;
   public float turnSpeed;
   public float AttackSpeed;
  
   //A virtual method can be overridden by subclasses. 
   //Virtual methods can be overridden when subclasses use methods of the same name declared as overides. 
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponent<Attacker>();
    }

    
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackwards();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void DoAttack();


}
