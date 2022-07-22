using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    
    public Transform cameraPos;
    
    void LateUpdate()
    {
        //cameraPos points to an empty gameobject on the player
        // which represents where the camera should place itself in the gameworld.
        transform.position = cameraPos.position;
    }
   
}
