using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiController : Controller
{
    public enum  AIStates 
   {
     GaurdPost, EnterVehicle, MoveToVehicle, VehicleChase, HumanChase, Attack, TakeCover, Flee, turnTowards
   };
   //Debug
    Color raycolor = Color.yellow;
     //Check Transition Example
    protected virtual bool isHasTarget()
    {
        // return the truth or falsity of this statement
        return (target != null);
    }
    protected virtual bool isDistanceLessThanTarget(GameObject thisTarget, float distance)
    { 
        if(vehicletarget == null) return false;
        
        if (Vector3.Distance (pawn.transform.position, thisTarget.transform.position) < distance ) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    protected virtual bool isShotIncoming()
    {
        if(pawn.GetComponentInChildren<Attacker>().isTookShot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected virtual bool isInVehicle()
    {
        // return the truth or falsity of this statement
        if(pawn.GetComponent<TankPawn>())
        {
           // Debug.Log("is in vehicle");
            return true;
        }
        else
        {
            // Debug.Log("is NOT in vehicle");
            return false;
        }
        
    }
   protected virtual bool isHealthLessThan(float amount)
    {
        if(pawn.GetComponent<TankHealth>())
        {
            TankHealth HealthComponant = pawn.GetComponent<TankHealth>();
    
            if(HealthComponant.currentHealth < amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }
   protected virtual bool isCanHear(GameObject thistarget)
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
  
  protected virtual bool isCanSee(GameObject thistarget)
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


    public Animator anim;
    public List<PlayerController> players;
    public List<AggressiveFSM> AggressiveaiPlayers;
    public List<TimidFSM> TimidaiPlayers;
    public List<TankPawn> Vehicles;
    public List<WayPointCluster> waypointclusters;

    public bool isPatrolLoop;
    public Transform[] Patrolwaypoints;
    public float waypointStopDistance = 5;
    public float AIMemory;
    private int currentWaypoint = 0;
    public GameObject target;
    public GameObject selftarget;
    public GameObject vehicletarget;
    public GameObject patrolTarget;
    private GameObject hitObject;
    public bool isControllingTank = false;
    public bool isControllingHuman = true;
    //Keep track of how long we spend in a state
    public float timeSinceLastStateChange;
    public float TimePassedSinceLastChange;

    public float fleeDistance = 30;
    public float vehicleVisRange = 80;
    public float targetVisRange = 50;
    public float targetAttackRange = 20;
    public float hearingDistance = 50;
    
    public float fov = 90;
    public AIStates currentState;
   
    //Override this function to create multiple AI personalitys which inhertit from this class. 
    public abstract void MakeDecisions();
    
     protected void ChangeState (AIStates newState)
    {
        
        // Change the current state
        currentState = newState;
        // Keep track of when this state change happened
        timeSinceLastStateChange = Time.time;
        TimePassedSinceLastChange = 0;

    }
    
    
    protected void DoGaurdPostState()
    {
        //Do what? //patrol here!
        if (GameManager.instance.Waypointcluster.Count > 0) 
        {
          if(Patrolwaypoints.Length > 0)
          {
            pawn.GetComponent<HumanPawn>().anim.SetBool("isMoving",true);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",false);
            pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",1);
             pawn.GetComponent<HumanPawn>().anim.SetBool("isShooting",false);
           pawn.GetComponent<HumanPawn>().anim.SetBool("isAiming",false);
            Patrol();
          }
          else{anim.SetBool("isMoving",true);}
        }
    }
    protected void DoMoveToVehicleState()
    {
        //Do what?
        if (GameManager.instance.Vehicles.Count > 0) 
        {
            Chase(vehicletarget, true);
            
            pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",true);
            pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",1);
             pawn.GetComponent<HumanPawn>().anim.SetBool("isShooting",false);
           pawn.GetComponent<HumanPawn>().anim.SetBool("isAiming",false);
            
            if(!isInVehicle())
            {
            Enter();
            }
        }
    }
    protected void DoHumanChaseState()
    {
         if (GameManager.instance.players.Count > 0) 
        {
            
           pawn.GetComponent<HumanPawn>().anim.SetBool("isMoving",true);
           pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",true);
           pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",1);
           pawn.GetComponent<HumanPawn>().anim.SetBool("isShooting",false);
           pawn.GetComponent<HumanPawn>().anim.SetBool("isAiming",false);
            
        //Do what?
        Chase(target, true);
        }
    }
    protected void DoTurnTowardsState()
    {
         if (GameManager.instance.players.Count > 0) 
        {
            
            pawn.GetComponent<HumanPawn>().anim.SetBool("isMoving",false);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",false);
            pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",0);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isShooting",false);
        //Do what?
          Chase(target, false);
        }
    }
    protected void DoVehicleChaseState( bool CanMove)
    {
         if (GameManager.instance.players.Count > 0) 
        {
          //Do what?
          Chase(target, CanMove);
        }
    }
    protected void DoAttackState(bool CanMove)
    {
         if (GameManager.instance.players.Count > 0) 
        {
            
            pawn.GetComponent<HumanPawn>().anim.SetBool("isMoving",false);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",false);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isAiming",true);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isShooting",true);

            pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",0);
            
          //Do what?
          Chase(target, CanMove);
          Attack();
        }
    }

    protected void DoFleeState(float Amount)
    {
         if (GameManager.instance.players.Count > 0) 
        {
            
            pawn.GetComponent<HumanPawn>().anim.SetBool("isMoving",true);
            pawn.GetComponent<HumanPawn>().anim.SetBool("isRunning",true);
            pawn.GetComponent<HumanPawn>().anim.SetFloat("moveDirY",1);
            
           //Do what?
           Flee(Amount);
        }
    }
    

    //overloading, different versions of the same method which take different data in the constructor
   
    //---------------------------------------------------------------------------------------------------------
    protected void Chase(Vector3 targetPosition, bool CanMove)
    {  
       if (GameManager.instance.players.Count > 0) 
      {
        if(target!=null)
        pawn.RotateTowards(targetPosition);
        //When the function is called decide whether it should move and rotate, or just rotate.
        if(!CanMove) return;
        pawn.MoveForward();
      }
    }
    protected void Chase(Transform targetTransform, bool CanMove)
    {  
        if(target!=null)
        Chase(targetTransform.position, CanMove);
    }
    protected void Chase(GameObject targetGameObject, bool CanMove)
    {  
        if(target!=null)
        Chase(targetGameObject.gameObject.transform, CanMove);
    }
      protected void Chase(Pawn targetPawn, bool CanMove)
    {
        if(target!=null)
        Chase(targetPawn.transform, CanMove);
    }
     protected void Chase(Controller targetController, bool CanMove)
    {
        if(target!=null)
        Chase(targetController.pawn, CanMove);
    }
   
    
     protected void Patrol()
    {
             // If we have a enough waypoints in our list to move to a current waypoint
        if (Patrolwaypoints.Length > currentWaypoint) 
        {
            // Then chase that waypoint
            Chase(Patrolwaypoints[currentWaypoint], true);
            // If we are close enough, move to the next point
            if (Vector3.Distance(pawn.transform.position, Patrolwaypoints[currentWaypoint].position) < waypointStopDistance) 
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

    
    
    protected void Attack()
    {
        pawn.Attack();
    }

    protected void Enter()
    {
        pawn.EnterVehicle();
    }

    protected void Flee(float Amount)
    {
        //Get the distance from the target
        float targetDistance = Vector3.Distance( target.transform.position, pawn.transform.position );
        //get the percentage from target
        float percentOfFleeDistance = targetDistance / Amount;
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
        Chase(pawn.transform.position + fleeVector, true);
    }

    protected void TargetNearestPlayer()
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


    protected void TargetNearestVehicle()
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

    protected void TargetNearestWaypointCluster()
    {
        //GameManager exists
        if (GameManager.instance != null) 
        {
            //list of vehicles exists
            if (GameManager.instance.Waypointcluster != null) 
            {
                //there are vehicles in it
                if (GameManager.instance.Waypointcluster.Count > 0) 
                {
                    //target the first vehicle in the list
                    waypointclusters = GameManager.instance.Waypointcluster;
                    WayPointCluster closestWaypoint = waypointclusters[0];
                    float closestWaypointDistance = Vector3.Distance(pawn.gameObject.transform.position, closestWaypoint.gameObject.transform.position);
                    foreach(WayPointCluster wayPointCluster in waypointclusters)
                    {
                        if (Vector3.Distance(pawn.transform.position, wayPointCluster.gameObject.transform.position) <= closestWaypointDistance)
                        {
                            closestWaypoint = wayPointCluster;
                            closestWaypointDistance = Vector3.Distance(pawn.transform.position, closestWaypoint.transform.position);
                        }
                    } 
                    //set the patrol cluster target
                    patrolTarget = closestWaypoint.gameObject;
                    //populate the AIs patrol waypoints with the children transforms of the ClusterObject
                    for(int i = 0; i < patrolTarget.transform.childCount; i++)
                    {
                        
                       Patrolwaypoints[i] = patrolTarget.transform.GetChild(i).transform;
                    }
                }
            }
        }
    }

    


  //FOR: testing
    protected void TargetPlayerOne()
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

