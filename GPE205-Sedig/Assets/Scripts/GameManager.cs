using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float RespawnTime;
    public float RespawnTimer;
    public List<TankPawn> Destroyedtanks;
    
    public List<HumanPawn> DeadPlayers;
    public List<HumanPawn> DeadAIPlayers;
    public Transform PlayerSpawnPoint;
    private GameObject newAiPawn;
    private Transform[] AISpawnPoints;


    private Transform[] TankType1SpawnPoints;
    private Transform[] TankType2SpawnPoints;
    
    private Transform[] WayPointSpawnPoints;
    public static GameManager instance;
    public List<PlayerController> players;
    public List<TimidFSM> TimidaiPlayers;
    public List<ExperiancedFSM> ExperiancedaiPlayers;
    public List<AggressiveFSM> AggressiveaiPlayers;
    public List<HumanPawn> humans;
    public List<TankPawn> Vehicles;
    public List<WayPointCluster> Waypointcluster;
    private void Awake()
    {
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

    private void SpawnPlayers()
    {
        GameObject PlayerControllerObj = Instantiate(PlayerControllerPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        GameObject newHumanPawnObj = Instantiate(HumanPawnPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation) as GameObject;
        Controller newController = PlayerControllerObj.GetComponent<Controller>();
        Pawn newPawn = newHumanPawnObj.GetComponent<Pawn>();
        newController.GetComponent<PlayerController>().orientation = newHumanPawnObj.GetComponent<Pawn>().Orientation.transform;
        
        newController.pawn = newPawn;
        newPawn.controller = newController;
    }

    private void SpawnAIPlayers()
    {
       for (int i = 0; i < AISpawnPoints.Length; i++) 
         {
              float rand = Random.Range(1,4); //Increase this as new ai personalities are added
              
              if (rand == 1)
              {
              GameObject newFSMObj = Instantiate(TimidAIPrefab,Vector3.zero,Quaternion.identity) as GameObject;  
              newAiPawn = Instantiate(HumanTimidAIPawnPrefab, AISpawnPoints[i].position, AISpawnPoints[i].rotation) as GameObject;
              Controller newController = newFSMObj.GetComponent<Controller>();
              TimidaiPlayers.Add(newFSMObj.GetComponent<TimidFSM>());
              Pawn newPawn = newAiPawn.GetComponent<Pawn>();
              newController.pawn = newPawn;
              newPawn.controller = newController;
              }
              if (rand == 2)
              {
              GameObject newFSMObj = Instantiate(AggressiveAIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
              newAiPawn = Instantiate(HumanAggressiveAIPawnPrefab, AISpawnPoints[i].position, AISpawnPoints[i].rotation) as GameObject;
              Controller newController = newFSMObj.GetComponent<Controller>();
              AggressiveaiPlayers.Add(newFSMObj.GetComponent<AggressiveFSM>());
              Pawn newPawn = newAiPawn.GetComponent<Pawn>();
              newController.pawn = newPawn;
              newPawn.controller = newController;
              }
              if (rand == 3)
              {
              GameObject newFSMObj = Instantiate(ExperiancedAIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
              newAiPawn = Instantiate(HumanExperiancedAIPawnPrefab, AISpawnPoints[i].position, AISpawnPoints[i].rotation) as GameObject;
              Controller newController = newFSMObj.GetComponent<Controller>();
              ExperiancedaiPlayers.Add(newFSMObj.GetComponent<ExperiancedFSM>());
              Pawn newPawn = newAiPawn.GetComponent<Pawn>();
              newController.pawn = newPawn;
              newPawn.controller = newController;
              }

              humans.Add(newAiPawn.GetComponent<HumanPawn>());
         }
    }

    private void SpawnVehicles()
    {
        for (int i = 0; i < TankType1SpawnPoints.Length; i++) 
        {
           GameObject newTankPawn = Instantiate(UatTankPawnPrefab, TankType1SpawnPoints[i].position, TankType1SpawnPoints[i].rotation) as GameObject;
           Vehicles.Add(newTankPawn.GetComponent<TankPawn>());
        }
        for (int i = 0; i < TankType2SpawnPoints.Length; i++) 
        {
           GameObject newTankPawn = Instantiate(MyTankPawnPrefab, TankType2SpawnPoints[i].position, TankType2SpawnPoints[i].rotation) as GameObject;
           Vehicles.Add(newTankPawn.GetComponent<TankPawn>());
        } 
    }

    private void SpawnWaypoints()
    {
        //spawn as many waypoint clusters as we fill the list of spawnpoints with when creating the level, to each of their respective spawn point positions.
         for (int i = 0; i < WayPointSpawnPoints.Length; i++) 
         {
              GameObject newWaypointcluster = Instantiate(WayPointClusterPrefab, WayPointSpawnPoints[i].position, WayPointSpawnPoints[i].rotation) as GameObject;
              Waypointcluster.Add(newWaypointcluster.GetComponent<WayPointCluster>());
         }
    }

    public GameObject PlayerControllerPrefab;
    public GameObject TimidAIPrefab;
    public GameObject AggressiveAIPrefab;
    public GameObject ExperiancedAIPrefab;
    public GameObject HumanPawnPrefab;

    public GameObject HumanTimidAIPawnPrefab;
    public GameObject HumanAggressiveAIPawnPrefab;
    public GameObject HumanExperiancedAIPawnPrefab;
    public GameObject MyTankPawnPrefab;
    public GameObject UatTankPawnPrefab;
    public GameObject WayPointClusterPrefab;
    


   private void RespawnCountdown()
   {
      if(RespawnTimer > 0)
      {
       RespawnTimer -= Time.deltaTime;
      }

      if(RespawnTimer <= 0)
      {
        RespawnDeadObjects();
      }
   }

   private void RespawnDeadObjects()
   {
    if(Destroyedtanks.Count > 0)

    {
         for (int i = 0; i < Destroyedtanks.Count; i++) 
         {
           GameObject newTankPawn = Instantiate(UatTankPawnPrefab, TankType1SpawnPoints[i].position, TankType1SpawnPoints[i].rotation) as GameObject;
           Vehicles.Add(newTankPawn.GetComponent<TankPawn>());
           Destroyedtanks.Clear();
         }
    }
    if(DeadAIPlayers.Count > 0)
    {
         for (int i = 0; i < DeadAIPlayers.Count; i++) 
         {
              float rand = Random.Range(1,3); //Increase this as new ai personalities are added
              
              if (rand == 1)
              {
              GameObject newFSMObj = Instantiate(TimidAIPrefab,Vector3.zero,Quaternion.identity) as GameObject;  
              newAiPawn = Instantiate(HumanTimidAIPawnPrefab, AISpawnPoints[i].position, AISpawnPoints[i].rotation) as GameObject;
              Controller newController = newFSMObj.GetComponent<Controller>();
              TimidaiPlayers.Add(newFSMObj.GetComponent<TimidFSM>());
              Pawn newPawn = newAiPawn.GetComponent<Pawn>();
              newController.pawn = newPawn;
              newPawn.controller = newController;
              }
              if (rand == 2)
              {
              GameObject newFSMObj = Instantiate(AggressiveAIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
              newAiPawn = Instantiate(HumanAggressiveAIPawnPrefab, AISpawnPoints[i].position, AISpawnPoints[i].rotation) as GameObject;
              Controller newController = newFSMObj.GetComponent<Controller>();
              AggressiveaiPlayers.Add(newFSMObj.GetComponent<AggressiveFSM>());
              Pawn newPawn = newAiPawn.GetComponent<Pawn>();
              newController.pawn = newPawn;
              newPawn.controller = newController;
              }
              DeadAIPlayers.Clear();
              humans.Add(newAiPawn.GetComponent<HumanPawn>());
         }
    }

         RespawnTimer = RespawnTime;
       
   }
    

    



}
