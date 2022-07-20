using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        //base refers to the base class, this will run the start function on the base class.
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();
    }


    //we will override the base class method and define how we want tanks to move specifically.
    public override void MoveForward()
    {
        Debug.Log("Move Forward");
    }

    public override void MoveBackwards()
    {
        Debug.Log("Move Backward");
    }

    public override void RotateClockwise()
    {
        Debug.Log("Rotate Clockwise");
    }

    public override void RotateCounterClockwise()
    {
        Debug.Log("Rotate Counter Clockwise");
    }

}
