using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn 
{
    public GameObject Driver;
    public float CannonShotVolume = 40;

    //Movement volume could be multiplied by velocity of the pawn
    public float MovementVolume = 5;
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
        MakeNoise(MovementVolume);
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
        MakeNoise(MovementVolume);
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
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    public override void Attack()
    {
         if(attacker == null)
        {
            Debug.LogWarning("Warning: No Attacker in TankPawn()!");
            return;
        }
        
        attacker.Attack(transform.forward, AttackSpeed);
        MakeNoise(CannonShotVolume);
    }
    public override void MoveLeft()
    {
        return;
    }
    public override void MoveRight()
    {
        return;
    }

    public override void MakeNoise(float Amount)
    {
        noiseMaker.volumeDistance = Amount;
    }

    public override void EnterVehicle()
    {
         //for vehicles this command will exit the vehicle and reactivate the human.
           Vector3 ExitLocation = new Vector3(transform.position.x - 3,transform.position.y,transform.position.z);
           
           Driver.transform.position = ExitLocation;

           Driver.SetActive(true);

           controller.pawn = Driver.GetComponent<HumanPawn>();
           
           controller.GetComponent<PlayerController>().orientation = Driver.GetComponent<Pawn>().Orientation.transform;
           
           controller.GetComponent<PlayerController>().isControllingHuman = true;
           controller.GetComponent<PlayerController>().SetCameraSettings();
           Driver = null;
    }

}
