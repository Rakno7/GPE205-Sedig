using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPawn : Pawn
{
    public bool canEnterVehicle = false;
    public GameObject VehicleToEnter;
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
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }

        mover.Move(transform.forward,moveSpeed);
       // Debug.Log("Move Forward");
    }

    public override void MoveBackwards()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }

        mover.Move(transform.forward, -moveSpeed);
        //Debug.Log("Move Backward");
    }
    public override void MoveLeft()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }
        mover.Move(transform.right, -moveSpeed);
    }
    public override void MoveRight()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
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
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    public override void DoAttack()
    {
         if(attacker == null)
        {
            Debug.LogWarning("Warning: No Attacker in HumanPawn()!");
            return;
        }
        
        attacker.Attack(transform.forward, AttackSpeed);
    }

    public override void EnterVehicle()
    {
        if(!canEnterVehicle)
        {
            return;
        }
        controller.pawn = VehicleToEnter.GetComponentInParent<Pawn>();
        controller.GetComponent<PlayerController>().orientation = VehicleToEnter.GetComponentInParent<Pawn>().Orientation.transform;
        VehicleToEnter.GetComponentInParent<Pawn>().controller = controller;
        //set the driver so we can reenable the human player later;
        VehicleToEnter.GetComponentInParent<TankPawn>().Driver = gameObject;
        controller.GetComponent<PlayerController>().SetCameraSettings();
        controller.GetComponent<PlayerController>().isControllingHuman = false;
        controller.GetComponent<PlayerController>().isControllingTank = true;
        gameObject.SetActive(false);
    }
}
