using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : Controller
{
   public enum  AIStates 
   {
     ChooseTarget, ChooseVehicleTarget, GaurdPost, EnterVehicle, MoveToVehicle, VehicleChase, HumanChase, Attack, TakeCover, Flee
   };

   //Debug
    Color raycolor = Color.yellow;
     //Check Transition Example
    protected bool isHasTarget()
    {
        // return the truth or falsity of this statement
        return (target != null);
    }
    protected bool isDistanceLessThanTarget(GameObject thisTarget, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, thisTarget.transform.position) < distance ) 
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

   
   public bool isCanHear(GameObject target)
    {
        // Get target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        // If they don't have one, return false
        if (noiseMaker == null) 
        {
            return false;
        }
        // If they arent making noise return false
        if (noiseMaker.volumeDistance <= 0) 
        {
            return false;
        }
        // If they are making noise, add the volumeDistance to this AI's hearingDistance
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;

        

        // If the distance betweenthis AI's pawn and the target is closer then the total distance, the AI hears it. 
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance) 
        {
            return true;
            
        }
        else 
        {
            return false;
        }
    }
  
  public bool isCanSee(GameObject target)
    {
        
        // Find the vector from the controlled pawn to the target
        Vector3 PawnToTargetVector = target.transform.position - pawn.transform.position;
        // Find the angle between the direction AI pawn is facing and the direction to the target.
        float angleToTarget = Vector3.Angle(PawnToTargetVector, pawn.transform.forward);
        
        RaycastHit hit;

        
        //Raycast the line of sight down the vector to target and output what it hits
        if(Physics.Raycast(pawn.transform.position, PawnToTargetVector, out hit, targetVisRange))
        {
          hitObject = hit.transform.gameObject;
        }
        
        //debug to show us what the raycast looks like and wheather it can see the target.
        
        Debug.DrawLine(pawn.transform.position, PawnToTargetVector * targetVisRange, raycolor);
        

        // if that angle is less than the AI's fov and line of sight hits the target
        if (angleToTarget < fov && hitObject == target) 
        {
            
            raycolor = Color.red;
            return true;
        }
        else 
        {
            raycolor = Color.white;
            return false;
        }
    }
    
    public List<PlayerController> players;
    public List<TankPawn> Vehicles;

    public bool isPatrolLoop;
    public Transform[] waypoints;
    public float waypointStopDistance = 5;
    private int currentWaypoint = 0;
    public GameObject target;
    public GameObject selftarget;
    public GameObject vehicletarget;
    private GameObject hitObject;
    public bool isControllingTank = false;
    public bool isControllingHuman = true;
    //Keep track of how long we spend in a state
    private float timeSinceLastStateChange;

    public float fleeDistance = 30;
    public float vehicleVisRange = 80;
    public float targetVisRange = 50;
    public float targetAttackRange = 20;
    public float hearingDistance = 50;
    
    public float fov = 90;
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
        //for testing. be to make sure AI has a target and its pawn hasnt been destroyed.
        if(target != null && pawn != null)
        {
           if(isCanHear(target))
           {
              Debug.Log("Heard Noise");
           }
           if(isCanSee(target))
           {
            Debug.Log("Saw Player");
           }
        }
        MakeDecisions();
        base.Update();
    }

    public void MakeDecisions()
    {   
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
            if (isDistanceLessThanTarget(target, targetVisRange) && isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                //When Ai has a target in range, and is in vehicle..
                if (isDistanceLessThanTarget(target, targetVisRange) && isInVehicle()) 
                {
                    ChangeState(AIStates.VehicleChase);
                }  
                //When AI has a target, not in a vehicle, and has no vehicle in range to get in..
                if (isDistanceLessThanTarget(target, targetVisRange) && !isInVehicle() && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange)) 
                {
                    ChangeState(AIStates.HumanChase);
                }   
                break;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------
            
                
            
                case AIStates.MoveToVehicle:
                DoMoveToVehicleState();
                //if were close enough to the vehicle to enter it
                if(isDistanceLessThanTarget(vehicletarget, 1) && !isInVehicle())
                {
                  ChangeState(AIStates.EnterVehicle);
                }
                //when some else takes the target vehicle and doesnt have a target in range
                if(!isInVehicle() && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isDistanceLessThanTarget(target, targetVisRange))
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //when some else takes the target vehicle but still has a target in range
                if(!isInVehicle() && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && isDistanceLessThanTarget(target, targetVisRange))
                {
                    ChangeState(AIStates.HumanChase);
                }
                //when AI gets in vehicle and still has a target in range
                if(isInVehicle() && isDistanceLessThanTarget(target, targetVisRange))
                {
                    ChangeState(AIStates.VehicleChase);
                }

                
                break;

                 //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                case AIStates.HumanChase:
                DoHumanChaseState();
                
                //When AI doesnt have a target or vehicle in range
                if (!isDistanceLessThanTarget(target, targetVisRange) && !isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.GaurdPost);
                }
                //When AI doesnt have a target but found a vehicle in range
                if (!isDistanceLessThanTarget(target, targetVisRange) && isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
                }
                //When AI has a target but found a vehicle in range (prioritise vehicle)
                if (!isDistanceLessThanTarget(target, targetVisRange) && isDistanceLessThanTarget(vehicletarget, vehicleVisRange) && !isInVehicle()) 
                {
                    ChangeState(AIStates.MoveToVehicle);
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

                break;
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
        if(waypoints.Length > 0)
        {
            Patrol();
        }
    }
    protected virtual void DoMoveToVehicleState()
    {
        //Do what?
        Chase(vehicletarget);
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

    protected virtual void DoFleeState()
    {
        //Do what?
        Flee();
    }
    

    //overloading, different versions of the same method which take different data in the constructor
   
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
    
     protected void Patrol()
    {
             // If we have a enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint) 
        {
            // Then chase that waypoint
            Chase(waypoints[currentWaypoint]);
            // If we are close enough, move to the next point
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance) 
            {
                currentWaypoint++;
            }
        }
        else if(isPatrolLoop)
        {
           RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        // Set the index back to 0
        currentWaypoint = 0;
    }

    
    
    public void Attack()
    {
        pawn.Attack();
    }

    public void Enter()
    {
        pawn.EnterVehicle();
    }

    protected void Flee()
    {
        //Get the distance from the target
        float targetDistance = Vector3.Distance( target.transform.position, pawn.transform.position );
        //get the percentage from target
        float percentOfFleeDistance = targetDistance / fleeDistance;
        //clamp the distance to flee
         percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        //invert so the farther AI is from target the less they will flee.
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        // Find the vector to target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the vector away from target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find flee vector
        Vector3 fleeVector = vectorAwayFromTarget.normalized * flippedPercentOfFleeDistance;
        // chase the flee Vector
        Chase(pawn.transform.position + fleeVector);
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


    private void OnDrawGizmos()
    {
         
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

