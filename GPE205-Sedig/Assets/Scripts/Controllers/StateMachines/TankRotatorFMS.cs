using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRotatorFMS : MonoBehaviour
{
    public  void Start()
    {
       // selftarget = pawn.gameObject;
       // ChangeState(AIStates.GaurdPost);
        
    }
    public GameObject target;
    public float RotatonX;
    public float RotatonY;
    public float rotationSpeed;

    public  void Update()
    { 
      if(GetComponentInParent<TankPawn>().controller!=null)     
        if(GetComponentInParent<TankPawn>().controller.gameObject.GetComponent<AiController>() && GetComponentInParent<TankPawn>().Driver !=null)
        {
        target = GetComponentInParent<TankPawn>().controller.gameObject.GetComponent<AiController>().target;
        Vector3 TargetVec = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(TargetVec, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
       

   
}
