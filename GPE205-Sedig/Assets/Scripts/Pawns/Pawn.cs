using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract classes act as an interface and cannot be a component on a gameObject.
// pawn child classes will inherit from this abstract class.
//the abstract class acts as a template for subclasses to inherit from, 
//and is not able to be instanced itself. 
public abstract class Pawn : MonoBehaviour
{  
   public Controller controller;
   public Mover mover;
   public Attacker attacker;
   public NoiseMaker noiseMaker;
   public float moveSpeed;
   public float turnSpeed;
   public float AttackSpeed;


   public GameObject Orientation;

   public Transform CameraSetting1;
   public Transform CameraSetting2;
   public Transform CameraSetting3;
  
   //A virtual method can be overridden by subclasses. 
   //Virtual methods can be overridden when subclasses use methods of the same name declared as overides. 
   //abstract methods MUST be overridden.
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        attacker = GetComponentInChildren<Attacker>();
        //Not all pawns will make noise so check this first. *This may not be nessisarry
        if(GetComponent<NoiseMaker>()!=null)
        {
            noiseMaker = GetComponent<NoiseMaker>();
        }
    }

    
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackwards();
    public abstract void MoveLeft();
    public abstract void MoveRight();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void Attack();
    public abstract void EnterVehicle();

    public abstract void MakeNoise(float Amount);


}
