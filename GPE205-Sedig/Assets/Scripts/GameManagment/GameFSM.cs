using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : GameStates
{
    public gameStates currentState;
    void Awake()
    {
        if(GameManager.instance !=null)
        {
            GameManager.instance.gameFSM = this;
        }
    }

   
    void Update()
    {
        //SetStates();
    }

    public override void SetStates()
    {   
        //-----------------------------------------------------------------------------------------------------------------
         switch (currentState)
        {
            case gameStates.Title:
            //work
            ActivateTitleScreen();
            //Transition
            
            break;
        }
        //------------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.MainMenu:
            //work
            ActivateMainMenu();

            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.Options:
            //work
            ActivateOptions();

            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.GamePlay:
            //work
            ActivateGamePlay();
            Debug.Log("isthishappening");
            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.ScoreScreen:
            //work
            ActivateScoreScreen();

            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.GameOver:
            //work
            ActivateGameOver();

            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
        switch (currentState)
        {
            case gameStates.Credits:
            //work
            ActivateCredits();

            //Transition
          
            break;
        }
        //-----------------------------------------------------------------------------------------------------------------
               
    }
}
