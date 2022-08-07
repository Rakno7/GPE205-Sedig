using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiancedFSM : AiController
{
    public GameObject PawnPrefab;
    public override void Start()
    {
        pawn = Instantiate(PawnPrefab, transform.position,Quaternion.identity).GetComponent<HumanPawn>();
        pawn.controller = this;

       if (GameManager.instance != null)
        {

         if (GameManager.instance.aiPlayers != null)
          {  
             GameManager.instance.aiPlayers.Add(this);
          }
          if (GameManager.instance.humans != null)
          {  
             GameManager.instance.humans.Add(pawn.GetComponent<HumanPawn>());
          }
        }
        selftarget = pawn.gameObject;
        ChangeState(AIStates.GaurdPost);
    }

    
    public override void Update()
    {
     MakeDecisions();
    }
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
            TimePassedSinceLastChange += Time.deltaTime;
            DoGaurdPostState();
            TargetNearestPlayer();
            TargetNearestVehicle();

            //Transition
             if(target != null)
            {
                if(isCanHear(target))
                {
                    ChangeState(AIStates.turnTowards);
                }

            //when experianced AI is not currently in a vehicle, it already knows where its at.
            if (!isInVehicle() && vehicletarget !=null) 
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
                 if(target == null)
                {
                    ChangeState(AIStates.GaurdPost);
                }
            }
                break;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                case AIStates.turnTowards:
                TimePassedSinceLastChange += Time.deltaTime;
                DoTurnTowardsState();
                //when AI can no longer hear target and forgets about them
                if(target == null || !isCanHear(target) && TimePassedSinceLastChange > AIMemory)
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when ai turns and sees the target
                if (isCanSee(target) && !isInVehicle()) 
                {
                    ChangeState(AIStates.HumanChase);
                }
                if (isCanSee(target) && isInVehicle()) 
                {
                    ChangeState(AIStates.VehicleChase);
                }


                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                case AIStates.MoveToVehicle:
                TimePassedSinceLastChange += Time.deltaTime;
                DoMoveToVehicleState();
                TargetNearestVehicle();
                //when some else takes the target vehicle
                if(vehicletarget!=null)
                {
                if(!isInVehicle() && vehicletarget.GetComponent<TankPawn>().Driver != null)
                {
                    ChangeState(AIStates.HumanChase);
                }
                }
                
                //when AI gets in vehicle and can see a target
                if(isInVehicle() && isCanSee(target))
                {
                    ChangeState(AIStates.VehicleChase);
                }
                //when AI gets in vehicle and can see a target
                if(isInVehicle() && !isCanSee(target))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                
                 if(target == null)
                {
                    ChangeState(AIStates.GaurdPost);
                }

                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                case AIStates.HumanChase:
                TimePassedSinceLastChange += Time.deltaTime;
                DoHumanChaseState();
                TargetNearestPlayer();
                TargetNearestVehicle();
                if(target == null)
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //When AI doesnt have a target or vehicle in range
                if (TimePassedSinceLastChange > AIMemory &&  !isCanSee(target) && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                
                //When AI has a target but found an empty vehicle in range (prioritise vehicle)
                if (isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle() && vehicletarget.GetComponent<TankPawn>().Driver == null)
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                 if (isDistanceLessThanTarget(target, targetAttackRange) && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle() && isCanSee(target))
                {
                    ChangeState(AIStates.Attack);
                }
                if (isDistanceLessThanTarget(target, targetAttackRange) && vehicletarget.GetComponent<TankPawn>().Driver != null && !isInVehicle() && isCanSee(target))
                {
                    ChangeState(AIStates.Attack);
                }
               
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
                case AIStates.VehicleChase:
                TimePassedSinceLastChange += Time.deltaTime;
                DoVehicleChaseState(true);
                 if(target == null)
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when experianced ai detects incoming shot
                if(isShotIncoming())
                {
                    ChangeState(AIStates.Flee);
                }

                //When AI doesnt have a target in range and they forget about the player
                if (!isCanSee(target) && TimePassedSinceLastChange > AIMemory) 
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
                TimePassedSinceLastChange += Time.deltaTime;

                DoAttackState(false);
                if(target == null)
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when experianced ai detects incoming shot
                if(isShotIncoming())
                {
                    ChangeState(AIStates.Flee);
                }
                if (!isDistanceLessThanTarget(target, targetAttackRange)) 
                {
                    ChangeState(AIStates.VehicleChase);
                }
                if (!isDistanceLessThanTarget(target, targetAttackRange) && !isInVehicle() && isCanSee(target)) 
                {
                    ChangeState(AIStates.HumanChase);
                }
                if(!isCanSee(target))
                {
                    ChangeState(AIStates.turnTowards);
                }
                break;
                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                case AIStates.Flee:
                TimePassedSinceLastChange += Time.deltaTime;
                DoFleeState(5);
                if(target == null || TimePassedSinceLastChange > 3)
                {
                    ChangeState(AIStates.VehicleChase);
                }                
                break;
        }
    }

    private void OnDrawGizmos()
    {
         if(Application.isPlaying)
         {
            if(target==null) return;
           NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
          
           float totalDistance = noiseMaker.volumeDistance + hearingDistance;        
           if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance) 
           {
               Gizmos.color = Color.red;
           }
           else 
           {
               Gizmos.color = Color.yellow;
           }
          
            Gizmos.DrawWireSphere(target.transform.position,noiseMaker.volumeDistance);
            Gizmos.DrawWireSphere(pawn.transform.position,hearingDistance);
          }
    }
}
