using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : Controller
{
   public enum  AIStates 
   {
     ChooseTarget, ChooseVehicleTarget, GaurdPost, EnterVehicle, VehicleChase, HumanChase, Attack, TakeCover, Flee
   };
     //Check Transition Example
    protected bool isHasTarget()
    {
        // return the truth or falsity of this statement
        return (target != null);
    }
    protected bool isDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance ) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    
    
    protected bool isInVehicle()
    {
        // return the truth or falsity of this statement
        return (selftarget != null);
    }

    public List<PlayerController> players;
    public List<TankPawn> Vehicles;
    public GameObject target;
    public GameObject selftarget;
    public GameObject vehicletarget;
    public bool isControllingTank = false;
    public bool isControllingHuman = true;
    //Keep track of how long we spend in a state
    private float timeSinceLastStateChange;

   public AIStates currentState;
   
   private void Awake()
   {
    
   }
    public override void Start()
    {
        //TEMP:to test the state.
        //use this later in state method:---target = GameManager.instance.players[0].pawn.gameObject;
         selftarget = pawn.gameObject;
        ChangeState(AIStates.GaurdPost);
        
        base.Start();
    }

    
    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public void MakeDecisions()
    {
        Debug.Log("Making Decisions");
        
        switch (currentState)
        {
            case AIStates.GaurdPost:
            //work
            DoGaurdPostState();
            TargetNearestPlayer();

            //TEST
            TargetNearestVehicle();

            //Transition
            //when AI has a target in range, and vehicle, and not currently in a vehicle..
            if (isDistanceLessThan(target, 50) && isDistanceLessThan(vehicletarget, 10)&& !isInVehicle()) 
                {
                    ChangeState(AIStates.EnterVehicle);
                }
                //When Ai has a target in range, and is in vehicle..
                if (isDistanceLessThan(target, 10) && isInVehicle()) 
                {
                    ChangeState(AIStates.VehicleChase);
                }  
                //When AI has a target, not in a vehicle, and has no vehicle in range to get in..
                if (isDistanceLessThan(target, 50) && !isInVehicle() && !isDistanceLessThan(vehicletarget, 10)) 
                {
                    ChangeState(AIStates.HumanChase);
                }   
                break;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                case AIStates.EnterVehicle:
                DoEnterVehicleState();
                //when the AI Gets in a vehicle and has a target in range
                if(isInVehicle() && isDistanceLessThan(target, 50))
                {
                    ChangeState(AIStates.VehicleChase);
                }
                //when the AI Gets in a vehicle and doesnt have a target in range
                if(isInVehicle() && !isDistanceLessThan(target, 50))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when some else takes the target vehicle and doesnt have a target in range
                if(!isInVehicle() && !isDistanceLessThan(vehicletarget, 10) && !isDistanceLessThan(target, 50))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when some else takes the target vehicle but still has a target in range
                if(!isInVehicle() && !isDistanceLessThan(vehicletarget, 10) && isDistanceLessThan(target, 50))
                {
                    ChangeState(AIStates.HumanChase);
                }

                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

            case AIStates.HumanChase:
                DoHumanChaseState();
                
                //When AI doesnt have a target or vehicle in range
                if (!isDistanceLessThan(target, 50) && !isDistanceLessThan(vehicletarget, 10) && !isInVehicle()) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //When AI doesnt have a target but found a vehicle in range
                if (!isDistanceLessThan(target, 50) && isDistanceLessThan(vehicletarget, 10) && !isInVehicle()) 
                {
                    ChangeState(AIStates.EnterVehicle);
                }
                //When AI has a target but found a vehicle in range (prioritise vehicle)
                if (!isDistanceLessThan(target, 50) && isDistanceLessThan(vehicletarget, 10) && !isInVehicle()) 
                {
                    ChangeState(AIStates.EnterVehicle);
                }
                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
                case AIStates.VehicleChase:
                DoVehicleChaseState();

                //When AI doesnt have a target
                if (!isDistanceLessThan(target, 50)) 
                {
                    ChangeState(AIStates.GaurdPost);
                }


                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
        }
    }
     public virtual void ChangeState ( AIStates newState)
    {
        
        // Change the current state
        currentState = newState;
        // Keep track of when this state change happened
        timeSinceLastStateChange = Time.time;

    }
    protected virtual void DoGaurdPostState()
    {
        //Do what?
    }
    protected virtual void DoEnterVehicleState()
    {
        //Do what?
    }
    protected virtual void DoHumanChaseState()
    {
        //Do what?
        Chase(target);
    }
    protected virtual void DoVehicleChaseState()
    {
        //Do what?
        Chase(target);
    }
    protected virtual void DoAttackState()
    {
        //Do what?
        Chase(target);
        Attack();
    }

    //overloading, different versions of the same method which take different data in the constructor
    
    public void Chase(Vector3 targetPosition)
    {  
        pawn.RotateTowards(targetPosition);
        pawn.MoveForward();
    }
    public void Chase(GameObject targetGameObject)
    {  
        Chase(target.gameObject.transform);
    }
    public void Chase(Transform targetTransform)
    {  
        Chase(targetTransform.position);
    }
     public void Chase(Controller targetController)
    {
        Chase(targetController.pawn);
    }
     public void Chase(Pawn targetPawn)
    {
        Chase(targetPawn.transform);
    }
    
    public void Attack()
    {
        pawn.Attack();
    }

    public void TargetNearestPlayer()
    {
        //GameManager exists
        if (GameManager.instance != null) 
        {
            //list of players exists
            if (GameManager.instance.players != null) 
            {
                //there are players in it
                if (GameManager.instance.players.Count > 0) 
                {
                    //target the first player in the list
                    players = GameManager.instance.players;
                    PlayerController closestPlayer = players[0];
                    float closestPlayerDistance = Vector3.Distance(pawn.gameObject.transform.position, closestPlayer.gameObject.transform.position);
                    foreach(PlayerController players in players)
                    {
                        if (Vector3.Distance(pawn.transform.position, players.pawn.transform.position) <= closestPlayerDistance)
                        {
                            closestPlayer = players;
                            closestPlayerDistance = Vector3.Distance(pawn.transform.position, closestPlayer.transform.position);
                        }
                    } 
                    Debug.Log("tried to find target");
                    target = closestPlayer.gameObject;
                }
            }
        }
    }
    public void TargetNearestVehicle()
    {
        //GameManager exists
        if (GameManager.instance != null) 
        {
            //list of vehicles exists
            if (GameManager.instance.Vehicles != null) 
            {
                //there are vehicles in it
                if (GameManager.instance.Vehicles.Count > 0) 
                {
                    //target the first vehicle in the list
                    Vehicles = GameManager.instance.Vehicles;
                    TankPawn closestVehicle = Vehicles[0];
                    float closestVehicleDistance = Vector3.Distance(pawn.gameObject.transform.position, closestVehicle.gameObject.transform.position);
                    foreach(TankPawn Vehicles in Vehicles)
                    {
                        if (Vector3.Distance(pawn.transform.position, Vehicles.gameObject.transform.position) <= closestVehicleDistance)
                        {
                            closestVehicle = Vehicles;
                            closestVehicleDistance = Vector3.Distance(pawn.transform.position, closestVehicle.transform.position);
                        }
                    } 
                    Debug.Log("tried to find target");
                    vehicletarget = closestVehicle.gameObject;
                }
            }
        }
    }


  //FOR: testing
    public void TargetPlayerOne()
    {
        //GameManager exists
        if (GameManager.instance != null) 
        {
            //list of players exists
            if (GameManager.instance.players != null) 
            {
                //there are players in it
                if (GameManager.instance.players.Count > 0) 
                {
                    //target the first player in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }


    
}

