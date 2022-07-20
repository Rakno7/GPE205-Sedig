using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    //We allow which pawn we want the controller to control to be set in the inspector. 
    public Pawn pawn;
    public virtual void Start()
    {

    }

   
    public virtual void Update()
    {
        
    }
     public abstract void ProcessInputs();
}
