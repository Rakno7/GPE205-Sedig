using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerSpawnPoint;
    public Transform MyTankSpawnPoint;
    public Transform UatTankSpawnPoint;
    public Transform EnemyTankSpawnPoint;
    public static GameManager instance;
    public List<PlayerController> players;
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
       SpawnPlayer();
       SpawnVehicles();
    }

    private void SpawnPlayer()
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

    private void SpawnVehicles()
    {
        GameObject newTankPawnObj = Instantiate(MyTankPawnPrefab, MyTankSpawnPoint.position, MyTankSpawnPoint.rotation) as GameObject;
        GameObject newUatTankPawnObj = Instantiate(UatTankPawnPrefab, UatTankSpawnPoint.position, UatTankSpawnPoint.rotation) as GameObject;
    }

    public GameObject PlayerControllerPrefab;
    public GameObject HumanPawnPrefab;
    public GameObject MyTankPawnPrefab;
    public GameObject UatTankPawnPrefab;



}
