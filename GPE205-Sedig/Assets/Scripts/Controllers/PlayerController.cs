using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //CAMERASTUFF-------------------------
    public  CameraFollowPlayer cameraFollowerScript;
    public Transform camFollowerTransform; 
    public Transform orientation;
   // public CameraController camController;
   
    public float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    public float RotationX, RotationY;
    public float rotationSpeed = 1f;
    

    //MOVEMENT-----------------------
    public GameObject playerCamera;
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode AttackKey;
    
    
    public override void Start()
    {
        base.Start();
    }

    
    public override void Update()
    {
         float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
         float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;

         RotationY += mouseX;
         RotationX -= mouseY;
        ProcessInputs();
        base.Update();
    }
    

    public override void ProcessInputs()
    {
       horizontalInput = Input.GetAxisRaw("Horizontal");
       verticalInput = Input.GetAxisRaw("Vertical");

      if(Input.GetKey(moveForwardKey))
       {
           pawn.MoveForward();
       }

      if (Input.GetKey(moveBackwardKey)) 
      {
          pawn.MoveBackwards();
      }  
      if(Input.GetKey(moveLeftKey))
       {
           pawn.MoveLeft();
       }

      if (Input.GetKey(moveRightKey)) 
      {
          pawn.MoveRight();
      }  
      if (Input.GetKey(rotateClockwiseKey)) 
      {
          pawn.RotateClockwise();
      }  
      if (Input.GetKey(rotateCounterClockwiseKey)) 
      {
          pawn.RotateCounterClockwise();
      }
      if(Input.GetKey(AttackKey))
      {
          pawn.DoAttack();
      }
     
    }
   
}
