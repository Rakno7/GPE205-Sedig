using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //CAMERASTUFF-------------------------
    public GameObject CurrentCamera;
    public  CameraFollowPlayer cameraFollowerScript;
    public Transform camFollowerTransform; 
    public Transform orientation;
   // public CameraController camController;
   
    public float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    public float RotationX, RotationY;
    public float rotationSpeed = 1f;
    public GameObject PlayerCamera;
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
    public KeyCode EnterVehicleKey;
    
    private void Awake()
    {
        

    }
    public override void Start()
    {
        if(isControllingTank)
        {
         CurrentCamera = PlayerCamera = Instantiate(PlayerCamera);
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        }
        if(isControllingHuman)
        {
        CurrentCamera = PlayerCamera = Instantiate(PlayerCamera);
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        }
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
      if(Input.GetKeyDown(EnterVehicleKey))
      {
          pawn.EnterVehicle();
      }
     if(isControllingHuman)
     {
        pawn.MouseRotate();
     }
    }

    public void SetCameraSettings()
    {
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
    }

   
}