using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isUseSeed;
    public string mapStringSeed;
    public bool isUseDateSeed;
    private int seed;
    public int MaxPlayers;
    public int MaxAIPlayers;
    
    public List<Spawner> PlayerSpawners;
    public List<Spawner> AiSpawners;
    public List<GameObject> LevelZones;

    public List<TankPawn> Destroyedtanks;
    public List<HumanPawn> DeadPlayers;
    public List<HumanPawn> DeadAIPlayers;
    public Transform PlayerSpawnPoint;
    private GameObject newAiPawn;
    public static GameManager instance;
    public List<PlayerController> players;
    public List<AiController> aiPlayers;
    public List<HumanPawn> humans;
    public List<TankPawn> Vehicles;
    public List<WayPointCluster> Waypointcluster;
    private void Awake()
    {
        if(isUseSeed && isUseDateSeed)
        {
            Debug.LogWarning("WARNING: date seed and string seed are both set in the Game manager. Only select one or the other. Jerk");
        }
        //if there isnt already a gamemanager, 
        //create an instance that wont be destroyed when a new scene is loaded
        if(instance == null)
        {
           instance = this;
           DontDestroyOnLoad(gameObject);
        }//otherwise, destroy this gameobject so there arent multiple GameManagers.
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetPlayerSpawns();
        ResetAiSpawns();
        //just for testing, on start, objects will add themselves to the lists.
           //SpawnWaypoints();
           //SpawnPlayers();
           //SpawnAIPlayers();
           //SpawnVehicles();
        //RespawnTimer = RespawnTime;
    }
   
    private void Update()
    {
        //this needs some work first
       //RespawnCountdown(); 
    }

    public void ResetPlayerSpawns()
    {
        //create an array of ints the same size of the number of spawners in the world.
        int[] spawnPointsToEnable = new int[PlayerSpawners.Count];
        //first set all items of the array to -1, because they default to 0, which will cause an extra spawn point to be enabled when iterating through later
         for(int x = 0; x < spawnPointsToEnable.Length; x++)
        {
            //create amount of random numbers equal to the max players
            spawnPointsToEnable[x] = -1;
            
        }
        
        for(int x = 0; x < MaxPlayers; x++)
        {
            //create amount of random numbers equal to the max players
            spawnPointsToEnable[x] = UnityEngine.Random.Range(0,PlayerSpawners.Count);
            Debug.Log(spawnPointsToEnable[x]);
        }
          
            //loop for the number equal to the amount of spawners for this item
            for(int i = 0; i < PlayerSpawners.Count; i++)
            {  
            bool isEqual = false;
            //iterate through the random numbers and see which ones match the current index
                for(int x = 0; x < spawnPointsToEnable.Length; x++)
                {
                   if(spawnPointsToEnable[x] == i)
                   {
                    isEqual = true;
                   }
                }
                //if the current index matches one of the random numbers set it active. 
             if(isEqual)
                {
                    PlayerSpawners[i].gameObject.SetActive(true);
                }
                else
                {
                   PlayerSpawners[i].gameObject.SetActive(false);
                }
            }   
    }
    public void ResetAiSpawns()
    {
        //create an array of ints the same size of the number of spawners in the world.
        int[] AispawnPointsToEnable = new int[AiSpawners.Count];
        
        //first set all items of the array to -1, because they default to 0, which will cause an extra spawn point to be enabled when iterating through later
         for(int x = 0; x < AispawnPointsToEnable.Length; x++)
        {
            //create amount of random numbers equal to the max players
            AispawnPointsToEnable[x] = -1;
            
        }
        for(int x = 0; x < MaxAIPlayers; x++)
        {
            //create amount of random numbers equal to the max players
            AispawnPointsToEnable[x] = UnityEngine.Random.Range(0,AiSpawners.Count + 1);
            
        }
          
            //loop for the number equal to the amount of spawners for this item
            for(int i = 0; i < AiSpawners.Count; i++)
            {  
                bool isEqual = false;
                    //iterate through the random numbers and see which ones match the current index
                    for(int x = 0; x < AispawnPointsToEnable.Length; x++)
                    {
                       if(AispawnPointsToEnable[x] == i)
                       {
                        isEqual = true;
                       }
                    }
                //if the current index matches one of the random numbers set it active. 
                if(isEqual)
                {
                    AiSpawners[i].gameObject.SetActive(true);
                }
                else
                {
                   AiSpawners[i].gameObject.SetActive(false);
                }
            }   
    }
     public void SetStringSeed()
    {
         seed = mapStringSeed.GetHashCode();
         UnityEngine.Random.InitState(seed);
    }
    public void SetMapOfTheDaySeed()
    {
         UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
    }
     public void RerandomizeSeed()
    { 
         UnityEngine.Random.InitState(DateToInt(DateTime.Now));
    }
    public int DateToInt(DateTime dateToUse)
    {
        return 
        dateToUse.Year
         + dateToUse.Month
          + dateToUse.Day
           + dateToUse.Hour
            + dateToUse.Minute
             + dateToUse.Second
              + dateToUse.Millisecond;
    }
}
