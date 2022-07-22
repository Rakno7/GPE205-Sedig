using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCamera : CameraController
{
   public KeyCode ChangeCamPositionKey;
    public override void Start()
    {
        base.Start();
    }

    
    public override void Update()
    {  
         base.Update();
          if(Input.GetKeyDown(ChangeCamPositionKey))
          {
            ChangeCameraPos();
          }
    }
    public override void LateUpdate()
    {
         
         RotationX = Mathf.Clamp(RotationX, -30f, 15f); //restrict cam rotaton angle
         Quaternion from = orientation.rotation;
         Quaternion to = camHolder.rotation;
         
         //apply rotation to camera
         camHolder.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         //Slerp the current position and desired position overtime
         orientation.rotation = Quaternion.Slerp(from, to, rotationSpeed * Time.deltaTime);

         //--------FOR:Instant movement along with camera-----------------
        // orientation.rotation = Quaternion.Euler(RotationX, RotationY, 0);
    }
     public void ChangeCameraPos()
    {
        if(cameraFollower.Setting <=2)
        {
        cameraFollower.Setting +=1;
        }
        else
        {
        cameraFollower.Setting = 1;
        }
      
    }

}
