using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerSpawnPoint;
    public Transform MyTankSpawnPoint;

    public Transform HumanSpawnPoint1;
    public Transform HumanSpawnPoint2;

    public Transform UatTankSpawnPoint1;
    public Transform UatTankSpawnPoint2;
    
    public static GameManager instance;
    public List<PlayerController> players;
    public List<TankPawn> Vehicles;
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
        //Temp: on Start Spawn for testing.
       SpawnPlayers();
       SpawnHumans();
       SpawnVehicles();
    }

    private void SpawnPlayers()
    {
        GameObject PlayerControllerObj = Instantiate(PlayerControllerPrefab,Vector3.zero,Quaternion.identity) as GameObject;

        GameObject newHumanPawnObj = Instantiate(HumanPawnPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation) as GameObject;
        Controller newController = PlayerControllerObj.GetComponent<Controller>();
        Pawn newPawn = newHumanPawnObj.GetComponent<Pawn>();

        newController.GetComponent<PlayerController>().isControllingTank = false;
        newController.GetComponent<PlayerController>().isControllingHuman = true;
        newController.GetComponent<PlayerController>().orientation = newHumanPawnObj.GetComponent<Pawn>().Orientation.transform;
        

        newController.pawn = newPawn;
        newPawn.controller = newController;
    }

    private void SpawnHumans()
    {
        //TODO: in the future do a for loop to spawn each of the objects

        //Spawn first human
        GameObject AIControllerObj1 = Instantiate(AIControllerPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        

        GameObject newHumanPawnObj1 = Instantiate(HumanPawnPrefab, HumanSpawnPoint1.position, HumanSpawnPoint1.rotation) as GameObject;
        Controller newController1 = AIControllerObj1.GetComponent<Controller>();
        Pawn newPawn1 = newHumanPawnObj1.GetComponent<Pawn>();

        newController1.GetComponent<AiController>().isControllingTank = false;
        newController1.GetComponent<AiController>().isControllingHuman = true;

        newController1.pawn = newPawn1;
        newPawn1.controller = newController1;

        //Spawn Second Human
        GameObject AIControllerObj2 = Instantiate(AIControllerPrefab,Vector3.zero,Quaternion.identity) as GameObject;

        GameObject newHumanPawnObj2 = Instantiate(HumanPawnPrefab, HumanSpawnPoint2.position, HumanSpawnPoint2.rotation) as GameObject;
        Controller newController2 = AIControllerObj2.GetComponent<Controller>();
        Pawn newPawn2 = newHumanPawnObj2.GetComponent<Pawn>();

        newController2.GetComponent<AiController>().isControllingTank = false;
        newController2.GetComponent<AiController>().isControllingHuman = true;

        newController2.pawn = newPawn2;
        newPawn2.controller = newController2;
        
        

        
    }


    private void SpawnVehicles()
    {
        GameObject newTankPawnObj = Instantiate(MyTankPawnPrefab, MyTankSpawnPoint.position, MyTankSpawnPoint.rotation) as GameObject;
        GameObject newUatTankPawnObj1 = Instantiate(UatTankPawnPrefab, UatTankSpawnPoint1.position, UatTankSpawnPoint1.rotation) as GameObject;
        GameObject newUatTankPawnObj2 = Instantiate(UatTankPawnPrefab, UatTankSpawnPoint2.position, UatTankSpawnPoint2.rotation) as GameObject;
        
        //add vehicles to the list.
        Vehicles.Add(newUatTankPawnObj1.GetComponent<TankPawn>());
        Vehicles.Add(newUatTankPawnObj2.GetComponent<TankPawn>());
    }

    public GameObject PlayerControllerPrefab;
    public GameObject AIControllerPrefab;
    public GameObject HumanPawnPrefab;
    public GameObject MyTankPawnPrefab;
    public GameObject UatTankPawnPrefab;



}
