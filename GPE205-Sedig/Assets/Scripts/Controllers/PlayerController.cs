using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
       if (GameManager.instance != null)
        {

         if (GameManager.instance.players != null)
          {
             
             GameManager.instance.players.Add(this);
          }
        }


        if(isControllingTank)
        {
         CurrentCamera = PlayerCamera = Instantiate(PlayerCamera);
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        }
        if(isControllingHuman)
        {
        CurrentCamera = PlayerCamera = Instantiate(PlayerCamera);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        }
        base.Start();
    }

    
    public override void Update()
    {
         CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerCamera = this.PlayerCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().CurrentCamera = CurrentCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = this.isControllingHuman;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = this.isControllingTank;
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
          pawn.Attack();
      }
      if(Input.GetKeyDown(EnterVehicleKey))
      {
          pawn.EnterVehicle();
      }
     
    }

    public void SetCameraSettings()
    {
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
    }

    public void OnDestroy()
    { 
        if (GameManager.instance != null) 
        {   
            if (GameManager.instance.players != null) 
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }

   
}
