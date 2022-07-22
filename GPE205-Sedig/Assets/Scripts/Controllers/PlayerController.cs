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
    public GameObject TankCamera;
    public Transform TankOrientation;
    public GameObject HumanCamera;
    public Transform HumanOrientation;
    public bool isControllingHuman;
    public bool isControllingTank;

    //MOVEMENT-----------------------
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode AttackKey;
    
    private void Awake()
    {
        if(isControllingTank)
        {
        TankCamera = Instantiate(TankCamera);
        TankCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        TankCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        TankCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        TankCamera.GetComponentInChildren<CameraMovement1>().orientation = TankOrientation;
        }
        if(isControllingHuman)
        {
        HumanCamera = Instantiate(HumanCamera);
        HumanCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        HumanCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        HumanCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        HumanCamera.GetComponentInChildren<CameraMovement1>().orientation = HumanOrientation;
        }

    }
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
