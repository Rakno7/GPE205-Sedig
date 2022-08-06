using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    public GameObject PawnPrefab;
    public Animator anim;
    //CAMERASTUFF-------------------------
    private GameObject CurrentCamera;
    public Transform camFollowerTransform; 
    public Transform orientation;
    private float animRotationSpeed = 2;
    private float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    public float RotationX, RotationY;
    public float rotationSpeed = 1f;
    public GameObject PlayerCamera;
    public bool isControllingHuman;
    public bool isControllingTank;
    private float MoveX;
    private float MoveY;

    //MOVEMENT-----------------------
    public KeyCode AimKey;
    public KeyCode walkKey;
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
       pawn = Instantiate(PawnPrefab, transform.position,Quaternion.identity).GetComponent<HumanPawn>();
       pawn.controller = this;
       pawn.controller.GetComponent<PlayerController>().orientation = pawn.Orientation.transform;
       anim = pawn.GetComponent<HumanPawn>().anim;
        

       if (GameManager.instance != null)
        {

         if (GameManager.instance.players != null)
          {  
             GameManager.instance.players.Add(this);
          }
          if (GameManager.instance.humans != null)
          {  
             GameManager.instance.humans.Add(pawn.GetComponent<HumanPawn>());
          }
        }


        if(isControllingTank)
        {
         CurrentCamera =  Instantiate(PlayerCamera);
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        }
        if(isControllingHuman)
        {
        CurrentCamera = Instantiate(PlayerCamera);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        //set the spine for aiming. The spine is rotated by calling the Spine rotation function when aiming on the camera movement script.
        CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerSpine = pawn.GetComponent<HumanPawn>().spineToRotate;
        }
        base.Start();
    }

    
    public override void Update()
    {
         CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerCamera = PlayerCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().CurrentCamera = CurrentCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = isControllingHuman;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = isControllingTank;
         float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
         float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;
         
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationY += mouseX;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationX -= mouseY;
        ProcessInputs();
        base.Update();
    }
    

    public override void ProcessInputs()
    {
       horizontalInput = Input.GetAxisRaw("Horizontal");
       verticalInput = Input.GetAxisRaw("Vertical");
       if(Input.GetKey(AttackKey))
       {
        anim.SetBool("isShooting",true);
       }
       else
       {
        anim.SetBool("isShooting",false);
       }
       if(Input.GetKey(AimKey))
       {
        anim.SetBool("isAiming",true);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = true;
       }
       else
       {
         anim.SetBool("isAiming",false);
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = false;
       }

       if(Input.GetKey(walkKey) || Input.GetKey(moveBackwardKey) || Input.GetKey(AimKey))
       {
        anim.SetBool("isRunning",false);
         pawn.moveSpeed = pawn.walkSpeed;
       }
       else if(pawn !=null && pawn.GetComponent<HumanPawn>()&& !Input.GetKey(moveBackwardKey) && !Input.GetKey(AimKey))
       {
        anim.SetBool("isRunning",true);
         pawn.moveSpeed = pawn.runSpeed;
       }

      if(Input.GetKey(moveForwardKey))
       {
           pawn.MoveForward();
           anim.SetBool("isMoving", true);
           
           MoveY += animRotationSpeed * Time.deltaTime;
           MoveY = Mathf.Clamp(MoveY, 0, 1);
           
           anim.SetFloat("moveDirY", MoveY);
           
       }
       if(!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey))
       {
           if(MoveY > 0)
           {
             MoveY -= animRotationSpeed * Time.deltaTime;
             MoveY = Mathf.Clamp(MoveY, 0, 1);
           }
           if(MoveY < 0)
           {
             MoveY += animRotationSpeed * Time.deltaTime;
             MoveY = Mathf.Clamp(MoveY, -1, 0);
           }
           
           anim.SetFloat("moveDirY", MoveY);
       }
       
      if (Input.GetKey(moveBackwardKey)) 
      {
          anim.SetBool("isMoving", true);
          pawn.MoveBackwards();
          MoveY -= animRotationSpeed * Time.deltaTime;
          MoveY = Mathf.Clamp(MoveY, -1, 0);
           
           anim.SetFloat("moveDirY", MoveY);
      }  
      if(Input.GetKey(moveLeftKey))
       {
           anim.SetBool("isMoving", true);
           pawn.MoveLeft();
          
           MoveX -= animRotationSpeed * Time.deltaTime;
           MoveX = Mathf.Clamp(MoveX, -1, 1);
           anim.SetFloat("moveDirX", MoveX);
       }
      if (Input.GetKey(moveRightKey)) 
      {
          anim.SetBool("isMoving", true);
          pawn.MoveRight();
          MoveX += animRotationSpeed * Time.deltaTime; 
          MoveX = Mathf.Clamp(MoveX, -1, 1);
          anim.SetFloat("moveDirX", MoveX);
      }  
      if(!Input.GetKey(moveLeftKey) && !Input.GetKey(moveRightKey))
      {
         if (MoveX > 0)
         {
          MoveX -= animRotationSpeed * Time.deltaTime;
          MoveX = Mathf.Clamp(MoveX, 0, 1);
         }
         if (MoveX < 0)
         {
          MoveX += animRotationSpeed * Time.deltaTime;
          MoveX = Mathf.Clamp(MoveX, -1, 0);
         }
          
          anim.SetFloat("moveDirX", MoveX);
      }
      if(!Input.GetKey(moveLeftKey) && !Input.GetKey(moveRightKey) &&!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey))
      {
         anim.SetBool("isMoving", false);
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

     
      else
      {
        if(pawn != null)
        pawn.MakeNoise(2);
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
                Destroy(CurrentCamera);
                //later on this should temporarily spawn a new camera following the ai which killed the player until the player respawns.
                Destroy(gameObject);
            }
        }
    }

   
}
