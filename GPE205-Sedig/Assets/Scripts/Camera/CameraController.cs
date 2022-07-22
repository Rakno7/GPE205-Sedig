using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    public  CameraFollowPlayer cameraFollower;
    public float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    public float RotationX, RotationY;
    public float rotationSpeed = 1f;
    public Transform orientation;
    public Transform camHolder; 
    public CameraController camController;
    
    public virtual void Start()
    {
        cameraFollower = gameObject.GetComponentInParent<CameraFollowPlayer>();
        Cursor.lockState = CursorLockMode.Locked;
    }
     
    
   public virtual void Update()
    {         
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;

         RotationY += mouseX;
         RotationX -= mouseY;
         ProcessInputs(); 
    }
    public abstract void LateUpdate();
    
    public void ProcessInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
   
}
