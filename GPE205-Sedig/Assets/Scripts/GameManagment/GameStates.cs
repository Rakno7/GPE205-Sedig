using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStates : MonoBehaviour
{
    public enum  gameStates 
   {
     Title, MainMenu, Options, GamePlay, ScoreScreen, GameOver, Credits 
   };

   public GameObject TitleScreenStateObject;
   public GameObject MainMenuStateObject;
   public GameObject OptionsStateObject;
   public GameObject GamePlayStateObject;
   public GameObject ScoreScreenStateObject;
   public GameObject GameOverStateObject;
   public GameObject CreditsStateObject;
   
    public abstract void SetStates();

    public virtual void DeactivateAllStates()
    {
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsStateObject.SetActive(false);
        GamePlayStateObject.SetActive(false);
        ScoreScreenStateObject.SetActive(false);
        GameOverStateObject.SetActive(false);
        CreditsStateObject.SetActive(false);
    }

//---------------------------------------------------------------------------------------------------------------------------------------

    public virtual void ActivateTitleScreen()
    {
      DeactivateAllStates();
      TitleScreenStateObject.SetActive(true);
    }
    public virtual void ActivateMainMenu()
    {
      DeactivateAllStates();
      MainMenuStateObject.SetActive(true);
    }
    public virtual void ActivateOptions()
    {
      DeactivateAllStates();
      OptionsStateObject.SetActive(true);
    }
    public virtual void ActivateGamePlay()
    {
      DeactivateAllStates();
      GamePlayStateObject.SetActive(true);
      if(GameManager.instance !=null)
      {
        if(!GameManager.instance.isMapGenerated)
        {
        GameManager.instance.GenerateMap();
        }
      }
    }
    public virtual void ActivateScoreScreen()
    {
      DeactivateAllStates();
      ScoreScreenStateObject.SetActive(true);
    }
    public virtual void ActivateGameOver()
    {
      DeactivateAllStates();
      GameOverStateObject.SetActive(true);
    }
    public virtual void ActivateCredits()
    {
      DeactivateAllStates();
      CreditsStateObject.SetActive(true);
    }

//---------------------------------------------------------------------------------------------------------------------------------------


    public virtual void DeactivateTitleScreen()
    {

    }
    public virtual void DeactivateMainMenu()
    {

    }
    public virtual void DeactivateOptions()
    {

    }
    public virtual void DeactivateGamePlay()
    {

    }
    public virtual void DeactivateScoreScreen()
    {

    }
    public virtual void DeactivateGameOver()
    {

    }
    public virtual void DeactivateCredits()
    {

    }


    //---------------------------------------------------------------------------------------------------------------------------------------
 
}
