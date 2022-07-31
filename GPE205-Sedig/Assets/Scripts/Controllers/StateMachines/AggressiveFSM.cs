using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveFSM : AiController
{
    public float thisAImemory;
    public override void Start()
    {
        AIMemory = thisAImemory;
    }

    
    public override void Update()
    {
        base.Update();
    }

    public override void MakeDecisions()
    {   
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
                DoVehicleChaseState();
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

                DoAttackState();

                if (!isDistanceLessThanTarget(target, targetVisRange)) 
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
}
