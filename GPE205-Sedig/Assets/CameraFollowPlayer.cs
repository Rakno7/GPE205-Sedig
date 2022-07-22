using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    
    public int Setting = 1;
    public Transform cameraSetting1;
    public Transform cameraSetting2;
    public Transform cameraSetting3;
    
    void LateUpdate()
    {
        //cameraPos points to an empty gameobject on the player
        // which represents where the camera should place itself in the gameworld.
        if(Setting == 1)
        {
        transform.position = cameraSetting1.position;
        }
        if(Setting == 2)
        {
        transform.position = cameraSetting2.position;
        }
        if(Setting == 3)
        {
        transform.position = cameraSetting3.position;
        }
        

    }
   
}
