using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    //We allow which pawn we want the controller to control to be set in the inspector. 
    public int Score;
    public Pawn pawn;
    
    //public bool isControllingHuman;
    //public bool isControllingTank;
    
    public Attacker attacker;

    
    public virtual void Start()
    {
      
    }
    public virtual void Update()
    {
    }
     public virtual void ProcessInputs()
     {

     }
     public virtual void AddScore(int Amount)
     {
         Score += Amount;
     }
      public virtual void SubtractScore(int Amount)
     {
        Score -= Amount;
     }
     //when gameplay starts iterate through all the controllers in the game manager and clear their score.
      public virtual void ClearScore()
     {
        Score = 0;
     }
}
