using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    //We allow which pawn we want the controller to control to be set in the inspector. 
    public Pawn pawn;
    
    //public bool isControllingHuman;
    //public bool isControllingTank;
    
    public Attacker attacker;

    
    public virtual void Start()
    {

    }

   
    public virtual void Update()
    {
        //TODO: find a more reliable way then tags to detect which pawn is being used.
       // if(pawn.gameObject.tag == "Tank")
       // {
       //     isControllingTank = true;
       //     isControllingHuman = false;
       // }
       // if(pawn.gameObject.tag == "Human")
       // {
       //    isControllingHuman = true;
       //    isControllingTank = false; 
       // }
    }
     public abstract void ProcessInputs();
}
