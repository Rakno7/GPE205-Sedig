using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement1 : PlayerController
{
   public KeyCode ChangeCamPositionKey;
    public override void Start()
    {
        //cameraFollower = gameObject.GetComponentInParent<CameraFollowPlayer>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    
    public override void Update()
    {  
         base.Update();
          if(Input.GetKeyDown(ChangeCamPositionKey))
          {
            ChangeCameraPos();
          }
    }
    public void LateUpdate()
    {
         
         RotationX = Mathf.Clamp(RotationX, -30f, 15f); //restrict cam rotaton angle
         Quaternion from = orientation.rotation;
         Quaternion to = camFollowerTransform.rotation;
         
         //apply rotation to camera
         camFollowerTransform.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         //Slerp the current position and desired position overtime
         if(isControllingTank)
         {
         orientation.rotation = Quaternion.Slerp(from, to, rotationSpeed * Time.deltaTime);
         }
         if(isControllingHuman)
         {
          orientation.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         }

         //--------FOR:Instant movement along with camera-----------------
        // orientation.rotation = Quaternion.Euler(RotationX, RotationY, 0);
    }

    //TODO: I think this should be in the PlayerController
     public void ChangeCameraPos()
    {
        if(cameraFollowerScript.Setting <=2)
        {
        cameraFollowerScript.Setting +=1;
        }
        else
        {
        cameraFollowerScript.Setting = 1;
        }
      
    }
}
