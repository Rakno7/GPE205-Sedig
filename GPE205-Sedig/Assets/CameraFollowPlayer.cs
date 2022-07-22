using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    public Controller controller;
    public int Setting = 1;
    public Transform CameraSetting1;
    public Transform CameraSetting2;
    public Transform CameraSetting3;
    
   // public bool isHuman;
    
    void LateUpdate()
    {
        
        //cameraPos points to an empty gameobject on the player Pawn
        // which represents where the camera should place itself in the gameworld.
        //if(!isHuman)
        //{
          if(Setting == 1)
          {
          transform.position = CameraSetting1.position;
          }
          if(Setting == 2)
          {
          transform.position = CameraSetting2.position;
          }
          if(Setting == 3)
          {
          transform.position = CameraSetting3.position;
          }
       //}

       // if(isHuman)
       // {
       // transform.position = HumanCameraSetting1.position;
       // }
    }   
}
