using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //TODO: This should eventually become an abstract class, with subclasses overriding the Update function for different camera movement depending on the vehicle.
    private float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    private float RotationX, RotationY;
    public Transform orientation;
    public Transform camHolder; 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
     
    
    void Update()
    {
         float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
         float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;

         RotationY += mouseX;
         RotationX -= mouseY;
         RotationX = Mathf.Clamp(RotationX, -30f, 15f); //restrict view 

         //apply rotation
         camHolder.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         orientation.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         MovementRotation(); 
    }
    void MovementRotation()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
