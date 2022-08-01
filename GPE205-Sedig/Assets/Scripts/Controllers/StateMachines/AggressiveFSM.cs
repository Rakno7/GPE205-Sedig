using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveFSM : AiController
{
   private void Awake()
   {
    
   }
    public override void Start()
    {
        //TEMP:to test the state.
        //use this later in state method:---target = GameManager.instance.players[0].pawn.gameObject;
         selftarget = pawn.gameObject;
        ChangeState(AIStates.GaurdPost);
        

        //TODO Populate patrol waypoints with nearest waypoints group GameObjects
        base.Start();
    }

    
    public override void Update()
    {

     MakeDecisions();
    }


    //Override this function to create multiple AI personalitys which inhertit from this class. 
    public override void MakeDecisions()
    {   
         // Debug.Log("isthisworking?");
         switch (currentState)
        {
            case AIStates.GaurdPost:
            //work
            if(waypointclusters !=null)
            {
            TargetNearestWaypointCluster();
            }
            DoGaurdPostState();
            TargetNearestPlayer();
           
            

            //TEST
            TargetNearestVehicle();

            //Transition

            //when AI has a target in range, and vehicle, and not currently in a vehicle..
            if (isDistanceLessThanTarget(target, targetVisRange) && isCanSee(target) && isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                //When Ai has a target in range, and is in vehicle..
                if (isDistanceLessThanTarget(target, targetVisRange) && isInVehicle() && isCanSee(target)) 
                {
                    ChangeState(AIStates.VehicleChase);
                }  
                //When AI has a target, not in a vehicle, and has no vehicle in range to get in..
                if (isDistanceLessThanTarget(target, targetVisRange) && !isInVehicle() && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && isCanSee(target)) 
                {
                    ChangeState(AIStates.HumanChase);
                }   
                break;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                
            
                case AIStates.MoveToVehicle:
                DoMoveToVehicleState();
                TargetNearestVehicle();
                //when some else takes the target vehicle and doesnt have a target in range
                if(!isInVehicle() && vehicletarget.GetComponent<TankPawn>().Driver != null)
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when some else takes the target vehicle but still has a target in range
                if(!isInVehicle() && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange))
                {
                    ChangeState(AIStates.HumanChase);
                }
                //when AI gets in vehicle
                if(isInVehicle())
                {
                    ChangeState(AIStates.VehicleChase);
                }

                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                case AIStates.HumanChase:
                DoHumanChaseState();
                TargetNearestVehicle();
                
                //When AI doesnt have a target or vehicle in range
                if (!isDistanceLessThanTarget(target, targetVisRange) && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                
                //When AI has a target but found an empty vehicle in range (prioritise vehicle)
                if (isDistanceLessThanTarget(target, targetVisRange) && isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle() && vehicletarget.GetComponent<TankPawn>().Driver == null)
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                 //When AI can no longer see or hear target
                if (isDistanceLessThanTarget(target, targetVisRange) && !isCanSee(target) && !isCanHear(target))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
                case AIStates.VehicleChase:
                DoVehicleChaseState(true);
                TargetNearestPlayer();
                //When AI doesnt have a target
                if (!isDistanceLessThanTarget(target, targetVisRange)) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when the target is in range to attack// Maybe I can put the range on the tank pawn being controlled, as the other tank can aim up and shoot farther.
                if (isDistanceLessThanTarget(target, targetAttackRange)) 
                {
                    ChangeState(AIStates.Attack);
                }

                 //When AI can no longer see or hear target
                if (isDistanceLessThanTarget(target, targetVisRange) && !isCanSee(target) && !isCanHear(target))
                {
                    ChangeState(AIStates.GaurdPost);
                }

                break;
                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
                case AIStates.Attack:

                DoAttackState(false);

                if (!isDistanceLessThanTarget(target, targetVisRange) && timeSinceLastStateChange > AIMemory) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                if (!isDistanceLessThanTarget(target, targetAttackRange)) 
                {
                    ChangeState(AIStates.VehicleChase);
                }

                 //When enough time has passed and the AI can no longer see or hear target 
                if (!isCanSee(target) && !isCanHear(target) && timeSinceLastStateChange > AIMemory)
                {
                    ChangeState(AIStates.GaurdPost);
                }

                break;
        }
    }

    private void OnDrawGizmos()
    {
         
       // NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
    //
      //
       // float totalDistance = noiseMaker.volumeDistance + hearingDistance;        
       // if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance) 
       // {
       //     Gizmos.color = Color.red;
       //     
       // }
       // else 
       // {
       //     Gizmos.color = Color.yellow;
       // }
//
       //  Gizmos.DrawWireSphere(target.transform.position,noiseMaker.volumeDistance);
       //  Gizmos.DrawWireSphere(pawn.transform.position,hearingDistance);
    }

}
