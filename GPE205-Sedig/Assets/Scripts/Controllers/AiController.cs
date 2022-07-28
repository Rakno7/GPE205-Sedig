using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : Controller
{
   public enum  AIStates 
   {
     ChooseTarget, ChooseVehicleTarget, GaurdPost, EnterVehicle, MoveToVehicle, VehicleChase, HumanChase, Attack, TakeCover, Flee
   };
     //Check Transition Example
    protected bool isHasTarget()
    {
        // return the truth or falsity of this statement
        return (target != null);
    }
    protected bool isDistanceLessThanTarget(GameObject target, float distance)
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
    protected bool isDistanceLessThanVehicleTarget(GameObject vehicletarget, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, vehicletarget.transform.position) < distance ) 
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
        if(pawn.GetComponent<TankPawn>())
        {
            Debug.Log("is in vehicle");
            return true;
        }
        else
        {
            Debug.Log("is NOT in vehicle");
            return false;
        }
        
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
            if (isDistanceLessThanTarget(target, 100) && isDistanceLessThanVehicleTarget(vehicletarget, 100) && !isInVehicle()) 
                {
                    Debug.Log("tried to switch states");
                    ChangeState(AIStates.MoveToVehicle);
                }
                //When Ai has a target in range, and is in vehicle..
                if (isDistanceLessThanTarget(target, 100) && isInVehicle()) 
                {
                    ChangeState(AIStates.VehicleChase);
                }  
                //When AI has a target, not in a vehicle, and has no vehicle in range to get in..
                if (isDistanceLessThanTarget(target, 500) && !isInVehicle() && !isDistanceLessThanVehicleTarget(vehicletarget, 100)) 
                {
                    ChangeState(AIStates.HumanChase);
                }   
                break;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                
            
                case AIStates.MoveToVehicle:
                DoMoveToVehicleState();
                //if were close enough to the vehicle to enter it
                if(isDistanceLessThanVehicleTarget(vehicletarget, 1) && !isInVehicle())
                {
                  ChangeState(AIStates.EnterVehicle);
                }
                //when some else takes the target vehicle and doesnt have a target in range
                if(!isInVehicle() && !isDistanceLessThanVehicleTarget(vehicletarget, 100) && !isDistanceLessThanTarget(target, 100))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when some else takes the target vehicle but still has a target in range
                if(!isInVehicle() && !isDistanceLessThanVehicleTarget(vehicletarget, 100) && isDistanceLessThanTarget(target, 500))
                {
                    ChangeState(AIStates.HumanChase);
                }
                //when AI gets in vehicle and still has a target in range
                if(isInVehicle() && isDistanceLessThanTarget(target, 500))
                {
                    ChangeState(AIStates.VehicleChase);
                }

                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                case AIStates.HumanChase:
                DoHumanChaseState();
                
                //When AI doesnt have a target or vehicle in range
                if (!isDistanceLessThanTarget(target, 500) && !isDistanceLessThanVehicleTarget(vehicletarget, 100) && !isInVehicle()) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //When AI doesnt have a target but found a vehicle in range
                if (!isDistanceLessThanTarget(target, 500) && isDistanceLessThanVehicleTarget(vehicletarget, 100) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                //When AI has a target but found a vehicle in range (prioritise vehicle)
                if (!isDistanceLessThanTarget(target, 500) && isDistanceLessThanVehicleTarget(vehicletarget, 100) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
                case AIStates.VehicleChase:
                DoVehicleChaseState();
                TargetNearestPlayer();
                //When AI doesnt have a target
                if (!isDistanceLessThanTarget(target, 500)) 
                {
                    ChangeState(AIStates.GaurdPost);
                }


                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------
                
        }
    }
     public virtual void ChangeState (AIStates newState)
    {
        
        // Change the current state
        currentState = newState;
        // Keep track of when this state change happened
        timeSinceLastStateChange = Time.time;

    }
    protected virtual void DoGaurdPostState()
    {
        //Do what? //patrol here!
    }
    protected virtual void DoMoveToVehicleState()
    {
        //Do what?
        Debug.Log("searching for vehicle");
        SeekVehicle(vehicletarget);
        if(!isInVehicle())
        {
        Enter();
        }
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
    public void SeekVehicle(Vector3 vehiclePosition)
    {  
        pawn.RotateTowards(vehiclePosition);
        pawn.MoveForward();
    }
    public void SeekVehicle(Transform vehicleTransform)
    {  
        SeekVehicle(vehicleTransform.position);
    }
    public void SeekVehicle(GameObject vehicleGameobject)
    {  
        SeekVehicle(vehicleGameobject.gameObject.transform);
    }
    public void SeekVehicle(Pawn vehiclePawn)
    {  
        SeekVehicle(vehiclePawn.transform);
    }
    
    //---------------------------------------------------------------------------------------------------------
    public void Chase(Vector3 targetPosition)
    {  
        pawn.RotateTowards(targetPosition);
        pawn.MoveForward();
    }
    public void Chase(Transform targetTransform)
    {  
        Chase(targetTransform.position);
    }
    public void Chase(GameObject targetGameObject)
    {  
        Chase(targetGameObject.gameObject.transform);
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

    public void Enter()
    {
        Debug.Log("Tried to Enter vehicle");
        pawn.EnterVehicle();
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
                    target = closestPlayer.GetComponent<PlayerController>().pawn.gameObject;
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

