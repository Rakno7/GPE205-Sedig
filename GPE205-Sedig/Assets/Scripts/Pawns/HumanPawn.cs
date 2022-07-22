using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPawn : Pawn
{
       public override void Start()
    {
        //base refers to the base class, this will run the start function on the base class.
        base.Start();
    }

    public override void Update()
    {
        base.Start();
    }


    //we will override the base class method and define how we want tanks to move specifically.
    public override void MoveForward()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }

        mover.Move(transform.forward,moveSpeed);
       // Debug.Log("Move Forward");
    }

    public override void MoveBackwards()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }

        mover.Move(transform.forward, -moveSpeed);
        //Debug.Log("Move Backward");
    }
    public override void MoveLeft()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        mover.Move(transform.right, -moveSpeed);
    }
    public override void MoveRight()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        mover.Move(transform.right, moveSpeed);
    }

    public override void RotateClockwise()
    {
        return;
    }

    public override void RotateCounterClockwise()
    {
         return;
    }
    public override void DoAttack()
    {
         if(attacker == null)
        {
            Debug.LogWarning("Warning: No Attacker in TankPawn()!");
            return;
        }
        
        attacker.Attack(transform.forward, AttackSpeed);
    }
}
