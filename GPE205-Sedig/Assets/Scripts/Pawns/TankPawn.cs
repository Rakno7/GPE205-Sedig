using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn 
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

    public override void RotateClockwise()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn!");
            return;
        }

        mover.Rotate(turnSpeed);
        //Debug.Log("Rotate Clockwise");
    }

    public override void RotateCounterClockwise()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }

        mover.Rotate(-turnSpeed);
       // Debug.Log("Rotate Counter Clockwise");
    }
    public override void MouseRotate()
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
    public override void MoveLeft()
    {
        return;
    }
    public override void MoveRight()
    {
        return;
    }

}
