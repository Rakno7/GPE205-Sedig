using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn 
{
    public GameObject Driver;
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

    public override void EnterVehicle()
    {
        //if(controller.pawn != gameObject.GetComponent<TankPawn>())
        //{
        //    return;
        //}
         //for vehicles this command will exit the vehicle and reactivate the human.
         
           
           //TODO:for some reason the transform rotation of the human driver gets messed up upon exit.
           Vector3 ExitLocation = new Vector3(transform.position.x - 3,transform.position.y,transform.position.z);
           
           Driver.transform.position = ExitLocation;

           //Driver.transform.rotation = gameObject.GetComponentInChildren<Attacker>().gameObject.transform.rotation;
           Driver.SetActive(true);

           controller.pawn = Driver.GetComponent<HumanPawn>();
           
           controller.GetComponent<PlayerController>().orientation = Driver.GetComponent<Pawn>().Orientation.transform;
           
           controller.GetComponent<PlayerController>().isControllingTank = false;
           controller.GetComponent<PlayerController>().isControllingHuman = true;
           controller.GetComponent<PlayerController>().SetCameraSettings();
           Driver = null;
           
           
       
    }

}
