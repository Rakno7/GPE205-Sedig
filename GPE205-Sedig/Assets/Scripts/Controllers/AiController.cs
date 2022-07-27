using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : Controller
{
   public enum  AIStates 
   {
     GaurdPost, Search, EnterVehicle, VehicleChase, HumanChase, Attack, TakeCover, Flee
   };
     //Check Transition Example
    protected bool IsDistanceLessThan(GameObject target, float distance)
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

    public GameObject target;
    //Keep track of how long we spend in a state
    private float timeSinceLastStateChange;

   public AIStates currentState;
    public override void Start()
    {
        //TEMP:to test the state.
        //use this later in state method:---target = GameManager.instance.players[0].pawn.gameObject;
         
        ChangeState(AIStates.VehicleChase);
        
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
                
                break;
            case AIStates.Search:
                
                DoSearchState();
                
                break;
            case AIStates.HumanChase:
                DoHumanChaseState();

                
                break;
                
                case AIStates.VehicleChase:
                DoVehicleChaseState();

                
                break;
                
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
    protected virtual void DoSearchState()
    {
        //Do what?
       Search(target);
    }
    protected virtual void DoHumanChaseState()
    {
        //Do what?
        Search(target);
    }
    protected virtual void DoVehicleChaseState()
    {
        //Do what?
        Debug.Log("Are we searching?");
        Search(target);
    }

    public void GaurdPost()
    {
        //Do Gaurd Post
    }

    //overloading, different versions of the same method which take different data in the constructor
    
    public void Search(Vector3 targetPosition)
    {  
        pawn.RotateTowards(targetPosition);
        pawn.MoveForward();
    }
    public void Search(GameObject targetGameObject)
    {  
        Search(target.gameObject.transform);
    }
    public void Search(Transform targetTransform)
    {  
        Search(targetTransform.position);
    }
     public void Search(Controller targetController)
    {
        Search(targetController.pawn);
    }
     public void Search(Pawn targetPawn)
    {
        Search(targetPawn.transform);
    }
    

    public void HumanChase()
    {
        //Do Human Chase
    }
}

