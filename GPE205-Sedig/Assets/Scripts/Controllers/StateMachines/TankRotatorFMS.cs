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
        if(GetComponentInParent<TankPawn>().controller.gameObject.GetComponent<AiController>())
        {
         target = GetComponentInParent<TankPawn>().controller.gameObject.GetComponent<AiController>().target;
       // 
       //  Quaternion from = Quaternion.Euler(transform.position.x,transform.position.y,0).normalized;
       //  Quaternion to = Quaternion.Euler(target.transform.position.x,target.transform.position.y,0).normalized; 
       //   
       //  //Slerp the current position and desired position overtime
       //  transform.rotation = Quaternion.Slerp(from,to, rotationSpeed * Time.deltaTime);

         Vector3 TargetVec = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(TargetVec, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }
       

   // public override void MakeDecisions()
   // {   
   //      switch (currentState)
   //     {
   //         case AIStates.GaurdPost:
   //         //work
   //         DoGaurdPostState();
   //         TargetNearestPlayer();
   //         //Transition
   //         
   //             //When Ai has a target in range, and is in vehicle..
   //             if (isDistanceLessThanTarget(target, targetVisRange) && isCanSee(target) || isCanHear(target))
   //             {
   //                 ChangeState(AIStates.VehicleChase);
   //             }  
   //             break;
   //              //-------------------------------------------------------------------------------------------------------------------------------------------------------------
   //             
   //             case AIStates.VehicleChase:
   //             DoVehicleChaseState(false);
   //             //When AI doesnt have a target
   //             if (!isDistanceLessThanTarget(target, targetVisRange)) 
   //             {
   //                 ChangeState(AIStates.GaurdPost);
   //             }
   //             //when the target is in range to attack// Maybe I can put the range on the tank pawn being controlled, as the other tank can aim up and shoot farther.
   //             if (isDistanceLessThanTarget(target, targetAttackRange) && isCanSee(target)) 
   //             {
   //                 ChangeState(AIStates.Attack);
   //             }
//
   //              //When AI can no longer see or hear target
   //             if (isDistanceLessThanTarget(target, targetVisRange) && !isCanSee(target) && !isCanHear(target))
   //             {
   //                 ChangeState(AIStates.GaurdPost);
   //             }
//
   //             break;
   //              //-------------------------------------------------------------------------------------------------------------------------------------------------------------
   //             
   //             case AIStates.Attack:
//
   //             DoAttackState(false);
//
   //             if (!isDistanceLessThanTarget(target, targetVisRange) && timeSinceLastStateChange > AIMemory) 
   //             {
   //                 ChangeState(AIStates.GaurdPost);
   //             }
   //             if (!isDistanceLessThanTarget(target, targetAttackRange)) 
   //             {
   //                 ChangeState(AIStates.VehicleChase);
   //             }
   //             break;
   //     }
   // } 
}
