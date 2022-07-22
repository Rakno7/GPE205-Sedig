using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    public Controller controller;
    public int Setting = 1;
    public Transform tankCameraSetting1;
    public Transform tankCameraSetting2;
    public Transform tankCameraSetting3;
    public Transform HumanCameraSetting1;
    
    void LateUpdate()
    {
        
        //cameraPos points to an empty gameobject on the player Pawn
        // which represents where the camera should place itself in the gameworld.
        if(controller.isControllingTank)
        {
          if(Setting == 1)
          {
          transform.position = tankCameraSetting1.position;
          }
          if(Setting == 2)
          {
          transform.position = tankCameraSetting2.position;
          }
          if(Setting == 3)
          {
          transform.position = tankCameraSetting3.position;
          }
        }

        if(controller.isControllingHuman)
        {
          transform.position = HumanCameraSetting1.position;
        }
    }   
}
